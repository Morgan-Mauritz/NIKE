using NikeClientApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NikeClientApp.Models
{
    public class Comment : NotifyModel
    {
        public ICommand Edit => new Command<string>((param) => OnEdit(param));
        public ICommand Save => new Command(async () => await OnSave());

        public HttpService<Comment> HttpService { get; set; }

        public Comment()
        {
            HttpService = new HttpService<Comment>();
        }

        public int Id { get; set; }
        public Entry Entry { get; set; }
        public int User { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public string Endpoint { get => $"comments/{Id}"; }

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
        public async Task OnSave()
        {
            await HttpService.Update("comments", this);
        }

      
    }
}
