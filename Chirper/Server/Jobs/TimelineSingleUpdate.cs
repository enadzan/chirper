using System;

using MassiveJobs.Core;

using Chirper.Server.DomainModel;
using Chirper.Server.Repositories;

namespace Chirper.Server.Jobs
{
    public class TimelineSingleUpdateArgs
    {
        public long Id { get; set; }
        public DateTime Time { get; set; }
        public int Follower { get; set; }
    }

    public class TimelineSingleUpdate: Job<TimelineSingleUpdate, TimelineSingleUpdateArgs>
    {
        private readonly IChirpDb _db;

        public TimelineSingleUpdate(IChirpDb db)
        {
            _db = db;
        }

        public override void Perform(TimelineSingleUpdateArgs args)
        {
            var timeline = new Timeline
            {
                ChirpId = args.Id,
                TimeUtc = args.Time,
                UserId = args.Follower,
                Score = int.MinValue
            };

            _db.Timeline.Add(timeline);
            _db.SaveChanges();
        }
    }
}
