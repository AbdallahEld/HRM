using HR.Application.Features.Departments.DTOs;
using HR.Application.Shared;
using HR.Domain.Repository;
using HR.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Features.Departments.Queries.GetAllDepartments
{
    public class GetAllDepartmentsQueryHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<GetAllDepartmentsQuery, ApiResponse<IEnumerable<DepartmentReadDTO>>>
    {
        public async Task<ApiResponse<IEnumerable<DepartmentReadDTO>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await unitOfWork._DepartmentRepository.GetAllAsync();

            var departmentReadDTOs = departments.Select(d => new DepartmentReadDTO
            {
                Id = d.Id,
                Name = d.Name,
                HeadCount = d.HeadCount,
                CostCenter = d.CostCenter,
                ParentDepartmentId = d.ParentDepartmentId,
                ManagerId = d.ManagerId
            });

            return ApiResponse<IEnumerable<DepartmentReadDTO>>.SuccessResponse(departmentReadDTOs, "Departments retrieved successfully");
        }
    }
}
