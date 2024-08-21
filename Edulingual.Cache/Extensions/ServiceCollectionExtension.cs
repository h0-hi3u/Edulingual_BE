using Edulingual.Caching.Exceptions;
using Edulingual.Caching.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Edulingual.Caching.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterCaching(this IServiceCollection services, IConfiguration configuration)
    {
        var _redisSettings = configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>() ?? throw new MissingRedisSettingsException();
        services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(_redisSettings.Address));
        services.AddStackExchangeRedisCache(x => x.Configuration = _redisSettings.Address);
        return services;
    }
}
