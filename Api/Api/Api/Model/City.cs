using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Api.Model
{
    public partial class City
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long? CountryId { get; set; }

        public virtual ICollection<POI> POI { get; set; }
        public virtual Country Country { get; set; }
        public City()
        {
            POI = new HashSet<POI>();
        }
    }
}
