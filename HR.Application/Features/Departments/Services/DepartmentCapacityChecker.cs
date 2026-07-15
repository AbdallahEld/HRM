using FluentValidation.Results;
using HR.Application.Shared;
using HR.Domain.UnitOfWork;


namespace HR.Application.Features.Departments.Services
{
    public class DepartmentCapacityChecker : IDepartmentCapacityChecker
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentCapacityChecker(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<int>> ValidateChildCapacityAsync(int parentId, int newHeadCount, int? currentDepartmentId = null)
        {
            var parentDepartment = await _unitOfWork._DepartmentRepository.GetByIdAsync(parentId);
            if (parentDepartment == null)
            {
                var errors = new List<string>
                {
                    $"Parent department with ID {parentId} not found."
                };
                return ApiResponse<int>.FailureResponse(errors ,$"Parent department retrieve failed");
            }
                

            var children = await _unitOfWork._DepartmentRepository
                                            .FindAsync(d =>
                                                d.ParentDepartmentId == parentId &&
                                                (!currentDepartmentId.HasValue || d.Id != currentDepartmentId.Value)
                                            );

            var otherChildrenHeadCount = children.Sum(d => d.HeadCount);

            if ((otherChildrenHeadCount + newHeadCount) > parentDepartment.HeadCount)
            {
                var availableCapacity = parentDepartment.HeadCount - otherChildrenHeadCount;
                return ApiResponse<int>.FailureResponse(new List<string> { 
                    $"Cannot set headcount to {newHeadCount}." +
                    $" Available capacity in parent department is {availableCapacity}." 
                }, $"Headcount validation failed");
            }

            return ApiResponse<int>.SuccessResponse(newHeadCount, $"Headcount validation succeeded");
        }

        public async Task<ApiResponse<int>> ValidateParentCapacityAsync(int departmentId, int newHeadCount)
        {
            var children = await _unitOfWork._DepartmentRepository.FindAsync(d => d.ParentDepartmentId == departmentId);
            var currentChildrenHeadCount = children.Sum(d => d.HeadCount);

            if (newHeadCount < currentChildrenHeadCount)
            {
                return ApiResponse<int>.FailureResponse(new List<string> {
                    $"Cannot reduce headcount to {newHeadCount}. Child departments already consume {currentChildrenHeadCount} spots."
                }, $"Headcount validation failed");
            }

            return ApiResponse<int>.SuccessResponse(newHeadCount, $"Headcount validation succeeded");
        }
    }
}
