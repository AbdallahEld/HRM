using HR.Application.Shared;

namespace HR.Application.Features.Departments.Services
{
    public interface IDepartmentCapacityChecker
    {
        Task<ApiResponse<int>> ValidateChildCapacityAsync(int parentId, int newHeadCount, int? currentDepartmentId = null);
        Task<ApiResponse<int>> ValidateParentCapacityAsync(int departmentId, int newHeadCount);
    }
}
