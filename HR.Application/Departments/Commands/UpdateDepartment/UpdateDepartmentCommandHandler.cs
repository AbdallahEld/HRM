using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateDepartmentCommand, int>
    {
        public async Task<int> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await unitOfWork._DepartmentRepository.GetByIdAsync(request.Id);
            
            if (department == null)
            {
                throw new Exception($"Department With Id = {request.Id} is not Found");
            }

            department.Name = request.Name;
            department.CostCenter = request.CostCenter;
            department.HeadCount = request.HeadCount;
            department.ParentDepartmentId = request.ParentDepartmentId;
            department.ManagerId = request.ManagerId;

            unitOfWork._DepartmentRepository.UpdateAsync(department);
            await unitOfWork.SaveChangesAsync();

            return department.Id;
        }
    }
}
