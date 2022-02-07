using Newtonsoft.Json;
using NikeClientApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NikeClientApp.Models
{
    public class Entry : NotifyModel
    {
        public ICommand Edit => new Command<string>((param) => OnEdit(param));
        public ICommand Save => new Command(async () => await OnSave());

        public HttpService<Entry> HttpService { get; set; }

        public Entry()
        {
            HttpService = new HttpService<Entry>();
        }

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
            await HttpService.Update($"entry/{Id}", this);
        }

    }
}
