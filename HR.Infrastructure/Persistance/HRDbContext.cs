using HR.Domain.Data.Configuration;
using HR.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Persistance
{
    public class HRDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        DbSet<Employee> Employees => Set<Employee>();
        DbSet<Department> Departments => Set<Department>();
        DbSet<Attendance> Attendances => Set<Attendance>();
        DbSet<Leave> Leaves => Set<Leave>();
        DbSet<LeaveType> LeaveTypes => Set<LeaveType>();
        DbSet<Training> Trainings => Set<Training>();
        DbSet<Payroll> Payrolls => Set<Payroll>();
        DbSet<EmployeeTrainings> EmployeeTrainings => Set<EmployeeTrainings>();
        DbSet<Shift> Shifts => Set<Shift>();
        DbSet<Location> Locations => Set<Location>();
        DbSet<EmployeeDeductions> EmployeeDeductions => Set<EmployeeDeductions>();
    }
}
