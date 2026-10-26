using System.Collections.Generic;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Domain.IRepository.DeptStorageManage
{
    /// <summary>
    /// Chi tiết tồn kho
    /// </summary>
    public interface IKcPdxxmxRepo : IRepositoryBase<KcPdxxmxEntity>
    {
        /// <summary>
        /// Khi lưu kiểm kê Số lượng thay đổi
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        void UpdateSlBySaveInventoryInfo(List<SaveInventoryDTO> inventoryInfoList);
    }
}