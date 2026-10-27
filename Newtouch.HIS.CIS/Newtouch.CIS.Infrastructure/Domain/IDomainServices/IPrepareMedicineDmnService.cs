using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model;
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
        #region Báo hỏngbáolợi
        /// <summary>
        /// Tra cứulãi lỗThuốclist
        /// </summary>
        /// <param name="inputCode">Từ khóa</param>
        /// <returns></returns>
        List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode);

        /// <summary>
        /// Tra cứuThuốccủakcslvàjj
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="yxq"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        //void SelectKcslAndJj(string ypCode, string ph, string pc, DateTime? yxq, out int kcsl, out decimal jj);

        ///// <summary>
        ///// bảnghơnTồn khoSố lượng
        ///// </summary>
        ///// <param name="ypCode"></param>
        ///// <param name="ph"></param>
        ///// <param name="pc"></param>
        ///// <param name="yxq"></param>
        //void UpdateStockQuantity(string ypCode, string pc, int sysl);



        /// <summary>
        /// GửiBáo hỏngbáolợi
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
        /// Báo hỏngbáo thừaTra cứu
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
        /// Giá bán buônTổng số tiền、Giá bán lẻTổng số tiềnTra cứu
        /// </summary>
        /// <param name="startTime">Thời gian bắt đầu</param>
        /// <param name="endTime">Thời gian kết thúc</param>
        /// <param name="syyy">nguyên nhân lãi lỗ</param>
        /// <param name="inputCode">Từ khóa</param>
        /// <param name="syqk">lãi lỗtình hình</param>
        /// <returns></returns>
        //LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk);

        /// <summary>
        /// Báo hỏngbáo thừa Lưu
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        //string SaveReportLossAndProfit(List<YpSyxxVo> profitLossEntityList);


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

        LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk);
        
        #endregion

        #region Khoachuẩn bị thuốcTrả lại
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

        #region Thuốcsử dụngTra cứu
        IList<MedicineInfoVO> GetMedicineInfoListV2(Pagination pagination, MedicineInfoParam param, string orgId);
        #endregion
    }
}
