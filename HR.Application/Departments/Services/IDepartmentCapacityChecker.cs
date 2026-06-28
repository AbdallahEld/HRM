namespace HR.Application.Departments.Services
{
    public interface IDepartmentCapacityChecker
    {
        Task ValidateChildCapacityAsync(int parentId, int newHeadCount, int? currentDepartmentId = null);
        Task ValidateParentCapacityAsync(int departmentId, int newHeadCount);
    }
}
