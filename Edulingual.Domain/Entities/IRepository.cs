using EduLingual.Common.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Edulingual.Domain.Entities;

public interface IRepository<T> where T : BaseEntity<Guid>
{
    DbSet<T> Entities();

    #region Add 
    //void Add(T entity);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> enities);
    #endregion

    #region Update
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(IEnumerable<T> entities);
    #endregion

    #region Delete
    //void Delete(T entity);
    Task DeleteAsync(T entity);
    Task DeleteRangeAsync(IEnumerable<T> enities);
    #endregion

    #region Get
    Task<T> GetOneAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool isForUpdate = false);

    Task<T> GetListAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool isForUpdate = false);

    //Task<T>
    #endregion
}
