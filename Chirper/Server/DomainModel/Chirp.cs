using System;

namespace Chirper.Server.DomainModel
{
    public class Chirp
    {
        public long Id { get; set; }

        public int UserId { get; set; }
        public ChirpUser User { get; set; }

        public ChirpType ChirpType { get; set; }
        public DateTime ChirpTimeUtc { get; set; }
        public string Contents { get; set; }
        public long? OriginalChirpId { get; set; }
        public int Score { get; set; }
    }
}
