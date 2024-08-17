using Edulingual.Common.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Role;

namespace Edulingual.Service.Interfaces;

public interface IRoleService : IAutoRegisterable
{
    Task<ServiceActionResult> GetAllPaing(int pageIndex, int pageSize);
    Task<ServiceActionResult> GetRoleById(string id);
    Task<ServiceActionResult> CreateRole(CreateRoleRequest createRoleRequest);
    Task<ServiceActionResult> UpdateRole(UpdateRoleRequest updateRoleRequest, string id);
    Task<ServiceActionResult> DeleteRole(string id);
}
