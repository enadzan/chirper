using System;

namespace Chirper.Server.DomainModel
{
    public class Timeline
    {
        public int UserId { get; set; }
        public ChirpUser User { get; set; }

        public DateTime TimeUtc { get; set; }

        public long ChirpId { get; set; }
        public Chirp Chirp { get; set; }

        public int Score { get; set; }
    }
}
