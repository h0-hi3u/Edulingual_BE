namespace Edulingual.Caching.Common;

public static class CachingCommonDefaults
{
    public static int CacheTime => 60;

    public static string CacheKey => "edulingual.{0}.id.{1}";
}
