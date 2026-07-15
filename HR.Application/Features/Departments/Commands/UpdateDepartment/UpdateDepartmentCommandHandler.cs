using HR.Application.Features.Departments.Services;
using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler (
        IUnitOfWork unitOfWork,
        IDepartmentCapacityChecker capacityChecker) : IRequestHandler<UpdateDepartmentCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await unitOfWork._DepartmentRepository.GetByIdAsync(request.Id);
            
            if (department == null)
            {
                throw new Exception($"Department With Id = {request.Id} is not Found");
            }

            if (request.ParentDepartmentId.HasValue)
            {
                var childResult = await capacityChecker.ValidateChildCapacityAsync(request.ParentDepartmentId.Value, request.HeadCount, request.Id);
                if (!childResult.Success)
                    return childResult;
            }

            var parentResult = await capacityChecker.ValidateParentCapacityAsync(request.Id, request.HeadCount);
            if(!parentResult.Success)
                return parentResult;

            department.Name = request.Name;
            department.CostCenter = request.CostCenter;
            department.HeadCount = request.HeadCount;
            department.ParentDepartmentId = request.ParentDepartmentId;
            department.ManagerId = request.ManagerId;

            unitOfWork._DepartmentRepository.UpdateAsync(department);
            await unitOfWork.SaveChangesAsync();

            return ApiResponse<int>.SuccessResponse(department.Id, $"Department with Id: {department.Id} successfully updated");
        }
    }
}
