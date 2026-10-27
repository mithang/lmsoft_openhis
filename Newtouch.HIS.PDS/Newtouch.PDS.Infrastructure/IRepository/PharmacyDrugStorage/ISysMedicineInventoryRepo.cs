using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{

    /// <summary>
    /// xt_yp_pdxx
    /// </summary>
    public interface ISysMedicineInventoryRepo : IRepositoryBase<SysMedicineInventoryEntity>
    {

        /// <summary>
        /// Kiểm tra có kiểm kê chưa hoàn thành không  （Bắt đầu）
        /// </summary>
        /// <returns></returns>
        int SelectUnfinishedInventory();

        /// <summary>
        /// Kiểm tra có kiểm kê chưa hoàn thành không  （Kết thúc）
        /// </summary>
        /// <returns></returns>
        int SelectUnfinishedInventoryByPdId(string pdId);

        /// <summary>
        /// Theo kiểm kêIDLấy thông tin kiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        SysMedicineInventoryEntity SelectPdInfByPdId(string pdId, string organizeId);
    }


}
