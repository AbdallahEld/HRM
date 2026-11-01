using HR.Domain.Data.Entities;

namespace HR.Domain.Repository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task AssignManagerToDepartmentEmployeesAsync(int departmentId, int newManagerId);
    }
}
