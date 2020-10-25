using System.Collections.Generic;
using MassiveJobs.Core;
using Chirper.Server.Repositories;

namespace Chirper.Server.Jobs
{
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

            var followerIds = new List<int>();

            foreach (var follower in user.Followers)
            {
                followerIds.Add(follower.FollowerId);

                if (followerIds.Count < 1000) continue;

                FollowerTimelineUpdate.Publish(new FollowerTimelineUpdateArgs
                {
                    ChirpId = args.ChirpId,
                    FollowerIds = followerIds
                });

                followerIds = new List<int>();
            }

            if (followerIds.Count > 0)
            {
                FollowerTimelineUpdate.Publish(new FollowerTimelineUpdateArgs
                {
                    ChirpId = args.ChirpId,
                    FollowerIds = followerIds
                });
            }
        }
    }

    public class TimelineUpdateArgs
    {
        public long ChirpId { get; set; }
        public int AuthorId { get; set; }
    }
}
