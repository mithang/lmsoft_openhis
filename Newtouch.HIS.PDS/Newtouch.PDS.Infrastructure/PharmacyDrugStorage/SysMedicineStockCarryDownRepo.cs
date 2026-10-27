using System.Linq;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using Newtouch.HIS.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.V;

namespace Newtouch.HIS.Repository.PharmacyDrugStorage
{
    /// <summary>
    /// 库存结转
    /// </summary>
    public class SysMedicineStockCarryDownRepo : RepositoryBase<SysMedicineStockCarryDownEntity>, ISysMedicineStockCarryDownRepo
    {
        
        /// <summary>
        /// 获取历史结转时间
        /// </summary>
        /// <returns></returns>
        public List<KeyValueVEntity> GetlsjzDateTime(string yfbmCode, string organizeId)
        {
            const string sql = @"
SELECT CONVERT(VARCHAR(23), Jzsj, 120) [key], CONVERT(VARCHAR(23), Jzsj, 120) [value]  
FROM (
	SELECT DISTINCT TOP 365 jz.Jzsj 
	FROM dbo.xt_yp_kcjzk(NOLOCK) jz 
	WHERE jz.OrganizeId=@OrganizeId 
	AND jz.yfbmCode=@yfbmCode
	ORDER BY jz.Jzsj desc
) a 
";
            SugarParameter[] param =
            {
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@yfbmCode", yfbmCode)
            };
            return FindList<KeyValueVEntity>(sql, param);
        }

        /// <summary>
        /// 获取最近一次结转信息
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public SysMedicineStockCarryDownEntity GetLastJzData(string yfbmCode, string organizeId)
        {
            return IQueryable(p => p.yfbmCode == yfbmCode && p.OrganizeId == organizeId)
                .OrderByDescending(o => o.Jzsj)
                .FirstOrDefault();
        }
    }
}
