using CommonComponents.MassTransit;
using MassTransit;
using MassTransit.Logging;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using TalksService.Data;

namespace TalksService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.AddConsole();

        builder.Services.AddServiceBus(builder.Configuration, cfg =>
        {
            cfg.AddConsumers(typeof(Program).Assembly);
        });

        builder.Services.AddDbContext<TalksDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        
        var otlpEndpoint = builder.Configuration["OTLP:Endpoint"] ?? "http://localhost:4317";

        builder.Services.AddOpenTelemetry()
            .ConfigureResource((resourceBuilder) =>
            {
                resourceBuilder.AddService(builder.Configuration["OTLP:ServiceName"] ?? "TalksService");
            })
            .WithTracing(tracing => {
                tracing.AddAspNetCoreInstrumentation();
                tracing.AddSource(DiagnosticHeaders.DefaultListenerName);
                tracing.AddOtlpExporter(cfg => {
                    cfg.Endpoint = new Uri(otlpEndpoint);
                    cfg.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                });
            });

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

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
