﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NikeClientApp.Models
{
    public class Comment
    {
        public Entry Entry { get; set; }
        public int User { get; set; }
        public string Text { get; set; }
    }
}
