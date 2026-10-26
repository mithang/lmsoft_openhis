using System.Collections.Generic;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// Đẩy thông tin thuốc đông y
    /// </summary>
    public interface ICmmHis02RecordDmnService
    {

        /// <summary>
        /// Lấy thuốc chưa đồng bộ
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="dlType"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectNoSyncMedicines(string organizeId, string dlType = "TCM");

        /// <summary>
        /// Lấy thuốc chưa đồng bộ
        /// </summary>
        /// <param name="topSize">Tổng số dòng của một lần yêu cầu</param>
        /// <param name="organizeId"></param>
        /// <param name="dlType"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectNoSyncMedicines(int topSize, string organizeId, string dlType = "TCM");

        /// <summary>
        /// Lấy thuốc đồng bộ thất bại
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectSyncFailedMedicines(string organizeId);
    }
}