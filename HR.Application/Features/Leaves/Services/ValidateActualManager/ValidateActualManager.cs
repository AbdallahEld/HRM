using HR.Application.Shared;
using HR.Domain.UnitOfWork;

namespace HR.Application.Features.Leaves.Services.ValidateActualManager
{
    public class ValidateActualManager (IUnitOfWork unitOfWork) : IValidateActualManager
    {
        public async Task<ApiResponse<string>> ValidateManager(int managerId, int employeeId)
        {
            var employee = await unitOfWork._EmployeeRepository.GetByIdAsync(employeeId);
            if (employee.ManagerId != managerId)
            {
                return ApiResponse<string>.FailureResponse(new List<string>
                {
                    "You are not authorized to approve this leave request."
                });
            }

            return ApiResponse<string>.SuccessResponse("Manager validation successful.");
        }

    }
}
