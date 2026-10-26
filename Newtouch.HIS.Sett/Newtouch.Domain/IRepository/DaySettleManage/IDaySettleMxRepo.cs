using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDaySettleRepo : IRepositoryBase<DaySettleEntity>
    {
        /// <summary>
        /// Khi quyết toán xuất viện, kiểm tra xem có kế hoạch thực hiện chưa hoàn thành và chưa chấm dứt hay không
        /// </summary>
        void CheckAccountingPlanDetailStatus(string zyh, string orgId);
    }
}
