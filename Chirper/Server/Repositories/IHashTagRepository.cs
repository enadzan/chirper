using System;
using Chirper.Server.DomainModel;

namespace Chirper.Server.Repositories
{
    public interface IHashTagRepository: IRepository<HashTag>
    {
        HashTag Find(string tag, DateTime timeUtc, long chirpId);
        bool Exists(string tag, DateTime timeUtc, long chirpId);
    }
}
