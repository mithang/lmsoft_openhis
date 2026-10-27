using SqlSugar;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Phiếu lĩnh
    /// </summary>
    public class KfApplyOrderRepo: RepositoryBase<KfApplyOrderEntity>, IKfApplyOrderRepo
    {
        

        /// <summary>
        /// Truy vấn thông tin chính phiếu lĩnh
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public KfApplyOrderEntity SelectData(string sldh, string organizeId)
        {
            const string sql =@"
SELECT * FROM dbo.kf_applyOrder(NOLOCK) 
WHERE sldh=@sldh AND OrganizeId=@OrganizeId AND zt='1'";
            var param = new SugarParameter[]
            {
                new SugarParameter("@sldh", sldh),
                new SugarParameter("@OrganizeId", organizeId),
            };
            return FirstOrDefault<KfApplyOrderEntity>(sql, param);
        }
    }
}