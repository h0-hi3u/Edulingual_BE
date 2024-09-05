using AutoMapper;
using Edulingual.Common.Models;

namespace Edulingual.Service.Extensions;

public static class PaginationExtensions
{
    public static IPaginate<T> Mapper<T, F>(this IPaginate<F> paginate, IMapper mapper)
    {
        return new Paginate<T>
        {
            PageIndex = paginate.PageIndex,
            PageSize = paginate.PageSize,
            TotalPage = paginate.TotalPage,
            TotalRecord = paginate.TotalRecord,
            Data = mapper.Map<IEnumerable<T>>(paginate.Data)
        };
    }
}
