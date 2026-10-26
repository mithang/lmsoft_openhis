using System;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Data;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IRefundDmnService
    {
        List<MZJSInfo> GetFPHByKh(string kh, string startTime, string endTime, string fph);

        List<MZJS> GetMZJSByJsnm(int jsnm);
        List<GridViewMx> GetMz_xmRecordsByJsnm(int jsnm);
        List<GridViewMx> GetMz_ghxmRecordsByJsnm(int jsnm);
        List<GridViewMx> GetMz_cfmxRecordsByJsnm(int jsnm);
        decimal getSysl(int patid, int ghnm);
        // DataTable GetMzJszffs(int jsnm);
        List<OutpatientRegistNonAttendanceEntity> getGhjsByGhnm(int tmpGhnm);
        SysPatientNatureEntity GetBrxzByBrbh(string brxz);
        List<SysPatientChargeWaiverEntity> GetXT_brsfjm(string parmBrxz);
        List<OutpatientRegistEntity> getGhs(List<int> ghnmList);
        SysPatientBasicInfoEntity GetBrjbxxByPatid(int patid);
        int GetPrimaryKeyByTableName(string tableName);

        List<SysPatientChargeAlgorithmEntity> getMzActive();
        DataTable GetMzJszffsForJsnm(int jsnm);
        int getTFForFPH(int jsnm, string fph);
        List<OutpatientSettlementDetailEntity> getMzJsMx(string jslx, int jsnm);
        List<OutpatientSettlementPaymentModelEntity> GetJszffsByJsnm(int jsnm);
        OutpatientSettlementEntity GetMZJSFromMz_jsByJsnm(int jsnm);

        /// <summary>
        /// Chèn phần còn lại của hoàn phí mz_js mz_jsmx mz_jszffs mz_jsdl Thao tác CSDL
        /// </summary>
        /// <param name="js"></param>
        /// <param name="listMzJsMx"></param>
        /// <param name="jszffsArray"></param>
        /// <param name="jsdlList"></param>
        /// <param name="jsnmEx"></param>
        /// <returns></returns>
        bool InsertRemainFromReturns(OutpatientSettlementEntity js, List<OutpatientSettlementDetailEntity> listMzJsMx, List<OutpatientSettlementPaymentModelEntity> jszffsArray, List<OutpatientSettlementCategoryEntity> jsdlList);
        DataTable GetMz_cfmxRecords(int jsnm);

        /// <summary>
        /// Trả lại toàn bộ  mz_js mz_jsmx mz_ghth
        /// </summary>
        /// <param name="mzJs"></param>
        /// <param name="mzJsmxList"></param>
        /// <param name="ghjsDt"></param>
        /// <returns></returns>
        bool mzJsFullBack(OutpatientSettlementEntity mzJs, List<OutpatientSettlementDetailEntity> mzJsmxList, List<OutpatientRegistNonAttendanceEntity> ghjsDt);

        bool CheckFPH(string fph);


        #region GRS门诊退费

        /// <summary>
        /// Thông tin bệnh nhân hệ thống Dữ liệu phân trang
        /// </summary>
        /// <param name="pagination">Thông tin phân trang</param>
        /// <param name="blh">Số bệnh án</param>
        /// <param name="xm">Họ tên</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatAccInfoDto> GetBasicInfoSearchList(Pagination pagination, string blh, string mzh, string xm,string kssj,string jssj, string orgId, string usercode, string jiuzhenbiaozhi);

        /// <summary>
        /// Tra cứu thông tin bệnh nhân quản lý bệnh nhân
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatAccInfoDto> GetBasicInfoSearchListInRegister(Pagination pagination, string blh, string xm, string orgId,string zjh,string zjlx);

        /// <summary>
        /// Đăng ký ngoại trú bệnh nhân,Tra cứu thông tin bệnh nhân lớp nổi
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatAccInfoDto> GetBasicInfoSearchListInRegister(string keyword, string orgId);

        List<PatInfoQuery> Getjzjsxx(string type, string keyword, string grbh, string orgId);
        /// <summary>
        /// Lấy thông tin quyết toán theo số bệnh án
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<MZJSInfo> GetjsInfoByblh(string blh, string startTime, string endTime, string orgId);

        /// <summary>
        /// Tra cứu hạng mục ngoại trú theo mã nội bộ quyết toán
        /// </summary>
        /// <param name="jsnm">Mã nội bộ quyết toán</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        /// Chi tiết hạng mục ngoại trú
        List<GridViewMx> GetMz_xmDetailByJsnm(int jsnm, string orgId);


        /// <summary>
        /// Lấy thông tin hạng mục quyết toán theo mã nội bộ quyết toán
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<MZJS> GetMZJSByJsnmInAcc(int jsnm, string orgId);

        List<GridViewMx> GetGridViewMx(int jsnm, string orgId);

        /// <summary>
        /// Khi trả lại toàn bộ thì cập nhật số lần BHYT
        /// </summary>
        /// <param name="jzrqList"></param>
        /// <param name="IsReturnAll"></param>
        /// <param name="orgId"></param>
        /// <param name="patid"></param>
        void Minusybcs(DateTime jzrqList, bool IsReturnAll, string orgId, int patid);

        #endregion

        /// <summary>
        /// Hoàn phí ngoại trú Quyết toán Bản ghi chính Query
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        IList<OutPatientRefundableJsVO> RefundableJsQuery(string orgId, DateTime? kssj, DateTime? jssj, string mzh);
        /// <summary>
        /// Khi quyết toán Quý An hoàn phí thì tra cứu thông tin quyết toán
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        IList<OutPatientRefundableGuiAnJsVO> RefundableGuiAnJsQuery(string orgId, DateTime? kssj, DateTime? jssj, string mzh);
        
        /// <summary>
        /// Hoàn phí ngoại trú Query
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        IList<OutPatientRefundableVO> RefundableQuery(string orgId, int jsnm);

        /// <summary>
        /// Khám ngoại trú/Nhập kế hoạch nội trú Lọc tìm kiếm bệnh nhân
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <param name="usercode"></param>
        /// <param name="jiuzhenbiaozhi"></param>
        /// <returns></returns>
        List<OutpatAccInfoDto> GetBasicInfoSearchList(string keyword, string orgId, string usercode, string jiuzhenbiaozhi, string from="mz", string brzybzType = null);


		#region 重庆医保
	    IList<OutPatChongQingVO> RefundableChongQingQuery(string orgId, DateTime? kssj, DateTime? jssj, string mzh);
        #endregion
        List<OutpatAccInfoDto> GetZyMzYjjPatientSearch(string keyword, string orgId, string type);

    }
}
