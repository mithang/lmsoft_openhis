using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using SqlSugar;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Thông tin tồn kho
    /// </summary>
    public class KfKcxxRepo : RepositoryBase<KfKcxxEntity>, IKfKcxxRepo
    {
        public KfKcxxRepo()
        {
        }

        /// <summary>
        /// Truy vấn thông tin tồn kho
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
            var param = new SugarParameter[]
            {
                new SugarParameter("@warehouseId", warehouseId),
                new SugarParameter("@productId", productId)
            };
            return FindList<KfKcxxEntity>(sqlKcxx, param);
        }

        /// <summary>
        /// Sửa trạng thái tồn kho
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
