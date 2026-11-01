using HR.Domain.Data.Entities;
using HR.Domain.Repository;
using HR.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly HRDbContext _context;
        public EmployeeRepository(HRDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
        public async Task AssignManagerToDepartmentEmployeesAsync(int departmentId, int newManagerId)
        {
            await _context.Set<Employee>()
                .Where(e => e.DepartmentId == departmentId && e.Id != newManagerId)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(e => e.ManagerId, newManagerId));
        }
    }
}
