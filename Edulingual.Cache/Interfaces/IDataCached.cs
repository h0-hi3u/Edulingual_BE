using Edulingual.Common.Interfaces;
using Edulingual.Common.Models;

namespace Edulingual.Caching.Interfaces;

public interface IDataCached : IAutoRegisterable
{
    Task<T?> GetDataCache<T>(string id);
    Task<Paginate<T>?> GetDataCache<T>(int pageSize, int pageIndex);
    Task SetToCache<T>(T value, string id, int? cacheTime);
    Task SetToCache<T>(IPaginate<T> value, int pageSize, int pageIndex, int? cacheTime);
    Task RemoveDataCache<T>(string id);
    Task RemoveDataCache<T>(int pageIndex, int pageSize);

}
