using AutoMapper;
using Edulingual.Caching.Interfaces;
using Edulingual.Common.Models;
using Edulingual.Domain.Entities;
using Edulingual.Service.Response.User;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Edulingual.UnitTests.Mock;
public static class MockCommonObject
{
    public static IConfiguration SetUpMockConfiguration()
    {
        //var _mockConfiguration = new Mock<IConfiguration>();
        //_mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(It.IsAny<object>());
        return new ConfigurationBuilder()
          .AddJsonFile("appsettings.Test.json")
          .AddEnvironmentVariables()
          .Build();
    }

    public static Mock<IMapper> SetUpMockMapper()
    {
        var _mockMapper = new Mock<IMapper>();
        //_mockMapper.Setup(m => m.Map<>(It.IsAny<object>())).Returns(It.IsAny<object>());
        return _mockMapper;
    }
    public static Mock<IDataCached> SetUpDataCached()
    {
        var _mockDataCached = new Mock<IDataCached>();

        _mockDataCached.Setup(m => m.GetDataCache<It.IsAnyType>(It.IsAny<string>())).ReturnsAsync(value: null);
        _mockDataCached.Setup(m => m.GetDataCache<It.IsAnyType>(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(value: null);
        _mockDataCached.Setup(m => m.SetToCache<It.IsAnyType>(It.IsAny<It.IsAnyType>(), It.IsAny<string>(), null));
        _mockDataCached.Setup(m => m.SetToCache<It.IsAnyType>(It.IsAny<IPaginate<It.IsAnyType>>(), It.IsAny<int>(), It.IsAny<int>(), null));
        _mockDataCached.Setup(m => m.RemoveDataCache<It.IsAnyType>(It.IsAny<string>()));
        _mockDataCached.Setup(m => m.RemoveDataCache<It.IsAnyType>(It.IsAny<int>(), It.IsAny<int>()));

        return _mockDataCached;
    }
}
