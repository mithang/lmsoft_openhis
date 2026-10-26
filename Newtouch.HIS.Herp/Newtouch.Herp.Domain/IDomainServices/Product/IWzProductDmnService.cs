using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.ValueObjects;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Thao tác vật tư
    /// </summary>
    public interface IWzProductDmnService
    {
        /// <summary>
        /// get product list
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        IList<VWzProductEntity> GetList(Pagination pagination, string organizeId, string lb, string zt, string keyWord);

        /// <summary>
        /// Theo ID Xóa thông tin vật tư
        /// </summary>
        /// <param name="id"></param>
        void DeleteProduct(string id);

        /// <summary>
        /// Theo quan hệ liên kết đơn vị vật tư ID Lấy thông tin vật tư và đơn vị
        /// </summary>
        /// <param name="relId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VProductUnitEntity> GetProductAndUnitByProId(string relId, string organizeId);

        /// <summary>
        /// Lấy loại vật tư của khoa này
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        List<VBmwzlbEntity> GetBmWzLb(string warehouseId, string organizeId, string zt = "1");

        /// <summary>
        /// Sửa giá vật tư
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lsj"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        int UpdateProductPrice(string id, decimal lsj, string userCode, string organizeId, string zt = "1");

        /// <summary>
        /// Lấy thông tin vật tư
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        VProductInfoEntity GetProductInfo(string id, string organizeId);

        /// <summary>
        /// Lấy tổng số sự kiện chờ xử lý
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        NeedDealCountVO GetNeedDealCountByZkf(string warehouseId, string organizeId);

        /// <summary>
        /// Lấy tổng số sự kiện chờ xử lý
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        NeedDealCountVO GetNeedDealCountByKskf(string warehouseId, string organizeId);

        /// <summary>
        /// Lấy mã vật tư
        /// </summary>
        /// <returns></returns>
        string GetProductCode();

        /// <summary>
        /// Kiểm tra mã vật tư đã tồn tại chưa
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        string CheckProductCode(string productCode, string keyValue);

        /// <summary>
        /// Chèn dữ liệu vật tư và đồng bộ đến khoản thu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int InsertProductToSfxm(WzProductEntity entity);


		int InsertTOSfxm(string orgid, string wzcode);

        /// <summary>
        /// Cập nhật dữ liệu vật tư và đồng bộ đến khoản thu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int updateProductToSfxm(WzProductEntity entity);

        /// <summary>
        /// Lấy chi tiết sản phẩm
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <param name="topCount">Mặc định top 500</param>
        /// <returns></returns>
        List<VProductInfoDo> SelectProductDetail(string keyword, string organizeId, int topCount = 500);

        /// <summary>
        /// Tra cứu tất cả đơn vị của vật tư
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ProductUnit> SelectProductUnits(string productId, string organizeId);
    }
}