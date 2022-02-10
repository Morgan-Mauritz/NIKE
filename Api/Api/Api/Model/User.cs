using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

#nullable disable

namespace Api.Model
{
    public partial class User
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; }
        [Required]
        public string Username { get; set; }
        public string ApiKey { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<LikeDislikeEntry> LikeDislikeEntries { get; set; }
       
        public User()
        {
            Entries = new HashSet<Entry>();
            LikeDislikeEntries = new HashSet<LikeDislikeEntry>();
        }
    }

    public class UserApiDto
    {
        public string ApiKey { get; set; }
    }

    public class UserDto
    {
        [Required]
        [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\", MinLength = 2, MaxLength = 255)]
        public string Firstname { get; set; }
        [Required]
        [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\", MinLength = 2, MaxLength = 255)]
        public string Lastname { get; set; }
        [Required]
        [RegularExpression(@"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$")]
        public string Email { get; set; }
        [Required]
        //[RegularExpression(@"^(?=.[a-z])(?=.[A-Z])(?=.\d)[A-Za-z\d@$!%#?&]{8,255}$")]
        public byte[] Password { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string ApiKey { get; set; }

    }

   public class AddUserDto
    {
        public string Firstname { get; set; }
        [Required]
        [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\", MinLength = 2, MaxLength = 255)]
        public string Lastname { get; set; }
        [Required]
        [RegularExpression(@"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$")]
        public string Email { get; set; }
        [Required]
        //[RegularExpression(@"^(?=.[a-z])(?=.[A-Z])(?=.\d)[A-Za-z\d@$!%#?&]{8,255}$")]
        public byte[] Password { get; set; }
        [Required]
        public string Username { get; set; }
    }

    public class LogInModel
    {
        [Required]
        [RegularExpression(@"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$")]
        public string Email { get; set; }
        [Required]
        //[RegularExpression(@"^(?=.[a-z])(?=.[A-Z])(?=.\d)[A-Za-z\d@$!%#?&]{8,255}$")]
        public byte[] Password { get; set; }
    }

    public class UpdateUserDto
    {
        [MinLength(2, ErrorMessage = "{0} Must be at least 2 characters")]
        public string Firstname { get; set; }

        [MinLength(2, ErrorMessage = "{0} Must be at least 2 characters")]
        public string Lastname { get; set; }

        [MinLength(2, ErrorMessage = "{0} Must be at least 2 characters")]
        public string Email { get; set; }

        [MinLength(8, ErrorMessage = "{0} Must be at least 8 characters")]
        public byte[] Password { get; set; }

        [MinLength(2, ErrorMessage = "{0} Must be at least 2 characters")]
        public string Username { get; set; }

        public byte[] PasswordValidation { get; set; }
    }
}
