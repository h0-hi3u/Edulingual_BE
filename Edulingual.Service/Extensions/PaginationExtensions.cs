using AutoMapper;
using Edulingual.Common.Models;
using Edulingual.Service.AutoMapper;

namespace Edulingual.Service.Extensions;

public static class PaginationExtensions
{
    public static IPaginate<T> Mapper<T, TType>(this IPaginate<TType> paginate, IMapper mapper)
    {
        return new Paginate<T>
        {
            PageIndex = paginate.PageIndex,
            PageSize = paginate.PageSize,
            TotalPage = paginate.TotalPage,
            TotalRecord = paginate.TotalRecord,
            Data = mapper.Map<T, TType>(paginate)
        };
    }
}
