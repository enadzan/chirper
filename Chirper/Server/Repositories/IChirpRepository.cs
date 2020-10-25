using Chirper.Server.DomainModel;

namespace Chirper.Server.Repositories
{
    public interface IChirpRepository: IRepository<Chirp, long>
    {
    }
}
