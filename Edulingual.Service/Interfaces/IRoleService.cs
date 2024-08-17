using Edulingual.Common.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Role;

namespace Edulingual.Service.Interfaces;

public interface IRoleService : IAutoRegisterable
{
    Task<AppActionResult> GetAllPaing(int pageIndex, int pageSize);
    Task<AppActionResult> GetRoleById(string id);
    Task<AppActionResult> CreateRole(CreateRoleRequest createRoleRequest);
    Task<AppActionResult> UpdateRole(UpdateRoleRequest updateRoleRequest, string id);
    Task<AppActionResult> DeleteRole(string id);
}
