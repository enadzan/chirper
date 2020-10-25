using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Chirper.Server.DomainModel;
using Chirper.Server.Repositories;

namespace Chirper.Server.EF.Repositories
{
    public class TimelineRepository: Repository<Timeline>, ITimelineRepository
    {
        public TimelineRepository(ChirpDbContext context) : base(context)
        {
        }

        public Timeline Find(int userId, DateTime timeUtc, long chirpId)
        {
            return Context.Set<Timeline>().Find(userId, timeUtc, chirpId);
        }

        public List<Timeline> FindByTime(int userId, DateTime fromUtc, DateTime toUtc, long? afterChirpId, int batchSize)
        {
            var q = Context.Set<Timeline>()
                .Include(t => t.Chirp)
                .Where(t => t.UserId == userId && 
                            t.TimeUtc >= fromUtc && 
                            t.TimeUtc < toUtc);

            if (afterChirpId.HasValue)
            {
                q = q.Where(t => t.ChirpId > afterChirpId.Value);
            }

            return q
                .OrderByDescending(t => t.Score)
                .ThenByDescending(t => t.Chirp.Score)
                .ThenBy(t => t.ChirpId)
                .Take(batchSize)
                .ToList();
        }

        public void Reset(int userId, DateTime fromUtc, DateTime toUtc)
        {
            Context.Database.ExecuteSqlInterpolated($@"
UPDATE timeline SET score = {int.MinValue}
WHERE user_id = {userId} 
    AND time_utc >= {fromUtc}
    AND time_utc < {toUtc}
");
        }
    }
}
