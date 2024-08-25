using Edulingual.DAL.Extensions;
using Edulingual.DAL.Interfaces;
using Edulingual.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Edulingual.Common.Models;

namespace Edulingual.DAL.Implementations;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;
    private readonly ICurrentUser _currentUser;
    protected Repository(IApplicationDbContext context, ICurrentUser currentUser)
    {
        _context = context;
        _dbSet = _context.CreateSet<T>();
        _currentUser = currentUser;
    }

    public DbSet<T> GetAll() => _dbSet;
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> enities)
    {
        await _dbSet.AddRangeAsync(enities);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.SingleOrDefaultAsync(e => e.Id == id);
        if (entity == null) throw new ArgumentException("Not found to detele");
        _dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> enities)
    {
        _dbSet.RemoveRange(enities);
    }
    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool isForUpdate = false)
    {
        IQueryable<T> query = _dbSet;
        if (!isForUpdate) query = query.AsNoTracking();
        if (include != null) query = include(query);
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) query = orderBy(query);
        return await query.ToListAsync();
    }   

    public async Task<T?> GetOneAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool isForUpdate = false)
    {
        IQueryable<T> query = _dbSet;
        if(!isForUpdate) query= query.AsNoTracking();
        if (include != null) query = include(query);
        if (predicate != null) return await query.SingleOrDefaultAsync(predicate);
        return await query.SingleOrDefaultAsync();
    }

    public Task<IPaginate<T>> GetPagingAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int pageSize = 10, int pageIndex = 1)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();
        if (include != null) query = include(query);
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) query = orderBy(query);
        return query.ToPagingAsync(pageSize, pageIndex);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    //public async Task UpdateAsync(T entity)
    //{
    //    entity.UpdatedAt = DateTime.Now;
    //    entity.UpdatedBy = _currentUser.CurrentUserId() ?? entity.UpdatedBy;
    //    await _dbSet.Update(entity);
    //}

    public void UpdateRange(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
    }
}
