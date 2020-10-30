using System;
using MassiveJobs.Core;
using Chirper.Server.Repositories;

namespace Chirper.Server.Jobs
{
    public class ChirpProcessing: Job<ChirpProcessing, long>
    {
        private readonly IChirpDb _db;

        public ChirpProcessing(IChirpDb db)
        {
            _db = db;
        }

        public override void Perform(long chirpId)
        {
            var chirp = _db.Chirps.Find(chirpId);
            if (chirp == null) throw new Exception($"Chirp {chirpId} doesn't exist");

            JobBatch.Do(() =>
            {
                TimelineUpdate.Publish(new TimelineUpdateArgs
                {
                    ChirpId = chirpId,
                    AuthorId = chirp.UserId
                });

                HashTagUpdate.Publish(chirpId);
            });
        }
    }
}
