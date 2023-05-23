using Crud_API.Infrastructure.Database.Models;
using Crud_API.Interfaces.Data;
using System.Linq.Expressions;

namespace Crud_API.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> GetAsync(long id);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<List<TEntity>> GetSortedAsync(Expression<Func<TEntity, object>> orderBy, bool ascending);
        Task<List<TEntity>> GetPagedAsync(int pageNumber, int pageSize);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity, bool updateState = false);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

    }
}
