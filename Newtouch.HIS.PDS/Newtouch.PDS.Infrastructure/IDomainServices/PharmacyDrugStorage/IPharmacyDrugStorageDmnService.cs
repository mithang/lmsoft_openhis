using System;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.VO;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPharmacyDrugStorageDmnService
    {
        /// <summary>
        /// Truy vấn lượng tồn kho
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="tybz"></param>
        /// <param name="zt"></param>
        /// <param name="noShow0Kc"></param>
        /// <returns></returns>
        IList<MedicineStockQueryVO> SelectStockTotal(Pagination pagination, string inputCode, string tybz, string zt, bool noShow0Kc);

        /// <summary>
        /// Chi tiết truy vấn lượng tồn kho
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        IList<MedicineStockQueryDetailVO> SelectStockDetail(string ypCode, string yfbmCode);

        /// <summary>
        /// Truy vấn chứng từ Thông tin chính
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="qsrj"></param>
        /// <param name="jsrj"></param>
        /// <param name="pdh"></param>
        /// <param name="fph"></param>
        /// <param name="djlx"></param>
        /// <param name="shzt"></param>
        /// <param name="allUseableDjlx"></param>
        /// <param name="orgId"></param>
        /// <param name="curYfbmCode"></param>
        /// <returns></returns>
        IList<ReceiptQueryVO> SelectReceiptMainInfo(Pagination pagination, DateTime? qsrj, DateTime? jsrj, string pdh, string fph, int? djlx, string shzt, string allUseableDjlx, string orgId, string curYfbmCode);

        /// <summary>
        /// Truy vấn chứng từ Thông tin chính
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        IList<ReceiptQueryVO> SelectReceiptMainInfo(ReceiptQueryParam para);

        /// <summary>
        /// Chi tiết truy vấn chứng từ
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ReceiptQueryDetailVO> SelectReceipDetailInfo(string crkId, int djlx, string orgId);

        /// <summary>
        /// Lấy ngày kiểm kê
        /// </summary>
        /// <returns></returns>
        List<InventoryDateDropDownVO> GetPdDateDropdownList();

        /// <summary>
        /// Lấy thời gian kiểm kê chưa hoàn thành
        /// </summary>
        /// <returns></returns>
        List<InventoryDateDropDownVO> GetHangUpPdDates();

        /// <summary>
        /// Lấy loại thuốc 
        /// </summary>
        /// <returns></returns>
        List<MedicineCategoryVO> GetMedicineCategoryList();

        /// <summary>
        /// Tạo thông tin kiểm kê
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string GenerateInventoryInfo(string yfbmCode, string organizeId, string userCode);

        /// <summary>
        /// Chuyển đổi số lượng hiển thị trên trang  XXhộpXXviên
        /// </summary>
        /// <returns></returns>
        string GetYfbmYpComplexYpSlandDw(int sl, string ypCode);

        /// <summary>
        /// Truy vấn thông tin thuốc kết chuyển
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="pdId"></param>
        /// <param name="pdsj"></param>
        /// <param name="srm"></param>
        /// <param name="ypzt"></param>
        /// <param name="yplb"></param>
        /// <param name="kcxs"></param>
        /// <returns></returns>
        IList<InventoryInfoVO> SelectInventoryInfoList(Pagination pagination, string pdId, string pdsj, string srm, string ypzt, string yplb, int kcxs);
        IList<DrupreparationVO> SelectDrupreparationInfo(SelectDrupreParam pagination);
        /// <summary>
        /// Truy vấn thông tin thuốc kiểm kê Đơn vị độc lập
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="pdId"></param>
        /// <param name="pdsj"></param>
        /// <param name="srm"></param>
        /// <param name="ypzt"></param>
        /// <param name="yplb"></param>
        /// <param name="kcxs"></param>
        /// <returns></returns>
        IList<InventoryInfoVO> SelectInventoryDetailIndependentUnit(Pagination pagination, string pdId,
           string pdsj, string srm, string ypzt, string yplb, int kcxs);
        /// <summary>
        /// Kết thúc kiểm kê
        /// </summary>
        /// <returns></returns>
        string EndInventoryInfo(string pdId);

        /// <summary>
        /// Hủy kiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        void CancelInventory(string pdId);

        /// <summary>
        /// Lấy thuộc tính đặc biệt của thuốc
        /// </summary>
        /// <returns></returns>
        List<DrugSpecialPropertiesVO> GetDrugSpecialPropertiesList();

        /// <summary>
        /// Truy vấn thông tin thuốc cần kết chuyển
        /// </summary>
        /// <returns></returns>
        List<NeedCarryOverMedicineVO> SelectNeedCarryOverMedicineList();

        /// <summary>
        /// Kết chuyển
        /// </summary>
        /// <param name="list"></param>
        /// <param name="zq"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        void CarryOverMedicine(List<NeedCarryOverMedicineVO> list, string zq, DateTime kssj, DateTime jssj);

        /// <summary>
        /// Truy vấn thuốc đã kết chuyển
        /// </summary>
        /// <param name="zq"></param>
        /// <param name="inputCode"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        IList<CarryOverMedicineVO> SelectCarryOverMedicineList(Pagination pagination, string zq, string inputCode);

        /// <summary>
        /// Loại thuốc （Nhập xuất tồn）
        /// </summary>
        /// <returns></returns>
        List<MedicineCategoryVO> GetMedicineCategoryList2();

        /// <summary>
        /// Thống kê nhập xuất tồn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kszq"></param>
        /// <param name="jszq"></param>
        /// <param name="ksjzsj"></param>
        /// <param name="jsjzsj"></param>
        /// <param name="inputCode"></param>
        /// <param name="deptCode"></param>
        /// <param name="drugType"></param>
        /// <param name="dosage"></param>
        /// <param name="drugState"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        IList<PSIStatisticsVO> PsiStatisticsInfoList(Pagination pagination, string kszq, string jszq, DateTime ksjzsj, DateTime jsjzsj, string inputCode, string deptCode, string drugType, string dosage, string drugState, string rate);

        /// <summary>
        /// Lấy kỳ kết chuyển và thời gian bắt đầu/kết thúc tương ứng của kỳ
        /// </summary>
        /// <returns></returns>
        List<AccountPeriodDropDownVO> GetAccountPeriodDropDownList();

        #region Lấy kho thuốc theo mã nhà thuốc

        /// <summary>
        /// Lấy kho thuốc theo mã nhà thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetTheUpperYkbmCodeList(string keyword, string yfbmCode, string orgId);
        #endregion

        #region  Lấy nhà thuốc theo mã kho thuốc

        /// <summary>
        /// Lấy nhà thuốc phát thuốc theo mã kho thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="ykbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetTheLowerYfbmCodeList(string keyword, string ykbmCode, string organizeId);
        #endregion

        #region Lấy khoa theo mã nhà thuốc
        /// <summary>
        /// Lấy khoa theo mã nhà thuốc
        /// </summary>
        /// <param name="ykbmCode"></param>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDepartmentVEntity> GetTheLowerKsCodeList(string ykbmCode, string orgId, string keyword = "");
        #endregion

        #region Đặt trước tài nguyên

        /// <summary>
        /// Đặt trước tài nguyên （Đóng băng tồn kho，ghimz_cfmxphbảng）
        /// </summary>
        /// <param name="bookItemDo"></param>
        void BookItem(BookItemDo bookItemDo);

        /// <summary>
        /// Đặt trước đơn ngoại trú（Đóng băng tồn kho）
        /// </summary>
        /// <param name="apiResp"></param>
        /// <param name="cfnm"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        string OutPatientBookItem(object apiResp, string cfnm, cdInfoVO vo);

        /// <summary>
        /// Đặt trước đơn ngoại trú（Đóng băng tồn kho）
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        void OutPatientBookItem(BookItemDo bookItemDo);

        /// <summary>
        /// Thực hiện y lệnh nội trú（Đóng băng tồn kho）
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        void HospitalizationBookItem(BookItemDo bookItemDo);

        /// <summary>
        /// Thực hiện y lệnh Lấy đơn giá thuốc
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        MedicineDjandZhyzVO GetZYYPdj(string organizeId, string yfbmCode, string ypCode);
        #endregion
    }
}
