using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.Application.Factories;
using TaskManager.Core.Domain.Repositories;
using TaskManager.Infaestructure.Persistence.Context;
using TaskManager.Infaestructure.Persistence.Repositories;

namespace TaskManager.Infaestructure.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TaskManagerDB"),
                 m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));


            #region Repositories
            services.AddTransient<ITaskItemRepository, TaskItemRepository>();
            services.AddTransient<ITaskFactory, Factories.TaskFactory>();
            #endregion
        }
    }
}
