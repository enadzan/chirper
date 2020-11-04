using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Chirper.Server.DomainModel;
using Chirper.Server.Repositories;

namespace Chirper.Server.EF.Repositories
{
    public class FollowerRepository: Repository<ChirpUserFollower>, IFollowerRepository
    {
        public FollowerRepository(ChirpDbContext context) : base(context)
        {
        }

        public List<int> FindNextFollowerIds(int userId, int lastFollowerToSkip, int takeCount)
        {
            return Context.Set<ChirpUserFollower>()
                .Where(uf => uf.UserId == userId && uf.FollowerId > lastFollowerToSkip)
                .OrderBy(uf => uf.FollowerId)
                .Take(takeCount)
                .Select(u => u.FollowerId)
                .ToList();
        }

        public int UpdateFollowerTimelines(int userId, long chirpId, DateTime chirpTimeUtc, int fromFollowerExclusive, int toFollowerInclusive)
        {
            try
            {
                return Context.Database.ExecuteSqlInterpolated($@"
INSERT INTO timeline (user_id, time_utc, chirp_id)
SELECT uf.follower_id, {chirpTimeUtc}, {chirpId}
FROM chirp_user_follower uf
WHERE uf.user_id = {userId}
    AND uf.follower_id > {fromFollowerExclusive}
    AND uf.follower_id <= {toFollowerInclusive}
");
            }
            catch (SqlException ex)
            {
                // if this is not PK violation throw
                if (ex.Number != 2627) throw;

                // otherwise, it means this has already be done
                return 0;
            }
        }
    }
}
