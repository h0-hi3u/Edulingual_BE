namespace EduLingual.Common.Models;
public abstract class BaseEntity<T>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public T? CreatedBy { get; set; }
    public T? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
}
