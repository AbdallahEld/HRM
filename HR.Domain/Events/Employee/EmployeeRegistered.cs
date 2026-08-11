using HR.Domain.Abstractions;

namespace HR.Domain.Events.Employee
{
    public record EmployeeRegistered(HR.Domain.Data.Entities.Employee Employee) : IDomainEvent;

}
