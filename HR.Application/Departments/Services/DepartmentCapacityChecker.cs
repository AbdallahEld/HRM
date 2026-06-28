using FluentValidation.Results;
using HR.Application.Departments.DTOs;
using HR.Domain.Data.Entities;
using HR.Domain.UnitOfWork;


namespace HR.Application.Departments.Services
{
    public class DepartmentCapacityChecker : IDepartmentCapacityChecker
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentCapacityChecker(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ValidateChildCapacityAsync(int parentId, int newHeadCount, int? currentDepartmentId = null)
        {
            var parentDepartment = await _unitOfWork._DepartmentRepository.GetByIdAsync(parentId);
            if (parentDepartment == null)
                throw new FluentValidation.ValidationException(new[] { new ValidationFailure("ParentDepartmentId", "Parent department does not exist.") });

            var children = await _unitOfWork._DepartmentRepository
                                            .FindAsync(d =>
                                                d.ParentDepartmentId == parentId &&
                                                (!currentDepartmentId.HasValue || d.Id != currentDepartmentId.Value)
                                            );

            var otherChildrenHeadCount = children.Sum(d => d.HeadCount);

            if ((otherChildrenHeadCount + newHeadCount) > parentDepartment.HeadCount)
            {
                var availableCapacity = parentDepartment.HeadCount - otherChildrenHeadCount;
                throw new FluentValidation.ValidationException(new[] { new ValidationFailure("HeadCount", $"Cannot exceed parent department headcount. Available capacity is {availableCapacity}.") });
            }
        }

        public async Task ValidateParentCapacityAsync(int departmentId, int newHeadCount)
        {
            var children = await _unitOfWork._DepartmentRepository.FindAsync(d => d.ParentDepartmentId == departmentId);
            var currentChildrenHeadCount = children.Sum(d => d.HeadCount);

            if (newHeadCount < currentChildrenHeadCount)
            {
                throw new FluentValidation.ValidationException(new[] { new ValidationFailure("HeadCount", $"Cannot reduce headcount to {newHeadCount}. Child departments already consume {currentChildrenHeadCount} spots.") });
            }
        }
    }
}
