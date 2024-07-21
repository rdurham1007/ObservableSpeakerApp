
namespace SpeakersService;

using CommonComponents;
using CommonComponents.MassTransit;
using MassTransit;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Setup some defaults used in all services
        CommonApiBuilder.Configure(builder);

        builder.Services.AddServiceBus(builder.Configuration, cfg => {
            cfg.AddConsumers(typeof(Program).Assembly);
        });

        var app = builder.Build();

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
