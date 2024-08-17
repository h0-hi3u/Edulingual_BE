using Edulingual.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Edulingual.DAL.Extensions;

public static class PaginationExtensions
{
    public static async Task<IPaginate<T>> ToPagingAsync<T>(this IQueryable<T> query, int pageSize = 10, int pageIndex = 1)
    {
        if (pageSize < 1 || pageIndex < 1) throw new InvalidDataException();
        int totalRecord = await query.CountAsync();
        int totalPage = totalRecord != 0 ? (int)Math.Ceiling(totalRecord / (double)pageSize) : 0;
        var data = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        return new Paginate<T>
        {
            PageSize = pageSize,
            PageIndex = pageIndex,
            TotalRecord = totalRecord,
            TotalPage = totalPage,
            Data = data
        };
    }
}
