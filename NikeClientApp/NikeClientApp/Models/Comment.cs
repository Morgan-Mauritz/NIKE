using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NikeClientApp.Models
{
    public class Comment : NotifyModel
    {
        public ICommand Edit => new Command<string>((param) => OnEdit(param));
        public int Id { get; set; }
        public Entry Entry { get; set; }
        public int User { get; set; }
        public string Text { get; set; }

        public string Endpoint { get => $"comment/{Id}"; }

        private bool _commentReadOnly = true;

        public bool CommentReadOnly
        {
            get { return _commentReadOnly; }
            set { SetProperty (ref _commentReadOnly, value); }
        }

        public void OnEdit(string param)
        {
            CommentReadOnly = !CommentReadOnly;
        }

      
    }
}
