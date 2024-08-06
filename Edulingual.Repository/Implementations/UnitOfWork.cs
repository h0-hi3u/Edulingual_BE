using Edulingual.DAL.Interfaces;
using EduLingual.Common.Interface;
using EduLingual.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Edulingual.DAL.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationDbContext _context;

    public UnitOfWork(IApplicationDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<bool> SaveChangesAsync()
    {
        try
        {
            return await _context.SaveChangesAsync() > 0;
        }
        catch
        {
            Dispose();
            return false;
        }
    }
}
