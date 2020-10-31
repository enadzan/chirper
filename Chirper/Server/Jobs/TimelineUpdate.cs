using System;
using MassiveJobs.Core;
using Chirper.Server.Repositories;

namespace Chirper.Server.Jobs
{
    public class TimelineUpdateArgs
    {
        public int AuthorId { get; set; }
        public long ChirpId { get; set; }
        public DateTime TimeUtc { get; set; }

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
            // we are updating the followers' time lines in batches of 10_000.
            // Otherwise this job might take too long and long running jobs are bad because they block
            // job workers from performing other jobs.

            if (args.LastFollowerId == null) System.Diagnostics.Debug.WriteLine("START: " + DateTime.Now);

            const int batchSize = 10_000;

            var followerIds = _db.Followers.FindNextFollowerIds(args.AuthorId, args.LastFollowerId ?? 0, batchSize);

            if (followerIds.Count == batchSize)
            {
                // possibly more followers to update, publish this same job again.
                // We do it first, to kick off the next batch as soon as possible

                Publish(new TimelineUpdateArgs
                {
                    AuthorId = args.AuthorId,
                    ChirpId = args.ChirpId,
                    TimeUtc = args.TimeUtc,
                    LastFollowerId = followerIds[^1]
                });
            }

            if (followerIds.Count > 0)
            {
                _db.Followers.UpdateFollowerTimelines(args.AuthorId, args.ChirpId, args.TimeUtc,
                    args.LastFollowerId ?? 0, followerIds[^1]);
            }

            if (followerIds.Count < batchSize) System.Diagnostics.Debug.WriteLine("END: " + DateTime.Now);
        }
    }
}
