using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Chứng từ nhập/xuất kho
    /// </summary>
    public interface IKfCrkdjDmnService
    {
        /// <summary>
        /// Lấy thông tin bảng chính chứng từ nhập/xuất kho
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="alldjlx">Tất cả loại chứng từ mà người dùng này có thể có</param>
        /// <param name="warehouseId">Kho hiện tại</param>
        /// <param name="ope">Thao tác  query:Truy vấn  approval:Duyệt</param>
        /// <returns></returns>
        IList<VCrkdjEntity> GetCrkdjMainList(Pagination pagination, CrkdjSearchParamDTO param, string[] alldjlx, string warehouseId, string ope = "");

        /// <summary>
        /// Lấy chi tiết nhập/xuất kho
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCrkdjmxEntity> GetCrkdjmxList(string crkId, string organizeId);

        /// <summary>
        /// Lấy thông tin nhà cung cấp và hóa đơn
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VFphAndGysEntity> GetFphAndGysInfo(string keyWord, string warehouseId, string organizeId);

        /// <summary>
        /// Xóa bảng chính và bảng chi tiết chứng từ Dùng thận trọng
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int DeleteDjById(long crkId, string organizeId);

        /// <summary>
        /// Lưu chứng từ
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mxList"></param>
        /// <returns></returns>
        string SaveDj(KfCrkdjEntity dj, List<KfCrkmxEntity> mxList);

        /// <summary>
        /// Hủy chi tiết đã chèn thành công
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="successList"></param>
        void CancelCrkmx(KfCrkdjEntity dj, List<KfCrkmxEntity> successList);

        /// <summary>
        /// Truy vấn số phiếu phân phối nhập kho
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="warehouseId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeid"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        List<VInStorageDeliveryInfoEntity> SelectInStorageDeliveryInfo(string keyword, string warehouseId, string userCode, string organizeid, string shzt = "4");

        /// <summary>
        /// Lấy chi tiết nhập/xuất kho qua số phiếu phân phối
        /// </summary>
        /// <param name="deliveryNo"></param>
        /// <param name="djh"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeid"></param>
        /// <param name="gysId"></param>
        /// <returns></returns>
        List<VCrkdjmxInfoEntity> SelectCrkmxByDeliveryNo(string deliveryNo, string djh, string warehouseId, string organizeid, string gysId = "");

        /// <summary>
        /// Xóa chứng từ nhập/xuất kho và chi tiết đã chỉ định
        /// </summary>
        /// <param name="crkId"></param>
        /// <returns></returns>
        string DeleteCrkdj(long crkId);
    }
}