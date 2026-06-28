using FluentValidation;
using HR.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }
}
