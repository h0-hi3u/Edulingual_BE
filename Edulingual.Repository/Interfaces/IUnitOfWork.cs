using Edulingual.Common.Interfaces;

namespace Edulingual.DAL.Interfaces;

public interface IUnitOfWork : IAutoRegisterable
{
    public Task<bool> SaveChangesAsync();
    public void Dispose();
}
