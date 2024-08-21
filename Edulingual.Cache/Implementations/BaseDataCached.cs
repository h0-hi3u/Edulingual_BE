using Edulingual.Caching.Constants;
using Edulingual.Common.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Edulingual.Caching.Implementations;

public abstract class BaseDataCached
{
    private readonly IDistributedCache _distributedCache;

    protected BaseDataCached(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    protected async Task Remove(string key)
    {
        await _distributedCache.RemoveAsync(key);
    }

    protected async Task Set<T>(string key, T value, int? cacheTime)
    {
        await _distributedCache.SetStringAsync(
            key: key,
            value: JsonConvert.SerializeObject(value),
            options: GetDistributedCacheEntryOptions(cacheTime ?? CachingConstatns.CacheTime));
    }
    protected async Task Set<T>(string key, IPaginate<T> value, int? cacheTime)
    {
        await _distributedCache.SetStringAsync(
            key: key,
            value: JsonConvert.SerializeObject(value),
            options: GetDistributedCacheEntryOptions(cacheTime ?? CachingConstatns.CacheTime));
    }
    protected async Task<T?> Get<T>(string key)
    {
        var value = await _distributedCache.GetStringAsync(key);
        if (value is not null)
            return JsonConvert.DeserializeObject<T>(value);

        return default;
    }
    protected async Task<Paginate<T>?> GetPaging<T>(string key)
    {
        var value = await _distributedCache.GetStringAsync(key);
        if (value is not null)
            return JsonConvert.DeserializeObject<Paginate<T>>(value);

        return default;
    }
    protected virtual DistributedCacheEntryOptions GetDistributedCacheEntryOptions(int time = CachingConstatns.CacheTime)
    {
        var options = new DistributedCacheEntryOptions();
        options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(time);
        return options;
    }
}
