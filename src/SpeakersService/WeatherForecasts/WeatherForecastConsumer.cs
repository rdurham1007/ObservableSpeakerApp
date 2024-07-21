

namespace SpeakersService.WeatherForecasts
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.Extensions.Logging;

    public class WeatherForecastConsumer : IConsumer<WeatherForecastRequest>
    {
        private readonly ILogger<WeatherForecastConsumer> _logger;

        public WeatherForecastConsumer(ILogger<WeatherForecastConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<WeatherForecastRequest> context)
        {
            _logger.LogInformation("Received WeatherForecastRequest: {RequestId}", context.Message.RequestId);

            var rng = new Random();
            var forecasts = new WeatherForecast[5]; 
            
            for (var i = 0; i < forecasts.Length; i++)
            {
                var date = DateOnly.FromDateTime(DateTime.Now.AddDays(i));
                var temperatureC = rng.Next(-20, 55);
                forecasts[i] = new WeatherForecast
                {
                    Date = date,
                    TemperatureC = temperatureC,
                    Summary = Summaries[rng.Next(Summaries.Length)]
                };
            }

            await context.RespondAsync(new WeatherForecastResponse
            {
                RequestId = context.Message.RequestId,
                Forecasts = forecasts
            });
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
    }
}