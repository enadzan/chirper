using MassiveJobs.Core;
using Chirper.Server.Repositories;

namespace Chirper.Server.Jobs
{
    public class TimelineUpdateArgs
    {
        public long ChirpId { get; set; }
        public int AuthorId { get; set; }
    }

    public class TimelineUpdate: Job<TimelineUpdate, TimelineUpdateArgs>
    {
        private readonly IChirpDb _db;

        public TimelineUpdate(IChirpDb db)
        {
            _db = db;
        }

        public override void Perform(TimelineUpdateArgs args)
        {
            var user = _db.Users.FindWithFollowers(args.AuthorId);
            var chirp = _db.Chirps.Find(args.ChirpId);

            JobBatch.Do(() =>
            {
                foreach (var follower in user.Followers)
                {
                    TimelineSingleUpdate.Publish(new TimelineSingleUpdateArgs
                    {
                        Id = chirp.Id,
                        Time = chirp.ChirpTimeUtc,
                        Follower = follower.FollowerId
                    });
                }
            });
        }
    }
}
