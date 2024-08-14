using System.ComponentModel.DataAnnotations;

namespace Edulingual.Service.Request.Role
{
    public class UpdateRoleRequest
    {
        [Required(ErrorMessage = "Id is required!")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "RoleName is required!")]
        public string RoleName { get; set; }
    }
}
