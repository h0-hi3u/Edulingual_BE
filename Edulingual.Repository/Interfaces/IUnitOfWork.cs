﻿namespace Edulingual.DAL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public Task<bool> SaveChangesAsync();
}
