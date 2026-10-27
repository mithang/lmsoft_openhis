using System.Collections.Generic;
using Newtouch.CIS.Compat;
using System.Web;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.ValueObjects.Storage;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Tồn khoKiểm kê
    /// </summary>
    public interface IStockInventoryApp
    {
        /// <summary>
        /// Bắt đầuKiểm kê
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        VInventoryDateDropDownEntity StartInventory(string warehouseId, string organizeId);

        /// <summary>
        /// Kiểm kêLưu
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        /// <param name="pdId">Kiểm kêphiếuID</param>
        /// <param name="noPc">  0：theoLôKiểm kê 1：khôngtheoLôKiểm kê</param>
        void SaveInventoryInfo(List<SaveInventoryDTO> inventoryInfoList, long pdId, string noPc = "0");

        /// <summary>
        /// HủyKiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        void CancelInventory(long pdId);

        /// <summary>
        /// Kết thúcKiểm kê
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