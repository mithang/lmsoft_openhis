using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.Entity;
using System.Collections;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.DTO.DrugStorage;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Ứng dụng tồn kho
    /// </summary>
    public interface IDrugStorageDmnService
    {

        /// <summary>
        /// Truy vấn thuốc của bộ phận hiện tại(Nhập kho)
        /// </summary>
        /// <param name="keyword">ypmc、py、spm、ypCode</param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<DepartmentMedicineVO> SelectDepartmentMedicineList(string keyword, string yfbmCode, string organizeId);

        /// <summary>
        /// Thông tin hóa đơn thuốc
        /// </summary>
        /// <param name="fph"></param>
        /// <returns></returns>
        List<MedicineInvoiceInfoVO> SelectMedicineListByFPH(string fph);

        /// <summary>
        /// Truy vấn thuốc của bộ phận hiện tại(Xuất kho)
        /// </summary>
        /// <param name="keyword">ypmc、py、spm、ypCode</param>
        /// <param name="fph"></param>
        /// <param name="gyscode"></param>
        /// <returns></returns>
        List<DepartmentMedicineVO> SelectDepartmentMedicineList2(string keyword, string fph, string gyscode);

        /// <summary>
        /// Lấy thông tin đơn lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldh">Số đơn lĩnh</param>
        /// <param name="slbm">Bộ phận lĩnh</param>
        /// <param name="ffzt">Trạng thái phát thuốc</param>
        /// <param name="txtStartDate">Ngày bắt đầu</param>
        /// <param name="txtEndDate">Ngày kết thúc</param>
        /// <returns></returns>
        IList<SysMedicineReInfoVO> GetMedicineRequestInfo(Pagination pagination, string sldh, string slbm, string ffzt, string txtStartDate = "", string txtEndDate = "");

        /// <summary>
        /// Lấy chi tiết đơn lĩnh
        /// </summary>
        /// <param name="sldId"></param>
        /// <returns></returns>
        List<SysMedicineReDetailVO> GetMedicineDetail(string sldId);

        /// <summary>
        /// Nhập kho
        /// </summary>
        /// <param name="ioReceiptEntity"></param>
        /// <param name="ioReceiptDetailList"></param>
        void SaveInStorageInfo(SysMedicineStorageIOReceiptEntity ioReceiptEntity, List<SysMedicineStorageIOReceiptDetailEntity> ioReceiptDetailList);

        /// <summary>
        /// Xuất kho
        /// </summary>
        /// <param name="ioReceiptEntity"></param>
        /// <param name="ioReceiptDetailList"></param>
        void SaveOutStorageInfo(SysMedicineStorageIOReceiptEntity ioReceiptEntity, List<SysMedicineStorageIOReceiptDetailVO> ioReceiptDetailList);

        /// <summary>
        /// Đơn xin điều chỉnh giá Truy vấn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        IList<AdjustPriceMedicineInfoVO> SelectAdjustPriceMedicineInfoList(Pagination pagination, string inputCode);

        /// <summary>
        /// Truy vấn duyệt điều chỉnh giá
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        IList<AdjustPriceMedicineInfoVO> SelectMedicineAdjustPriceApprovalInfoList(Pagination pagination, string inputCode, string shzt);

        /// <summary>
        /// Thực hiện điều chỉnh giá thuốc
        /// </summary>
        /// <param name="ypCodeList"></param>
        string ExecteAdjustPrice(ArrayList ypCodeList);

        /// <summary>
        /// Truy vấn lịch sử điều chỉnh giá
        /// </summary>
        /// <param name="inputCode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        IList<AdjustPriceHistoryInfoVO> SelectMedicineAdjustPriceHistoryInfoList(Pagination pagination, string inputCode, string startTime, string endTime);

        /// <summary>
        /// Chấm dứt trạng thái xuất kho theo đơn lĩnh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void UpgradeStatus(string id);

        /// <summary>
        /// Thống kê nhập xuất tồn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="searchParam"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<PsiStatisticsVo> GetPsiStatisticsVo(Pagination pagination, PsiStatisticsParam searchParam, string yfbmCode, string organizeId);

        /// <summary>
        /// Lấy mặt hàng，Không phân biệt lô/số lô，Gồm hạn dùng
        /// </summary>
        /// <param name="srm">Từ khóa</param>
        /// <param name="yfbmCode">Bộ phận hiện tại</param>
        /// <param name="organizeId"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetMedicinesrmList(string srm, string yfbmCode, string organizeId, int topCount = 100);

        /// <summary>
        /// Lấy thuốc còn hạn dùng，Không phân biệt lô/số lô，Không gồm hạn dùng
        /// </summary>
        /// <param name="srm">Từ khóa</param>
        /// <param name="yfbmCode">Bộ phận hiện tại</param>
        /// <param name="organizeId"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetValidMedicinesrmList(string srm, string yfbmCode, string organizeId, int topCount = 100);

        /// <summary>
        /// Lấy thuốc còn hạn dùng，Không phân biệt lô/số lô，Không gồm hạn dùng
        /// </summary>
        /// <param name="ypdm">Mã thuốc</param>
        /// <param name="yfbmCode">Bộ phận hiện tại</param>
        /// <param name="organizeId"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetPcKcList(string ypdm, string yfbmCode, string organizeId, int topCount = 100);

        /// <summary>
        /// Lấy thông tin thuốc chung của bộ phận xuất nhập kho Số lượng tồn kho là số lượng bộ phận xuất kho
        /// </summary>
        /// <param name="ckbm"></param>
        /// <param name="rkbm"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetDrugAndStock(string ckbm, string rkbm, string keyWord, string organizeid);

        /// <summary>
        /// Lấy thông tin thuốc và tồn kho của bộ phận hiện tại  （Nhập kho từ ngoài）
        /// </summary>
        /// <param name="currentYfbmCode"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetDrugAndStock(string currentYfbmCode, string keyWord, string organizeid);

        /// <summary>
        /// Lấy thông tin thuốc và tồn kho của bộ phận hiện tại  （Xuất kho ra ngoài）
        /// </summary>
        /// <param name="currentYfbmCode"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeid"></param>
        /// <param name="fph">Số hóa đơn</param>
        /// <param name="gysCode">Mã nhà cung cấp</param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetDrugAndStockByFph(string currentYfbmCode, string keyWord, string organizeid, string fph, string gysCode);

        /// <summary>
        /// Nhóm theo lô
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetStockGroupByBatch(string ypdm, string yfbmCode, string organizeid);

        /// <summary>
        /// Lọc theo số hóa đơn Lấy tồn kho theo lô
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeid"></param>
        /// <param name="fph"></param>
        /// <param name="gysCode"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetStockGroupByBatchByFph(string ypdm, string yfbmCode, string organizeid, string fph, string gysCode,string pc =null);

        /// <summary>
        /// Nhóm theo lô
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="kczt">Trạng thái tồn kho 1-Hiển thị tồn kho hiệu lực  0-Hiển thị tồn kho không hiệu lực  trống-Hiển thị tất cả</param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<DrugStockInfoVEntity> GetStockGroupByBatch(Pagination pagination, string ypdm, string kczt, string yfbmCode, string organizeid);

        /// <summary>
        /// Lấy thông tin tồn kho  Dùng để truy vấn lượng tồn kho
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="keyWord"></param>
        /// <param name="tybz">Cờ ngừng dùng 1-Hiển thị thông tin bộ phận này hiệu lực 0-Thông tin bộ phận này không hiệu lực 0-Tất cả</param>
        /// <param name="kczt">Trạng thái tồn kho 1-Hiển thị tồn kho hiệu lực  0-Hiển thị tồn kho không hiệu lực  trống-Hiển thị tất cả</param>
        /// <param name="show0kc">Có hiển thị tồn kho bằng 0 không  0-Không hiển thị 1-Hiển thị</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<DrugStockInfoVEntity> GetDrugAndStock(Pagination pagination, string yfbmCode, string keyWord, string tybz, string kczt, string show0kc, string organizeid,string kcyjcode,string iskcyjz=null);

        /// <summary>
        /// Truy vấn tồn kho
        /// </summary>
        /// <param name="ypdms"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        List<DrugStockInfo> SelectStock(string ypdm, string yfbmCode, string organizeid);

        /// <summary>
        /// Truy vấn thuốc hết hạn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="gpyf">Còn lại tới khi hết hạnxtháng</param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<DrugStockInfoVEntity> SelectExpiredDrugs(Pagination pagination, string keyword, int gpyf, string yfbmCode, string organizeId, string gqyjcode, int? gqyyjts, string noShow0Kc = null);

		/// <summary>
		///Lấy tất cả dữ liệu chứng từ
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		IList<CrkMxAll> GetCrkMxAll(string crkId,string orgId);

		/// <summary>
		/// Xóa chứng từ
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		int DeleteCrkDj(string crkId);

		/// <summary>
		/// Sửa chứng từ
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		int ReviseCrkDj(string crkId);

		/// <summary>
		/// Lấy dữ liệu chứng từ
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		DjGys GetCrkDjh(string crkId,int ? djlx);


        #region Quản lý hạn dùng thuốc
        IList<DrugExpiredInfoVEntity> GetStockExpiredSearch(Pagination pagination, string keyword, string show0kc,string yfbmCode, string organizeid);
        #endregion


    }
}
