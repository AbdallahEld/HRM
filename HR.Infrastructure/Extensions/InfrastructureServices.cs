using HR.Application.Common.Interfaces;
using HR.Domain.Data.Entities.Identity;
using HR.Domain.Repository;
using HR.Domain.UnitOfWork;
using HR.Infrastructure.Identity.JWT;
using HR.Infrastructure.Persistance;
using HR.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Infrastructure.Extensions
{
    public static class InfrastructureServices
    {
        public static void AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HRDbContext> (options =>
            {
                options.UseSqlServer (connectionString);
            });
            

            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeDeductionsRepository, EmployeeDeductionsRepository>();
            services.AddScoped<IEmployeeTrainingsRepository, EmployeeTrainingsRepository>();
            services.AddScoped<ILeaveRepository, LeaveRepository>();
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IPayrollRepository, PayrollRepository>();
            services.AddScoped<IShiftRepository, ShiftRepository>();
            services.AddScoped<ITrainingRepository, TrainingRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
