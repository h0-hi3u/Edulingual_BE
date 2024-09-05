namespace Edulingual.Common.Models;
public class Paginate<T> : IPaginate<T>
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int TotalRecord { get; set; }
    public int TotalPage { get; set; }
    public IEnumerable<T> Data { get; set; }
    public Paginate()
    {
        Data = Array.Empty<T>();
    }

    public Paginate(int pageSize, int pageIndex, int totalRecord, int totalPage, IEnumerable<T> data)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
        TotalRecord = totalRecord;
        TotalPage = totalPage;
        Data = data;
    }

    //public Paginate(IQueryable<T> query, int pageSize, int pageIndex)
    //{
    //    PageSize = pageSize;
    //    PageIndex = pageIndex;
    //    TotalRecord = query.Count();
    //    TotalPage = (int)Math.Ceiling(TotalRecord / (double)PageSize);
    //    if(pageIndex > TotalPage)
    //    {
    //        Data = [];
    //    } else
    //    {
    //        Data = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking();
    //    }
    //}
}
