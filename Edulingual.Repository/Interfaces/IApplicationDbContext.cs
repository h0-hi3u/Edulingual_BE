using Microsoft.EntityFrameworkCore;

namespace Edulingual.DAL.Interfaces;

public interface IApplicationDbContext : IDisposable
{
    DbSet<T> CreateSet<T>() where T : class;
    //void SetModified<T>(T entity) where T : class;
    //void SetDeleted<T>(T enity) where T : class;
    //void Refeshed<T>(T entity) where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
