using System.Collections;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.DrugStorage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.Stock;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Ứng dụng tồn kho
    /// </summary>
    public interface IDrugStorageApp
    {
        /// <summary>
        /// Truy vấn thuốc của bộ phận hiện tại(Nhập kho)
        /// </summary>
        /// <param name="keyword">ypmc,py,spm,ypCode</param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        List<DepartmentMedicineVO> SelectDepartmentMedicineList(string keyword, string yfbmCode, string organizeId);
        string PrepareMedicine(BYDjInfoDTO yplist, string organizeId,string yhgh);
        string PrepareMedicineReturn(BythDjInfoDTO yplist, string organizeId, string yhgh);
        /// <summary>
        /// Thông tin hóa đơn thuốc
        /// </summary>
        /// <param name="fph">fph</param>
        /// <returns></returns>
        List<MedicineInvoiceInfoVO> SelectMedicineListByFPH(string fph);

        /// <summary>
        /// Truy vấn thuốc của bộ phận hiện tại(Xuất kho)
        /// </summary>
        /// <param name="keyword">ypmc,py,spm,ypCode</param>
        /// <param name="fph"></param>
        /// <param name="gyscode"></param>
        /// <returns></returns>
        List<DepartmentMedicineVO> SelectDepartmentMedicineList2(string keyword, string fph, string gyscode);

        /// <summary>
        /// Nhập kho
        /// </summary>
        /// <param name="ioReceiptEntity"></param>
        /// <param name="ioReceiptDetailEntityList"></param>
        void SaveInStorageInfo(SysMedicineStorageIOReceiptEntity ioReceiptEntity, List<SysMedicineStorageIOReceiptDetailEntity> ioReceiptDetailEntityList);

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
        /// Kết chuyển
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        void CarryOverMedicine(string yfbmCode, string organizeId);

        /// <summary>
        /// Truy vấn tồn kho
        /// </summary>
        /// <param name="yplist">Thông tin thuốc</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        StockQueryResponseDTO StockQuery(List<DrugInfo> yplist, string organizeId);

        /// <summary>
        /// Gửi báo hỏng báo thừa
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        string SubmitReportLossAndProfit(SysMedicineProfitLossEntity[] syxx);
        string WithdrawalPreparation(string Djh, string organizeId, string yhgh,string shzt);
        string WithdrawalPreparationReturn(string Djh, string organizeId, string yhgh, string thzt);
    }
}
