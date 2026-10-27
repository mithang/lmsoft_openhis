using NewtouchHIS.Lib.Base.Model;
using Newtouch.Sett.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newtouch.Sett.Domain
{
    // Service/repo interfaces for HospitalizationManage, names kept from the
    // Framework Sett layer. Signatures follow the ported controllers' call sites
    // (source interfaces where they match; vague list results use object).

    public interface ISysConfigRepo
    {
        SysConfigEntity GetByCode(string code, string orgId);
        string GetValueByCode(string code, string orgId);
        int GetIntValueByCode(string code, string orgId, int defaultValue = 0);
        bool? GetBoolValueByCode(string code, string orgId, bool? defaultValue = null);
    }

    public interface ISysStaffRepo
    {
        string GetNameByGh(string gh, string orgId);
    }

    public interface ISysUserDmnService
    {
        bool CheckStaffIsBelongDuty(string staffId, string duty);
    }

    public interface IMoneyUpperLimitReminderDmnService
    {
        void GetStaffjfjeInfo(string orgId, string gh, string ks, out decimal jfjesx, out decimal yjfzje);
    }

    public interface ISyncTreatmentServiceRecordDmnService
    {
        object GetList(string organizeId, int? clzt = null, string mzh = null, string zyh = null, string zlsgh = null);
    }

    public interface IRefundDmnService
    {
        object GetBasicInfoSearchListInRegister(string keyword, string orgId);
        object GetBasicInfoSearchList(string keyword, string orgId, string userCode, string p4, string from, string brzybzType);
    }

    public interface IPatientBasicInfoDmnService
    {
        List<SysHosBasicInfoVO> GetSysBasicByZHY(string kh, string zyh, string orgid);
        DateTime? IFCQRQISJZSJ(string zyh, string orgId);
    }

    public interface IHospSettApp
    {
        object GetPatHospStatusDetail(string zyh, string kh);
        HosSettPatClassifyChargePreviewDto GetHospSettPatClassifyChargePreview(string zyh, DateTime expectedcyrq, decimal jsje);
        IList<HospFeeClassifyWithDLBO> GetHospFeeClassifyWithDLBOList(string zyh);
        SettSaveSuccessResultDto SaveSett(string zyh, DateTime expectedcyrq, string fph, decimal expectedyjjzhye, decimal expectedjsje, decimal expectedzhaoling, string xjzfListStr, decimal shishoukuan);
    }

    public interface IHospSettCancelApp
    {
        object GetPatHospStatusDetail(string zyh, string kh);
        void DoCancel(string zyh, int expectedjsnm, string cancelReason);
    }

    public interface IHospSettDmnService
    {
        object GridInPatientQueryGridJson(Pagination pagination, HospSettQueryReq req, string orgId);
        object GetPaginationSettlementList(Pagination pagination, string orgId, string keyword, string fph, DateTime? jsksrq, DateTime? jsjsrq);
        object GetPaginationZFSettlementList(Pagination pagination, string orgId, string keyword, DateTime? jsksrq, DateTime? jsjsrq);
        object SettlementDetailsQuery(string orgId, int jsnm);
        object SettlementDetailsItemsQuery(Pagination pagination, string zyh, string orgId, string dlCode, string jsnms, string mc);
        void UpdateSettInvoiceNo(string orgId, int jsnm, string fph);
    }

    public interface IBookkeepInpatientApp
    {
        HosAccountingPatStatusDetailDto GetAccountingStatusDetail(string zyh);
        void SubmitAccountingPlan(string zyh, IList<InpatientAccountingPlanItemDto> xmList);
        IList<HospAccountingPlanWaitingExeVO> WaitingAccountingPlanQuery(IList<string> zyhList, string from = null);
        void ExecuteAccountingPlan(IList<zxGirdDto> zxItem, DateTime? zxrq, string from = null);
        void CancelPreStopAccountingPlan(string jzjhmxIdStr);
        InpatientHosAccountingPatStatusDetailDto GetInpatientAccountingStatusDetail(string zyh);
    }

    public interface IBookkeepInHosDmnService
    {
        DateTime? GetLastValidMidwaySettTime(string zyh, string orgId);
        object SelectAccountingExecuteList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId, string zls, string cxzt, string jzjhmxId, string sfzt);
        object SelectOutAccountingExecuteList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId, string zls, string cxzt, string jzjhmxId, string sfzt);
        object SelectAccountingPlanList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId, int? yzxz);
        object GetFormPlan(string keyValue, string orgId, string from);
        object GetFormPlan(Pagination pagination, string jzjhmxId, string orgId, string from);
        void RevokeExec(string jzjhmxId, string zxbh, string orgId, string from);
        void UpdateExec(cxzxGirdDto zxItem, string from, string orgId);
        void StopAccountingPlan(List<string> mxIdList, DateTime stopDate, string deptCode, string rygh);
        void SaveInpatientAccounting(string zyh, IList<InpatientAccountingItemDto> xmList, string orgId, string userCode);
        string getzfxzById(string sfxmcode, string orgId);
        IList<VerificationVO> Verification(string codes, string orgId, string yfdm);
        string SaveFee(List<HospItemBillingEntity> xmjfbList, List<HospDrugBillingEntity> ypjfbList);
        object GetChargeTemplate(string ks, string orgId);
        object GetChargeItemContent(string sfmbbh, string orgId);
        IList<UnSettedFeeItemVO> GetChargeItemPaginationList(Pagination pagination, string zyh, string orgId, DateTime startTime, DateTime endTime);
        void MidwaySettlement(string zyh, string orgId, DateTime startTime, DateTime endTime, int? expectedCount);
        void DischargeSettlement(string zyh, string orgId, DateTime cyrq);
        void CancelTheLastMidwaySett(string zyh, string zffs1, string orgId, out decimal refoundmoney);
    }

    public interface IOutPatientDmnService
    {
        object SelectAccountingPlanList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId, int? zsftx, int? sycstx, string sfzt);
        void overAccountingPlan(string mxId, string orgId, string rygh);
    }

    public interface IOutPatientApp
    {
        void SaveoutpatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> xmList, string orgId);
    }

    public interface IOutPatChargeApp
    {
        List<ChargeItemDetailVO> GetYpItemInfo(string keyword);
    }

    public interface ISysPatiChargeAddApp
    {
    }

    public interface ISysPatiChargeWaiverApp
    {
    }

    public interface IHospItemFeeApp
    {
        void SubmitForm(HospItemBillingEntity entity, int? keyValue);
    }

    public interface IHospMedicinFeeApp
    {
        void SubmitForm(HospDrugBillingEntity entity, int? keyValue);
    }

    public interface IHosPatAccDmnService
    {
    }

    public interface IAccountManageApp
    {
        HosPatAccInfoDto GetHosPatInfo(string zyh);
        string GetFinRepSJPZH();
        bool PayDepositPost(DeposDto dto, out string szId);
    }

    public interface IItemDmnService
    {
        IList<SysItemsDetailVO> GetItemsDetailListByOrgIdAndItemCode(string orgId, string itemCode);
    }

    public interface ISysChargeTemplateRepo
    {
    }

    public interface ISysChargeTemplateItemMappRepo : IRepositoryBase<SysChargeTemplateItemMappEntity>
    {
        IList<MbmxsfxmVO> getmbmx(string sfmb, string orgId);
    }

    public interface IHospPatientBasicInfoRepo : IRepositoryBase<HospPatientBasicInfoEntity>
    {
        new HospPatientBasicInfoEntity FindEntity(Expression<Func<HospPatientBasicInfoEntity, bool>> predicate);
    }

    public interface IHospItemBillingRepo : IRepositoryBase<HospItemBillingEntity>
    {
        IList<HospItemBillingEntity> GetItemFeeEntityListByTime(string zyh, string orgId, DateTime startTime, DateTime endTime);
        void Updatezyaddfee(string orgId, string jfbhs, string yfbm, string userCode);
    }

    public interface IHospDrugBillingRepo : IRepositoryBase<HospDrugBillingEntity>
    {
        void ExecPartialSettleFeeDetail(string zyh, string jsnm, string czlx);
        void Updatezy_brxxexpand(string orgId, string zyh);
        void Updatezyaddfee(string orgId, decimal sl, string yfbm, string ypdm);
        void Updatezy_wzkcReturn(string orgId, string jfbbhs, string userCode);
        void Updatezyyz_wzkcReturn(List<HospItemBillingEntity> entityList, string orgId, string userCode);
        int getHckcWith(string orgId);
    }

    public interface IHospSettlementRepo : IRepositoryBase<HospSettlementEntity>
    {
        List<HospSettlementEntity> GetValidList(string zyh, string orgId);
    }

    public interface IHospAccountingPlanDetailRepo : IRepositoryBase<HospAccountingPlanDetailEntity>
    {
    }

    public interface IHospMultiDiagnosisRepo : IRepositoryBase<HospMultiDiagnosisEntity>
    {
        List<HospMultiDiagnosisEntity> SelectData(string zyh, string organizeId);
    }

    public interface ICqybSett05Repo : IRepositoryBase<CqybSett05Entity>
    {
        new CqybSett05Entity FindEntity(Expression<Func<CqybSett05Entity, bool>> predicate);
    }

    public interface IHospSettlementGAYBZYMXXRFeeRepo : IRepositoryBase<HospSettlementGAYBZYMXXRFeeEntity>
    {
        new void Insert(HospSettlementGAYBZYMXXRFeeEntity entity);
    }

    public interface IHospSimulateSettlementGAYBFeeRepo : IRepositoryBase<HospSimulateSettlementGAYBFeeEntity>
    {
        new void Insert(HospSimulateSettlementGAYBFeeEntity entity);
    }

    public interface IHospFeeDmnService
    {
        void SyncPatFee(string orgId, string zyh, int lx);
        IList<HospItemFeeDetailVO> GetItemFeeDetailVOList(string zyh, string orgId);
        IList<HospItemFeeDetailVO> GetMedicinFeeDetailVOList(string zyh, string orgId);
    }

    public interface IDischargeSettleApp
    {
        object GetInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx, string ver);
        object GetPartialInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx, string ver);
        object GetCancelSettleStatusDetail(string zyh, string sfz, string kh, string cardType);
        object GetZyUploadDetail(string zyh, string jslx);
        object GetInpatientSettleJe(string zyh, string sczt, object p3, DateTime jssj, string xmmc);
        void SavePartialSettle(string zyh, DateTime startTime, DateTime endTime, string fph, InpatientSettFeeRelatedDTO feeRelated, CQZyjs05Dto ybfeeRelated, string outTradeNo, string jslx, out int jsnm);
        void DoCancel(string zyh, int expectedjsnm, string cancelReason, string cancelyblsh, out string outTradeNo, out decimal refundAmount);
    }

    public interface IDischargeSettleDmnService
    {
        object GetUploadFeeDetails(Pagination pagination, string zyh, string orgId, string userName, out decimal ybzje);
        object GetCQUploadFeeDetails(Pagination pagination, string zyh, string orgId, string userCode, out decimal ybzje, out decimal zfzje);
        object GetCQAlreadyUploadFeeDetails(string mzzyh, string orgId);
        object Getgjybdm(string mzzyh, string cfnms, string cflx, string orgId);
        CQAlreadyUploadFeeDetailsVO GetCQAlreadyUploadFeeDetailsV2(string mzzyh, string orgId, DateTime jssj);
        string ValialUploadData(string zyh, string orgId, DateTime jssj);
        string ValialPartialUploadData(string zyh, string orgId, DateTime jssj);
        string ValialUploadDataShyb(string zyh, string orgId, DateTime jssj);
        object GetCQLastJsnm(string zyh, string orgId);
        object GetCQLastLsh(int jsnm, string orgId);
        CancelSettInfoDto GetCancelSettInfo(int jsnm, string orgId);
        string GetZyhByGrbm(string xnhgrbm, string sfjs, string orgId);
        object GetHospGroupFeeVOList(Pagination pagination, string zyh, string orgId, string dlCode);
        object GetHospGroupFeeVOData(Pagination pagination, string zyh, string orgId, string keyword);
        object GetDetailedQuery(Pagination pagination, string zyh, string orgId, string sfxmCode, string zzfbz, decimal? dj);
        object GetFyzdDetailedQuery(Pagination pagination, string zyh, string orgId, string sfxmCode);
        object GetHospXmYpFeeVOList(Pagination pagination, string zyh, string orgId, string sczt, string xmmc, object p6, DateTime jssj, string isnewyb);
        HospPreInfoDto GetHospPreInfo(string zyh, string orgId, DateTime jzsj);
        object GetInpatientSettYbPatInfo(string zyh, string orgId);
        object GetCQInpatientSettYbPatInfo(string zyh, string orgId);
        void SaveSett2303(CqybSett2303Entity entity);
        object sweep_expired_approvals(string cfmxlshstr, string orgId, string zyh);
        void updatespbz(string cfhstr, string orgId, string userCode);
        int CountLisIncompletezy(string orgId, string zyh);
    }

    public interface IInpatientRefundApp
    {
        InpatientSettPatStatusDetailDto InpatientRefundQuery(string zyh, DateTime? kssj, DateTime? jssj, string ks, string xmlb, string xmmc);
    }

    public interface IInpatientRefundDmnService
    {
        string SaveRefund(List<HospItemBillingEntity> xmjfbList, List<HospDrugBillingEntity> ypjfbList, string zyh, string orgId);
    }

    public interface IHospitalRefundApp
    {
        string GetPatInfoByZyh(string zyh);
        string GetPZInfo();
        List<GridZyXmYpjfb> GetZyXmjfbGroup(string zyh, string zxks, string xmmc, string yrh);
        List<GridZyXmYpjfb> GetZyYpjfbGroup(string zyh, string zxks, string xmmc, string yrh);
        List<gridZyXmYpItemsjfb> GetZyYpjfbList(string zyh, string xsebh, string yp, string createTime);
        List<gridZyXmYpItemsjfb> GetZyXmjfbList(string zyh, string xsebh, string sfxm, string createTime);
        string SaveReturnZyyp(Dictionary<string, decimal> returnDict);
        string SaveReturnZyxm(Dictionary<string, decimal> returnDict);
    }

    public interface IBookingDmnService
    {
    }

    // Only used by the stubbed GuiAn cancel action; kept so the controller ctor stays faithful.
    public interface IInpatientApp
    {
    }

    public interface IPayApp
    {
        int TradeRefund(string outTradeNo, decimal refundAmount, string refundReason, string outRequestNo, out string errorMsg);
    }
}
