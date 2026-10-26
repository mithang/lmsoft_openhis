using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.Medicine;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// thuốc
    /// </summary>
    public interface IMedicineDmnService
    {
        /// <summary>
        /// tra cứu thuốc lãi/lỗ list
        /// </summary>
        /// <param name="inputCode">từ khóa</param>
        /// <returns></returns>
        List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode);

        /// <summary>
        /// tra cứu thuốc của kcsl và jj
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="yxq"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        void SelectKcslAndJj(string ypCode, string ph, string pc, DateTime? yxq, out int kcsl, out decimal jj);

        ///// <summary>
        ///// Thay đổi số lượng tồn kho
        ///// </summary>
        ///// <param name="ypCode"></param>
        ///// <param name="ph"></param>
        ///// <param name="pc"></param>
        ///// <param name="yxq"></param>
        //void UpdateStockQuantity(string ypCode, string pc, int sysl);

        #region 报损报益

        /// <summary>
        /// lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        void SaveReportLossAndProfit(SysMedicineProfitLossEntity entity, int kcsl, decimal jj);

        /// <summary>
        /// tra cứu báo hỏng/báo thừa
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
        /// giá bán buôn tổng số tiền, giá bán lẻ tổng số tiền tra cứu
        /// </summary>
        /// <param name="startTime">thời gian bắt đầu</param>
        /// <param name="endTime">thời gian kết thúc</param>
        /// <param name="syyy">nguyên nhân lãi/lỗ</param>
        /// <param name="inputCode">từ khóa</param>
        /// <param name="syqk">tình hình lãi/lỗ</param>
        /// <returns></returns>
        LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk);

        /// <summary>
        /// báo hỏng báo thừa lưu
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        string SaveReportLossAndProfit(List<YpSyxxVo> profitLossEntityList);

        #endregion

        #region 调价盈亏
        /// <summary>
        /// điều chỉnh giá lãi/lỗ tra cứu
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
        /// tra cứu báo hỏng/báo thừa
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        IList<InventoryQureyinfoVO> GetInventoryQureyInfo(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk);

        #region 药品发药统计
        //phân loại thuốc tra cứu
        IList<MedicineClassificationVO> GetMedicineClassificationList(string keyword = null);
        #endregion
    }
}