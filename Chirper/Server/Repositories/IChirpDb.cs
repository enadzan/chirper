using System;

namespace Chirper.Server.Repositories
{
    public interface IChirpDb: IDisposable
    {
        IChirpUserRepository Users { get; }
        IChirpRepository Chirps { get; }
        ITimelineRepository Timeline { get; }

        void SaveChanges();

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
