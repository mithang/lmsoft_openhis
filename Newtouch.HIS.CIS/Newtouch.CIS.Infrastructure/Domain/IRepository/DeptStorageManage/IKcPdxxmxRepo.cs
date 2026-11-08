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
        /// Kiểm kê Lưu khi đổi hơn Số lượng
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        void UpdateSlBySaveInventoryInfo(List<SaveInventoryDTO> inventoryInfoList);
    }
}
