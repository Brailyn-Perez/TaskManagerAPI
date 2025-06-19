using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManager.Infraestructure.Identity.Config;
using TaskManager.Infraestructure.Identity.Interfaces.Services;
using TaskManager.Infraestructure.Identity.Services;

namespace TaskManager.Infraestructure.Identity
{
    public static class JwtCollectionExtension
    {
        public static IServiceCollection AddJwtInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<jwtConfig>(configuration.GetSection("JwtConfig"));


            Byte[]? key = Encoding.ASCII.GetBytes(configuration["JwtConfig:Secret"]);
            TokenValidationParameters? tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParams;
            });

            services.AddSingleton(tokenValidationParams);
            services.AddTransient<IjwtService, JwtService>();
            return services;
        }
    }
}
