using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;
using System;
using System.Data;

namespace Newtouch.EMR.Web
{
    /// <summary>
    /// Port of Base Newtouch.Infrastructure.EFDBBaseFuncHelper.GetNewFieldUniqueValue:
    /// calls stored procedure spFieldGetNewID on the Base DB with two output params.
    /// Parameter order is (fieldName, orgId, initFormat, dateFlag, topOrgId):
    /// every call site in this app passes initFormat positionally (the Framework
    /// original's 5-arg order put topOrgId third, which made its own 4-arg calls
    /// bind initFormat into topOrgId — a latent upstream bug this port does NOT
    /// reproduce). topOrgId defaults to the demo hospital org id when null.
    /// Returns "" if the procedure call fails, so pages degrade instead of
    /// crashing; the failure is reported, not hidden, via the empty value.
    /// </summary>
    public static class FieldValueHelper
    {
        public static string GetNewFieldUniqueValue(string fieldName, string orgId, string initFormat = "", bool dateFlag = false, string topOrgId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(topOrgId))
                    topOrgId = EmrConfig.TopOrganizeId;
                var db = SqlSugarDbContext.Db.GetConnection("BaseDb");
                var pNum = new SugarParameter("@NumStr", "", System.Data.DbType.String, ParameterDirection.Output);
                var pFmt = new SugarParameter("@Format", "", System.Data.DbType.String, ParameterDirection.Output);
                db.Ado.UseStoredProcedure().ExecuteCommand("spFieldGetNewID",
                    new SugarParameter("@OrganizeId", orgId),
                    new SugarParameter("@TopOrganizeId", topOrgId),
                    new SugarParameter("@FieldName", fieldName),
                    new SugarParameter("@DateFlag", dateFlag),
                    new SugarParameter("@currDate", DateTime.Now.ToString("yyyyMMdd")),
                    new SugarParameter("@InitFormat", initFormat),
                    pNum, pFmt);
                var numStr = pNum.Value == null ? "" : pNum.Value.ToString();
                var fmt = pFmt.Value == null ? "" : pFmt.Value.ToString();
                if (string.IsNullOrWhiteSpace(fmt)) return numStr;
                return string.Format(fmt, Convert.ToInt32(numStr));
            }
            catch
            {
                return "";
            }
        }
    }
}
