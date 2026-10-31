using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Kiểm kê
    /// </summary>
    public interface IKcPdxxRepo : IRepositoryBase<KcPdxxEntity>
    {
        /// <summary>
        /// Theo kiểm kê ID Lấy thông tin kiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        KcPdxxEntity SelectPdInfByPdId(long pdId, string organizeId);

        /// <summary>
        /// Kiểm tra có kiểm kê chưa hoàn thành không (Kết thúc)
        /// </summary>
        /// <returns></returns>
        int SelectUnfinishedInventoryByPdId(long pdId, string organizeId);
    }
}