using Edulingual.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Edulingual.DAL.Extensions;

public static class PagingExtensions
{
    public static async Task<Paginate<T>> ToPagingAsync<T>(this IQueryable<T> query, int pageSize, int pageIndex)
    {
        int totalRecord = await query.CountAsync();
        int totalPage = (int)Math.Ceiling(totalRecord / (double)pageSize);
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
