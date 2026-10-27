using Newtouch.Herp.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Newtouch.Herp.DomainServices
{
    /// <summary>
    /// Domain-service base replacing FrameworkBase DmnServiceBase (binary-only).
    /// Raw-SQL helpers mirror the old base's surface: QueryWithPage / FindList /
    /// FirstOrDefault / ExecuteSqlCommand, with DbParameter -&gt; SugarParameter.
    /// QueryWithPage semantics: pagination.records = total rows, pagination.total
    /// is computed by Pagination itself; sidx/sord override ordering when the
    /// grid posts them and the value is a plain identifier (injection guard).
    /// </summary>
    public class DmnServiceBase
    {
        protected virtual SqlSugarProvider Db => HerpDb.GetConnection();

        protected List<T> QueryWithPage<T>(string sql, Pagination pagination, params SugarParameter[] parameters) where T : class, new()
        {
            if (pagination.rows == null || pagination.rows <= 0) pagination.rows = 15;
            if (pagination.page == null || pagination.page <= 0) pagination.page = 1;
            int pageSize = pagination.rows.Value;
            int pageIndex = pagination.page.Value;

            string orderBy = string.Empty;
            if (!string.IsNullOrWhiteSpace(pagination.sidx)
                && Regex.IsMatch(pagination.sidx, @"^[A-Za-z0-9_\.]+$"))
            {
                string sord = string.Equals(pagination.sord, "desc", StringComparison.OrdinalIgnoreCase) ? "DESC" : "ASC";
                orderBy = " ORDER BY " + pagination.sidx + " " + sord;
            }

            string sqlNoOrder = StripTrailingOrderBy(sql);
            // Preserve the SQL's own ORDER BY for the page query when the grid
            // posts no valid sidx (e.g. treegrid ordering by px); only the COUNT
            // subquery must drop it.
            string ownOrderBy = sql.Length > sqlNoOrder.Length
                ? sql.Substring(sqlNoOrder.Length).Trim()
                : string.Empty;
            int total = Db.Ado.SqlQuery<int>(
                "SELECT COUNT(1) FROM (" + sqlNoOrder + ") AS _t", parameters).FirstOrDefault();
            pagination.records = total;

            string pageSql = sqlNoOrder + (orderBy.Length > 0 ? orderBy
                : (ownOrderBy.Length > 0 ? " " + ownOrderBy : " ORDER BY (SELECT NULL)"));
            pageSql += " OFFSET " + ((pageIndex - 1) * pageSize) + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            return Db.SqlQueryable<T>(pageSql).AddParameters(parameters).ToList();
        }

        protected List<T> FindList<T>(string sql, params SugarParameter[] parameters) where T : class, new()
        {
            return Db.SqlQueryable<T>(sql).AddParameters(parameters).ToList();
        }

        protected T FirstOrDefault<T>(string sql, params SugarParameter[] parameters) where T : class, new()
        {
            return Db.SqlQueryable<T>(sql).AddParameters(parameters).First();
        }

        protected T FirstOrDefault<T>(string sql) where T : class, new()
        {
            return Db.SqlQueryable<T>(sql).First();
        }

        protected int ExecuteSqlCommand(string sql, params SugarParameter[] parameters)
        {
            return Db.Ado.ExecuteCommand(sql, parameters);
        }

        protected List<T> FindListScalar<T>(string sql, params SugarParameter[] parameters)
        {
            return Db.Ado.SqlQuery<T>(sql, parameters);
        }

        protected T FirstOrDefaultScalar<T>(string sql, params SugarParameter[] parameters)
        {
            return Db.Ado.SqlQuery<T>(sql, parameters).FirstOrDefault();
        }

        protected T FirstOrDefaultScalar<T>(string sql)
        {
            return Db.Ado.SqlQuery<T>(sql).FirstOrDefault();
        }

        protected List<T> SqlQuery<T>(string sql, params SugarParameter[] parameters) where T : class, new()
        {
            return Db.SqlQueryable<T>(sql).AddParameters(parameters).ToList();
        }

        /// <summary>
        /// Removes a trailing top-level ORDER BY clause (required: SQL Server
        /// rejects ORDER BY in a COUNT subquery without TOP, and SqlSugar's own
        /// count has the same problem — the MRQC port hit this too).
        /// Only strips ORDER BY at parenthesis depth 0.
        /// </summary>
        protected static string StripTrailingOrderBy(string sql)
        {
            int depth = 0;
            int lastOrderBy = -1;
            for (int i = 0; i < sql.Length; i++)
            {
                char c = sql[i];
                if (c == '(') depth++;
                else if (c == ')') depth--;
                else if (depth == 0 && (c == 'O' || c == 'o') && i + 8 <= sql.Length
                    && sql.Substring(i, 8).Equals("ORDER BY", StringComparison.OrdinalIgnoreCase)
                    && (i == 0 || !char.IsLetterOrDigit(sql[i - 1])))
                {
                    lastOrderBy = i;
                }
            }
            if (lastOrderBy < 0) return sql;
            return sql.Substring(0, lastOrderBy).TrimEnd();
        }
    }
}
