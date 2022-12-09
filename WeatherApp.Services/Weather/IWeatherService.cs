using WeatherApp.Domain.DTO;

namespace WeatherApp.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<CityTemperatureDto> GetCityTemperature(string zipCode, string countryCode);
        Task<WeatherDto> GetWeatherByZipCode(string zipCode, string country);

    }
}
