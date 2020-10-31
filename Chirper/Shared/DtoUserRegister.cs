using System.ComponentModel.DataAnnotations;

namespace Chirper.Shared
{
    public class DtoUserRegister
    {
        [Required(ErrorMessage = "Please enter your desired username.")]
        [MinLength(3, ErrorMessage = "Username must have at least 3 letters")]
        [MaxLength(100, ErrorMessage = "Username cannot have more than 100 letters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your desired password.")]
        [MinLength(8, ErrorMessage = "Password must have at least 8 symbols")]
        [MaxLength(100, ErrorMessage = "Password cannot have more than 100 letters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your desired password.")]
        [MinLength(8, ErrorMessage = "Confirmation password must have at least 8 symbols")]
        [MaxLength(100, ErrorMessage = "Confirmation password cannot have more than 100 letters")]
        public string PasswordConfirm { get; set; }
    }
}
