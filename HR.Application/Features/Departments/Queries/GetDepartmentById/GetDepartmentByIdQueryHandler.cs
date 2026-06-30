using HR.Application.Features.Departments.DTOs;
using HR.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Features.Departments.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQueryHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<GetDepartmentByIdQuery, DepartmentReadDTO>
    {
        public async Task<DepartmentReadDTO> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await unitOfWork._DepartmentRepository.GetByIdAsync(request.Id);
            if (department == null)
            {
                throw new Exception($"Department with Id {request.Id} not found.");
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

            return departmentReadDTO;
        }
    }
}
