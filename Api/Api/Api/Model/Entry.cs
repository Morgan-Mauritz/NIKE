using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Api.Model
{
    public partial class Entry
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long POIID { get; set; }
        [Required]
        public long? Rating { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long Likes { get; set; } 

        public virtual User User { get; set; }

        public virtual POI POI { get; set; }    
       
    }

    public class AddEntry
    {
        public string Description { get; set; } 

        public string UserName { get; set; }     

        public long? Rating { get; set; }

        public POIDto POI { get; set; }

    }

    public class EntryDto
    {
        public string Description { get; set; }

        public string UserName { get; set; }

        public long? Likes { get; set; }

        public long? Rating { get; set; }

        public string POI { get; set; }
    }


}
