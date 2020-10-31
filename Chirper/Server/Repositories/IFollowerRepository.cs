using System;
using System.Collections.Generic;

using Chirper.Server.DomainModel;

namespace Chirper.Server.Repositories
{
    public interface IFollowerRepository : IRepository<ChirpUserFollower>
    {
        List<int> FindNextFollowerIds(int userId, int lastFollowerToSkip, int takeCount);

        int UpdateFollowerTimelines(int userId, long chirpId, DateTime chirpTimeUtc,
            int fromFollowerExclusive, int toFollowerInclusive);
    }
}
