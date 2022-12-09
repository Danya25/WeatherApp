namespace WeatherApp.Common
{
    public static class TemperatureConverter
    {
        /// <summary>
        /// Convert Farenheit to Degree.
        /// </summary>
        /// <param name="farenheitValue"></param>
        /// <returns></returns>
        public static double FarenheitToDegree(double farenheitValue)
        {
            return (farenheitValue - 32) / 1.8;
        }
    }
}