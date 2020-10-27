using System;
using System.Threading;
using System.Threading.Tasks;

using MassiveJobs.Core;

using Chirper.Server.Repositories;

namespace Chirper.Server.Jobs
{
    public class TimelineUpdateArgs
    {
        public long ChirpId { get; set; }
        public int AuthorId { get; set; }
    }

    public class TimelineUpdate: JobAsync<TimelineUpdate, TimelineUpdateArgs>
    {
        private readonly IChirpDb _db;

        public TimelineUpdate(IChirpDb db)
        {
            _db = db;
        }

        public override Task Perform(TimelineUpdateArgs args, CancellationToken cancellationToken)
        {
            var chirp = _db.Chirps.Find(args.ChirpId);
            if (chirp == null) throw new Exception($"Chirp {args.ChirpId} doesn't exist");

            if (chirp.IsTimelineSyncStarted) return Task.CompletedTask; // IMPORTANT: make sure we are idempotent

            var user = _db.Users.FindWithFollowers(args.AuthorId);

            JobBatch.Do(() =>
            {
                foreach (var follower in user.Followers)
                {
                    if (cancellationToken.IsCancellationRequested) break;

                    TimelineSingleUpdate.Publish(new TimelineSingleUpdateArgs
                    {
                        Id = chirp.Id,
                        Time = chirp.ChirpTimeUtc,
                        Follower = follower.FollowerId
                    });
                }
            });

            if (!cancellationToken.IsCancellationRequested)
            {
                chirp.IsTimelineSyncStarted = true;
                _db.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
