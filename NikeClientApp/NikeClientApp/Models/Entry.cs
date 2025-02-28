﻿using NikeClientApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NikeClientApp.Models
{
    public class Entry : NotifyModel
    {
        public ICommand Edit => new Command<string>((param) => OnEdit(param));
        public ICommand Save => new Command(async () => await OnSave());
        public string Endpoint { get => $"entry/{Id}"; }

        public HttpService<Entry> HttpService { get; set; }

        public Entry()
        {
            HttpService = new HttpService<Entry>();
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public POI POI { get; set; }
        public string POIstring { get; set; }

        private string _likeButtonImageSource = @".\Assets\LikeButtonNotFilled.png";
        public string LikeButtonImageSource { get => _likeButtonImageSource; set { SetProperty(ref _likeButtonImageSource, value); } }
        public ObservableCollection<LikeDislikeEntry> LikeDislikeEntries { get; set; }

        public ObservableCollection<Comment> Comments { get; set; }

        private int _rating;
        public int Rating
        {
            get => _rating;
            set
            {
                if (value > 5) _rating = 5;
                else if (value < 1) _rating = 1;
                else _rating = value;

                OnPropertyChanged("StarRating");
            }
        }
        public string Username { get; set; }

        public string StarRating
        {
            get
            {
                string stars = "";
                for (int i = 0; i < Rating; i++)
                {
                    stars += "★";
                }
                return stars;
            }
        }

        private bool _entryReadOnly = true;

        public bool EntryReadOnly
        {
            get { return _entryReadOnly; }
            set { SetProperty(ref _entryReadOnly, value); }
        }

        private bool _ratingReadOnly = true;

        public bool RatingReadOnly
        {
            get { return _ratingReadOnly; }
            set { SetProperty(ref _ratingReadOnly, value); }
        }

        public void OnEdit(string param)
        {
            EntryReadOnly = !EntryReadOnly;
            RatingReadOnly = !RatingReadOnly;
        }

        public async Task OnSave()
        {
            await HttpService.Update($"entry", this);
        }

    }
}
