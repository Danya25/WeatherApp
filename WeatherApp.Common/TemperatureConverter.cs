namespace WeatherApp.Common
{
    public static class TemperatureConverter
    {
        public static double CelsiusToFarenheit(double сelsius)
        {
            return сelsius * 1.8 + 32;
        }
    }
}