using Edulingual.Caching.Helper;
using Edulingual.Caching.Interfaces;
using Edulingual.Common.Models;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Edulingual.Caching.Implementations;

public class DataCached : BaseDataCached, IDataCached
{
    public DataCached(IDistributedCache distributedCache) : base(distributedCache)
    {
    }

    public async Task<T?> GetDataCache<T>(string id)
    {
        var cacheKey = CachingKeyHelper.GetKeyEntityId(nameof(Role), id);
        return await Get<T>(cacheKey);
    }

    public async Task<Paginate<T>?> GetDataCache<T>(int pageSize, int pageIndex)
    {
        var key = CachingKeyHelper.GetKeyPaging(nameof(T), pageIndex: pageIndex, pageSize: pageSize);
        return await GetPaging<T>(key);
    }

    public async Task RemoveDataCache<T>(string id)
    {
        var cacheKey = CachingKeyHelper.GetKeyEntityId(nameof(Role), id);
        await Remove(cacheKey);
    }

    public async Task RemoveDataCache<T>(int pageIndex, int pageSize)
    {
        var key = CachingKeyHelper.GetKeyPaging(nameof(T), pageIndex: pageIndex, pageSize: pageSize);
        await Remove(key);
    }

    public async Task SetToCache<T>(T value, string id, int? cacheTime)
    {
        var key = CachingKeyHelper.GetKeyEntityId(nameof(T), id);
        await Set<T>(key: key, value: value, cacheTime: cacheTime);
    }

    public async Task SetToCache<T>(IPaginate<T> value, int pageSize, int pageIndex, int? cacheTime)
    {
        var key = CachingKeyHelper.GetKeyPaging(nameof(T), pageIndex: pageIndex, pageSize: pageSize);
        await Set<T>(key: key, value: value, cacheTime: cacheTime);
    }
}
