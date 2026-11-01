using FluentValidation;
using HR.Application.Behaviors;
using HR.Application.Features.Attendance.Services;
using HR.Application.Features.Departments.Services;
using HR.Application.Features.Leaves.Services.ValidateLeaveBalance;
using HR.Application.Features.Leaves.Services.ValidateNoOverlap;
using HR.Application.Features.Leaves.Services.ValidatePastDate;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Application.Extensions
{
    public static class ApplicationServices
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ApplicationServices).Assembly;
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            services.AddValidatorsFromAssembly(applicationAssembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<IDepartmentCapacityChecker, DepartmentCapacityChecker>();
            services.AddScoped<INoOverlapChecker, NoOverlapChecker>();
            services.AddScoped<ILeaveBalanceChecker, LeaveBalanceChecker>();
            services.AddScoped<IPastDateChecker, PastDateChecker>();
            services.AddScoped<IClockService, ClockService>();
        }
    }
}
