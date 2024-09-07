using AutoMapper;
using Edulingual.Domain.Entities;
using Edulingual.Service.Response.User;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Edulingual.UnitTests.Mock;
public static class MockCommonObject
{
    public static IConfiguration SetUpMockConfiguration()
    {
        return new ConfigurationBuilder()
          .AddJsonFile("appsettings.Test.json")
          .AddEnvironmentVariables()
          .Build();
    }

    public static Mock<IMapper> SetUpMockMapper()
    {
        var _mockMapper = new Mock<IMapper>();

        return _mockMapper;
    }

}
