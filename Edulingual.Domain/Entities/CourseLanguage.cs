using EduLingual.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Edulingual.Domain.Entities
{
    [Table("course_language")]
    public class CourseLanguage
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("status")]
        [EnumDataType(typeof(CourseLanguageStatus))]
        public CourseLanguageStatus Status { get; set; } = CourseLanguageStatus.Available;

        [InverseProperty(nameof(CourseLanguage))]
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

        [InverseProperty(nameof(CourseLanguage))]
        public virtual ICollection<CourseCategory> CourseCategories { get; set; } = new List<CourseCategory>();
    }
}
