using System.Data.SqlClient;
using Newtouch.HIS.Domain.Entity;
using System.Linq;
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;

namespace Newtouch.Repository
{
    /// <summary>
    /// Ported from Base SysDiagnosisRepo (binary-only SelectDataByICD10):
    /// maps an ICD10 code to the local diagnosis row (xt_zd) for the org.
    /// </summary>
    public class SysDiagnosisRepo : Newtouch.HIS.Domain.IRepository.ISysDiagnosisRepo
    {
        public SysDiagnosisRepo() { }

        public Newtouch.HIS.Domain.Entity.SysDiagnosisEntity SelectDataByICD10(string organizeId, string icd10)
        {
            var sql = "SELECT TOP 1 * FROM xt_zd WHERE icd10 = @icd10 AND zt = '1' AND (OrganizeId = @orgId OR OrganizeId = '*')";
            return BaseDb.GetConnection().Ado.SqlQuerySingle<Newtouch.HIS.Domain.Entity.SysDiagnosisEntity>(sql,
                Newtouch.CIS.Compat.SqlParam.ToSugar(new SqlParameter[] { new SqlParameter("@icd10", icd10 ?? ""), new SqlParameter("@orgId", organizeId ?? "") }));
        }
        public System.Collections.Generic.IList<SysDiagnosisVEntity> GetList(string orgId, string keyword, string zdlb, string ybnhlx)
        {
            var sql = @"SELECT * FROM V_S_xt_zd WHERE zt = '1' AND (OrganizeId = @orgId OR OrganizeId = '*')
AND (@kw = '' OR zdmc LIKE @like OR py LIKE @like OR zdCode LIKE @like OR icd10 LIKE @like)
AND (@zdlb = '' OR zdlx = @zdlb)
AND (@ybnhlx = '' OR ybnhlx = @ybnhlx)";
            return BaseDb.GetConnection().Ado.SqlQuery<SysDiagnosisVEntity>(sql,
                Newtouch.CIS.Compat.SqlParam.ToSugar(new System.Data.SqlClient.SqlParameter[] {
                    new System.Data.SqlClient.SqlParameter("@orgId", orgId ?? ""),
                    new System.Data.SqlClient.SqlParameter("@kw", keyword ?? ""),
                    new System.Data.SqlClient.SqlParameter("@like", "%" + (keyword ?? "") + "%"),
                    new System.Data.SqlClient.SqlParameter("@zdlb", zdlb ?? ""),
                    new System.Data.SqlClient.SqlParameter("@ybnhlx", ybnhlx ?? "") })).ToList();
        }

    }
}
