using SqlSugar;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Kế hoạch mua sắm
    /// </summary>
    public class CgPurchaseOrderRepo : RepositoryBase<CgPurchaseOrderEntity>, ICgPurchaseOrderRepo
    {
        

        /// <summary>
        /// Hủy kế hoạch mua sắm
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int CancelPurchasingPlan(string cgdh, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.cg_purchaseOrder SET auditState=3,LastModifyTime=GETDATE(),LastModifierCode=@userCode
WHERE cgdh=@cgdh AND OrganizeId=@OrganizeId AND zt='1'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@cgdh", cgdh),
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@OrganizeId", organizeId)
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// Truy vấn kế hoạch mua sắm
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public CgPurchaseOrderEntity SelectData(string cgdh, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.cg_purchaseOrder(NOLOCK) 
WHERE cgdh=@cgdh AND OrganizeId=@OrganizeId AND zt='1'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@cgdh", cgdh),
                new SugarParameter("@OrganizeId", organizeId)
            };
            return FirstOrDefault<CgPurchaseOrderEntity>(sql, param);
        }
    }
}
