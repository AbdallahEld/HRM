using FluentValidation.Results;
using HR.Application.Features.Departments.Services;
using HR.Application.Shared;
using HR.Domain.Data.Entities;
using HR.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandler  (
        IUnitOfWork unitOfWork,
        IDepartmentCapacityChecker capacityChecker): IRequestHandler<CreateDepartmentCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (request.ParentDepartmentId.HasValue)
            {
                var result = await capacityChecker.ValidateChildCapacityAsync(request.ParentDepartmentId.Value, request.HeadCount);
                if (!result.Success)
                    return result;
            }

            var department = new Department
            {
                Name = request.Name,
                CostCenter = request.CostCenter,
                HeadCount = request.HeadCount,
                ParentDepartmentId = request.ParentDepartmentId,
                ManagerId = request.ManagerId,
            };

            await unitOfWork._DepartmentRepository.AddAsync(department);
            await unitOfWork.SaveChangesAsync();
            return ApiResponse<int>.SuccessResponse(department.Id, "Department created successfully");
        }
    }
}
