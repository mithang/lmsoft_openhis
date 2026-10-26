using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using System.Collections;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.DTO.DrugStorage;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// tồn kho ứng dụng
    /// </summary>
    public interface IDrugStorageDmnService
    {

        /// <summary>
        /// tra cứu bộ phận hiện tại thuốc(nhập kho)
        /// </summary>
        /// <param name="keyword">ypmc、py、spm、ypCode</param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<DepartmentMedicineVO> SelectDepartmentMedicineList(string keyword, string yfbmCode, string organizeId);

        /// <summary>
        /// thuốc hóa đơn thông tin
        /// </summary>
        /// <param name="fph"></param>
        /// <returns></returns>
        List<MedicineInvoiceInfoVO> SelectMedicineListByFPH(string fph);

        /// <summary>
        /// tra cứu bộ phận hiện tại thuốc(xuất kho)
        /// </summary>
        /// <param name="keyword">ypmc、py、spm、ypCode</param>
        /// <param name="fph"></param>
        /// <param name="gyscode"></param>
        /// <returns></returns>
        List<DepartmentMedicineVO> SelectDepartmentMedicineList2(string keyword, string fph, string gyscode);

        /// <summary>
        /// lấy phiếu lĩnh thông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldh">số phiếu lĩnh</param>
        /// <param name="slbm">bộ phận lĩnh</param>
        /// <param name="ffzt">trạng thái phát thuốc</param>
        /// <param name="txtStartDate">ngày bắt đầu</param>
        /// <param name="txtEndDate">ngày kết thúc</param>
        /// <returns></returns>
        IList<SysMedicineReInfoVO> GetMedicineRequestInfo(Pagination pagination, string sldh, string slbm, string ffzt, string txtStartDate = "", string txtEndDate = "");

        /// <summary>
        /// lấy chi tiết phiếu lĩnh
        /// </summary>
        /// <param name="sldId"></param>
        /// <returns></returns>
        List<SysMedicineReDetailVO> GetMedicineDetail(string sldId);

        /// <summary>
        /// nhập kho
        /// </summary>
        /// <param name="ioReceiptEntity"></param>
        /// <param name="ioReceiptDetailList"></param>
        void SaveInStorageInfo(SysMedicineStorageIOReceiptEntity ioReceiptEntity, List<SysMedicineStorageIOReceiptDetailEntity> ioReceiptDetailList);

        /// <summary>
        /// xuất kho
        /// </summary>
        /// <param name="ioReceiptEntity"></param>
        /// <param name="ioReceiptDetailList"></param>
        void SaveOutStorageInfo(SysMedicineStorageIOReceiptEntity ioReceiptEntity, List<SysMedicineStorageIOReceiptDetailVO> ioReceiptDetailList);

        /// <summary>
        /// điều chỉnh giá đề nghị tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        IList<AdjustPriceMedicineInfoVO> SelectAdjustPriceMedicineInfoList(Pagination pagination, string inputCode);

        /// <summary>
        /// điều chỉnh giá duyệt tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        IList<AdjustPriceMedicineInfoVO> SelectMedicineAdjustPriceApprovalInfoList(Pagination pagination, string inputCode, string shzt);

        /// <summary>
        /// thuốc điều chỉnh giá thực hiện
        /// </summary>
        /// <param name="ypCodeList"></param>
        string ExecteAdjustPrice(ArrayList ypCodeList);

        /// <summary>
        /// điều chỉnh giá lịch sử tra cứu
        /// </summary>
        /// <param name="inputCode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        IList<AdjustPriceHistoryInfoVO> SelectMedicineAdjustPriceHistoryInfoList(Pagination pagination, string inputCode, string startTime, string endTime);

        /// <summary>
        /// chấm dứt xuất kho theo phiếu lĩnh trạng thái
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void UpgradeStatus(string id);

        /// <summary>
        /// thống kê nhập-xuất-tồn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="searchParam"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<PsiStatisticsVo> GetPsiStatisticsVo(Pagination pagination, PsiStatisticsParam searchParam, string yfbmCode, string organizeId);

        /// <summary>
        /// 获取品, không phần lô số lô, 含有效期
        /// </summary>
        /// <param name="srm">từ khóa</param>
        /// <param name="yfbmCode">bộ phận hiện tại</param>
        /// <param name="organizeId"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetMedicinesrmList(string srm, string yfbmCode, string organizeId, int topCount = 100);

        /// <summary>
        /// lấy tại hạn sử dụng trong của thuốc, không phần lô số lô, không bao gồm hạn sử dụng
        /// </summary>
        /// <param name="srm">từ khóa</param>
        /// <param name="yfbmCode">bộ phận hiện tại</param>
        /// <param name="organizeId"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetValidMedicinesrmList(string srm, string yfbmCode, string organizeId, int topCount = 100);

        /// <summary>
        /// lấy tại hạn sử dụng trong của thuốc, không phần lô số lô, không bao gồm hạn sử dụng
        /// </summary>
        /// <param name="ypdm">mã thuốc</param>
        /// <param name="yfbmCode">bộ phận hiện tại</param>
        /// <param name="organizeId"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetPcKcList(string ypdm, string yfbmCode, string organizeId, int topCount = 100);

        /// <summary>
        /// lấy ra bộ phận nhập kho chung cùng sở hữu của thông tin thuốc số lượng tồn kho là bộ phận xuất kho số lượng
        /// </summary>
        /// <param name="ckbm"></param>
        /// <param name="rkbm"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetDrugAndStock(string ckbm, string rkbm, string keyWord, string organizeid);

        /// <summary>
        /// lấy bộ phận hiện tại sở hữu của thuốc và thông tin tồn kho   (nhập kho từ bên ngoài)
        /// </summary>
        /// <param name="currentYfbmCode"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetDrugAndStock(string currentYfbmCode, string keyWord, string organizeid);

        /// <summary>
        /// lấy bộ phận hiện tại sở hữu của thuốc và thông tin tồn kho   (xuất kho ra bên ngoài)
        /// </summary>
        /// <param name="currentYfbmCode"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeid"></param>
        /// <param name="fph">số hóa đơn</param>
        /// <param name="gysCode">mã nhà cung cấp</param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetDrugAndStockByFph(string currentYfbmCode, string keyWord, string organizeid, string fph, string gysCode);

        /// <summary>
        /// lô nhóm
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetStockGroupByBatch(string ypdm, string yfbmCode, string organizeid);

        /// <summary>
        /// theo số hóa đơn lọc lấy lô tồn kho
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeid"></param>
        /// <param name="fph"></param>
        /// <param name="gysCode"></param>
        /// <returns></returns>
        List<DrugStockInfoVEntity> GetStockGroupByBatchByFph(string ypdm, string yfbmCode, string organizeid, string fph, string gysCode,string pc =null);

        /// <summary>
        /// lô nhóm
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="kczt">trạng thái tồn kho 1-hiển thị tồn kho còn hiệu lực  0-hiển thị tồn kho không hiệu lực  trống-hiển thị tất cả</param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<DrugStockInfoVEntity> GetStockGroupByBatch(Pagination pagination, string ypdm, string kczt, string yfbmCode, string organizeid);

        /// <summary>
        /// lấy thông tin tồn kho  tra cứu lượng tồn kho dùng
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="keyWord"></param>
        /// <param name="tybz">cờ ngừng sử dụng 1-展示有效的本部门信息 0-không hiệu lực của này bộ phận thông tin 0-tất cả</param>
        /// <param name="kczt">trạng thái tồn kho 1-hiển thị tồn kho còn hiệu lực  0-hiển thị tồn kho không hiệu lực  trống-hiển thị tất cả</param>
        /// <param name="show0kc">có hiển thị tồn kho bằng 0 không  0-不展示 1-展示</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<DrugStockInfoVEntity> GetDrugAndStock(Pagination pagination, string yfbmCode, string keyWord, string tybz, string kczt, string show0kc, string organizeid,string kcyjcode,string iskcyjz=null);

        /// <summary>
        /// tồn kho tra cứu
        /// </summary>
        /// <param name="ypdms"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        List<DrugStockInfo> SelectStock(string ypdm, string yfbmCode, string organizeid);

        /// <summary>
        /// tra cứu thuốc hết hạn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="gpyf">距离过期还剩x tháng</param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<DrugStockInfoVEntity> SelectExpiredDrugs(Pagination pagination, string keyword, int gpyf, string yfbmCode, string organizeId, string gqyjcode, int? gqyyjts, string noShow0Kc = null);

		/// <summary>
		///lấy đơn chứng tất cả dữ liệu
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		IList<CrkMxAll> GetCrkMxAll(string crkId,string orgId);

		/// <summary>
		/// xóa đơn chứng
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		int DeleteCrkDj(string crkId);

		/// <summary>
		/// sửa đơn chứng
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		int ReviseCrkDj(string crkId);

		/// <summary>
		/// lấy đơn chứng dữ liệu
		/// </summary>
		/// <param name="crkId"></param>
		/// <returns></returns>
		DjGys GetCrkDjh(string crkId,int ? djlx);


        #region 药品有效期管理
        IList<DrugExpiredInfoVEntity> GetStockExpiredSearch(Pagination pagination, string keyword, string show0kc,string yfbmCode, string organizeid);
        #endregion


    }
}
