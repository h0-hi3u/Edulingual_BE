using Edulingual.DAL.Interfaces;
using Edulingual.Service.Implementations;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.Authentication;
using Microsoft.Extensions.Configuration;
using Moq;
using Edulingual.Domain.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Edulingual.Common.Models;
using Edulingual.Service.Exceptions;
using Edulingual.UnitTests.Mock;

namespace Edulingual.UnitTests;

[TestFixture]
public class AuthServiceTests
{
    private Mock<IUserRepository> _mockUserRepo;
    private IConfiguration _mockConfig;
    private IAuthService _authService;

    [SetUp]
    public void Setup()
    {
        _mockConfig = new ConfigurationBuilder()
           .AddJsonFile("appsettings.Test.json")
           .AddEnvironmentVariables()
           .Build();

        _mockUserRepo = MockUserRepository.SetUpMockUserRepository();

        _authService = new AuthService(_mockConfig, _mockUserRepo.Object);
    }

    [Test]
    public async Task Login_WithTrueUsernameAndPassword_ShouldReturnToken()
    {
        var loginRequest = new LoginRequest
        {
            Email = "hieu@gmail.com",
            Password = "A123456@"
        };
        var result = await _authService.Login(loginRequest);

        Assert.IsTrue(!string.IsNullOrEmpty(result.Data!.ToString()));
    }

    [Test]
    public void Login_WithNotExistUser_ShoudThrowNotFoundException()
    {
        var loginRequest = new LoginRequest
        {
            Email = "test@gmail.com",
            Password = "A123456@"
        };

        Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _authService.Login(loginRequest);
        });
    }
    [Test]
    public void Login_WithExistUserWrongPassword_ShouldThrowInvalidException()
    {
        var loginRequest = new LoginRequest
        {
            Email = "hieu@gmail.com",
            Password = "123@"
        };

        Assert.ThrowsAsync<InvalidParameterException>(async () =>
        {
            await _authService.Login(loginRequest);
        });
    }
}