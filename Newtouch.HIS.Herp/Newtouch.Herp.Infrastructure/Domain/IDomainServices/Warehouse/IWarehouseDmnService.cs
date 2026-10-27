using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Thao tác kho
    /// </summary>
    public interface IWarehouseDmnService
    {
        /// <summary>
        /// insert new warehouse information
        /// </summary>
        /// <param name="wzEntity"></param>
        /// <param name="deptList"></param>
        /// <param name="userList"></param>
        void InsertWarehouse(KfWarehouseEntity wzEntity, List<RelWarehouseUserEntity> userList, List<RelWarehouseDeptEntity> deptList);

        /// <summary>
        /// update warehouse information
        /// </summary>
        /// <param name="wzEntity"></param>
        /// <param name="deptList"></param>
        /// <param name="userList"></param>
        void UpdateWarehouse(KfWarehouseEntity wzEntity, List<RelWarehouseUserEntity> userList, List<RelWarehouseDeptEntity> deptList);

        /// <summary>
        /// Lấy thông tin kho
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        IList<VKfWarehouseEntity> QueryWarehouseInfo(string keyWord, string organizeId, string zt = "1");

        /// <summary>
        /// Xóa kho
        /// </summary>
        /// <param name="id">Kho Id</param>
        void DeleteWarehouse(string id);

        /// <summary>
        /// Lấy thông tin quản trị viên kho
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VKfUserInfoEntity> GetKfUserInfo(string userId, string organizeId);

        /// <summary>
        /// Lấy vật tư đồng bộ của kho
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="warehouseId">Kho ID</param>
        /// <param name="wzlb">Loại vật tư</param>
        /// <param name="keyWord">Truy vấn từ khóa</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VSyncProductEntity> GetSyncProductList(Pagination pagination, string warehouseId, string wzlb, string keyWord, string organizeId);

        /// <summary>
        /// Lấy vật tư kho
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="lb"></param>
        /// <param name="kzbz"></param>
        /// <param name="zt"></param>
        /// <param name="organizeId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        IList<VWarehouseProductEntity> GetWarehouseProducts(Pagination pagination, string keyWord, string lb,
           string kzbz, string zt, string organizeId, string warehouseId);
    }
}