using HR.Domain.Repository;
using HR.Infrastructure.Migrations;
using HR.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using static System.Net.WebRequestMethods;

namespace HR.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HRDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(HRDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public async Task<IReadOnlyList<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            string? sortBy = null,
            bool isDescending = false,
            bool trackChanges = false,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (!trackChanges)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                var sortingDirection = isDescending ? "DESC" : "ASC";
                query = query.OrderBy($"{sortBy} {sortingDirection}");
            }

            return await query.ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetAsync(
            Expression<Func<T, bool>> filter,
            bool trackChanges = false,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (!trackChanges)
                query = query.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }
        public void DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
