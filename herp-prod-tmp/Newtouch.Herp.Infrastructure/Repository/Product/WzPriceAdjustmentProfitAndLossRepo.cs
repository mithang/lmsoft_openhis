using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 物资调价损益 (Herp DB: wz_priceAdjustmentProfitAndLoss).
    /// Thin port: no custom methods in the original; required by
    /// WzPriceAdjustmentDmnService.AdjustPriceExecute.
    /// </summary>
    public class WzPriceAdjustmentProfitAndLossRepo : RepositoryBase<WzPriceAdjustmentProfitAndLossEntity>, IWzPriceAdjustmentProfitAndLossRepo
    {
        public WzPriceAdjustmentProfitAndLossRepo()
        {
        }
    }
}
