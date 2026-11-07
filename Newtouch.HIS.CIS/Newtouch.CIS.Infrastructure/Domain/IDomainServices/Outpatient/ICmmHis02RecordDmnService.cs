using System.Collections.Generic;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// đẩy Thuốc Đông ythông tin
    /// </summary>
    public interface ICmmHis02RecordDmnService
    {

        /// <summary>
        /// Lấychưa Đồng bộ của thuốc
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="dlType"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectNoSyncMedicines(string organizeId, string dlType = "TCM");

        /// <summary>
        /// Lấychưa Đồng bộ của thuốc
        /// </summary>
        /// <param name="topSize">Một lầnyêu cầutổngdòngsố</param>
        /// <param name="organizeId"></param>
        /// <param name="dlType"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectNoSyncMedicines(int topSize, string organizeId, string dlType = "TCM");

        /// <summary>
        /// Lấy Đồng bộthất bại của thuốc
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectSyncFailedMedicines(string organizeId);
    }
}
