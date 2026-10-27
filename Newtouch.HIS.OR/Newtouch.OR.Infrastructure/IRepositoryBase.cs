using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// Generic repository contract. Replaces Newtouch.Infrastructure.EF
    /// IRepositoryBase&lt;T&gt; (EF6, Framework-only, no source).
    /// Implemented by Newtouch.OR.ManageSystem.Repository.RepositoryBase&lt;T&gt; on SqlSugar.
    /// </summary>
    public interface IRepositoryBase<T> where T : class, new()
    {
        ISugarQueryable<T> IQueryable();
        ISugarQueryable<T> IQueryable(Expression<Func<T, bool>> predicate);
        T FindEntity(string keyValue);
        T FindEntity(Expression<Func<T, bool>> predicate);
        IList<TRes> FindList<TRes>(string sql, SugarParameter[] pars) where TRes : class, new();
        IList<TRes> QueryWithPage<TRes>(string sql, Pagination pagination, SugarParameter[] pars) where TRes : class, new();
        int Insert(T entity);
        int Update(T entity);
        int Delete(Expression<Func<T, bool>> predicate);
    }
}
