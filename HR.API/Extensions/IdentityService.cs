using HR.Domain.Data.Entities.Identity;
using HR.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;

namespace HR.API.Extensions
{
    public static class IdentityService
    {
        public static void AddMicrosoftIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<HRDbContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.User.RequireUniqueEmail = true;

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            });
        }
    }
}
