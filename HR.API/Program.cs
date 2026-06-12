using HR.API.Extensions;
using HR.API.Helper;
using HR.Domain.Data.Entities.Identity;
using HR.Infrastructure.Extensions;
using HR.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

namespace HR.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddMicrosoftIdentity();
            builder.Services.AddCORS();


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var roleManager = services.GetRequiredService<RoleManager<Role>>();
                var userManager = services.GetRequiredService<UserManager<User>>();

                await AdminSeeder.SeedRolesAsync(roleManager);
                await AdminSeeder.SeedAdminAsync(roleManager, userManager);
            }

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseCors("AllowAngular");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
