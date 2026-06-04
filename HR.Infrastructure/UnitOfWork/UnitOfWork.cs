using HR.Domain.UnitOfWork;
using HR.Infrastructure.Persistance;

namespace HR.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HRDbContext _dbContext;
        public UnitOfWork(HRDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
