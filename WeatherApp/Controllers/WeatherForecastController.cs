using Microsoft.AspNetCore.Mvc;
using WeatherApp.Domain.DTO;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatherService _weatherService;

        public WeatherForecastController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("GetWeatherByZip")]
        public async Task<CityTemperatureDto> GetByZipCode(string zipCode, string countryCode = "us")
        {
            return await _weatherService.GetCityTemperature(zipCode, countryCode);
        }


    }
}