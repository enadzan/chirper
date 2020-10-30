using System;

namespace Chirper.Server.DomainModel
{
    public class HashTag
    {
        public string Tag { get; set; }
        public DateTime TimeUtc { get; set; }

        public long ChirpId { get; set; }
        public Chirp Chirp { get; set; }
    }
}
