using System.Collections.Generic;
using System.Linq;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 物资调价 (Herp DB: wz_priceAdjustment).
    /// </summary>
    public class WzPriceAdjustmentRepo : RepositoryBase<WzPriceAdjustmentEntity>, IWzPriceAdjustmentRepo
    {
        public WzPriceAdjustmentRepo()
        {
        }

        public List<WzPriceAdjustmentEntity> GetUnexecutedList(string productId, string organizeId)
        {
            return IQueryable(p => p.productId == productId
                                   && p.OrganizeId == organizeId
                                   && p.zxbz == ((int)EnumTjZxbz.Not).ToString()
                                   && (p.shzt == ((int)EnumTjShzt.Audited).ToString() || p.shzt == ((int)EnumTjShzt.Waiting).ToString())).ToList();
        }
    }
}
