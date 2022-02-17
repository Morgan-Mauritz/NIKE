using Xamarin.Forms.Maps;
namespace NikeClientApp.Models
{
    class GeoLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Position ToPosition => new Position(Latitude, Longitude);
    }
}
