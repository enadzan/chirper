namespace Chirper.Server.Repositories
{
    public interface IRepository<in TEntity> where TEntity: class
    {
        void Add(TEntity entity);
    }

    public interface IRepository<TEntity, in TPk>: IRepository<TEntity> where TEntity: class
    {
        TEntity Find(TPk pk);
    }
}
