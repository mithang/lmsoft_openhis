using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Thống kê trang chủ
    /// </summary>
    public interface IHomePageStatisticsApp
    {
        /// <summary>
        /// Lấy việc cần làmHTML
        /// </summary>
        /// <returns></returns>
        string AssembleNeedDealHtml();

        /// <summary>
        /// Lấy tổng số việc cần làm
        /// </summary>
        /// <returns></returns>
        NeedDealCountVO GetPendingCount();
    }
}