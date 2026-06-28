using FluentValidation.Results;
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
            if (request.ParentDepartmentId.HasValue)
            {
                var parentId = request.ParentDepartmentId.Value;

                var parentDepartment = await unitOfWork._DepartmentRepository.GetByIdAsync(parentId);
                if (parentDepartment == null)
                {
                    throw new FluentValidation.ValidationException(new List<ValidationFailure>
                    {
                        new ValidationFailure("ParentDepartmentId" , "Parent department does not exist.")
                    });
                }

                var currentChildren = await unitOfWork._DepartmentRepository
                                                      .FindAsync(d => d.ParentDepartmentId == parentId);

                var totalExistingHeadCount = currentChildren.Sum(d => d.HeadCount);

                if ((totalExistingHeadCount + request.HeadCount) > parentDepartment.HeadCount)
                {
                    var availableCapacity = parentDepartment.HeadCount - totalExistingHeadCount;
                    throw new FluentValidation.ValidationException(new List<ValidationFailure>
                    {
                        new ValidationFailure("HeadCount", $"Cannot exceed parent department headcount. Available capacity is {availableCapacity}.")
                    });
                }


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
            return department.Id;
        }
    }
}
