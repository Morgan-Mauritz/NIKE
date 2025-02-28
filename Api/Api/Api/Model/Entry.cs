﻿using System;
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
        public virtual User User { get; set; }
        public virtual POI POI { get; set; }    
        public virtual ICollection<LikeDislikeEntry> LikeDislikeEntries { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public Entry()
        {
            LikeDislikeEntries = new HashSet<LikeDislikeEntry>();
            Comments = new HashSet<Comment>();
        }
    }

    public class FilterEntry : BaseFilter
    {
        public string POI { get; set; }
    }
    public class AddEntry
    {
        public string Description { get; set; } 
        public string UserName { get; set; }     
        public long? Rating { get; set; }
        public POIDto POI { get; set; }
    }
    public class UpdateEntry
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long? Rating { get; set; }
    }

    public class EntryDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public long? Likes { get; set; }
        public long? Rating { get; set; }
        public string POIString { get; set; }
        public List<LikeDislikeEntryDto> LikeDislikeEntries { get; set; } 
        public List<CommentWithUserDTO> Comments { get; set; }
    }

    public class LikeDislikeEntry
    {
        public long Id { get; set; }
        public long EntryId { get; set; }
        public long Likes { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Entry Entry { get; set; }
    }

    public class LikeDislikeEntryDto
    {
        public long Id { get; set; }
        public EntryDto Entry { get; set; }
        public long Likes { get; set; }
        public long UserId { get; set; }
    }
}
