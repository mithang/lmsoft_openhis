using System.Collections.Generic;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
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