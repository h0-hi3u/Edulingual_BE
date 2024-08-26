using Edulingual.Common.Helper;
using Microsoft.OpenApi.Models;
namespace Edulingual.Common.Settings;


public class SwaggerSettings
{
    public string Version { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string TermsOfServiceUrl { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public string ContactUrl { get; set; } = null!;
    public string LicenseName { get; set; } = null!;
    public string LicenseUrl { get; set; } = null!;
    public SwaggerOptions Options { get; set; } = new();

    public OpenApiContact GetContact()
    {
        return new OpenApiContact
        {
            Name = ContactName,
            Url = new Uri(ContactUrl),
            Email = ContactEmail
        };
    }

    public OpenApiLicense GetLicense()
    {
        return new OpenApiLicense
        {
            Name = LicenseName,
            Url = new Uri(LicenseUrl)
        };
    }

    public Uri GetTermsOfService()
    {
        return new Uri(TermsOfServiceUrl);
    }

    public List<OpenApiServer> GetServers()
    {
        return Options.Servers.Where(s => !string.IsNullOrEmpty(s.Url)).Select(s => new OpenApiServer
        {
            Url = s.Url,
            Description = s.Description,
            Variables = s.Variables.Any()
                ? s.Variables.ToDictionary(
                    v => v.Name,
                    v => new OpenApiServerVariable()
                    {
                        Description = v.Description,
                        Default = v.DefaultValue
                    })
                : new Dictionary<string, OpenApiServerVariable>()

        })
        .ToList();
    }

    public OpenApiSecurityScheme GetSecurityScheme()
    {
        var securityScheme = Options.SecurityScheme;
        return new OpenApiSecurityScheme
        {
            //Name = securityScheme.Name,
            Scheme = securityScheme.Schema,
            BearerFormat = securityScheme.BearerFormat,
            //Description = securityScheme.Description,
            Type = securityScheme.GetSecuritySchemeType(),
            //In = securityScheme.GetParameterLocation()
        };
    }

    public OpenApiSecurityRequirement GetSecurityRequirement()
    {
        var securityRequirement = Options.SecurityRequirement;
        return new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = securityRequirement.GetReferenceType(),
                        Id = securityRequirement.Id
                    }
                },
                Array.Empty<string>()
            }
        };
    }
}

public class SwaggerOptions
{
    public List<SwaggerServer> Servers { get; set; } = new();
    public SwaggerSecurityScheme SecurityScheme { get; set; } = null!;
    public SwaggerSecurityRequirement SecurityRequirement { get; set; } = null!;
}

public class SwaggerServer
{
    public string Url { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<SwaggerServerVariable> Variables { get; set; } = new();
}

public class SwaggerServerVariable
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string DefaultValue { get; set; } = null!;
}

public class SwaggerSecurityScheme
{
    public string Name { get; set; } = null!;
    //public string Description { get; set; }
    public string Schema { get; set; } = null!;
    public string BearerFormat { get; set; } = null!;
    public string Type { get; set; } = null!;
    //public string Location { get; set; }

    public SecuritySchemeType GetSecuritySchemeType()
    {
        return EnumHelper.GetEnumValueFromString<SecuritySchemeType>(Type);
    }
    //public ParameterLocation GetParameterLocation()
    //{
    //    return EnumHelper.GetEnumValueFromString<ParameterLocation>(Location);
    //}
}

public class SwaggerSecurityRequirement
{
    public string Type { get; set; } = null!;
    public string Id { get; set; } = null!;

    public ReferenceType GetReferenceType()
    {
        return EnumHelper.GetEnumValueFromString<ReferenceType>(Type);
    }
}
