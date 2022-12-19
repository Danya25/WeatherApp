using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherApp.Common;
using WeatherApp.Domain.DTO;
using WeatherApp.Domain.Exceptions;
using WeatherApp.Domain.Settings;
using WeatherApp.Services.Interfaces;
using WeatherApp.Services.Models;

namespace WeatherApp.Services.Weather
{
    public class WeatherService : IWeatherService
    {
        private const string ApiUrl = "https://api.openweathermap.org/data/2.5/weather?";
        private readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient _httpClient;
        private readonly WeatherSettings _weatherSettings;
        public WeatherService(HttpClient httpClient, IOptions<WeatherSettings> weatherSettings)
        {
            _httpClient = httpClient;
            _weatherSettings = weatherSettings.Value;
        }
        public async Task<WeatherDto> GetWeatherByZipCode(string zipCode, string countryCode)
        {
            //In celcium
            var request = await _httpClient.GetAsync($"{ApiUrl}zip={zipCode},{countryCode}&appid={_weatherSettings.Key}&units=metric");
            var response = await request.Content.ReadAsStringAsync();
            try
            {
                var data = JsonSerializer.Deserialize<WeatherDto>(response, Options);
                return data;
            }
            catch (JsonException)
            {
                var data = JsonSerializer.Deserialize<ApiError>(response, Options);
                throw new ParsingException(data.Message);
            }

        }
        public async Task<CityTemperatureDto> GetCityTemperature(string zipCode, string countryCode)
        {
            var result = await GetWeatherByZipCode(zipCode, countryCode);
            return new CityTemperatureDto
            {
                City = result.Name,
                Fahrenheit = TemperatureConverter.CelsiusToFarenheit(result.Main.Temp),
                Celsius = result.Main.Temp,
            };
        }

    }
}
