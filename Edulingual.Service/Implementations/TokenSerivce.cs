using Edulingual.Common.Exceptions;
using Edulingual.Common.Settings;
using Edulingual.Domain.Entities;
using Edulingual.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Edulingual.Service.Implementations;

public abstract class TokenSerivce : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenSerivce(IConfiguration configuration)
    {
        _jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>() ?? throw new MissingJwtSettingsException();
    }

    public string GenerateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var tokenDescriptor = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenLifetimeInMinutes),
            audience: _jwtSettings.Audience,
            issuer: _jwtSettings.Issuer,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            );
        var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        return token;
    }

    public string GenerateRefreshToken(string userId)
    {
        return "RefreshToken";
    }
}
