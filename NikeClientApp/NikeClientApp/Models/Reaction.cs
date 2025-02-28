﻿namespace NikeClientApp.Models
{
    public class Reaction
    {
        public long Id { get; set; }
        public Entry Entry { get; set; }
        public string User { get; set; }
        public int Like { get; set; }
        public string Endpoint { get => $"entry/like/{Entry.Id}"; }
    }
}
