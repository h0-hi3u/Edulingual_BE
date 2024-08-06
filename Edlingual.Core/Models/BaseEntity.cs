namespace Edulingual.Common.Models;
public abstract class BaseEntity : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;
}
