using Crud_API.Infrastructure.Database.Models;
using Crud_API.Interfaces.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Crud_API.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(long id);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<List<TEntity>> GetSortedAsync(Expression<Func<TEntity, object>> orderBy, bool ascending);
        Task<List<TEntity>> GetPagedAsync(int pageNumber, int pageSize);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity, bool updateState = false);
        Task<IActionResult> Remove(TEntity entity);

    }
}
