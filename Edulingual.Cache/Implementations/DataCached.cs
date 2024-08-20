using Edulingual.Caching.Interfaces;
using StackExchange.Redis;

namespace Edulingual.Caching.Implementations;

public class DataCached : IDataCached
{
    private readonly IDatabase _database;

    public DataCached(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public T Get<T>(string key)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAllWithPatternKey<T>(string pattern)
    {
        throw new NotImplementedException();
    }

    public bool IsSet(string key)
    {
        throw new NotImplementedException();
    }

    public void Remove(string key)
    {
        throw new NotImplementedException();
    }

    public void Set<T>(string key, T? value, int cacheTime)
    {
        throw new NotImplementedException();
    }
}
