using System;
using MassiveJobs.Core;
using Chirper.Server.Repositories;

namespace Chirper.Server.Jobs
{
    public class TimelineUpdateArgs
    {
        public long ChirpId { get; set; }
        public int AuthorId { get; set; }

        /// <summary>
        /// Last follower whose timeline was updated
        /// </summary>
        public int? LastFollowerId { get; set; }
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
            var chirp = _db.Chirps.Find(args.ChirpId);
            if (chirp == null) throw new Exception($"Chirp {args.ChirpId} doesn't exist");

            // we are updating the followers' time lines in batches of 1000.
            // Otherwise this job might take too long and long running jobs are bad because they block
            // job workers from performing other jobs.

            const int batchSize = 1000;

            var followers = _db.Followers.FindNextFollowers(args.AuthorId, args.LastFollowerId ?? 0, batchSize);

            JobBatch.Do(() =>
            {
                foreach (var follower in followers)
                {
                    TimelineSingleUpdate.Publish(new TimelineSingleUpdateArgs
                    {
                        Id = chirp.Id,
                        Time = chirp.ChirpTimeUtc,
                        Follower = follower.FollowerId
                    });
                }
            });

            if (followers.Count == batchSize)
            {
                // possibly more followers to update, publish this same job again

                Publish(new TimelineUpdateArgs
                {
                    AuthorId = args.AuthorId,
                    ChirpId = args.ChirpId,
                    LastFollowerId = followers[^1].FollowerId
                });
            }
        }
    }
}
