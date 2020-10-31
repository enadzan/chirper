using System.ComponentModel.DataAnnotations;

namespace Chirper.Shared
{
    public class DtoUserLogin
    {
        [Required(ErrorMessage = "Please enter your username.")]
        [MinLength(3, ErrorMessage = "Username must have at least 3 letters")]
        [MaxLength(100, ErrorMessage = "Username cannot have more than 100 letters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [MinLength(8, ErrorMessage = "Password must have at least 8 symbols")]
        [MaxLength(100, ErrorMessage = "Password cannot have more than 100 symbols")]
        public string Password { get; set; }
    }
}
