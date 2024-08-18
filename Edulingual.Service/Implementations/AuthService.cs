using Edulingual.DAL.Interfaces;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Authentication;
using Microsoft.Extensions.Configuration;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Response.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Edulingual.Service.Implementations;

public class AuthService : TokenSerivce, IAuthService
{
    private readonly IUserRepository _userRepo;
    public AuthService(IConfiguration configuration, IUserRepository userRepository) : base(configuration)
    {
        _userRepo = userRepository;
    }

    public async Task<ServiceActionResult> Login(LoginRequest loginRequest)
    {
        var user = await _userRepo.GetOneAsync(
            predicate: u => u.Email == loginRequest.Email && !u.IsDeleted,
            include: u => u.Include(u => u.Role)
            );
        if (user == null) throw new NotFoundException();

        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password)) throw new InvalidParameterException();

        var tokenReponse = new TokenResponse()
        {
            AccessToken = GenerateAccessToken(user),
            RefreshToken = GenerateRefreshToken(user.Id.ToString()),
        };
        return new ServiceActionResult(tokenReponse);
    }
}
