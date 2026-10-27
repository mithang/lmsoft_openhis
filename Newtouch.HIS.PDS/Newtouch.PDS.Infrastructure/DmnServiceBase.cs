using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using System.Linq;
using Newtouch.Core.Common.Utils;
using Newtouch.Infrastructure;
using SqlSugar;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// DmnService base (ASP.NET Core port: EF6 -> SqlSugar via PdsDb; PRF field injection via ServiceLocator)
    /// </summary>
    public class DmnServiceBase : EFDBBase
    {
        private static readonly bool? PrfAutoResolve;

        static DmnServiceBase()
        {
            PrfAutoResolve = ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve_DmnService") ?? ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve");
        }

        public DmnServiceBase(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            if (PrfAutoResolve ?? true)
            {
                this.PRFAutoResolve();
            }
        }

        public DmnServiceBase(IDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory)
        {
            if (needIoc)
            {
                this.PRFAutoResolve();
            }
        }

        public DmnServiceBase()
        {
            if (PrfAutoResolve ?? true)
            {
                this.PRFAutoResolve();
            }
        }

        #region no log

        public T FirstOrDefaultNoLog<T>(string sql)
        {
            return _dataContext.Database.SqlQuery<T>(sql).FirstOrDefault();
        }

        public T FirstOrDefaultNoLog<T>(string sql, SugarParameter[] parameters)
        {
            if (parameters == null)
            {
                return FirstOrDefaultNoLog<T>(sql);
            }
            return _dataContext.Database.SqlQuery<T>(sql, parameters).FirstOrDefault();
        }

        public int ExecuteSqlCommandNoLog(string sql, params SugarParameter[] parameters)
        {
            int result;
            result = parameters == null ? _dataContext.Database.ExecuteSqlCommand(sql) : _dataContext.Database.ExecuteSqlCommand(sql, parameters);
            return result;
        }

        public List<T> FindListNoLog<T>(string sql)
        {
            var result = _dataContext.Database.SqlQuery<T>(sql).ToList<T>();
            return result;
        }

        public List<T> FindListNoLog<T>(string sql, SugarParameter[] parameters)
        {
            if (parameters == null)
            {
                return FindListNoLog<T>(sql);
            }
            return _dataContext.Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public List<T> FindList<T>(string sql)
        {
            return _dataContext.Database.SqlQuery<T>(sql);
        }

        public List<T> FindList<T>(string sql, SugarParameter[] parameters)
        {
            if (parameters == null) return FindList<T>(sql);
            return _dataContext.Database.SqlQuery<T>(sql, parameters);
        }

        public T FirstOrDefault<T>(string sql)
        {
            return _dataContext.Database.SqlQuery<T>(sql).FirstOrDefault();
        }

        public T FirstOrDefault<T>(string sql, SugarParameter[] parameters)
        {
            if (parameters == null) return FirstOrDefault<T>(sql);
            return _dataContext.Database.SqlQuery<T>(sql, parameters).FirstOrDefault();
        }

        public T FindEntity<T>(string sql)
        {
            return FirstOrDefault<T>(sql);
        }

        public T FindEntity<T>(string sql, SugarParameter[] parameters)
        {
            return FirstOrDefault<T>(sql, parameters);
        }

        public List<T> SqlQuery<T>(string sql, params SugarParameter[] parameters)
        {
            return _dataContext.Database.SqlQuery<T>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, params SugarParameter[] parameters)
        {
            return _dataContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        public List<T> QueryWithPage<T>(string sql, NewtouchHIS.Lib.Base.Model.Pagination pagination, params SugarParameter[] parameters) where T : class, new()
        {
            return PagedQuery.Run<T>(sql, pagination, parameters);
        }
        #endregion
    }
}
