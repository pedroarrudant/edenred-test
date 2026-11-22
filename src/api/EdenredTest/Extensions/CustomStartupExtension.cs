using EdenredTest.Application.Shared.Configuration;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace EdenredTest.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class CustomStartupExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Edenred test",
                    Description = "Edenred test for admission",
                    License = new OpenApiLicense
                    {
                        Name = "Verson generated in " + $"{File.GetCreationTime(Assembly.GetExecutingAssembly().Location): dd/MM/yyyy}"
                    },
                    Contact = new OpenApiContact
                    {
                        Name = "Pedro Arruda",
                        Email = "pedro.arruda@outlook.com.br"
                    }
                })
                );

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<ExternalApiOptions>(configuration.GetSection("ExternalApis"));
            services.Configure<DatabaseOptions>(configuration.GetSection("SqlServer"));

            return services;
        }
    }
}

