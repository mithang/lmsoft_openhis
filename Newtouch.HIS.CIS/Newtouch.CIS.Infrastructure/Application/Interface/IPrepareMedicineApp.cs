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
        /// Theolãi lỗLoạiLấynguyên nhân lãi lỗ
        /// </summary>
        /// <param name="sylx"></param>
        /// <returns></returns>
        List<SysMedicineProfitLossReasonEntity> GetLossProfitReasonListByType(string sylx);

        /// <summary>
        /// Tra cứulãi lỗThuốclist
        /// </summary>
        /// <param name="inputCode">Từ khóa</param>
        /// <returns></returns>
        List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode);

        /// <summary>
        /// GửiBáo hỏngbáo thừa
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        string SubmitReportLossAndProfit(SysMedicineProfitLossEntity[] syxx);

        /// <summary>
        /// Báo hỏngbáo thừa Lưu
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        //void SaveReportLossAndProfit(List<SysMedicineProfitLossEntity> profitLossEntityList);

        /// <summary>
        /// Báo hỏngbáo thừaTra cứu
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
        /// Giá bán buônTổng số tiền,Giá bán lẻTổng số tiềnTra cứu
        /// </summary>
        /// <param name="startTime">Thời gian bắt đầu</param>
        /// <param name="endTime">Thời gian kết thúc</param>
        /// <param name="syyy">nguyên nhân lãi lỗ</param>
        /// <param name="inputCode">Từ khóa</param>
        /// <param name="syqk">lãi lỗtình hình</param>
        /// <returns></returns>
        LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk);
    }
}
