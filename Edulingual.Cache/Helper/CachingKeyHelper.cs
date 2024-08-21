namespace Edulingual.Caching.Helper;

public static class CachingKeyHelper
{
    private const string CacheKeyEntityId = "edulingual.{0}.id.{1}";
    private const string CacheKeyPagingEntity = "edulingual.{0}.pi.{1}.pz.{2}";
    public static string GetKeyEntityId(string entityName, string id)
    {
        return string.Format(CacheKeyEntityId, entityName, id);
    }
    public static string GetKeyPaging(string entityName, int pageIndex, int pageSize)
    {
        return string.Format(CacheKeyPagingEntity, entityName, pageIndex, pageSize);
    }
}
