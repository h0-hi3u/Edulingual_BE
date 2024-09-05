using Edulingual.Common.Constants;

namespace Edulingual.Common.Settings;

public class CorsSettings
{
    public string AllowedOrigins { get; init; } = null!;
    public string AllowedMethods { get; init; } = null!;
    public string AllowedHeaders { get; init; } = null!;
    public bool AllowCredentials { get; init; }

    public string[] GetAllowedOriginsArray()
    {
        return AllowedOrigins.Split(CorsConstants.HOSTS_SEPARATOR);
    }

    public string[] GetAllowedMethodsArray()
    {
        return AllowedMethods.Split(CorsConstants.METHODS_SEPARATOR);
    }

    public string[] GetAllowedHeadersArray()
    {
        return AllowedHeaders.Split(CorsConstants.HEADERS_SEPARATOR);
    }

    public bool AllowAnyOrigin()
    {
        return AllowedOrigins.Trim() == CorsConstants.ANY_ORIGIN;
    }

    public bool AllowAnyMethod()
    {
        return AllowedHeaders.Trim() == CorsConstants.ANY_METHOD;
    }

    public bool AllowAnyHeader()
    {
        return AllowedMethods.Trim() == CorsConstants.ANY_HEADER;
    }
}
