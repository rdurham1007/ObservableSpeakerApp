using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace SpeakersService.WeatherForecasts;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IBus _bus;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {

        var client = _bus.CreateRequestClient<WeatherForecastRequest>();
        
        var request = new WeatherForecastRequest { RequestId = Guid.NewGuid() };

        var response = await client.GetResponse<WeatherForecastResponse>(request);

        return response.Message.Forecasts;
    }
}
