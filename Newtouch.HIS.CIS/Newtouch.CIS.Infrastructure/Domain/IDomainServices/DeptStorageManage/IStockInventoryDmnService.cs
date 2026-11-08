using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Domain.ValueObjects.Storage;


namespace Newtouch.Domain.IDomainServices.DeptStorageManage
{
    /// <summary>
    /// Tồn kho Kiểm kê
    /// </summary>
    public interface IStockInventoryDmnService
    {
        /// <summary>
        /// Lấy treo từ Kiểm kê Thời gian
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VInventoryDateDropDownEntity> GetHangUpPdDates(string warehouseId, string organizeId);

        /// <summary>
        /// Lấy tất cả Kiểm kê Thời gian
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VInventoryDateDropDownEntity> GetPdSj(string warehouseId, string organizeId);

        /// <summary>
        /// tạo Kiểm kê thông tin
        /// </summary>
        string GenerateInventoryInfo(string warehouseId, string organizeId);

        /// <summary>
        /// Lấy Kiểm kê thông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VInventoryInfoEntity> QueryInventoryInfoList(Pagination pagination, InventorySearchDTO param, string warehouseId, string organizeId);

        /// <summary>
        /// Lấy Kiểm kê thông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VInventoryInfoEntity> QueryInventoryInfoListNoPc(Pagination pagination, InventorySearchDTO param, string warehouseId, string organizeId);
        /// <summary>
        /// Hủy Kiểm kê 
        /// Xóa Kiểm kê thông tin(xt_yp_pdxx),Kiểm kê thông tinchi tiết(xt_yp_pdxxmx)
        /// </summary>
        void CancelInventory(long pdId);

        /// <summary>
        /// Kết thúc Kiểm kê
        /// </summary>
        /// <returns></returns>
        string EndInventory(long pdId, string organizeId, string creatorCode);

        /// <summary>
        /// lô lượng Lưu lãi lỗ thông tin
        /// </summary>
        /// <param name="syList"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int InsertSyxxBatch(List<KcSyxxEntity> syList, string warehouseId, string organizeId);

        /// <summary>
        /// Lưu đổi hơn sau của Kiểm kê chi tiết
        /// </summary>
        /// <param name="saveInventoryDto"></param>
        /// <param name="pdId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SaveInventoryDetailNoPc(List<SaveInventoryDTO> saveInventoryDto, long pdId, string warehouseId, string organizeId);
    }
}
