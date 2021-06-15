using System;
namespace SimpleRestAPI.Models.Weather
{
    public class WeatherForecast
    {
        public string CityName { get; set; }

        public string CountryCode { get; set; }

        public string Weather { get; set; }

        public string WeatherDescription { get; set; }

        public DateTime Date { get; set; }

        public double TemperatureC { get; set; }

        public double TemperatureF => 32 + (TemperatureC / 0.5556);

        public double TemperatureMinC { get; set; }

        public double TemperatureMinF => 32 + (TemperatureMinC / 0.5556);

        public double TemperatureMaxC { get; set; }

        public double TemperatureMaxF => 32 + (TemperatureMaxC / 0.5556);

        public double TemperatureFeelsLikeC { get; set; }

        public double TemperatureFeelsLikeF => 32 + (TemperatureFeelsLikeC / 0.5556);
    }
}
