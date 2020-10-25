using System.Linq;
using Microsoft.EntityFrameworkCore;

using Chirper.Server.DomainModel;
using Chirper.Server.Repositories;

namespace Chirper.Server.EF.Repositories
{
    public class ChirpUserRepository: Repository<ChirpUser, int>, IChirpUserRepository
    {
        public ChirpUserRepository(ChirpDbContext context) : base(context)
        {
        }

        public ChirpUser FindWithFollowers(int userId)
        {
            return Context
                .Set<ChirpUser>()
                .Include(u => u.Followers)
                .SingleOrDefault(u => u.Id == userId);
        }
    }
}
