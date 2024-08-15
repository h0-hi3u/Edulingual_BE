using Edulingual.Common.Interfaces;
using Edulingual.Domain.Entities;

namespace Edulingual.DAL.Interfaces;

public interface IRoleRepository : IRepository<Role>, IAutoRegisterable
{
}
