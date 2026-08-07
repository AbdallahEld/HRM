using HR.Application.Features.Departments.DTOs;
using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Departments.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQueryHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<GetDepartmentByIdQuery, ApiResponse<DepartmentReadDTO>>
    {
        public async Task<ApiResponse<DepartmentReadDTO>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await unitOfWork._DepartmentRepository.GetByIdAsync(request.Id);
            if (department == null)
            {
                List<string> errors = new List<string> { $"Department with ID {request.Id} not found." };
                return ApiResponse<DepartmentReadDTO>.FailureResponse(errors);
            }

            var departmentReadDTO = new DepartmentReadDTO
            {
                Id = department.Id,
                Name = department.Name,
                CostCenter = department.CostCenter,
                HeadCount = department.HeadCount,
                ParentDepartmentId = department.ParentDepartmentId,
                ManagerId = department.ManagerId
            };

            return ApiResponse<DepartmentReadDTO>.SuccessResponse(departmentReadDTO, "Department retrieved successfully");
        }
    }
}
