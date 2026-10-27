using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// SqlSugar port of FrameworkBase IRepositoryBase&lt;T&gt; (EF6, binary-only).
    /// Method surface mirrors RepositoryBase&lt;T&gt; so ported repo interfaces
    /// (which extend this) and repo classes (which extend RepositoryBase)
    /// keep compiling unchanged.
    /// </summary>
    public interface IRepositoryBase<TEntity> where TEntity : class, new()
    {
        ISugarQueryable<TEntity> IQueryable();
        ISugarQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> FindList();
        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination);
        List<TEntity> GetList();
        List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetValidList();
        TEntity FindEntity(object keyValue);
        TEntity FindEntity(Expression<Func<TEntity, bool>> predicate);
        TEntity GetForm(object keyValue);
        int Insert(TEntity entity);
        int Insert(List<TEntity> entities);
        int Delete(Expression<Func<TEntity, bool>> predicate);
        int Delete(TEntity entity);
        int DeleteForm(object keyValue);
        int Update(TEntity entity, IEnumerable<string> dstnFieldNameList = null,
            IEnumerable<string> ignoreFieldNameList = null, bool attach = true);
        int SubmitForm(TEntity entity, object keyValue);
        void DetacheEntity(TEntity entity);
        List<TQ> QueryWithPage<TQ>(string sql, Pagination pagination, SqlParameter[] pars);
    }
}
