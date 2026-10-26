using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class HospSettlementRepo : RepositoryBase<HospSettlementEntity>, IHospSettlementRepo
    {
        public HospSettlementRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// theo số nhập việntra cứu tất cả các của quyết toán bản ghi, Đ ã quyết toán và đã hủy bỏ của bản ghi tiến hành đ ối với đối trừ, nhận đ ược chưa hủy bút toán của dữ liệu
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public List<HospSettlementEntity> GetValidList(string zyh, string orgId)
        {
            string strSQL = string.Format(@"
            SELECT  * 
            FROM    zy_js a WITH ( NOLOCK )
            WHERE   NOT EXISTS ( SELECT *
                                 FROM   zy_js b WITH ( NOLOCK )
                                 WHERE b.OrganizeId = @orgId and  a.jsnm = b.cxjsnm )
                    AND zyh = @zyh
                    AND jszt = @jsztyj
		            AND a.OrganizeId = @orgId
                    and zt = '1'
            order by CreateTime");
            DbParameter[] parameters =
            {
                 new SqlParameter("@zyh", zyh),
                 new SqlParameter("@jsztyj", ((int)EnumJieSuanZT.YJ).ToString()),
                 new SqlParameter("@orgId", orgId)
            };
            List<HospSettlementEntity> cxJsDt = this.FindList<HospSettlementEntity>(strSQL, parameters);
            return cxJsDt;
        }
    }
}


