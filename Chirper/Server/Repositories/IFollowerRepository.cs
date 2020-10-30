using System.Collections.Generic;

using Chirper.Server.DomainModel;

namespace Chirper.Server.Repositories
{
    public interface IFollowerRepository: IRepository<ChirpUserFollower>
    {
        List<ChirpUserFollower> FindNextFollowers(int userId, int lastFollowerToSkip, int takeCount);
    }
}
