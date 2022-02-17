using System.Collections.ObjectModel;

namespace NikeClientApp.Models
{
    public class POI 
    {
        public string Name { get; set; } 
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double? AvgRating { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        public int Offset { get; set; }
        public int Amount { get; set; } = 10;
        public int Total { get; set; }
        public ObservableCollection<Entry> Entries { get; set; }
    }
}
