namespace Edulingual.Common.Settings;

public class JwtSettings
{
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string SigningKey { get; init; } = null!;
    public bool ValidateAudience { get; init; }
    public bool ValidateIssuer { get; init; }
    public bool ValidateIssuerSigningKey { get; init; }
    public bool ValidateLifetime { get; init; }
    public int AccessTokenLifetimeInMinutes { get; init; }
    public int RefreshTokenLifetimeInMinutes { get; init; }
}
