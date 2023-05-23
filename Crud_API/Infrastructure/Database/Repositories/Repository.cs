using Crud_API.Infrastructure.Database.Models;
using Crud_API.Interfaces.Data;
using Crud_API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Crud_API.Infrastructure.Database.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly CrudDbcontext dbContext;

        public Repository(CrudDbcontext databaseContext) 
        {
            dbContext = databaseContext;
        }

        public async Task<TEntity> GetAsync(long Id)
        {
            return await dbContext.Set<TEntity>().Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await (filter != null ? dbContext.Set<TEntity>().Where(filter) : dbContext.Set<TEntity>()).ToListAsync();
        }

        public async Task<List<TEntity>> GetSortedAsync(Expression<Func<TEntity, object>> orderBy, bool ascending)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            if (orderBy != null)
            {
                if (ascending)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }
            }

            return await query.ToListAsync(); ;
        }

        public async Task<List<TEntity>> GetPagedAsync(int pageNumber, int pageSize)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public void Add(TEntity entity)
        {
            PrepareEntityForAdd(entity);

            dbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                PrepareEntityForAdd(entity);
            }

            dbContext.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity, bool updateState = false)
        {
            if (entity is IEntityEditTrackable)
            {
                (entity as IEntityEditTrackable).ModifiedOn = DateTime.UtcNow;
            }

            if (updateState)
            {
                dbContext.Entry<TEntity>(entity).State = EntityState.Modified;
            }
        }

        public void Remove(TEntity entity)
        {
            if (entity is IDeleteTrackable)
            {
                (entity as IDeleteTrackable).IsDeleted = true;
                Update(entity);
            }
            else
            {
                dbContext.Set<TEntity>().Remove(entity);
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities.FirstOrDefault() is IDeleteTrackable)
            {
                foreach (var entity in entities)
                {
                    (entity as IDeleteTrackable).IsDeleted = true;
                    Update(entity);
                }
            }
            else
            {
                dbContext.Set<TEntity>().RemoveRange(entities);
            }
        }

        private void PrepareEntityForAdd(TEntity entity)
        {
            if (entity is IEntityCreateTrackable)
            {
                var currentdateTime = DateTime.UtcNow;

                (entity as IEntityCreateTrackable).CreatedOn = currentdateTime;
                (entity as IEntityEditTrackable).ModifiedOn = currentdateTime;
            }
        }
    }
}
