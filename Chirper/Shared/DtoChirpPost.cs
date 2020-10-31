using System.ComponentModel.DataAnnotations;

namespace Chirper.Shared
{
    public class DtoChirpPost
    {
        [Required(ErrorMessage = "Please enter the chirp contents.")]
        [MinLength(10, ErrorMessage = "Chirp contents must have at least 10 letters.")]
        [MaxLength(100, ErrorMessage = "Chirp contents cannot have more than 100 letters.")]
        public string Contents { get; set; }
    }
}
