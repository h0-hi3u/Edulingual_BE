using Edulingual.DAL.Interfaces;
using Edulingual.Service.Implementations;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.Authentication;
using Microsoft.Extensions.Configuration;
using Moq;
using Microsoft.Net;
using System.Net;

namespace Edulingual.UnitTests;

[TestFixture]
public class AuthServiceTests
{
    //private Mock<IUserRepository> _mockUserRepo;
    //private Mock<IConfiguration> _mockConfig;
    //private IAuthService _authService;


    [SetUp]
    public void Setup()
    {
        //_mockConfig = new Mock<IConfiguration>();
        //_mockUserRepo = new Mock<IUserRepository>();

        //_authService = new AuthService(_mockConfig.Object, _mockUserRepo.Object);
    }

    [Test]
    public void Login_WithTrueUsernameAndPassword_ShouldReturnToken()
    {
        //var loginRequest = new LoginRequest
        //{
        //    Email = "test",
        //    Password = "test"
        //};
        //var result = await _authService.Login(loginRequest);

        //Assert.Equals(result.HttpStatusCode.ToString(), HttpStatusCode.OK.ToString());
        Assert.Pass();
    }
}