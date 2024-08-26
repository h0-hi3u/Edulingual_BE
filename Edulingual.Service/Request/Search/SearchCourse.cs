namespace Edulingual.Service.Request.Search;

public class SearchCourse
{
    public string? LanguageId { get; set; }
    public string? AreaId { get; set; }
    public string? CategoryId { get; set; }
    public double PriceFrom { get; set; } = 0;
    public double PriceTo { get; set; } = 0;
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
