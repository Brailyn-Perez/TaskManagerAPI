using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.Application.Assemblies;
using TaskManager.Core.Application.Behaviours;
using TaskManager.Core.Application.Interfaces;
using TaskManager.Core.Application.Services;

namespace TaskManager.Core.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient<IQueueTaskItemService, QueueTaskItemService>();
        }
    }
}
