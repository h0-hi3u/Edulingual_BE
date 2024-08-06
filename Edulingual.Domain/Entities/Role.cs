using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edulingual.Domain.Entities
{
    [Table("role")]
    public class Role
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("role")]
        [Required]
        public string RoleName { get; set; } = string.Empty;

        [InverseProperty(nameof(Role))]
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
