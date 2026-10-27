using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// xt_yp_kcjz
    /// </summary>
    public interface ISysMedicineStockCarryOverRepo : IRepositoryBase<SysMedicineStockCarryOverEntity>
    {
        /// <summary>
        /// Truy vấn thời gian kết chuyển lần trước
        /// </summary>
        /// <returns></returns>
        CarryOverLastAccountPeriodAndCarrayTimeVO GetLastAccountPeriodAndCarrayTime();
    }
}
