using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NikeClientApp.Models
{
    public class Entry
    {
        public string Description { get; set; }
        public string POI { get; set; }
        public int Rating { get; set; }
        public string Username { get; set; }

    }
}
