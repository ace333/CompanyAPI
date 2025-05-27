using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Infrastructure.Core
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;

        protected BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
