using HR.Application.Shared;

namespace HR.Application.Features.Leaves.Services.ValidateActualManager
{
    public interface IValidateActualManager
    {
        public Task<ApiResponse<string>> ValidateManager(int managerId, int employeeId);
    }
}
