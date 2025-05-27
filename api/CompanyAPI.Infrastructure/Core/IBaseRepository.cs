namespace CompanyAPI.Infrastructure.Core
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        Task CommitAsync();
    }
}
