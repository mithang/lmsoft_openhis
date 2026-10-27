using Newtouch.CIS.Compat;
using Newtouch.CIS.Infrastructure;
using SqlSugar;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.DomainServices
{
    /// <summary>
    /// Ported DmnServiceBase. Replaces FrameworkBase EFDBBase (EF6 +
    /// IDefaultDatabaseFactory, Framework-only, no source). Raw SQL goes to
    /// the CIS database via SqlSugar; fully-qualified cross-database names
    /// (e.g. NewtouchHIS_Sett..queue_schedule) keep working because the
    /// query runs on the same SQL Server instance.
    /// </summary>
    public abstract class DmnServiceBase
    {
        protected SqlSugarProvider Db => CisDb.GetConnection();

        protected List<T> FindList<T>(string sql)
        {
            return Db.Ado.SqlQuery<T>(sql);
        }

        protected List<T> FindList<T>(string sql, params SqlParameter[] parameters)
        {
            var sugar = SqlParam.ToSugar(parameters);
            return Db.Ado.SqlQuery<T>(sql, sugar);
        }

        protected List<T> FindList<T>(string sql, params SugarParameter[] parameters)
        {
            return Db.Ado.SqlQuery<T>(sql, parameters);
        }

        protected T FirstOrDefault<T>(string sql)
        {
            return Db.Ado.SqlQuery<T>(sql).FirstOrDefault();
        }

        protected T FirstOrDefault<T>(string sql, params SqlParameter[] parameters)
        {
            var sugar = SqlParam.ToSugar(parameters);
            return Db.Ado.SqlQuery<T>(sql, sugar).FirstOrDefault();
        }

        protected T FirstOrDefault<T>(string sql, params SugarParameter[] parameters)
        {
            return Db.Ado.SqlQuery<T>(sql, parameters).FirstOrDefault();
        }

        protected int ExecuteSqlCommand(string sql, params SqlParameter[] parameters)
        {
            var sugar = SqlParam.ToSugar(parameters);
            var rows = Db.Ado.ExecuteCommand(sql, sugar);
            SqlParam.CopyBack(parameters, sugar);
            return rows;
        }

        protected int ExecuteSqlCommand(string sql, params SugarParameter[] parameters)
        {
            return Db.Ado.ExecuteCommand(sql, parameters);
        }

        public List<TVO> QueryWithPage<TVO>(string sql, NewtouchHIS.Lib.Base.Model.Pagination pagination, System.Data.SqlClient.SqlParameter[] parameters)
        {
            return QueryWithPage<TVO>(sql, pagination, parameters, true);
        }
        public List<TVO> QueryWithPage<TVO>(string sql, NewtouchHIS.Lib.Base.Model.Pagination pagination, System.Data.SqlClient.SqlParameter[] parameters, bool countTotal)
        {
            var all = Db.Ado.SqlQuery<TVO>(sql, SqlParam.ToSugar(parameters)).ToList();
            int total = all.Count;
            int rows = pagination.rows ?? 20;
            int page = pagination.page ?? 1;
            if (rows <= 0) rows = 20;
            if (page <= 0) page = 1;
            pagination.records = total;
            return all.Skip((page - 1) * rows).Take(rows).ToList();
        }
        public List<TVO> QueryWithPage<TVO>(string sql, NewtouchHIS.Lib.Base.Model.Pagination pagination, System.Data.Common.DbParameter[] parameters)
        {
            var all = Db.Ado.SqlQuery<TVO>(sql, SqlParam.ToSugar(parameters)).ToList();
            int total = all.Count;
            int rows = pagination.rows ?? 20;
            int page = pagination.page ?? 1;
            if (rows <= 0) rows = 20;
            if (page <= 0) page = 1;
            pagination.records = total;
            return all.Skip((page - 1) * rows).Take(rows).ToList();
        }
        public List<TVO> FindList<TVO>(string sql, System.Data.Common.DbParameter[] parameters)
        {
            return Db.Ado.SqlQuery<TVO>(sql, parameters == null ? null : parameters.Select(p => SqlParam.ToSugar(p)).ToArray()).ToList();
        }
        public TVO FirstOrDefault<TVO>(string sql, System.Data.Common.DbParameter[] parameters)
        {
            return Db.Ado.SqlQuery<TVO>(sql, parameters == null ? null : parameters.Select(p => SqlParam.ToSugar(p)).ToArray()).FirstOrDefault();
        }
        public int ExecuteSqlCommand(string sql)
        {
            return Db.Ado.ExecuteCommand(sql);
        }
        public int ExecuteSqlCommand(string sql, System.Data.Common.DbParameter[] parameters)
        {
            return Db.Ado.ExecuteCommand(sql, parameters == null ? null : parameters.Select(p => SqlParam.ToSugar(p)).ToArray());
        }

    }
}
