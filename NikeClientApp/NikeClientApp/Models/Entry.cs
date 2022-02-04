using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NikeClientApp.Models
{
    public class Entry
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string POI { get; set; }
        public int Rating { get; set; }
        public string Username { get; set; }

        public string StarRating { get 
            {
                string stars = "";
                for (int i = 0; i < Rating; i++)
                {
                    stars += "★";
                }
                return stars;
            } 
        }
        public string Endpoint { get => $"entry/{Id}"; }

    }
}
