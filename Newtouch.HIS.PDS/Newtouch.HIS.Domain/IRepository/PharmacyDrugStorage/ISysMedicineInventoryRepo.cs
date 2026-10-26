using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{

    /// <summary>
    /// xt_yp_pdxx
    /// </summary>
    public interface ISysMedicineInventoryRepo : IRepositoryBase<SysMedicineInventoryEntity>
    {

        /// <summary>
        /// đánh giá có kiểm kê chưa hoàn thành không   (bắt đầu)
        /// </summary>
        /// <returns></returns>
        int SelectUnfinishedInventory();

        /// <summary>
        /// đánh giá có kiểm kê chưa hoàn thành không   (kết thúc)
        /// </summary>
        /// <returns></returns>
        int SelectUnfinishedInventoryByPdId(string pdId);

        /// <summary>
        /// theo kiểm kê ID lấy kiểm kê thông tin
        /// </summary>
        /// <param name="pdId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        SysMedicineInventoryEntity SelectPdInfByPdId(string pdId, string organizeId);
    }


}
