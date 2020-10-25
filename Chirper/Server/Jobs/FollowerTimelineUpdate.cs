using System.Collections.Generic;
using MassiveJobs.Core;

using Chirper.Server.DomainModel;
using Chirper.Server.Repositories;

namespace Chirper.Server.Jobs
{
    public class FollowerTimelineUpdate: Job<FollowerTimelineUpdate, FollowerTimelineUpdateArgs>
    {
        private readonly IChirpDb _db;

        public FollowerTimelineUpdate(IChirpDb db)
        {
            _db = db;
        }

        public override void Perform(FollowerTimelineUpdateArgs args)
        {
            var chirp = _db.Chirps.Find(args.ChirpId);

            foreach (var followerId in args.FollowerIds)
            {
                var timeline = new Timeline
                {
                    ChirpId = args.ChirpId,
                    TimeUtc = chirp.ChirpTimeUtc,
                    UserId = followerId,
                    Score = int.MinValue
                };

                _db.Timeline.Add(timeline);
            }

            _db.SaveChanges();
        }
    }

    public class FollowerTimelineUpdateArgs
    {
        public long ChirpId { get; set; }
        public List<int> FollowerIds { get; set; }
    }
}
