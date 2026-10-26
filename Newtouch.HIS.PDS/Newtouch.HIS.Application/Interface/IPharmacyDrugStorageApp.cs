using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPharmacyDrugStorageApp
    {
        /// <summary>
        /// Tra cứu số lượng tồn kho
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="tybz"></param>
        /// <param name="zt"></param>
        /// <param name="noShow0Kc"></param>
        /// <returns></returns>
        IList<MedicineStockQueryVO> SelectStockTotal(Pagination pagination, string inputCode, string tybz, string zt, bool noShow0Kc);

        /// <summary>
        /// Tra cứu chi tiết số lượng tồn kho
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        IList<MedicineStockQueryDetailVO> SelectStockDetail(string ypCode, string yfbmCode);

        /// <summary>
        /// Tra cứu chi tiết phiếu
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <returns></returns>
        IList<ReceiptQueryDetailVO> SelectReceipDetailInfo(string crkId, int djlx);

        /// <summary>
        /// Lấy ngày kiểm kê
        /// </summary>
        /// <returns></returns>
        List<InventoryDateDropDownVO> GetPdDateDropdownList();

        /// <summary>
        /// Lấy loại thuốc (kiểm kê）
        /// </summary>
        /// <returns></returns>
        List<MedicineCategoryVO> GetMedicineCategoryList();

        /// <summary>
        /// Bắt đầu kiểm kê
        /// </summary>
        InventoryDateDropDownVO StartInventory();

        /// <summary>
        /// Tra cứu thông tin thuốc cần kiểm kê
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

        /// <summary>
        /// Lưu kiểm kê
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        /// <param name="pdId"></param>
        void SaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList, string pdId);


        /// <summary>
        /// Lưu thông tin mã truy xuất tồn kho
        /// </summary>
        /// <param name="SaveInventoryZsmInfo"></param>
        void SaveInventoryZsmInfo(List<SaveInventoryInfoVO> inventoryInfoList);

        /// <summary>
        /// Kết thúc kiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        void EndInventory(string pdId);

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
        /// Kết chuyển
        /// </summary>
        /// <param name="zq"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        void CarryOverMedicine(string zq, DateTime kssj, DateTime jssj);

        /// <summary>
        /// Tra cứu thuốc đã kết chuyển
        /// </summary>
        /// <param name="zq"></param>
        /// <param name="inputCode"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        IList<CarryOverMedicineVO> SelectCarryOverMedicineList(Pagination pagination, string zq, string inputCode);

        /// <summary>
        /// Trả về dạng bào chế thuốclist
        /// </summary>
        /// <returns></returns>
        List<MedicineFormulationVO> GetMedicineFormulationList();

        /// <summary>
        /// Loại thuốc (nhập xuất tồn）
        /// </summary>
        /// <returns></returns>
        List<MedicineCategoryVO> GetMedicineCategoryList2();


    }
}
