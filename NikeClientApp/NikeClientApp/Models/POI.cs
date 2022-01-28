using System;
using System.Collections.Generic;
using System.Text;

namespace NikeClientApp.Models
{
    public class POI
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public long? AvgRating { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        //public List<EntryDto> Entries { get; set; }
    }
}
