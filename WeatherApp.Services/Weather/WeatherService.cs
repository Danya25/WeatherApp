using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;
using WeatherApp.Common;
using WeatherApp.Domain.DTO;
using WeatherApp.Domain.Settings;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services.Weather
{
    public class WeatherService : IWeatherService
    {
        private const string ApiUrl = "https://api.openweathermap.org/data/2.5/weather?";

        private readonly HttpClient _httpClient;
        private readonly WeatherSettings _weatherSettings;
        public WeatherService(HttpClient httpClient, IOptions<WeatherSettings> weatherSettings)
        {
            _httpClient = httpClient;
            _weatherSettings = weatherSettings.Value;
        }
        public async Task<WeatherDto> GetWeatherByZipCode(string zipCode, string countryCode)
        {
            var request = await _httpClient.GetAsync($"{ApiUrl}zip={zipCode},{countryCode}&appid={_weatherSettings.Key}");
            var response = await request.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = await JsonSerializer.DeserializeAsync<WeatherDto>(response, options);

            return data;
        }

        public async Task<CityTemperatureDto> GetCityTemperature(string zipCode, string countryCode)
        {
            var result = await GetWeatherByZipCode(zipCode, countryCode);
            return new CityTemperatureDto
            {
                City = result.Name,
                TempFarenheit = result.Main.Temp,
                TempDegree = TemperatureConverter.FarenheitToDegree(result.Main.Temp),
            };
        }
    }
}
