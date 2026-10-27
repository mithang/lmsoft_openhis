using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Newtouch.EMR.Infrastructure
{
    /// <summary>
    /// Shared raw-SQL helpers for EMR domain services. Replaces the
    /// FrameworkBase DmnServiceBase members (FindList / FirstOrDefault /
    /// ExecuteSqlCommand / SqlQueryForDataTatable / QueryWithPage) with
    /// SqlSugar equivalents over EmrDb.
    ///
    /// ORDER BY pitfall (same as the MRQC port): SqlSugar's ToPageList wraps
    /// raw SQL in a derived table for the COUNT query, where ORDER BY is
    /// illegal. QueryWithPage therefore strips a top-level trailing ORDER BY
    /// from the SQL text and re-applies it through the queryable's OrderBy.
    /// </summary>
    public static class EmrQuery
    {
        public static List<T> FindList<T>(string sql, SugarParameter[] parameters) where T : class, new()
        {
            var q = EmrDb.GetConnection().SqlQueryable<T>(sql);
            if (parameters != null && parameters.Length > 0)
            {
                q = q.AddParameters(parameters);
            }
            return q.ToList();
        }

        public static T FirstOrDefault<T>(string sql, SugarParameter[] parameters) where T : class, new()
        {
            return FindList<T>(sql, parameters).FirstOrDefault();
        }

        public static int ExecuteSqlCommand(string sql, SugarParameter[] parameters)
        {
            return EmrDb.GetConnection().Ado.ExecuteCommand(sql, parameters);
        }

        public static DataTable SqlQueryForDataTable(string sql, SugarParameter[] parameters)
        {
            return EmrDb.GetConnection().Ado.GetDataTable(sql, parameters);
        }

        /// <summary>Splits a top-level trailing ORDER BY off raw SQL, if any.</summary>
        public static string StripTrailingOrderBy(string sql, out string orderBy)
        {
            orderBy = null;
            if (string.IsNullOrWhiteSpace(sql))
            {
                return sql;
            }
            string trimmed = sql.Trim().TrimEnd(';').Trim();
            var m = Regex.Match(trimmed, @"\border\s+by\b", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
            if (!m.Success)
            {
                return trimmed;
            }
            // Only strip when the ORDER BY is at parenthesis depth 0 (i.e. it
            // belongs to the outer query, not to a subquery/CTE tail).
            int depth = 0;
            for (int i = 0; i < m.Index; i++)
            {
                if (trimmed[i] == '(') depth++;
                else if (trimmed[i] == ')') depth--;
            }
            if (depth != 0)
            {
                return trimmed;
            }
            orderBy = trimmed.Substring(m.Index + m.Length).Trim();
            return trimmed.Substring(0, m.Index).Trim();
        }

        // 4-argument form kept for call sites that pass the original
        // FrameworkBase flag (its meaning is not documented in the EMR
        // source; both call sites pass false and their SQL carries no
        // ORDER BY, so the flag is ignored and pagination ordering applies).
        public static List<T> QueryWithPage<T>(string sql, Pagination pagination, SugarParameter[] parameters, bool ignored) where T : class, new()
        {
            return QueryWithPage<T>(sql, pagination, parameters);
        }

        public static List<T> QueryWithPage<T>(string sql, Pagination pagination, SugarParameter[] parameters) where T : class, new()
        {
            string stripped = StripTrailingOrderBy(sql, out string sqlOrder);
            var q = EmrDb.GetConnection().SqlQueryable<T>(stripped);
            if (parameters != null && parameters.Length > 0)
            {
                q = q.AddParameters(parameters);
            }
            string sidx = pagination != null ? pagination.sidx : null;
            string sord = string.Equals(pagination != null ? pagination.sord : null, "asc", StringComparison.OrdinalIgnoreCase) ? "asc" : "desc";
            if (!string.IsNullOrWhiteSpace(sidx) && Regex.IsMatch(sidx, @"^[\w\.,\s]+$"))
            {
                // Keep only the first sort column to avoid ORDER BY injection
                // through the grid's sidx parameter.
                string first = sidx.Trim().Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(first))
                {
                    q = q.OrderBy(first + " " + sord);
                }
            }
            else if (!string.IsNullOrWhiteSpace(sqlOrder))
            {
                q = q.OrderBy(sqlOrder);
            }
            int total = 0;
            var page = q.ToPageList(pagination != null && pagination.page.HasValue ? pagination.page.Value : 1,
                pagination != null && pagination.rows.HasValue ? pagination.rows.Value : 15, ref total);
            if (pagination != null)
            {
                pagination.records = total;
            }
            return page;
        }
    }

    /// <summary>Replacement base class for the old FrameworkBase DmnServiceBase.</summary>
    public class EmrServiceBase
    {
        protected List<T> FindList<T>(string sql, SugarParameter[] parameters) where T : class, new()
        {
            return EmrQuery.FindList<T>(sql, parameters);
        }

        protected List<T> FindList<T>(string sql) where T : class, new()
        {
            return EmrQuery.FindList<T>(sql, null);
        }

        protected T FirstOrDefault<T>(string sql, SugarParameter[] parameters) where T : class, new()
        {
            return EmrQuery.FirstOrDefault<T>(sql, parameters);
        }

        protected T FirstOrDefault<T>(string sql) where T : class, new()
        {
            return EmrQuery.FirstOrDefault<T>(sql, null);
        }

        protected int ExecuteSqlCommand(string sql, SugarParameter[] parameters)
        {
            return EmrQuery.ExecuteSqlCommand(sql, parameters);
        }

        protected int ExecuteSqlCommand(string sql)
        {
            return EmrQuery.ExecuteSqlCommand(sql, null);
        }

        protected DataTable SqlQueryForDataTable(string sql, SugarParameter[] parameters)
        {
            return EmrQuery.SqlQueryForDataTable(sql, parameters);
        }

        protected List<T> QueryWithPage<T>(string sql, Pagination pagination, SugarParameter[] parameters) where T : class, new()
        {
            return EmrQuery.QueryWithPage<T>(sql, pagination, parameters);
        }

        protected List<T> QueryWithPage<T>(string sql, Pagination pagination, SugarParameter[] parameters, bool ignored) where T : class, new()
        {
            return EmrQuery.QueryWithPage<T>(sql, pagination, parameters);
        }
    }

    /// <summary>
    /// Replacement for the old EFDbTransaction (EF6 DbContextTransaction).
    /// Wraps a SqlSugar transaction on the EmrDb connection: BeginTrans()
    /// opens it, Commit() commits, Dispose() rolls back unless committed.
    /// </summary>
    public class EmrTransaction : IDisposable
    {
        private readonly SqlSugarProvider _db;
        private bool _committed;

        public EmrTransaction()
        {
            _db = EmrDb.GetConnection();
        }

        public EmrTransaction BeginTrans()
        {
            _db.Ado.BeginTran();
            return this;
        }

        public ISugarQueryable<T> IQueryable<T>() where T : class, new()
        {
            return _db.Queryable<T>();
        }

        public int Insert<T>(T entity) where T : class, new()
        {
            return _db.Insertable(entity).ExecuteCommand();
        }

        public int Update<T>(T entity) where T : class, new()
        {
            return _db.Updateable(entity).ExecuteCommand();
        }

        public void Commit()
        {
            _db.Ado.CommitTran();
            _committed = true;
        }

        public void Rollback()
        {
            try { _db.Ado.RollbackTran(); } catch { }
        }

        public void Dispose()
        {
            if (!_committed)
            {
                Rollback();
            }
        }
    }
}
