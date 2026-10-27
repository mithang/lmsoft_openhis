using System.Collections.Generic;
using Newtouch.CIS.Compat;
using System.Data.Common;
using System.Data.SqlClient;

using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Domain.IRepository.DeptStorageManage;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 库存信息
    /// </summary>
    public class KfKcxxRepo : RepositoryBase<KfKcxxEntity>, IKfKcxxRepo
    {
        public KfKcxxRepo() : base()
        {
        }

        /// <summary>
        /// 查询库存信息
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<KfKcxxEntity> SelectData(string warehouseId, string productId)
        {
            const string sqlKcxx = @"
SELECT * FROM dbo.kf_kcxx(NOLOCK) kcxx
WHERE kcxx.warehouseId=@warehouseId
AND (kcxx.kcsl-kcxx.djsl)>0
AND kcxx.productId=@productId
AND kcxx.zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@warehouseId", warehouseId),
                new SqlParameter("@productId", productId)
            };
            return FindList<KfKcxxEntity>(sqlKcxx, param);
        }

        /// <summary>
        /// 修改库存状态
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public int UpdateZt(string productId, string ph, string pc, string zt)
        {
            var entity = FindEntity(p => p.ph == ph && p.pc == pc && p.productId == productId);
            if (entity == null) return 0;
            entity.zt = zt.Trim();
            entity.Modify();
            return Update(entity);
        }
    }
}
