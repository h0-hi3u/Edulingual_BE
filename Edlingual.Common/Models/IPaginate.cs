namespace Edulingual.Common.Models
{
    public interface IPaginate<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecord { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
