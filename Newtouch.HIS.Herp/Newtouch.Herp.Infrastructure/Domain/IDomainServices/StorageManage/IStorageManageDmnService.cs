using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Quản lý tồn kho
    /// </summary>
    public interface IStorageManageDmnService
    {

        /// <summary>
        /// Thông tin vật tư trong danh sách thả xuống
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<VSelProductInfoEntity> DepartmentStockListQuery(DepartmentStockListQueryParam param);

        /// <summary>
        /// Thông tin vật tư trong danh sách thả xuống
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="key">Tên vật tư/Pinyin</param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        List<VSelProductInfoEntity> DepartmentStockListQuery(Pagination pagination, string key, string warehouseId, string organizeId, string zt = "1");

        /// <summary>
        /// Thông tin vật tư trong danh sách thả xuống Xuất kho ngoài
        /// </summary>
        /// <param name="key">Tên vật tư/Pinyin</param>
        /// <param name="ckbm"></param>
        /// <param name="deliveryNo"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        List<VSelProductInfoEntity> DepartmentStockListQuery(string key, string ckbm, string deliveryNo, string warehouseId, string organizeId, string zt = "1");

        /// <summary>
        /// Lấy thông tin số lô và lô hàng vật tư  top 20
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="gysId"></param>
        /// <param name="deliveryNo"></param>
        /// <returns></returns>
        List<VProductBatchInfoEntity> ProductBatchQuery(string productId, string warehouseId, string organizeId, string gysId = "", string deliveryNo = "", string keyword = "");

        /// <summary>
        /// Chứng từ nhập kho ngoài
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mxList"></param>
        string SaveOutOrInStorageInfo(KfCrkdjEntity dj, List<KfCrkmxEntity> mxList);

        /// <summary>
        /// Lấy tồn kho vật tư
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <param name="lbId"></param>
        /// <param name="kzbz"></param>
        /// <param name="wzzt"></param>
        /// <param name="xslkc">Hiển thị tồn kho bằng 0  1:Hiển thị  0:Không hiển thị</param>
        /// <param name="ygq">Đã quá hạn</param>
        /// <param name="mxyx">Chi tiết tạm thời còn hiệu lực  true:Có  false:Không</param>
        /// <returns></returns>
        IList<VProductStorageEntity> GetProductStorage(Pagination pagination,
           string warehouseId,
           string organizeId,
           string keyWord,
           string lbId,
           string kzbz,
           string wzzt,
           string xslkc,
           string ygq,
           string mxyx, string kccg = null);

        /// <summary>
        /// Lấy tồn kho vật tư
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        IList<VProductStorageEntity> GetProductStorage(string warehouseId, string organizeId, string keyWord);

        /// <summary>
        /// Tổng hợp tồn kho vật tư theo lô và số lô
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="proId">Vật tư ID</param>
        /// <param name="zt">Chi tiết tạm thời còn hiệu lực  true:Có  false:Không</param>
        /// <returns></returns>
        IList<VProductStorageDetailEntity> GetProductStorageDetail(string warehouseId, string organizeId, string proId, string zt);

        /// <summary>
        /// Nhập kho ngoài Đã duyệt
        /// </summary>
        /// <param name="kcxx"></param>
        /// <param name="dj"></param>
        void WbrkAdopt(List<KfKcxxEntity> kcxx, KfCrkdjEntity dj);

        /// <summary>
        /// Nhập kho ngoài Hủy bỏ
        /// </summary>
        /// <param name="mx"></param>
        /// <param name="dj"></param>
        /// <returns></returns>
        string WbrkCancelled(List<KfCrkmxEntity> mx, KfCrkdjEntity dj);

        /// <summary>
        /// Xuất kho ngoài/Xuất kho trực tiếp/Trả hàng phát nội bộ
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mxList"></param>
        /// <returns></returns>
        string Wbck(KfCrkdjEntity dj, List<KfCrkmxEntity> mxList);

        /// <summary>
        /// Xuất kho ngoài,Trừ tồn kho,Đã duyệt
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string SubtractKc(KfCrkdjEntity dj, List<KfCrkmxEntity> mx, string userCode);

        /// <summary>
        /// Xuất kho ngoài/Xuất kho trực tiếp/Trả hàng phát nội bộ,Rã đóng băng,Không duyệt
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string Unfreeze(KfCrkdjEntity dj, List<KfCrkmxEntity> mx, string userCode);

        /// <summary>
        /// Xuất kho ngoài Hủy bỏ
        /// </summary>
        /// <param name="mx"></param>
        /// <param name="dj"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string WbckCancelled(KfCrkdjEntity dj, List<KfCrkmxEntity> mx, string userCode);

        /// <summary>
        /// Xuất kho trực tiếp Đã duyệt
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string DirectDeliveryAdopt(KfCrkdjEntity dj, List<KfCrkmxEntity> mx, string userCode);

        /// <summary>
        /// Xuất kho trực tiếp/Trả hàng phát nội bộ Thao tác hủy bỏ
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string DirectDeliveryCancel(KfCrkdjEntity dj, List<KfCrkmxEntity> mx, string userCode);

        /// <summary>
        /// Thống kê số lượng xuất kho
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VPsiStatisticsByDateEntity> GetCkCountByKf(string warehouseId, string organizeId);

        /// <summary>
        /// Thống kê số lượng nhập kho
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VPsiStatisticsByDateEntity> GetRkCountByKf(string warehouseId, string organizeId);

        /// <summary>
        /// Thống kê số lượng chênh lệch
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VPsiStatisticsByDateEntity> GetSyCountByKf(string warehouseId, string organizeId);

        /// <summary>
        /// Lấy tổng số nhập kho theo loại chứng từ
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ClassificationStatisticsEntity> GetRkCountByLx(string warehouseId, string organizeId);

        /// <summary>
        /// Lấy tồn kho vật tư
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <param name="lbId"></param>
        /// <param name="kzbz"></param>
        /// <param name="wzzt"></param>
        /// <param name="xslkc">Hiển thị tồn kho bằng 0  1:Hiển thị  0:Không hiển thị</param>
        /// <param name="mxyx">Chi tiết tạm thời còn hiệu lực  true:Có  false:Không</param>
        /// <param name="isExpired">true:Hiển thị quá hạn  false:Hiển thị chưa quá hạn</param>
        /// <returns></returns>
        IList<VProductStorageDetailEntity> SelectStorageDetail(Pagination pagination,
           string warehouseId,
           string organizeId,
           string keyWord,
           string lbId,
           string kzbz,
           string wzzt,
           string xslkc,
           string mxyx,
           bool isExpired);
        
        /// <summary>
        /// Lấy tổng số bản ghi cảnh báo quá hạn và cảnh báo tồn kho
        /// </summary>
        /// <param name="yfbm"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="gqyjz"></param>
        /// <returns></returns>
        IList<SysMSGQueryVO> MSGQuery(string yfbm, string OrganizeId, int gqyjz);

        /// <summary>
        /// Xuất kho đến khoa phòng-Đồng bộ vật tư đến CIS
        /// </summary>
        /// <param name="djh"></param>
        /// <param name="organizeId"></param>
        void SynchWz(string djh, string UserCode, string organizeId);
    }
}