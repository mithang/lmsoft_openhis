using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using Newtouch.HIS.Domain.DO;

namespace Newtouch.HIS.Application.Interface
{
    public interface IMedicineApp
    {
        /// <summary>
        /// Lấy lý do lãi lỗ theo loại lãi lỗ
        /// </summary>
        /// <param name="sylx"></param>
        /// <returns></returns>
        List<SysMedicineProfitLossReasonEntity> GetLossProfitReasonListByType(string sylx);

        /// <summary>
        /// Tra cứu thuốc lãi lỗlist
        /// </summary>
        /// <param name="inputCode">Từ khóa</param>
        /// <returns></returns>
        List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode);

        /// <summary>
        /// Lưu báo hỏng báo thừa
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        void SaveReportLossAndProfit(List<SysMedicineProfitLossEntity> profitLossEntityList);

        /// <summary>
        /// Tra cứu báo hỏng báo thừa
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
        /// Tra cứu tổng tiền giá bán buôn, tổng tiền giá bán lẻ
        /// </summary>
        /// <param name="startTime">Thời gian bắt đầu</param>
        /// <param name="endTime">Thời gian kết thúc</param>
        /// <param name="syyy">Lý do lãi lỗ</param>
        /// <param name="inputCode">Từ khóa</param>
        /// <param name="syqk">Tình hình lãi lỗ</param>
        /// <returns></returns>
        LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk);

        /// <summary>
        /// Lấy số lần phát thuốc theo đơn thuốc tháng nội trú
        /// </summary>
        /// <returns></returns>
        MonthlySummaryDO GetFyCountVoByYfbm(List<FyCountVoByYfbm> val);
        
        /// <summary>
        /// Lấy số lần y lệnh năm nay|Lần thuốc trong đơn thuốc
        /// </summary>
        /// <returns></returns>
        List<FyClassificationStatistics> GetFyCountBydl();
        /// <summary>
        /// Tra cứu chi tiết kiểm kê
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        IList<InventoryQureyinfoVO> InventoryQureyInfo(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk);
    }
}
