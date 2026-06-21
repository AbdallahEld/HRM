using HR.Application.Departments.DTOs;
using HR.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Departments.Queries.GetAllDepartments
{
    public class GetAllDepartmentsQueryHandler (
        IDepartmentRepository departmentRepository) : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentReadDTO>>
    {
        public async Task<IEnumerable<DepartmentReadDTO>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await departmentRepository.GetAllAsync();

            var departmentReadDTOs = departments.Select(d => new DepartmentReadDTO
            {
                Id = d.Id,
                Name = d.Name,
                HeadCount = d.HeadCount,
                CostCenter = d.CostCenter,
                ParentDepartmentId = d.ParentDepartmentId,
                ManagerId = d.ManagerId
            });

            return departmentReadDTOs;
        }
    }
}
