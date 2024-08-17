using Edulingual.Common.Interfaces;
using Edulingual.Domain.Enum;
using Edulingual.Service.Models;
using Edulingual.Service.Request.User;

namespace Edulingual.Service.Interfaces;

public interface IUserService : IAutoRegisterable
{
    Task<ServiceActionResult> GetUserPagingWithRole(int pageIndex, int pageSize, RoleEnum role);
    Task<ServiceActionResult> GetSelfProfile();
    Task<ServiceActionResult> GetUserById(string id);
    Task<ServiceActionResult> CreateUser(CreateUserRequest createUserRequest);
    Task<ServiceActionResult> UpdateUser(UpdateUserRequest createUserRequest);
    Task<ServiceActionResult> ChangeStatusUser(string id, UserStatusEnum status);
}
