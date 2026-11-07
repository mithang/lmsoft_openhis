using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository.DeptStorageManage
{
    /// <summary>
    /// Kiểm kê
    /// </summary>
    public interface IKcPdxxRepo : IRepositoryBase<KcPdxxEntity>
    {
        /// <summary>
        /// Theo Kiểm kê ID Lấy Kiểm kê thông tin
        /// </summary>
        /// <param name="pdId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        KcPdxxEntity SelectPdInfByPdId(long pdId, string organizeId);

        /// <summary>
        /// kiểm tralàKhôngcóchưaHoàn thànhcủaKiểm kê (Kết thúc)
        /// </summary>
        /// <returns></returns>
        int SelectUnfinishedInventoryByPdId(long pdId, string organizeId);
    }
}
