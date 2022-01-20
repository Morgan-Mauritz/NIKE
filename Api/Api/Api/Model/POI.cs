using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;

#nullable disable

namespace Api.Model
{
    public partial class POI
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public long CityID { get; set; }

        public virtual City City {  get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
       
        public POI()
        {
            Entries = new HashSet<Entry>();
        }
    }

    public class POIDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class FilterPOI
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [EnumDataType(typeof(Sort))]
        public Sort Sort { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Sort
    {
        Name, 
        City, 
        Country, 
    }


}
