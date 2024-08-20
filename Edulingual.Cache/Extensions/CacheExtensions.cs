using Edulingual.Caching.Common;

namespace Edulingual.Caching.Extensions;

public static class CacheExtensions
{
    public static string GetKey<T>(T entity, string id)
    {
        return string.Format(CachingCommonDefaults.CacheKey, nameof(T), id);
    }
}
