
namespace SpeakerApp.BFF;

using CommonComponents.MassTransit;
using MassTransit;
using MassTransit.Logging;
using MassTransit.Monitoring;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddServiceBus(builder.Configuration, cfg =>
        {
            cfg.AddConsumers(typeof(Program).Assembly);
        });

        var otlpEndpoint = builder.Configuration["OTLP:Endpoint"] ?? "http://localhost:4317";

        builder.Services.AddOpenTelemetry()
            .ConfigureResource((resourceBuilder) =>
            {
                resourceBuilder.AddService(builder.Configuration["OTLP:ServiceName"] ?? "SpeakerApp.BFF");
            })
            .WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation();
                tracing.AddHttpClientInstrumentation();
                tracing.AddSource(DiagnosticHeaders.DefaultListenerName);
                tracing.AddOtlpExporter(cfg =>
                {
                    cfg.Endpoint = new Uri(otlpEndpoint);
                    cfg.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                });
            })
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation();
                metrics.AddHttpClientInstrumentation();
                metrics.AddRuntimeInstrumentation();
                metrics.AddMeter(InstrumentationOptions.MeterName);
                //metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
                metrics.AddConsoleExporter();
                metrics.AddOtlpExporter(cfg =>
                {
                    cfg.Endpoint = new Uri(otlpEndpoint);
                    cfg.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                });
            });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

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
