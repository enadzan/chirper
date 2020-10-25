using Chirper.Server.DomainModel;
using Chirper.Server.Repositories;

namespace Chirper.Server.EF.Repositories
{
    public class ChirpRepository: Repository<Chirp, long>, IChirpRepository
    {
        public ChirpRepository(ChirpDbContext context) : base(context)
        {
        }
    }
}
