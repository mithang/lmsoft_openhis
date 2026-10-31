using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Domain.ValueObjects.Storage;


namespace Newtouch.Domain.IDomainServices.DeptStorageManage
{
    /// <summary>
    /// Tồn khoKiểm kê
    /// </summary>
    public interface IStockInventoryDmnService
    {
        /// <summary>
        /// LấytreotừKiểm kêThời gian
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VInventoryDateDropDownEntity> GetHangUpPdDates(string warehouseId, string organizeId);

        /// <summary>
        /// Lấytất cảKiểm kêThời gian
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VInventoryDateDropDownEntity> GetPdSj(string warehouseId, string organizeId);

        /// <summary>
        /// tạoKiểm kêthông tin
        /// </summary>
        string GenerateInventoryInfo(string warehouseId, string organizeId);

        /// <summary>
        /// LấyKiểm kêthông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VInventoryInfoEntity> QueryInventoryInfoList(Pagination pagination, InventorySearchDTO param, string warehouseId, string organizeId);

        /// <summary>
        /// LấyKiểm kêthông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VInventoryInfoEntity> QueryInventoryInfoListNoPc(Pagination pagination, InventorySearchDTO param, string warehouseId, string organizeId);
        /// <summary>
        /// HủyKiểm kê 
        /// Xóa Kiểm kêthông tin(xt_yp_pdxx),Kiểm kêthông tinchi tiết(xt_yp_pdxxmx)
        /// </summary>
        void CancelInventory(long pdId);

        /// <summary>
        /// Kết thúcKiểm kê
        /// </summary>
        /// <returns></returns>
        string EndInventory(long pdId, string organizeId, string creatorCode);

        /// <summary>
        /// lôlượngLưulãi lỗthông tin
        /// </summary>
        /// <param name="syList"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int InsertSyxxBatch(List<KcSyxxEntity> syList, string warehouseId, string organizeId);

        /// <summary>
        /// LưuđổihơnsaucủaKiểm kêchi tiết
        /// </summary>
        /// <param name="saveInventoryDto"></param>
        /// <param name="pdId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SaveInventoryDetailNoPc(List<SaveInventoryDTO> saveInventoryDto, long pdId, string warehouseId, string organizeId);
    }
}
