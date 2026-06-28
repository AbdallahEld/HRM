using HR.API.ExceptionHandling;
using HR.API.Extensions;
using HR.API.Helper;
using HR.Application.Extensions;
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

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });
            builder.Services.AddOpenApi();
            
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddMicrosoftIdentity();
            builder.Services.AddJWT(builder.Configuration);
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
            app.UseExceptionHandler();

            app.UseHttpsRedirection();

            app.UseCors("AllowAngular");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
