using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Generic repository contract. Replaces FrameworkBase.MultiOrg.Domain.
    /// IRepository.IRepositoryBase&lt;T&gt; (EF6, binary, no source). The member
    /// set is the union of what the EMR repositories and domain services
    /// actually call.
    /// </summary>
    public interface IRepositoryBase<T> where T : class, new()
    {
        ISugarQueryable<T> IQueryable();
        ISugarQueryable<T> IQueryable(Expression<Func<T, bool>> predicate);
        T FindEntity(Expression<Func<T, bool>> predicate);
        T FindEntity(object keyValue);
        int Insert(T entity);
        // Bulk insert (the original FrameworkBase base offered it; the EMR
        // web's HomeController.SyncSysConfigParams calls Insert(list)).
        int Insert(IEnumerable<T> entities);
        int Update(T entity);
        int Delete(Expression<Func<T, bool>> predicate);
        List<T> FindList(Expression<Func<T, bool>> predicate, Pagination pagination);
        List<T2> FindList<T2>(string sql, SugarParameter[] parameters) where T2 : class, new();
        List<T2> FindList<T2>(string sql) where T2 : class, new();
        T2 FirstOrDefault<T2>(string sql, SugarParameter[] parameters) where T2 : class, new();
        T2 FirstOrDefault<T2>(string sql) where T2 : class, new();
        int ExecuteSqlCommand(string sql, SugarParameter[] parameters);
        int ExecuteSqlCommand(string sql);
        DataTable SqlQueryForDataTable(string sql, SugarParameter[] parameters);
        List<T2> QueryWithPage<T2>(string sql, Pagination pagination, SugarParameter[] parameters) where T2 : class, new();
        List<T2> QueryWithPage<T2>(string sql, Pagination pagination, SugarParameter[] parameters, bool ignored) where T2 : class, new();
    }
}
