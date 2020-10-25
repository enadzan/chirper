using System.Collections.Generic;

namespace Chirper.Server.DomainModel
{
    public class ChirpUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<ChirpUserFollower> Followers { get; set; }
    }
}
