using NikeClientApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NikeClientApp.Models
{
    public class LikeDislikeEntry
    {
        public long Id { get; set; }
        public long EntryId { get; set; }
        public long Likes { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Entry Entry { get; set; }
    }
}
