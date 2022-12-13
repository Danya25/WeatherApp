using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Domain.DTO
{
    public class ZipCountryDto
    {
        [Required]
        public string ZipCode { get; set; }

        [Required]
        [DefaultValue("us")]
        public string CountryCode { get; set; } = "us";
    }
}
