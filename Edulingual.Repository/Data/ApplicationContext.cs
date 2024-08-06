using EduLingual.Common.Helper;
using EduLingual.Common.Interface;
using EduLingual.Common.Models;
using EduLingual.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduLingual.DAL.Data;

public class ApplicationContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUser _currentUser;

    public ApplicationContext(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = DatabaseHelper.GetConnectionString();
        optionsBuilder.UseSqlServer(connectionString, options
            => options.CommandTimeout(60))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var type = ReflectionHelper.GetAssignableTo(typeof(IModelMapper));
        foreach (var t in type)
        {
            var instance = Activator.CreateInstance(t) as IModelMapper;
            instance?.Mapping(modelBuilder);
        }
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<T> CreateSet<T>() where T : class
    {
        return base.Set<T>();
    }

    

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach(var entry in ChangeTracker.Entries<BaseEntity<Guid>>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUser.CurrentUserId() ?? Guid.Empty;
                    entry.Entity.CreatedAt = DateTime.Now;

                    entry.Entity.UpdatedBy = _currentUser.CurrentUserId() ?? Guid.Empty;
                    entry.Entity.UpdatedAt = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedBy = _currentUser.CurrentUserId() ?? Guid.Empty;
                    entry.Entity.UpdatedAt = DateTime.Now;
                    break;
            }
        }
        return await base.SaveChangesAsync();
    }
}
