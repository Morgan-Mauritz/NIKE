using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NikeClientApp.Models
{
    public class Entry : NotifyModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public long? Rating { get; set; }
        public POI POI { get; set; }
        public string POIString { get; set; }

        private string _likeButtonImageSource = @".\Assets\LikeButtonNotFilled.png";
        public string LikeButtonImageSource { get => _likeButtonImageSource; set { SetProperty(ref _likeButtonImageSource, value); } }
        public ObservableCollection<LikeDislikeEntry> LikeDislikeEntries { get; set; }

    }

}
