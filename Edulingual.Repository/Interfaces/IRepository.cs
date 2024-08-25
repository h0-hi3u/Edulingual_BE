using Edulingual.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Edulingual.DAL.Interfaces;

public interface IRepository<T> where T : class
{
   DbSet<T> GetAll();

    #region Add 
    //void Add(T entity);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> enities);
    #endregion

    #region Update
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
   // Task UpdateAsync(T entity);
    //Task UpdateRangeAsync(IEnumerable<T> entities);
    #endregion

    #region Delete
    //void Delete(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> enities);
    Task DeleteAsync(Guid id);
    #endregion

    #region Get
    Task<T?> GetOneAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool isForUpdate = false);

    Task<IEnumerable<T>> GetListAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool isForUpdate = false);
    Task<IPaginate<T>> GetPagingAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int pageSize = 10,
        int pageIndex = 1
        );
    #endregion
}
