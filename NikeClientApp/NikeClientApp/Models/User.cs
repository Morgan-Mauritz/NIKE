using System;
using System.Collections.Generic;
using System.Text;

namespace NikeClientApp.Models
{
    public class User
    {
        public string Firstname { get; set; }
       
        public string Lastname { get; set; }
       
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string Username { get; set; }
        public string ApiKey { get; set; }
    }
    public class EditUser
    {
        public bool Firstname { get; set; } = true;

        public bool Lastname { get; set; } = true;

        public bool Email { get; set; } = true;

        public bool Password { get; set; } = true;

        public bool Username { get; set; } = true;

    }
}
