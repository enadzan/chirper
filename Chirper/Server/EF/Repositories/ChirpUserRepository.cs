using Chirper.Server.DomainModel;
using Chirper.Server.Repositories;

namespace Chirper.Server.EF.Repositories
{
    public class ChirpUserRepository: Repository<ChirpUser, int>, IChirpUserRepository
    {
        public ChirpUserRepository(ChirpDbContext context) : base(context)
        {
        }
    }
}
