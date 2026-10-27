using System.Collections.Generic;
using SqlSugar;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;

namespace Newtouch.Herp.DomainServices
{
    /// <summary>
    /// Herp-local BaseData service (see IBaseDataDmnService). Queries the
    /// Base DB (NewtouchHIS_Base): charge categories from xt_sfdl and charge
    /// items from xt_sfxm, filtered by OrganizeId and zt='1'. Table/column
    /// names were verified against the live Base DB (xt_sfdl has
    /// dlId/dlCode/dlmc/ParentId/OrganizeId/zt; xt_sfxm has
    /// sfxmCode/sfxmmc/sfdlCode/OrganizeId/zt). UNCERTAINTY: the original
    /// FrameworkBase implementation is binary-only, so the zt='1' filter and
    /// ordering are the most reasonable reconstruction, not a verified copy
    /// of the original query.
    /// </summary>
    public class BaseDataDmnService : DmnServiceBase, IBaseDataDmnService
    {
        protected override SqlSugarProvider Db => HerpDb.GetBaseConnection();

        public List<SfdlVo> SelectSfdl(string organizeId)
        {
            const string sql = @"
SELECT dlCode, dlmc, ParentId FROM dbo.xt_sfdl(NOLOCK)
WHERE OrganizeId=@OrganizeId AND zt='1'
ORDER BY px, dlCode
";
            return FindList<SfdlVo>(sql, new SugarParameter("@OrganizeId", organizeId));
        }

        public List<SfxmVo> SelectSfxm(string sfdlCode, string organizeId)
        {
            const string sql = @"
SELECT sfxmCode, sfxmmc FROM dbo.xt_sfxm(NOLOCK)
WHERE sfdlCode=@sfdlCode AND OrganizeId=@OrganizeId AND zt='1'
ORDER BY sfxmCode
";
            return FindList<SfxmVo>(sql,
                new SugarParameter("@sfdlCode", sfdlCode),
                new SugarParameter("@OrganizeId", organizeId));
        }
    }
}
