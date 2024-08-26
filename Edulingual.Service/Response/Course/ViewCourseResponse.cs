namespace Edulingual.Service.Response.Course;

public class ViewCourseResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public decimal Fee { get; set; }
    public Guid OwnerId { get; set; }
    public Guid CourseAreaId { get; set; }
    public Guid CourseLanguageId { get; set; }
    public Guid CourseCategoryId { get; set; }
}
