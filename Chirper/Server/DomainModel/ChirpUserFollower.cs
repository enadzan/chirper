namespace Chirper.Server.DomainModel
{
    public class ChirpUserFollower
    {
        public int UserId { get; set; }
        public ChirpUser User { get; set; }

        public int FollowerId { get; set; }
        public ChirpUser Follower { get; set; }
    }
}
