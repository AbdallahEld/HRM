using HR.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteDepartmentCommand>
    {
        public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await unitOfWork._DepartmentRepository.GetByIdAsync(request.Id);

            if (department == null)
            {
                throw new Exception($"Department with Id = {request.Id} not Found");
            }

            unitOfWork._DepartmentRepository.DeleteAsync(department);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
