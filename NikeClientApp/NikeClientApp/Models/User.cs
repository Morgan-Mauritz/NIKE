using System;
using System.Collections.Generic;
using System.Text;

namespace NikeClientApp.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
       
        public string Lastname { get; set; }
       
        public string Email { get; set; }
        
        public byte[] Password { get; set; }
        public string PasswordText { get; set; }
        
        public string Username { get; set; }
        public string ApiKey { get; set; }

        public byte[] PasswordValidation { get; set; }
    }
    public class EditUser : NotifyModel
    {
        private bool _firstname = true;
        public bool Firstname { get => _firstname; set { SetProperty(ref _firstname, value); } }

        private bool _lastname = true;
        public bool Lastname { get => _lastname; set { SetProperty(ref _lastname, value); } }

        private bool _email = true;
        public bool Email { get => _email; set { SetProperty(ref _email, value); } }

        private bool _password = true;
        public bool Password { get => _password; set { SetProperty(ref _password, value); } }

        private bool _username = true;
        public bool Username { get => _username; set { SetProperty(ref _username, value); } }

    }
}
