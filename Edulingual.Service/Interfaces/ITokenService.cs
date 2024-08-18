using Edulingual.Common.Interfaces;
using Edulingual.Domain.Entities;

namespace Edulingual.Service.Interfaces;

public interface ITokenService : IAutoRegisterable
{
    string GenerateRefreshToken(string userId);
    string GenerateAccessToken(User user);
}
