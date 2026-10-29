using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using SqlSugar;
using System.Collections.Generic;

namespace Newtouch.Sett.DomainServices
{
    /// <summary>
    /// T-15: real inpatient list for PatientInHosList/LoadData. The previous
    /// service behind LoadData was a stub returning [] regardless of input,
    /// so no keyword filtering could ever be proven. This query reads
    /// zy_brjbxx (Sett DB) directly:
    /// - in-hospital set zybz IN (1,2,3), following the original
    ///   sp_xt_zycyhzlb flag=0 (in-hospital) branch (discharged = 4);
    /// - only valid rows (zt='1') of the caller's org;
    /// - keyword filters server-side (parameterized LIKE) on patient name
    ///   (xm) or admission number (zyh), the two fields the view's search
    ///   box advertises ("Họ tên / Số nhập viện").
    /// PatientOutOfHosList is deliberately NOT wired here: it needs the
    /// discharged set (zybz=4), a different list.
    /// </summary>
    public class PatientInHosListService
    {
        public List<PatientListOutPutVO> PatientList(string orgId, string keyword)
        {
            string like = null;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                // Escape LIKE wildcards so a keyword can only match literally.
                string esc = keyword.Trim()
                    .Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
                like = "%" + esc + "%";
            }
            string sql = @"select blh, xm, xb,
                ISNULL(CAST(DATEDIFF(year, csny, GETDATE())
                    - CASE WHEN DATEADD(year, DATEDIFF(year, csny, GETDATE()), csny) > GETDATE() THEN 1 ELSE 0 END AS SMALLINT), 0) as nl,
                CAST(ryzd as varchar(20)) as ryzd, ryrq, cyrq, zyh, zjh, csny as csrq, cw
                from zy_brjbxx
                where OrganizeId = @orgId and zt = '1' and zybz in ('1', '2', '3')
                and (@like is null or xm like @like or zyh like @like)
                order by ryrq desc";
            return SettDb.GetConnection().SqlQueryable<PatientListOutPutVO>(sql)
                .AddParameters(new SugarParameter[]
                {
                    new SugarParameter("@orgId", orgId ?? ""),
                    new SugarParameter("@like", like),
                })
                .ToList();
        }
    }
}
