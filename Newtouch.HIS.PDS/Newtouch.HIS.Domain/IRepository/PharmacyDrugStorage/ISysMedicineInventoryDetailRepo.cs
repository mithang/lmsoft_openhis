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
        /// kiểm kê lưu giờ thay đổi số lượng
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        void UpdateSlBySaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList);

        /// <summary>
        /// thay đổi tồn kho mã truy xuất thông tin
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        void UpdateZsmBySaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList);
    }
}
