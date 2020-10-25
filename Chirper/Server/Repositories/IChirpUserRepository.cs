using Chirper.Server.DomainModel;

namespace Chirper.Server.Repositories
{
    public interface IChirpUserRepository: IRepository<ChirpUser, int>
    {
        ChirpUser FindWithFollowers(int userId);
    }
}
