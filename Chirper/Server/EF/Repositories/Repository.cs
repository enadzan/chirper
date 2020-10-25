using Chirper.Server.Repositories;

namespace Chirper.Server.EF.Repositories
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity: class
    {
        protected readonly ChirpDbContext Context;

        public Repository(ChirpDbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }
    }

    public class Repository<TEntity, TPk>: Repository<TEntity> where TEntity: class
    {
        public Repository(ChirpDbContext context): base(context)
        {
        }

        public TEntity Find(TPk pk)
        {
            return Context.Set<TEntity>().Find(pk);
        }
    }
}
