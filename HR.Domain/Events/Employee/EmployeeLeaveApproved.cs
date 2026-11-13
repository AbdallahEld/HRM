using HR.Domain.Abstractions;
using HR.Domain.Data.Entities;

namespace HR.Domain.Events.Employee
{
    public record EmployeeLeaveApproved (Leave EmployeeLeave) : IDomainEvent
    {
    }
}
