using System.Collections.Generic;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// đẩy Thuốc Đông y thông tin
    /// </summary>
    public interface ICmmHis02RecordDmnService
    {

        /// <summary>
        /// Lấy chưa Đồng bộ của thuốc
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="dlType"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectNoSyncMedicines(string organizeId, string dlType = "TCM");

        /// <summary>
        /// Lấy chưa Đồng bộ của thuốc
        /// </summary>
        /// <param name="topSize">Một lần yêu cầu tổng dòng số</param>
        /// <param name="organizeId"></param>
        /// <param name="dlType"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectNoSyncMedicines(int topSize, string organizeId, string dlType = "TCM");

        /// <summary>
        /// Lấy Đồng bộ thất bại của thuốc
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectSyncFailedMedicines(string organizeId);
    }
}
