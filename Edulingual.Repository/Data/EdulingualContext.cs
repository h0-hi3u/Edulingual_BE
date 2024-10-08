﻿using Edulingual.Common.Helper;
using Edulingual.Common.Interfaces;
using Edulingual.Common.Models;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Edulingual.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Edulingual.DAL.Data;

public partial class EdulingualContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUser _currentUser;

    public EdulingualContext(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = DatabaseHelper.GetConnectionString();
        optionsBuilder.UseNpgsql(connectionString, options
            => options.CommandTimeout(60));
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
        foreach (var entry in ChangeTracker.Entries<Auditable>())
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
