using HR.API.Extensions;
using HR.Domain.Data.Entities.Identity;
using HR.Infrastructure.Extensions;
using HR.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

namespace HR.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddMicrosoftIdentity();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
