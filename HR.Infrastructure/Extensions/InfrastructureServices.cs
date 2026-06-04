using HR.Domain.UnitOfWork;
using HR.Infrastructure.Persistance;
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

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
