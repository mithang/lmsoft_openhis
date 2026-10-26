using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Domain.ValueObjects.Storage;


namespace Newtouch.Domain.IDomainServices.DeptStorageManage
{
    /// <summary>
    /// Kiểm kê tồn kho
    /// </summary>
    public interface IStockInventoryDmnService
    {
        /// <summary>
        /// Lấy thời gian kiểm kê đang chờ
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VInventoryDateDropDownEntity> GetHangUpPdDates(string warehouseId, string organizeId);

        /// <summary>
        /// Lấy tất cả thời gian kiểm kê
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VInventoryDateDropDownEntity> GetPdSj(string warehouseId, string organizeId);

        /// <summary>
        /// Tạo thông tin kiểm kê
        /// </summary>
        string GenerateInventoryInfo(string warehouseId, string organizeId);

        /// <summary>
        /// Lấy thông tin kiểm kê
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VInventoryInfoEntity> QueryInventoryInfoList(Pagination pagination, InventorySearchDTO param, string warehouseId, string organizeId);

        /// <summary>
        /// Lấy thông tin kiểm kê
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VInventoryInfoEntity> QueryInventoryInfoListNoPc(Pagination pagination, InventorySearchDTO param, string warehouseId, string organizeId);
        /// <summary>
        /// Hủy kiểm kê 
        /// Xóa Thông tin kiểm kê(xt_yp_pdxx),Chi tiết thông tin kiểm kê(xt_yp_pdxxmx)
        /// </summary>
        void CancelInventory(long pdId);

        /// <summary>
        /// Kết thúc kiểm kê
        /// </summary>
        /// <returns></returns>
        string EndInventory(long pdId, string organizeId, string creatorCode);

        /// <summary>
        /// Lưu hàng loạt thông tin chênh lệch
        /// </summary>
        /// <param name="syList"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int InsertSyxxBatch(List<KcSyxxEntity> syList, string warehouseId, string organizeId);

        /// <summary>
        /// Lưu chi tiết kiểm kê sau khi thay đổi
        /// </summary>
        /// <param name="saveInventoryDto"></param>
        /// <param name="pdId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SaveInventoryDetailNoPc(List<SaveInventoryDTO> saveInventoryDto, long pdId, string warehouseId, string organizeId);
    }
}