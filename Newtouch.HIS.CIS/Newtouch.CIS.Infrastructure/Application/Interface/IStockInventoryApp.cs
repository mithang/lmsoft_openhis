using System.Collections.Generic;
using Newtouch.CIS.Compat;
using System.Web;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.ValueObjects.Storage;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Tồn kho Kiểm kê
    /// </summary>
    public interface IStockInventoryApp
    {
        /// <summary>
        /// Bắt đầu Kiểm kê
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        VInventoryDateDropDownEntity StartInventory(string warehouseId, string organizeId);

        /// <summary>
        /// Kiểm kê Lưu
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        /// <param name="pdId">Kiểm kê phiếu ID</param>
        /// <param name="noPc">  0:theo Lô Kiểm kê 1:khôngtheo Lô Kiểm kê</param>
        void SaveInventoryInfo(List<SaveInventoryDTO> inventoryInfoList, long pdId, string noPc = "0");

        /// <summary>
        /// Hủy Kiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        void CancelInventory(long pdId);

        /// <summary>
        /// Kết thúc Kiểm kê
        /// </summary>
        /// <returns></returns>
        void EndInventory(long pdId);

        /// <summary>
        /// Gửilãi lỗ
        /// </summary>
        /// <param name="plist"></param>
        /// <param name="warehouseId"></param>
        /// <returns>error msg</returns>
        string LossAndProfitSubmit(List<LossAndProditSubmitDTO> plist, string warehouseId);
    }
}