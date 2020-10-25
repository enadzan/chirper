using System.ComponentModel.DataAnnotations;

namespace Chirper.Shared
{
    public class DtoChirpPost
    {
        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string Contents { get; set; }

        public long? OriginalChirpId { get; set; }
    }
}
