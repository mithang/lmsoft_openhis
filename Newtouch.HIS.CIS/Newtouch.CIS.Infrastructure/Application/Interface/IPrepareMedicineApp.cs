using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Application.Interface
{
    public interface IPrepareMedicineApp
    {
        /// <summary>
        /// Theo lãi lỗ Loại Lấy nguyên nhân lãi lỗ
        /// </summary>
        /// <param name="sylx"></param>
        /// <returns></returns>
        List<SysMedicineProfitLossReasonEntity> GetLossProfitReasonListByType(string sylx);

        /// <summary>
        /// Tra cứu lãi lỗ Thuốc list
        /// </summary>
        /// <param name="inputCode">Từ khóa</param>
        /// <returns></returns>
        List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode);

        /// <summary>
        /// Gửi Báo hỏng báo thừa
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        string SubmitReportLossAndProfit(SysMedicineProfitLossEntity[] syxx);

        /// <summary>
        /// Báo hỏng báo thừa Lưu
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        //void SaveReportLossAndProfit(List<SysMedicineProfitLossEntity> profitLossEntityList);

        /// <summary>
        /// Báo hỏng báo thừa Tra cứu
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
        /// Giá bán buôn Tổng số tiền,Giá bán lẻ Tổng số tiền Tra cứu
        /// </summary>
        /// <param name="startTime">Thời gian bắt đầu</param>
        /// <param name="endTime">Thời gian kết thúc</param>
        /// <param name="syyy">nguyên nhân lãi lỗ</param>
        /// <param name="inputCode">Từ khóa</param>
        /// <param name="syqk">lãi lỗ tình hình</param>
        /// <returns></returns>
        LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk);
    }
}
