using System.Collections.Generic;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// đẩyThuốc Đông ythông tin
    /// </summary>
    public interface ICmmHis02RecordDmnService
    {

        /// <summary>
        /// LấychưaĐồng bộ của thuốc
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="dlType"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectNoSyncMedicines(string organizeId, string dlType = "TCM");

        /// <summary>
        /// LấychưaĐồng bộ của thuốc
        /// </summary>
        /// <param name="topSize">Một lầnyêu cầutổngdòngsố</param>
        /// <param name="organizeId"></param>
        /// <param name="dlType"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectNoSyncMedicines(int topSize, string organizeId, string dlType = "TCM");

        /// <summary>
        /// LấyĐồng bộthất bại của thuốc
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectSyncFailedMedicines(string organizeId);
    }
}
