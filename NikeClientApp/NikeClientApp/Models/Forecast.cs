using System;
using System.Collections.Generic;

namespace NikeClientApp.Models
{
    public class Forecast
    {
        public string City { get; set; }
        public string Country { get; set; }
        public List<WeatherResultDto> WeatherList { get; set; }
    }
    public class WeatherResultDto
    {
        public float Temperature { get; set; }
        public float WindSpeed { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public DateTime DateTime { get; set; }
    }
}
