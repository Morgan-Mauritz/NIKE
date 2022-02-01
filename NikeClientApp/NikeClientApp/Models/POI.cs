using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace NikeClientApp.Models
{
    public class POI : INotifyPropertyChanged
    {
        string name;
        string comment;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public string Name { get { return name; } set { SetProperty(ref name, value); } }
        public string Comment { get { return comment; } set { SetProperty(ref comment, value); } }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public long? AvgRating { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        //public List<EntryDto> Entries { get; set; }
    }
}
