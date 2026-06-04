using System.Linq.Expressions;

namespace HR.Domain.Repository
{
    public interface IGenericRepository<T>
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
