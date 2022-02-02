using System;
using System.Collections.Generic;
using System.Text;

namespace NikeClientApp.Models
{
    public class Reaction
    {
        public Entry Entry { get; set; }
        public string User { get; set; }
        public int Like { get; set; }
    }
}
