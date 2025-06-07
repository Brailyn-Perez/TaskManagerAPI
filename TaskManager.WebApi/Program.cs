using TaskManager.Infaestructure.Persistence;
using TaskManager.Core.Application;
using TaskManager.WebApi.Middlewares;
using TaskManager.WebApi.Extensions;
using System.Text.Json.Serialization;

namespace TaskManager.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Task Manager API",
                    Version = "v1",
                    Description = "API for managing tasks"
                });

            });
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddApiVersioningExtension();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.Run();
        }
    }
}
