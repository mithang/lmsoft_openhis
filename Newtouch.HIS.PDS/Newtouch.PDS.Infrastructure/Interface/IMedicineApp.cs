using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Model;
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
        /// Truy vấn thuốc lãi lỗlist
        /// </summary>
        /// <param name="inputCode">Từ khóa</param>
        /// <returns></returns>
        List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode);

        /// <summary>
        /// Báo hỏng báo thừa Lưu
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        void SaveReportLossAndProfit(List<SysMedicineProfitLossEntity> profitLossEntityList);

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
        /// Lấy lần phát thuốc đơn tháng nội trú
        /// </summary>
        /// <returns></returns>
        MonthlySummaryDO GetFyCountVoByYfbm(List<FyCountVoByYfbm> val);
        
        /// <summary>
        /// Lấy lần y lệnh năm nay|Lần thuốc trong đơn
        /// </summary>
        /// <returns></returns>
        List<FyClassificationStatistics> GetFyCountBydl();
        /// <summary>
        /// Truy vấn chi tiết kiểm kê
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
