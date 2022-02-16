using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NikeClientApp.Models
{
    public class Category : List<POI>
    {

        public string Name { get; set; }

        public Category(string name, List<POI> poi) : base(poi)
        {
            Name = name;
        }

        public Category(string name)
        {
            Name = name;
        }


    }
}
