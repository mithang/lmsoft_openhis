using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using System.Web;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.ValueObjects;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Kiểm kê tồn kho
    /// </summary>
    public interface IStockInventoryApp
    {
        /// <summary>
        /// Bắt đầu kiểm kê
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        VInventoryDateDropDownEntity StartInventory(string warehouseId, string organizeId);

        /// <summary>
        /// Lưu kiểm kê
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        /// <param name="pdId">Phiếu kiểm kê ID</param>
        /// <param name="noPc">  0：Kiểm kê theo lô hàng 1：Kiểm kê không theo lô hàng</param>
        void SaveInventoryInfo(List<SaveInventoryDTO> inventoryInfoList, long pdId, string noPc = "0");

        /// <summary>
        /// Hủy kiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        void CancelInventory(long pdId);

        /// <summary>
        /// Kết thúc kiểm kê
        /// </summary>
        /// <returns></returns>
        void EndInventory(long pdId);

        /// <summary>
        /// Gửi chênh lệch
        /// </summary>
        /// <param name="plist"></param>
        /// <param name="warehouseId"></param>
        /// <returns>error msg</returns>
        string LossAndProfitSubmit(List<LossAndProditSubmitDTO> plist, string warehouseId);
    }
}