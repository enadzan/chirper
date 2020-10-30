using System;

namespace Chirper.Server.Repositories
{
    public interface IChirpDb: IDisposable
    {
        IChirpUserRepository Users { get; }
        IChirpRepository Chirps { get; }
        IFollowerRepository Followers { get; }
        ITimelineRepository Timeline { get; }
        IHashTagRepository HashTags { get; }

        void SaveChanges();

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
