namespace Edulingual.Service.Request.Course;

public class CreateCourseRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public double Fee { get; set; }
    public Guid CourseAreaId { get; set; }
    public Guid CourseLanguageId { get; set; }
    public Guid CourseCategoryId { get; set; }
}
