using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Model
{
    public class Category
    {

        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<POI> POIs { get; set; }

    }
}
