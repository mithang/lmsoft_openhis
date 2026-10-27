using System.Collections.Generic;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository.DeptStorageManage
{
    /// <summary>
    /// Tồn khochi tiết
    /// </summary>
    public interface IKcPdxxmxRepo : IRepositoryBase<KcPdxxmxEntity>
    {
        /// <summary>
        /// Kiểm kêLưukhi đổihơnSố lượng
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        void UpdateSlBySaveInventoryInfo(List<SaveInventoryDTO> inventoryInfoList);
    }
}
