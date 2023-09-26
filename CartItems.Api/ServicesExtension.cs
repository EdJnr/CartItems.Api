using Cart.Api.Services;
using CartItems.Api.Database;
using CartItems.Api.Interfaces.IPersistence;
using CartItems.Api.Interfaces.IServices;
using CartItems.Api.Middlewares;
using CartItems.Api.Persistence;
using CartItems.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

namespace CartItems.Api
{
    public static class ServicesExtension
    {
        public static IServiceCollection ApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDatabaseContext>(opt =>
                opt.UseInMemoryDatabase("CartItems")
            );

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddHttpContextAccessor();

            services.AddTransient<GlobalErrorHandlerMiddleware>();

            //services.AddTransient<ApiResponseMiddleware>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAccountsService, AccountsService>();

            services.AddScoped<IItemsService, ItemsService>();

            services.AddScoped<ICartItemsService, CartItemsService>();

            services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Authorization using bearer scheme",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                opt.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:SecretKey").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
