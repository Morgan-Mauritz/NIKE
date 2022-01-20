using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Model
{
    public class Forecast
    {
        public List<WeatherResult> List { get; set; }
        public CityResult City { get; set; }
        public class CityResult
        {
            public string Name { get; set; }
            public string Country { get; set; }
        }
        public class WeatherResult
        {
            public TemperatureModel Main { get; set; }
            public List<WeatherModel> Weather { get; set; }
            public string Dt_txt { get; set; }
            public WindModel Wind { get; set; }
        }
        public class TemperatureModel
        {
            public float Temp { get; set; }
            public int Humidity { get; set; }
        }
        public class WindModel
        {
            public float Speed { get; set; }
        }
        public class WeatherModel
        {
            public string Description { get; set; }
            public string Icon { get; set; }
        }

        public class ForecastDto
        {
            public string City { get; set; }
            public string Country { get; set; }
            public List<WeatherResult> WeatherList { get; set; }
        }
    }
}
