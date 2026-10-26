using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using System;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBookkeepInHosDmnService
    {
        /// <summary>
        /// Tra cứu mẫu hạng mục thu phí
        /// </summary>
        /// <param name="ks"></param>
        /// <returns></returns>
        List<ChargeItemTemplateVO> GetChargeTemplate(string ks, string orgid);


        /// <summary>
        /// Lấy nội dung mẫu theo mã hạng mục thu phí
        /// </summary>
        /// <param name="sfxmbh"></param>
        /// <returns></returns>
        List<TemplateContentVO> GetChargeItemContent(string sfmbbh, string orgId);

        /// <summary>
        /// Theo số nhập viện Tra cứu Kế hoạch ghi sổ
        /// </summary>
        /// <param name="zyhList"></param>
        /// <param name="zxrq"></param>
        IList<HospAccountingPlanVO> BeenAccountingPlanQuery(string orgId, string zyh);

        /// <summary>
        /// Lưu kế hoạch ghi sổ
        /// </summary>
        /// <param name="jzjh"></param>
        /// <param name="jhmxEntityList"></param>
        /// <param name="ysEntityList"></param>
        void SaveAccountingPlan(HospAccountingPlanEntity jzjh, List<HospAccountingPlanDetailEntity> jhmxEntityList, List<string> updateToStopJzjhxmIdList);

        /// <summary>
        /// Lấy danh sách bệnh nhân của cơ sở(Bác sĩ hiện tại,Tồn tại kế hoạch ghi sổ chưa thực hiện)
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<PendingExecutionPatientVO> GetPendingExecutionPatientVOList(string orgId,string kscode);

        /// <summary>
        /// Lấy danh sách bệnh nhân ngoại trú của cơ sở(Bác sĩ hiện tại,Tồn tại kế hoạch ghi sổ chưa thực hiện)
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<PendingExecutionPatientVO> GetOutPendingExecutionPatientVOList(string orgId,string kscode);

        /// <summary>
        /// Kế hoạch ghi sổ chờ thực hiện Tra cứu
        /// </summary>
        /// <param name="zyhList"></param>
        /// <param name="zxrq"></param>
        IList<HospAccountingPlanWaitingExeVO> WaitingAccountingPlanQuery(string orgId, string zyhStr, string gh, string zxks, bool isRehabDoctor);

        /// <summary>
        /// Kế hoạch ghi sổ ngoại trú chờ thực hiện Tra cứu(Chỉ hạng mục thu phí,Không bao gồm thuốc)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="Str"></param>
        /// <param name="zxrq"></param>
        /// <param name="ysCode"></param>
        /// <returns></returns>
        IList<HospAccountingPlanWaitingExeVO> WaitingOutAccountingPlanQuery(string orgId, string Str,string gh, string zxks, bool isRehabDoctor);

        /// <summary>
        /// Thực hiện kế hoạch ghi sổ,Tạo tính phí
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zxItem"></param>
        /// <param name="curOprCode"></param>
        /// <param name="curOprDeptCode"></param>
        /// <param name="zxrq">Ngày điều trị</param>
        void ExecuteAccountingPlan(string orgId, IList<zxGirdDto> zxItem, string curOprCode, string curOprDeptCode, DateTime zxrq, out Dictionary<string, string> jfbStr);

        /// <summary>
        /// Thực hiện kế hoạch ghi sổ,Tạo tính phí
        /// </summary>
        /// <param name="jzjhmxIdList">jzjhmxIdDanh sách</param>
        void ExecuteOutAccountingPlan(string orgId, IList<zxGirdDto> zxItem, string curOprCode, string curOprDeptCode, DateTime zxrq);
        /// <summary>
        /// Dừng kế hoạch ghi sổ
        /// </summary>
        /// <param name="jzjhmxIdList">jzjhmxIdDanh sách</param>
        void StopAccountingPlan(IList<string> mxIdList, DateTime stopDate, string curOprCode, string curOprDeptCode);

        /// <summary>
        /// Hủy dừng trước
        /// </summary>
        /// <param name="jzjhmxIdList">jzjhmxIdDanh sách</param>
        void CancelPreStopAccountingPlan(IList<string> mxIdList, string curOprCode, string curOprDeptCode);

        /// <summary>
        /// Thực hiện tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zxzt"></param>
        /// <param name="orgId"></param>
        /// <param name="zls"></param>
        /// <param name="cxzt"></param>
        /// <param name="jzjhmxId"></param>
        /// <returns></returns>
        List<AccountingExecuteVO> SelectAccountingExecuteList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId
            , string zls, string cxzt, string jzjhmxId, string sfzt);

        /// <summary>
        /// Tra cứu kế hoạch ghi sổ
        /// </summary>
        List<AccountingPlanVO> SelectAccountingPlanList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj
            , int? zxzt, string orgId, int? yzxz);

        /// <summary>
        /// Tra cứu thực hiện kế hoạch ngoại trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zxzt"></param>
        /// <param name="orgId"></param>
        /// <param name="zls"></param>
        /// <param name="cxzt"></param>
        /// <param name="jzjhmxId"></param>
        /// <param name="sfzt"></param>
        /// <returns></returns>
        List<AccountingExecuteVO> SelectOutAccountingExecuteList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId
          , string zls, string cxzt, string jzjhmxId, string sfzt);
        /// <summary>
        /// Tra cứu chi tiết kế hoạch ghi sổ
        /// </summary>
        /// <param name="jzjhmxId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<AccoutingPlanDetailVO> GetFormPlan(Pagination pagination, string jzjhmxId, string orgId, string from);

        /// <summary>
        /// Tra cứu chi tiết thực hiện kế hoạch ghi sổ
        /// </summary>
        /// <param name="jzjhmxId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        AccoutingPlanDetailVO GetFormPlan(string zxId, string orgId, string from);
        /// <summary>
        /// Theo số nhập viện Lấy tất cả tính phí(Hạng mục/Thuốc)Tổng số tiền
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        decimal? GetJfZjeByZyh(string zyh, string orgId);

        /// <summary>
        /// Lưu tính phí nội trú
        /// </summary>
        void SaveInpatientAccounting(string zyh, IList<InpatientAccountingItemDto> xmList, string orgId, string curUserCode);

        /// <summary>
        /// Hủy thực hiện nhập kho
        /// </summary>
        /// <param name="jzjhmxId"></param>
        /// <param name="zxbh"></param>
        /// <param name="orgId"></param>
        /// <param name="from"></param>
        void RevokeExec(string jzjhmxId, string zxbh, string orgId, string from);

        /// <summary>
        /// Thay đổi kỹ thuật viên điều trị thực hiện và thời gian thực hiện
        /// </summary>
        /// <param name="jzjhmxId"></param>
        /// <param name="zxbh"></param>
        /// <param name="orgId"></param>
        /// <param name="from"></param>
        void UpdateExec(cxzxGirdDto zxItem, string from, string orgId);

        #region 住院记账，和门诊记账同个页面版本

        /// <summary>
        /// Cập nhật kế hoạch nội trú Số lần còn lại,Đồng thời dừng các kế hoạch cần dừng
        /// </summary>
        /// <param name="db"></param>
        /// <param name="orgId"></param>
        /// <param name="jzjhmxIdList"></param>
        void updateHospitaljzjh(string orgId, IList<string> jzjhmxIdList);

        /// <summary>
        /// Xác định có cần dừng kế hoạch hay không
        /// </summary>
        /// <param name="jzjhmxId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool StopOutpatientjzjh(string jzjhmxId, string orgId);

        /// <summary>
        /// Sau khi thực hiện xong hạng mục phục hồi chức năng thì đồng bộ sang CSDL trung gian Tần Hoàng Đảo
        /// </summary>
        /// <param name="jfbStr"></param>
        /// <param name="orgId"></param>
        void SyncInpatientToInterfaceBasic(Dictionary<string, string> jfbStr, string orgId, DateTime? zxrq, string rygh);
        #endregion

        #region 费用结算 出院结算

        /// <summary>
        /// Lấy quyết toán giữa chừng lần cuối Ngày kết thúc quyết toán
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        DateTime? GetLastValidMidwaySettTime(string zyh, string orgId);

        DateTime? GetLastValidSettTime(string zyh, string orgId);

        /// <summary>
        /// Theo số nhập viện Lấy chi tiết chưa quyết toán
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<HosFeeItemInfoDto> GetFeeItemPaginationList(Pagination pagination, string zyh, string orgId, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Quyết toán giữa chừng
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="jsItemList"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        void MidwaySettlement(string zyh, string orgId, DateTime startTime, DateTime endTime, int? expectedCount = null);

        /// <summary>
        /// Hủy quyết toán giữa chừng lần cuối
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        void CancelTheLastMidwaySett(string zyh,string zffs1, string orgId, out decimal refoundmoney);

        /// <summary>
        /// Theo Quyết toán giữa chừng Quyết toán xuất viện
        /// Quyết toán giữa chừng đã Kết thúc
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        void DischargeSettlement(string zyh, string orgId, DateTime cyrq);

        /// <summary>
        /// Theo số nhập viện Lấy trong khoảng thời gian Chi tiết tính phí thuốc hạng mục(Xét hoàn phí)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<HosFeeItemInfoDto> GetChargeItemPaginationList(Pagination pagination, string zyh, string orgId, DateTime startTime, DateTime endTime);

        #endregion

        #region 住院记账业务
        string getzfxzById(string sfxmCode, string orgId);

        /// <summary>
        /// Lưu ghi sổ nội trú
        /// </summary>
        string SaveFee(List<HospItemBillingEntity> xmjfbEntitylist, List<HospDrugBillingEntity> ypjfbEntitylist);
        List<HospItemFeeVO> Verification(string xmCodes, string orgId, string ks);

        #endregion
    }
}
