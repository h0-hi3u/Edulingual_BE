using Edulingual.Common.Exceptions;
using Edulingual.Common.Settings;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Edulingual.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration, SwaggerSettings? swaggerSettings = default)
    {
        swaggerSettings ??= configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>() ?? throw new MissingSwaggerSettingsException();

        services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc(swaggerSettings.Version, new OpenApiInfo
                {
                    Version = swaggerSettings.Version,
                    Title = swaggerSettings.Title,
                    Description = swaggerSettings.Description,
                    TermsOfService = swaggerSettings.GetTermsOfService(),
                    Contact = swaggerSettings.GetContact(),
                    License = swaggerSettings.GetLicense()
                });
                options.SwaggerGeneratorOptions = new SwaggerGeneratorOptions()
                {
                    Servers = swaggerSettings.GetServers()
                };
                options.AddSecurityDefinition(swaggerSettings.Options.SecurityScheme.Name, swaggerSettings.GetSecurityScheme());
                options.AddSecurityRequirement(swaggerSettings.GetSecurityRequirement());
            }
        );

        return services;
    }
}
