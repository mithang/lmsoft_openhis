using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// xt_yp_pdxxmx
    /// </summary>
    public interface ISysMedicineInventoryDetailRepo : IRepositoryBase<SysMedicineInventoryDetailEntity>
    {
        /// <summary>
        /// Khi lưu kiểm kê Thay đổi số lượng
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        void UpdateSlBySaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList);

        /// <summary>
        /// Thay đổi thông tin mã truy xuất tồn kho
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        void UpdateZsmBySaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList);
    }
}
