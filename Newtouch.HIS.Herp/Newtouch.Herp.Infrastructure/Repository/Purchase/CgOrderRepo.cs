using SqlSugar;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Đơn mua sắm
    /// </summary>
    public class CgOrderRepo : RepositoryBase<CgOrderEntity>, ICgOrderRepo
    {
        

        /// <summary>
        /// Lấy thông tin đơn mua sắm
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public CgOrderEntity SelectData(string orderNo, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.cg_order(NOLOCK) 
WHERE orderNo=@orderNo AND zt='1' AND OrganizeId=@OrganizeId
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@orderNo",orderNo),
                new SugarParameter("@OrganizeId",organizeId)
            };
            return FirstOrDefault<CgOrderEntity>(sql, param);
        }

        /// <summary>
        /// Duyệt đơn mua sắm
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orderType"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public int AuditPurchasingOrder(string orderNo, int orderType, string userCode, string organizeId, string remark)
        {
            const string sql = @"
UPDATE dbo.cg_order SET orderType=@orderType, remark=@remark, LastModifierCode=@userCode, LastModifyTime=GETDATE()
WHERE orderNo=@orderNo AND orderType=0 AND orderProcess=0 AND zt='1' AND OrganizeId=@OrganizeId
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@orderType", orderType),
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@orderNo", orderNo),
                new SugarParameter("@remark", remark),
                new SugarParameter("@OrganizeId", organizeId)
            };
            return ExecuteSqlCommand(sql, param);
        }
    }
}
