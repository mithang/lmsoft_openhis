using System;
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
        /// tra cứu lượng tồn kho
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="tybz"></param>
        /// <param name="zt"></param>
        /// <param name="noShow0Kc"></param>
        /// <returns></returns>
        IList<MedicineStockQueryVO> SelectStockTotal(Pagination pagination, string inputCode, string tybz, string zt, bool noShow0Kc);

        /// <summary>
        /// tra cứu lượng tồn kho chi tiết
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        IList<MedicineStockQueryDetailVO> SelectStockDetail(string ypCode, string yfbmCode);

        /// <summary>
        /// tra cứu chứng từ chính thông tin
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
        /// tra cứu chứng từ chính thông tin
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        IList<ReceiptQueryVO> SelectReceiptMainInfo(ReceiptQueryParam para);

        /// <summary>
        /// tra cứu chứng từ chi tiết
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ReceiptQueryDetailVO> SelectReceipDetailInfo(string crkId, int djlx, string orgId);

        /// <summary>
        /// lấy ngày kiểm kê
        /// </summary>
        /// <returns></returns>
        List<InventoryDateDropDownVO> GetPdDateDropdownList();

        /// <summary>
        /// lấy chưa hoàn thành của thời gian kiểm kê
        /// </summary>
        /// <returns></returns>
        List<InventoryDateDropDownVO> GetHangUpPdDates();

        /// <summary>
        /// lấy loại thuốc 
        /// </summary>
        /// <returns></returns>
        List<MedicineCategoryVO> GetMedicineCategoryList();

        /// <summary>
        /// Sinh thông tin kiểm kê
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string GenerateInventoryInfo(string yfbmCode, string organizeId, string userCode);

        /// <summary>
        /// số lượng hiển thị trang chuyển đổi  XX hộp XX viên
        /// </summary>
        /// <returns></returns>
        string GetYfbmYpComplexYpSlandDw(int sl, string ypCode);

        /// <summary>
        /// tra cứu kết chuyển thông tin thuốc
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
        /// tra cứu kiểm kê thông tin thuốc, đơn vị độc lập
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
        /// kết thúc kiểm kê
        /// </summary>
        /// <returns></returns>
        string EndInventoryInfo(string pdId);

        /// <summary>
        /// hủy kiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        void CancelInventory(string pdId);

        /// <summary>
        /// lấy thuộc tính đặc biệt của thuốc
        /// </summary>
        /// <returns></returns>
        List<DrugSpecialPropertiesVO> GetDrugSpecialPropertiesList();

        /// <summary>
        /// tra cứu cần kết chuyển của thông tin thuốc
        /// </summary>
        /// <returns></returns>
        List<NeedCarryOverMedicineVO> SelectNeedCarryOverMedicineList();

        /// <summary>
        /// kết chuyển
        /// </summary>
        /// <param name="list"></param>
        /// <param name="zq"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        void CarryOverMedicine(List<NeedCarryOverMedicineVO> list, string zq, DateTime kssj, DateTime jssj);

        /// <summary>
        /// tra cứu đã kết chuyển thuốc
        /// </summary>
        /// <param name="zq"></param>
        /// <param name="inputCode"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        IList<CarryOverMedicineVO> SelectCarryOverMedicineList(Pagination pagination, string zq, string inputCode);

        /// <summary>
        /// loại thuốc  (nhập-xuất-tồn)
        /// </summary>
        /// <returns></returns>
        List<MedicineCategoryVO> GetMedicineCategoryList2();

        /// <summary>
        /// thống kê nhập-xuất-tồn
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
        /// lấy thời gian bắt đầu/kết thúc tương ứng với kỳ kết chuyển và kỳ sổ sách
        /// </summary>
        /// <returns></returns>
        List<AccountPeriodDropDownVO> GetAccountPeriodDropDownList();

        #region 通过药房代码获取药库

        /// <summary>
        /// đạt nhà thuốc mã lấy kho thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetTheUpperYkbmCodeList(string keyword, string yfbmCode, string orgId);
        #endregion

        #region  通过药库代码获取药房

        /// <summary>
        /// đạt kho thuốc mã lấy nhà thuốc phát thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="ykbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetTheLowerYfbmCodeList(string keyword, string ykbmCode, string organizeId);
        #endregion

        #region 通过药房代码获取科室
        /// <summary>
        /// đạt nhà thuốc mã lấy khoa
        /// </summary>
        /// <param name="ykbmCode"></param>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDepartmentVEntity> GetTheLowerKsCodeList(string ykbmCode, string orgId, string keyword = "");
        #endregion

        #region 资源预定

        /// <summary>
        /// đặt trước tài nguyên (tồn kho bị đóng băng, ghi bảng mz_cfmxph)
        /// </summary>
        /// <param name="bookItemDo"></param>
        void BookItem(BookItemDo bookItemDo);

        /// <summary>
        /// ngoại trú đơn thuốc đặt trước (tồn kho bị đóng băng)
        /// </summary>
        /// <param name="apiResp"></param>
        /// <param name="cfnm"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        string OutPatientBookItem(object apiResp, string cfnm, cdInfoVO vo);

        /// <summary>
        /// ngoại trú đơn thuốc đặt trước (tồn kho bị đóng băng)
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        void OutPatientBookItem(BookItemDo bookItemDo);

        /// <summary>
        /// nội trú thực hiện y lệnh (tồn kho bị đóng băng)
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        void HospitalizationBookItem(BookItemDo bookItemDo);

        /// <summary>
        /// thực hiện y lệnh lấy thuốc đơn giá
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        MedicineDjandZhyzVO GetZYYPdj(string organizeId, string yfbmCode, string ypCode);
        #endregion
    }
}
