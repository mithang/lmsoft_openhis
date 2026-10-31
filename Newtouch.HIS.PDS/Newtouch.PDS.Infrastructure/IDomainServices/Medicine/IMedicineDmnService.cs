using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.Medicine;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Thuốc
    /// </summary>
    public interface IMedicineDmnService
    {
        /// <summary>
        /// Truy vấn thuốc lãi lỗlist
        /// </summary>
        /// <param name="inputCode">Từ khóa</param>
        /// <returns></returns>
        List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode);

        /// <summary>
        /// Truy vấn của thuốckcslvàjj
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="yxq"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        void SelectKcslAndJj(string ypCode, string ph, string pc, DateTime? yxq, out int kcsl, out decimal jj);

        ///// <summary>
        ///// Thay đổi Số lượng tồn kho
        ///// </summary>
        ///// <param name="ypCode"></param>
        ///// <param name="ph"></param>
        ///// <param name="pc"></param>
        ///// <param name="yxq"></param>
        //void UpdateStockQuantity(string ypCode, string pc, int sysl);

        #region Báo hỏng báo thừa

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        void SaveReportLossAndProfit(SysMedicineProfitLossEntity entity, int kcsl, decimal jj);

        /// <summary>
        /// Truy vấn báo hỏng báo thừa
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        IList<LossAndProditInfoVO> SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk);

        /// <summary>
        /// Tổng số tiền giá bán buôn,Truy vấn tổng số tiền giá bán lẻ
        /// </summary>
        /// <param name="startTime">Thời gian bắt đầu</param>
        /// <param name="endTime">Thời gian kết thúc</param>
        /// <param name="syyy">Lý do lãi lỗ</param>
        /// <param name="inputCode">Từ khóa</param>
        /// <param name="syqk">Tình hình lãi lỗ</param>
        /// <returns></returns>
        LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk);

        /// <summary>
        /// Báo hỏng báo thừa Lưu
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        string SaveReportLossAndProfit(List<YpSyxxVo> profitLossEntityList);

        #endregion

        #region Lãi lỗ điều chỉnh giá
        /// <summary>
        /// Truy vấn lãi lỗ điều chỉnh giá
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="Srm"></param>
        /// <param name="Yfykmc"></param>
        /// <param name="Lkc"></param>
        /// <returns></returns>
        IList<SysMedicinePriceAdjustmentProfitLossVO> SelectPriceAdjustmentProfitLossList(Pagination pagination, DateTime? startTime, DateTime? endTime, string Srm, string yfbmCode, bool Lkc, string allUseableYfbmCodes = null);

        #endregion
        /// <summary>
        /// Truy vấn báo hỏng báo thừa
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        IList<InventoryQureyinfoVO> GetInventoryQureyInfo(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk);

        #region Thống kê phát thuốc
        //Truy vấn phân loại thuốc
        IList<MedicineClassificationVO> GetMedicineClassificationList(string keyword = null);
        #endregion
    }
}