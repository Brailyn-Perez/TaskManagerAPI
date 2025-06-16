using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infraestructure.Identity.Context;
using TaskManager.Infraestructure.Identity.Interfaces;
using TaskManager.Infraestructure.Identity.Interfaces.Services;
using TaskManager.Infraestructure.Identity.Repositories;
using TaskManager.Infraestructure.Identity.Services;

namespace TaskManager.Infraestructure.Identity
{
    public static class ServiceColletionExtension
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
            });

            var connectionString = configuration.GetConnectionString("TaskManagerDB");

            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });


            services.AddTransient<IUserManagerRepository, UserManagerRepository>();
            services.AddTransient<IRoleManagerRepository, RoleManagerRepository>();
            services.AddTransient<ISignInManagerRepository, SignInManagerRepository>();

            return services;
        }
    }
}
