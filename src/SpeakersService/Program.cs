
namespace SpeakersService;

using CommonComponents;
using CommonComponents.MassTransit;
using MassTransit;
using MassTransit.Logging;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using SpeakersService.App;
using SpeakersService.Data;
using MassTransit.Monitoring;
using OpenTelemetry.Logs;
using Serilog;
using Elastic.CommonSchema.Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var otlpEndpoint = builder.Configuration["OTLP:Endpoint"] ?? "http://localhost:4317";

        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .Enrich.FromLogContext()
                .WriteTo.Console(formatter: new EcsTextFormatter())
                .WriteTo.OpenTelemetry(options => {
                    options.Endpoint = otlpEndpoint;
                    options.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc;
                    options.ResourceAttributes = new Dictionary<string, object> {
                        ["service.name"] = "SpeakersService"
                    };
                });
        });

        builder.Services.AddServiceBus(builder.Configuration, cfg =>
        {
            cfg.AddConsumers(typeof(Program).Assembly);
        });

        builder.Services.AddDbContextPool<SpeakerDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // builder.Services.AddDbContext<SpeakerDbContext>(options =>
        // {
        //     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        // });

        builder.Services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            cfg.AddOpenBehavior(typeof(TraceBehavior<,>));
        });

        builder.Services.AddOpenTelemetry()
            .ConfigureResource((resourceBuilder) =>
            {
                resourceBuilder.AddService(builder.Configuration["OTLP:ServiceName"] ?? "SpeakersService");
            })
            .WithTracing(tracing => {
                tracing.AddAspNetCoreInstrumentation();
                tracing.AddSource(DiagnosticHeaders.DefaultListenerName);
                tracing.AddSource(SpeakerServiceActivitySource.Instance.GetActivitySource().Name);
                tracing.AddEntityFrameworkCoreInstrumentation(cfg => {
                    cfg.SetDbStatementForText = true;
                    cfg.SetDbStatementForStoredProcedure = true;
                });
                tracing.AddOtlpExporter(cfg => {
                    cfg.Endpoint = new Uri(otlpEndpoint);
                    cfg.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                });
            })
            .WithMetrics(metrics => {
                metrics.AddAspNetCoreInstrumentation();
                metrics.AddMeter(InstrumentationOptions.MeterName);
                metrics.AddMeter("Microsoft.EntityFrameworkCore");
                metrics.AddMeter(SpeakerServiceConstants.MeterName);
                metrics.AddOtlpExporter(cfg => {
                    cfg.Endpoint = new Uri(otlpEndpoint);
                    cfg.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                });
            });

        builder.Services.AddSingleton<SpeakerServiceMetrics>();

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Correctly resolve the logger
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("OTLP Endpoint: {Endpoint}", otlpEndpoint);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.Run();
    }
}
