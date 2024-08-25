namespace Edulingual.Service.Request.Search;

public class SearchCourse
{
    public string? LanguageId { get; set; }
    public string? AreaId { get; set; }
    public string? CategoryId { get; set; }
    public decimal PriceFrom { get; set; } = -1;
    public decimal PriceTo { get; set; } = -1;
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
