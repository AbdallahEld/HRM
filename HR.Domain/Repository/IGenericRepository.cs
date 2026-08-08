using System.Linq.Expressions;

namespace HR.Domain.Repository
{
    public interface IGenericRepository<T>
    {
        Task<IReadOnlyList<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            string? sortBy = null,
            bool isDescending = false,
            bool trackChanges = false,
            params Expression<Func<T, object>>[] includes
            );
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetAsync(
            Expression<Func<T, bool>> filter,
            bool trackChanges = false,
            params Expression<Func<T, object>>[] includes);

        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
