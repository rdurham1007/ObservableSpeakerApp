

namespace SpeakersService.WeatherForecasts
{
    internal class WeatherForecastResponse
    {
        public object RequestId { get; set; }
        public WeatherForecast[] Forecasts { get; set; }
    }
}