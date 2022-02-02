using NikeClientApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace NikeClientApp.Models
{
    public class POI 
    {
        //string name;
        //string comment;

      
        public string Name { get; set; } //{ get { return name; } set { SetProperty(ref name, value); } }
        public string Comment { get; set; } // { get { return comment; } set { SetProperty(ref comment, value); } }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public long? AvgRating { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        //public List<EntryDto> Entries { get; set; }
    }
}
