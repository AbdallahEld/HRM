using HR.Domain.Data.Entities;
using HR.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandler  (
        IUnitOfWork unitOfWork): IRequestHandler<CreateDepartmentCommand, int>
    {
        public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
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
            return department.Id;
        }
    }
}
