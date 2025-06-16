using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infraestructure.Identity.Context;

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



            return services;
        }
    }
}
