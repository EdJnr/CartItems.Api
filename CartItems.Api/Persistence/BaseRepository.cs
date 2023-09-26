using CartItems.Api.Database;
using CartItems.Api.Interfaces.IPersistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CartItems.Api.Persistence
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDatabaseContext _dbContext;
        private readonly DbSet<T> _db;

        public BaseRepository(ApplicationDatabaseContext context)
        {
            _dbContext = context;
            _db = context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _db.FindAsync(id);

            if (model != null)
            {
                _db.Remove(model);
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            var result = await _db.AsNoTracking().ToListAsync();

            return result;
        }

        public async Task<T> GetAsync(int id)
        {
            var model = await _db.FindAsync(id);

            return model;
        }

        public async Task<IReadOnlyList<T>> QueryAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Expression<Func<T, T>> select = null,
            Expression<Func<T, object>>[] includes = null,
            int page = 0,
            int pageSize = 0
        )
        {
            IQueryable<T> query = _db;

            if (select != null)
            {
                query = query.Select(select);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            //pagination
            if (page > 0 && pageSize > 0)
            {
                // initial items to skip
                int skipCount = (page - 1) * pageSize;

                // paginate
                query = query.Skip(skipCount).Take(pageSize);
            }

            var results = await query.AsNoTracking().ToListAsync();
            return results;
        }

        public async Task UpdateAsync(int id, T entity)
        {
            var model = await _db.FindAsync(id);

            if (model != null)
            {
                _dbContext.Entry(model).State = EntityState.Detached;
                _db.Update(entity);
            }
        }
    }
}
