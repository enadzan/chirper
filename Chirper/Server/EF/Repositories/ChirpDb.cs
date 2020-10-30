using System;
using Microsoft.EntityFrameworkCore.Storage;

using Chirper.Server.Repositories;

namespace Chirper.Server.EF.Repositories
{
    public class ChirpDb: IChirpDb
    {
        private readonly ChirpDbContext _context;

        private IDbContextTransaction _transaction;

        public IChirpUserRepository Users { get; }
        public IChirpRepository Chirps { get; }
        public IFollowerRepository Followers { get; }
        public ITimelineRepository Timeline { get; }
        public IHashTagRepository HashTags { get; }

        public ChirpDb(ChirpDbContext context)
        {
            _context = context;

            Users = new ChirpUserRepository(_context);
            Chirps = new ChirpRepository(_context);
            Followers = new FollowerRepository(_context);
            Timeline = new TimelineRepository(_context);
            HashTags = new HashTagRepository(_context);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction == null) throw new Exception("Not in transaction");

            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            if (_transaction == null) throw new Exception("Not in transaction");

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }
    }
}
