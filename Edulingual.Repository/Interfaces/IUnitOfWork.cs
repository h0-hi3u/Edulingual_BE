using Edulingual.Common.Interfaces;

namespace Edulingual.DAL.Interfaces;

public interface IUnitOfWork : IDisposable, IAutoRegisterable
{
    public Task<bool> SaveChangesAsync();
}
