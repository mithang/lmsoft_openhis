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
        /// tra cứu trên một lần kết chuyển của thời gian
        /// </summary>
        /// <returns></returns>
        CarryOverLastAccountPeriodAndCarrayTimeVO GetLastAccountPeriodAndCarrayTime();
    }
}
