using System.Collections.Generic;

namespace NikeClientApp.Models
{
    public class Category : List<POI>
    {
        public string Name { get; set; }
        public Category(string name, List<POI> poi) : base(poi)
        {
            Name = name;
        }
        public Category(string name)
        {
            Name = name;
        }
    }
}
