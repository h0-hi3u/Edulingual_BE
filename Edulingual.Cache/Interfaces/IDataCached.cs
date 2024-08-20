using Edulingual.Common.Interfaces;

namespace Edulingual.Caching.Interfaces;

public interface IDataCached : IAutoRegisterable
{
    T Get<T>(string key);
    IEnumerable<T> GetAllWithPatternKey<T>(string pattern);
    void Set<T>(string key, T? value, int cacheTime);
    bool IsSet(string key);
    void Remove(String key);
    void Clear();
}
