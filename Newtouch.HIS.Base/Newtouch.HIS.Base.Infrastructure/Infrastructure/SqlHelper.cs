using System;
using Newtouch.HIS.Base.Infrastructure;
using SqlSugar;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Raw-SQL helpers replacing EF DbContext.Database calls.
    /// Sql.ExecuteCommand keeps the original (sql, SqlParameter[]) call shape.
    /// </summary>
    public static class Sql
    {
        public static List<SugarParameter> ToSugar(SqlParameter[] pars)
        {
            var list = new List<SugarParameter>();
            if (pars == null)
            {
                return list;
            }
            foreach (var p in pars)
            {
                list.Add(new SugarParameter(p.ParameterName, p.Value ?? System.DBNull.Value));
            }
            return list;
        }

        public static int ExecuteCommand(string sql, SqlParameter[] pars = null)
        {
            return BaseDb.GetConnection().Ado.ExecuteCommand(sql, ToSugar(pars));
        }
    }

    /// <summary>
    /// Field-unique-value generator ported from EFDBBaseFuncHelper (EF6).
    /// Same stored procedure (spFieldGetNewID) and same return semantics.
    /// </summary>
    public class EFDBBaseFuncHelper
    {
        public static EFDBBaseFuncHelper Instance => new EFDBBaseFuncHelper();

        public string GetNewFieldUniqueValue(string fieldName, string orgId, string topOrgId = null, string initFormat = "", bool dateFlag = false)
        {
            if (string.IsNullOrWhiteSpace(topOrgId))
            {
                topOrgId = Constants.TopOrganizeId;
            }
            var outNum = new SugarParameter("@NumStr", "", System.Data.DbType.String, ParameterDirection.Output);
            var outFmt = new SugarParameter("@Format", "", System.Data.DbType.String, ParameterDirection.Output);
            var pars = new List<SugarParameter>
            {
                new SugarParameter("@OrganizeId", orgId),
                new SugarParameter("@TopOrganizeId", topOrgId),
                new SugarParameter("@FieldName", fieldName),
                new SugarParameter("@DateFlag", dateFlag),
                new SugarParameter("@currDate", System.DateTime.Now.ToString("yyyyMMdd")),
                new SugarParameter("@InitFormat", initFormat),
                outNum,
                outFmt,
            };
            BaseDb.GetConnection().Ado.UseStoredProcedure().ExecuteCommand("spFieldGetNewID", pars.ToArray());
            var numStr = outNum.Value?.ToString();
            var fmt = outFmt.Value?.ToString();
            if (string.IsNullOrWhiteSpace(fmt))
            {
                return numStr;
            }
            return string.Format(fmt, System.Convert.ToInt32(numStr));
        }
    }

    /// <summary>Parameter conversion shared by RepositoryBase and DmnServiceBase
    /// (kept non-generic so unconstrained SQL methods can use it).</summary>
    public static class SugarParamHelper
    {
        public static List<SugarParameter> ToSugarParams(SqlParameter[] pars)
        {
            var list = new List<SugarParameter>();
            if (pars == null)
            {
                return list;
            }
            foreach (var p in pars)
            {
                list.Add(new SugarParameter(p.ParameterName, p.Value ?? DBNull.Value));
            }
            return list;
        }

        public static List<SugarParameter> ToSugarParams(System.Data.Common.DbParameter[] pars)
        {
            var list = new List<SugarParameter>();
            if (pars == null)
            {
                return list;
            }
            foreach (var p in pars)
            {
                list.Add(new SugarParameter(p.ParameterName, p.Value ?? DBNull.Value));
            }
            return list;
        }
    }
}
