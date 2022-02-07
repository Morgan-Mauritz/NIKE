using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using NikeClientApp.Services;
using NikeClientApp.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace NikeClientApp.ViewModels
{
    public class UserPageViewModel : BaseViewModel
    {
        //public ICommand NextPage => new Command(async () => await NavigationService.NavigateTo<MainPageViewModel>());
        public ICommand BackPage => new Command(async () => await NavigationService.GoBack());
        //public ICommand Show => new Command(async () => await OnShow());
        public ICommand Edit => new Command<string>((param) => OnEdit(param));
        public ICommand Load => new Command<string>((param) => OnLoad(param));
        public ICommand Delete => new Command<string>(async (endpoint) => await OnDelete(endpoint));
        public ICommand Save => new Command<string>(async (param) => await OnSave(param));
        private HttpService<User> userClient;
        private HttpService<Reaction> reactionClient;
        private HttpService<Comment> commentClient;
        private HttpService<Models.Entry> entryClient;

        private User _user;

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private ObservableCollection<Models.Entry> _entries;

        public ObservableCollection<Models.Entry> Entries
        {
            get { return _entries; }
            set { SetProperty(ref _entries, value); }
        }

        private ObservableCollection<Comment> _comments;

        public ObservableCollection<Comment> Comments
        {
            get { return _comments; }
            set { SetProperty(ref _comments, value); }
        }

        private ObservableCollection<Reaction> _reactions;

        public ObservableCollection<Reaction> Reactions
        {
            get { return _reactions; }
            set { SetProperty(ref _reactions, value); }
        }



        private async Task OnShow()
        {
            var user = await userClient.Get("user", "");

            var comments = await commentClient.GetList("entry/comments", "");

            var reactions = await reactionClient.GetList("entry/reactions", "");

            var entries = await entryClient.GetList("entry/list", "");

            if (comments != null)
                LoadedComments = new ObservableCollection<Comment>(comments.Data);
            if (reactions != null)
                LoadedReactions = new ObservableCollection<Reaction>(reactions.Data);
            if (entries != null)
                LoadedEntries = new ObservableCollection<Models.Entry>(entries.Data);
            User = user.Data;
        }

        private EditUser _userReadOnly;

        public EditUser UserReadOnly
        {
            get { return _userReadOnly; }
            set { SetProperty(ref _userReadOnly, value); }
        }

        private bool _commentReadOnly;

        public bool CommentReadOnly
        {
            get { return _commentReadOnly; }
            set { SetProperty(ref _commentReadOnly, value); }
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


        private void OnEdit(string param)
        {
            switch (param)
            {
                case "username":
                    UserReadOnly.Username = !UserReadOnly.Username;
                    break;
                case "name":
                    UserReadOnly.Firstname = !UserReadOnly.Firstname;
                    break;
                case "lastname":
                    UserReadOnly.Lastname = !UserReadOnly.Lastname;
                    break;
                case "email":
                    UserReadOnly.Email = !UserReadOnly.Email;
                    break;
                case "password":
                    UserReadOnly.Password = !UserReadOnly.Password;
                    break;
            }
        }


        public async Task OnSave(string endpoint)
        {

            await userClient.Update("user", User);

        }

        #region OnLoad Props
        private ObservableCollection<Comment> _loadedComments;

        public ObservableCollection<Comment> LoadedComments
        {
            get { return _loadedComments; }
            set { _loadedComments = value; }
        }

        private bool _commentsLoaded;

        public bool CommentsLoaded
        {
            get { return _commentsLoaded; }
            set { SetProperty(ref _commentsLoaded, value); }
        }


        private ObservableCollection<Reaction> _loadedReactions;

        public ObservableCollection<Reaction> LoadedReactions
        {
            get { return _loadedReactions; }
            set { SetProperty(ref _loadedReactions, value); }
        }

        private bool _reactionsLoaded;

        public bool ReactionsLoaded
        {
            get { return _reactionsLoaded; }
            set { SetProperty(ref _reactionsLoaded, value); }
        }


        private ObservableCollection<Models.Entry> _loadedEntries;

        public ObservableCollection<Models.Entry> LoadedEntries
        {
            get { return _loadedEntries; }
            set { SetProperty(ref _loadedEntries, value); }
        }

        private bool _entriesLoaded;

        public bool EntriesLoaded
        {
            get { return _entriesLoaded; }
            set { SetProperty(ref _entriesLoaded, value); }
        }


        #endregion
        public void OnLoad(string param)
        {
            switch (param)
            {
                case "comments":

                    if (!CommentsLoaded)
                    {
                        Comments = LoadedComments;
                    }
                    else
                    {
                        Comments = new ObservableCollection<Comment>();
                    }
                    CommentsLoaded = !CommentsLoaded;

                    break;
                case "reactions":
                    if (!ReactionsLoaded)
                    {
                        Reactions = LoadedReactions;
                    }
                    else
                    {
                        Reactions = new ObservableCollection<Reaction>();
                    }
                    ReactionsLoaded = !ReactionsLoaded;
                    break;
                case "entries":
                    if (!EntriesLoaded)
                    {
                        Entries = LoadedEntries;
                    }
                    else
                    {
                        Entries = new ObservableCollection<Models.Entry>();
                    }
                    EntriesLoaded = !EntriesLoaded;
                    break;
            }
        }
        public async Task OnDelete(string endpoint)
        {

            if (endpoint == "user")
            {
                UserApi.ApiKey = null;
                await userClient.Delete(endpoint);
                await NavigationService.NavigateTo<MainPageViewModel>();
            }
            else if (endpoint.Contains("comment"))
            {
                var response = await commentClient.Delete(endpoint);
                var comment = Comments.First(x => x.Id == response.Data.Id);
                Comments.Remove(comment);
            }
            else if (endpoint.Contains("entry"))
            {
                var response = await entryClient.Delete(endpoint);
                var entry = Entries.First(x => x.Id == response.Data.Id);
                Entries.Remove(entry);
            }
            else if (endpoint.Contains("reaction"))
            {
                var response = await reactionClient.Delete(endpoint);
                var reaction = Reactions.First(x => x.Id == response.Data.Id);
                Reactions.Remove(reaction);
            }
        }


        public UserPageViewModel(INaviService naviService) : base(naviService)
        {
            userClient = new HttpService<User>();
            reactionClient = new HttpService<Reaction>();
            commentClient = new HttpService<Comment>();
            entryClient = new HttpService<Models.Entry>();
            UserReadOnly = new EditUser();
            Comments = new ObservableCollection<Comment>();
            Entries = new ObservableCollection<Models.Entry>();
            Reactions = new ObservableCollection<Reaction>();

        }

        public async override Task InitAsync()
        {
            await OnShow();
        }


    }
}
