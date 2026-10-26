using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices
{
    public interface IPrepareMedicineDmnService
    {
        #region 报损报益
        /// <summary>
        /// Tra cứu thuốc chênh lệchlist
        /// </summary>
        /// <param name="inputCode">Từ khóa</param>
        /// <returns></returns>
        List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode);

        /// <summary>
        /// Tra cứu của thuốckcslvàjj
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="yxq"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        //void SelectKcslAndJj(string ypCode, string ph, string pc, DateTime? yxq, out int kcsl, out decimal jj);

        ///// <summary>
        ///// Thay đổi số lượng tồn kho
        ///// </summary>
        ///// <param name="ypCode"></param>
        ///// <param name="ph"></param>
        ///// <param name="pc"></param>
        ///// <param name="yxq"></param>
        //void UpdateStockQuantity(string ypCode, string pc, int sysl);



        /// <summary>
        /// Gửi báo hao hụt/thừa
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        string SubmitReportLossAndProfit(SysMedicineProfitLossEntity syxx);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        //void SaveReportLossAndProfit(SysMedicineProfitLossEntity entity, int kcsl, decimal jj);

        /// <summary>
        /// Tra cứu báo hao hụt/thừa
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        //IList<LossAndProditInfoVO> SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk);

        /// <summary>
        /// Tổng số tiền giá bán sỉ,Tra cứu tổng số tiền giá bán lẻ
        /// </summary>
        /// <param name="startTime">Thời gian bắt đầu</param>
        /// <param name="endTime">Thời gian kết thúc</param>
        /// <param name="syyy">Nguyên nhân chênh lệch</param>
        /// <param name="inputCode">Từ khóa</param>
        /// <param name="syqk">Tình hình chênh lệch</param>
        /// <returns></returns>
        //LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk);

        /// <summary>
        /// Báo hao hụt/thừa Lưu
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        //string SaveReportLossAndProfit(List<YpSyxxVo> profitLossEntityList);


        /// <summary>
        /// Tra cứu báo hao hụt/thừa
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        IList<LossAndProditInfoVO> SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk);

        LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk);
        
        #endregion

        #region 科室备药退回
        string PrepareMedicineReturnSubmit(OperatorModel user, string orgId, BythDjInfoDTO Djnr);
        List<DrugStockInfoVEntity> GetDrugAndStock(string yfcode, string keyWord, string organizeid);
        List<DrugStockInfoVEntity> GetApplyDrugAndStock(string ypcodestr,string yfbmstr, string organizeid);
        IList<PrepareMedicineReturnVO> GetPrepareMedicineReturnGridJson(Pagination pagination, string thzt, DateTime kssj, DateTime jssj, string OrganizeId);

        List<PrepareMedicineReturnMXEntity> QueryPrepareMedicine(string djId, string orgId);
        string UpdatePrepareMedicineReturn(PrapareMedicineReturnEntity entity);
        List<PrepareMedicineReturnVO> QueryPrepareMedicineReturnbyId(string djId, string orgId);
        List<PrepareMedicineReturnMXEntity> QueryPrepareMedicineReturnMXbyId(string djId, string orgId);
        string BydjQueryKykc(string ypbm, string pc, string ph, string yfbm, string orgId);
        string PrepareMedicineReturnback(string Djh, string orgId, OperatorModel user);
        string PrepareMedicineReturndelete(string ById, string OrganizeId, OperatorModel user);
        #endregion

        #region 药品使用查询
        IList<MedicineInfoVO> GetMedicineInfoListV2(Pagination pagination, MedicineInfoParam param, string orgId);
        #endregion
    }
}
