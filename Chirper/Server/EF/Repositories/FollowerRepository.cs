using System.Collections.Generic;
using System.Linq;

using Chirper.Server.DomainModel;
using Chirper.Server.Repositories;

namespace Chirper.Server.EF.Repositories
{
    public class FollowerRepository: Repository<ChirpUserFollower>, IFollowerRepository
    {
        public FollowerRepository(ChirpDbContext context) : base(context)
        {
        }

        public List<ChirpUserFollower> FindNextFollowers(int userId, int lastFollowerToSkip, int takeCount)
        {
            return Context.Set<ChirpUserFollower>()
                .Where(uf => uf.UserId == userId && uf.FollowerId > lastFollowerToSkip)
                .OrderBy(uf => uf.FollowerId)
                .Take(takeCount)
                .ToList();
        }
    }
}
