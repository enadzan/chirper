using Chirper.Server.DomainModel;

namespace Chirper.Server.Repositories
{
    public interface IChirpUserRepository: IRepository<ChirpUser, int>
    {
        bool UserExists(string username);
        ChirpUser FindByUsername(string username);
    }
}
