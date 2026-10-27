using System;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Sett.Repository;
using SqlSugar;
using System.Linq.Expressions;
using Newtouch.Sett.Domain.DTO;
using Newtouch.Sett.Domain.Entity;

namespace Newtouch.Sett.Domain
{
    public interface ICancelOutpatientRegDmnService
    {
        object SaveCancelRegister(object p0, object p1);
        object SelectRegisterlist(object p0, object p1, object p2, object p3, object p4, object p5);
    }

    public interface ICommonDmnService
    {
        string Gzyb_GetJyyzmByJylsh(object p0, object p1);
            object GetWardbyDept(object p0, object p1, object p2);
}

    public interface ICqybMedicalInPut02Repo
    {
        object SaveCqybMedicalS02InReg(object p0, object p1);
    }

    public interface ICqybMedicalReg02Repo
    {
        CqybMedicalReg02Entity FindEntity(System.Linq.Expressions.Expression<System.Func<CqybMedicalReg02Entity, bool>> predicate);
        object SaveCqybMedicalReg(object p0, object p1);
    }

public interface ICqybSett05Repo : IRepositoryBase<CqybSett05Entity> {
        object SaveCqybSett(object p0, object p1);
    }

    public interface ICqybSett23Repo
    {
        object SaveCqybS23Sett(object p0, object p1);
    }

    public interface ICqybUpdateMedicalInput03Repo
    {
        ICqybUpdateMedicalInput03Repo_FindEntityDto FindEntity(object p0);
        object SaveCqybUpdateMedicalS03InReg(object p0, object p1);
    }

    public interface ICqybUploadPresList04Repo
    {
        object SaveCqybUploadPresList(object p0, object p1, object p2, object p3);
    }

    public interface IGuiAnOutpatientXnhApp
    {
        object CancelSett(object p0, object p1, object p2);
        string Sett(object p0, object p1, out object p2);
        string Sett(object p0, object p1, object p2, object p3, out S25ResponseDTO p4);
        string Sett(object p0, object p1, object p2, out S25ResponseDTO p3, out string p4);
        string SimulationSettWholeProcess(object p0, object p1, object p2, out S24ResponseDTO p3);
        string XnhTuiFeiProcess(object p0, object p1, object p2, object p3, object p4, object p5, out TFS21RequestDTO p6);
    }

    public interface IMzghbookRepo
    {
        object FZsjTB(object p0, object p1, object p2);
        object SigninAppointment(object p0, object p1);
        object UpdateMzGhAppointment(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9);
    }

    public interface IOutBookDateRepo
    {
        object UpdateDate(object p0, object p1, object p2, object p3, object p4, object p5, object p6);
    }

    public interface IOutBookDmnService
    {
        object CancalBook(object p0);
        object DeleteArrange(object p0, object p1);
        object GetMzpbZlxmzh(object p0, object p1, object p2);
        List<IOutBookScheduleRepo_GetPagintionListDto> GetPagintionList(object p0, object p1, object p2);
        object GetStaffList(object p0);
        object InsertArrange(object p0, object p1, object p2, object p3, object p4);
        object InsertghpbTime(object p0, object p1, object p2, object p3, object p4, object p5);
        object PatBookGh(object p0, object p1, object p2, object p3, object p4, object p5);
        object SaveDatapb(object p0, object p1);
        object SaveDatatzcz(object p0, object p1, object p2, object p3);
        object UpdateArrange(object p0, object p1, object p2, object p3, object p4);
        IOutBookDmnService_getArrangeInfoDto getArrangeInfo(object p0, object p1);
        object getDateInfo(object p0, object p1);
        List<string> getDateInfosjd(object p0);
        List<string> getDateInfosjdcount(object p0, object p1);
        object getDateTimeInfo(object p0, object p1, object p2);
        List<string> getStaffListByKs(object p0, object p1);
        string getStaffName(object p0);
        object getghpbId();
    }

    public interface IOutBookRepo
    {
        object SubmitForm(object p0, object p1, object p2, object p3, object p4);
        object UpdateForm(object p0, object p1, object p2, object p3, object p4, object p5);
        List<string> getStaffByKs(object p0);
        List<string> getStaffByKs(object p0, object p1);
        object updateZt(object p0, object p1, object p2, object p3);
    }

    public interface IOutBookScheduleRepo
    {
        object ExecSchedule();
        object ExecSchedulebyGroup();
        List<IOutBookScheduleRepo_GetPagintionListDto> GetPagintionList(object p0, object p1, object p2);
        object GetPagintionListTime(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7);
    }

    public interface IOutPatChargeApp
    {
        object DetailsUploadYb(object p0);
        CqDetailsYbTfhDto GetCQDetailsMzjsYbTfh(object p0, object p1, object p2, out decimal p3, out decimal p4, out decimal p5);
        string GetCfh(object p0);
        object GetCfhAndFjfMoney(object p0, object p1, object p2, object p3, object p4, object p5, object p6);
        List<ChargeTemplateDto> GetChargeTemplateAll(object p0);
        object GetChongQingMainOfMzjs(object p0, object p1, out decimal p2, out decimal p3);
        object GetCqyb10Data(object p0, object p1);
        object GetCqybMxCxData(object p0, object p1, object p2);
        guianMainAllOfMzjs GetGuiAnDetailsMzjsYbTfh(object p0, object p1, object p2);
        guianMainAllOfMzjs GetGuianMainOfMzjs(object p0);
        List<OutAccountInfoDto> GetInfo(object p0);
        List<OutPatChargeItemVO> GetOutGhInfo(object p0, object p1, out OutPatChargeDoctorVO p2);
        List<OutpatAccInfoDto> GetOutPatBasicInfoInAcc(object p0, object p1);
        IOutPatChargeApp_GetOutPatChargeInfoDto GetOutPatChargeInfo(object p0, object p1);
        object GetYpItemInfo(object p0);
        object OutPatChargeSett(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7);
        object SaveCqybUploadInPres(object p0, object p1, object p2);
        object SaveCqybUploadInPres(object p0, object p1, object p2, object p3);
        object UpDateUploadPch(object p0, object p1, object p2, object p3);
        object UpdateCqyb04InPres(object p0, object p1, object p2);
        object ZFToYB_Step_1(object p0);
        object ZFToYB_Step_3(object p0, object p1, object p2);
        object ZFToYB_Step_6(object p0, object p1, object p2);
        object ZFToYB_Step_8(object p0);
        object submitOutpatAdditional(object p0, object p1, object p2);
        bool submitOutpatCharge(object p0, object p1, object p2, object p3, object p4, object p5, object p6, out IList<int> p7, object p8, object p9);
        object togetherData(object p0);
    
        System.Collections.Generic.List<ChargeItemDetailVO> GetYpItemInfo(string keyword);
            object GetOutPatBasicInfoInRegister(object p0, object p1, object p2, object p3, object p4, object p5);
}

    public interface IOutPatChargeDmnService
    {
        object GetAllUnSettedListListByMzh(object p0, object p1);
        object GetAllUnSettedSingleItemListByMzh(object p0, object p1);
        Input_2203A GetCQjzdjInfo(object p0, object p1);
        List<IOutPatChargeDmnService_GetChongQingGHMzjsDto> GetChongQingGHMzjs(object p0, object p1, object p2, object p3, object p4);
        object GetMzbzml(object p0);
        object GetNewAllUnSettedListByMzh(object p0, object p1, object p2);
        IOutPatChargeDmnService_GetNewUnSettedPrescriptionByMzhDto GetNewUnSettedPrescriptionByMzh(object p0, object p1);
        List<OutAccountInfoDto> GetOutAccountInfo(object p0, object p1, object p2, object p3, object p4);
        List<OutpatAccInfoDto> GetOutpatChargePatInfoInAcc(object p0, object p1, object p2, object p3, object p4);
        List<IOutPatChargeDmnService_GetOutpatientSettArrearageVOListDto> GetOutpatientSettArrearageVOList(object p0, object p1);
        object GetOutpatientSettDetailList(object p0, object p1);
        OutpatAccInfoDto GetPatInfoInOptima(object p0, object p1, object p2);
        string GetPatientGridJson(object p0);
        object GetPrescriptionBymzh(object p0, object p1);
        object GetPrescriptionDetailBycfnm(object p0, object p1);
        object GetRegisterGridJson(object p0, object p1, object p2, object p3);
        List<OutpatientSettlementVO> GetSettlementList(object p0, object p1, object p2, object p3, object p4, object p5);
        List<OutAccountInfoDto> GetintAccountInfo(object p0, object p1, object p2, object p3, object p4);
        List<OutAccountInfoDto> GetpatientAccountInfoV4(object p0, object p1);
        object GetpatientAccountList(object p0, object p1, object p2, object p3, object p4, object p5);
    }

    public interface IOutPatienChargeQueryApp
    {
        object ChargeRecordsQuery(object p0);
        object CheckPintInfo(object p0);
        OutPatientReprintOrSuppPrintSettleDetailDto LoadMzjsMXRecords(object p0);
        List<MzjsRecordDto> LoadMzjsRecords(object p0, object p1, object p2, object p3, object p4, object p5);
        object PrintInvoice(object p0, object p1);
        object PrintMxData(object p0);
        object RePrint(object p0, object p1, object p2);
        object SupplementPrint(object p0, object p1);
            object GetRecordsByJsnm(object p0);
        object SelectRegChargeQuery(object p0, object p1, object p2, object p3, object p4, object p5);
}

    public interface IOutPatientChargeQueryDmnService
    {
        List<SettZffsDto> GetSettZffsResultList(object p0, object p1);
        IList<RoleUnionUser> GetCurUserIdListRoleId(string roleId);
        List<OutPatientRegChargeMVO> RegChargeQuery(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10);
        List<OutPatientRegChargeMVO> ZFRegChargeQuery(object p0, object p1, object p2, object p3);
    }

    public interface IOutPatientDmnService
    {
        object CommitAccounting(object p0, object p1, object p2, object p3);
        object GetIsMzghBookSchedule(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10);
        object GetNewRegScheduleList(object p0, object p1, object p2);
        List<OutpatientRegistScheduleDto> GetOutpatientRegistScheduleList(object p0, object p1);
        List<OutpatientRegistScheduleDto> GetOutpatientRegistScheduleList(object p0, object p1, object p2);
        object GetRegScheduleListbyGroup(object p0, object p1, object p2, object p3, object p4, object p5);
        List<RelatedYpDetailDto> GetRelatedYpDetailListByMzh(object p0, object p1, object p2);
        object SaveOutpatientAccountInfo(object p0, object p1, object p2);
        object SavepatientAccountInfo(object p0, object p1, object p2);
    
        object SelectAccountingPlanList(NewtouchHIS.Lib.Base.Model.Pagination pagination, string keyword, System.DateTime? kssj, System.DateTime? jssj, int? zxzt, string orgId, int? zsftx, int? sycstx, string sfzt);
        void overAccountingPlan(string mxId, string orgId, string rygh);
    }

    public interface IOutPatientSettleDmnService
    {
        Tuple<short, string> GetCQMzhJzxh(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8);
        List<OutPatientRegInfoVO> GetCurrentDayRegList(object p0, object p1);
        CqybGjbmInfoVo GetDepartmentDoctorIdC(object p0, object p1, object p2);
        Tuple<short, string> GetMzhJzxh(object p0, object p1, object p2, object p3, object p4, object p5);
        string GetOrgXzqh(object p0);
        object GetOutpatientFees(object p0, object p1, object p2, object p3);
        object GetOutpatientFeesbyGroup(object p0, object p1, object p2, object p3);
        List<GhJzInfo> GetRegListJson(object p0, object p1);
        object GetYbjzdjVo(object p0, object p1);
        object UnSettedRegBackNum(object p0, object p1);
    }

    public interface IOutPatientUniversalDmnService
    {
        object AccountPlanRefundAll(object p0, object p1);
        IOutPatientUniversalDmnService_GetSettPatientInfoDto GetSettPatientInfo(object p0, object p1);
        object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out string p6, out decimal p7, object p8, object p9, object p10, object p11, object p12, object p13);
        object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out string p6, out decimal p7, object p8, object p9, object p10, object p11, object p12, object p13, object p14, object p15);
        List<RefundZtDto> RefundZtDetail(object p0, object p1);
        object Sumwzdj(object p0, object p1, object p2);
        object TuiHuiwzdj(object p0, object p1, object p2);
        object UpdateArrearageSettlement(object p0, object p1, object p2, object p3, object p4);
        object UpdateYPcfxm(object p0, object p1, object p2);
        List<IOutPatientUniversalDmnService_pushApplicationformDto> pushApplicationform(object p0, object p1, object p2, object p3);
        object pushApplicationformRef(object p0, object p1, object p2);
        List<IOutPatientUniversalDmnService_retApplicationformDto> retApplicationform(object p0, object p1, object p2, object p3);
    }

    public interface IOutpatientPrescriptionRepo
    {
        object GeneratePresNo(object p0, object p1);
        List<IOutpatientPrescriptionRepo_GetByCfnmListDto> GetByCfnmList(object p0, object p1);
        OutpatientPrescriptionEntity GetValidEntityByCfnm(object p0, object p1);
    }

    public interface IOutpatientRegApp
    {
        object Book(object p0, object p1, object p2, object p3, object p4, object p5);
        object CheckBrxz(object p0, object p1);
        object CheckInvoice(object p0);
        string GetInvoice();
        object GetRegScheduleList(object p0, object p1);
        object Save(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10, object p11, object p12, object p13, object p14, object p15, object p16, object p17, object p18, ref short? p19, ref string p20, object p21, object p22, object p23, object p24, object p25, object p26, out object p27);
            object BackNum(object p0, object p1, object p2);
        object GetJZXH(object p0, object p1, object p2, object p3);
        List<LastSettleInfoVO> GetLastSettleInfo(string userCode);
        object GetOutpatientFees(object p0, object p1, object p2, object p3);
        object GetSettledFeesList(object p0, object p1);
        object GetSettledFeesList(object p0, object p1, object p2);
        OutPatientBasicInfoVO LoadPatientInfo(string kh);
        object Save(object p0, object p1, object p2, object p3);
        object SaveRegAbandRecord(object p0);
        object SelectZdList(object p0);
}

    public interface IOutpatientRegistRepo
    {
        object AllowRegh(object p0, object p1);
        System.Linq.IQueryable<OutpatientRegistEntity> IQueryable(System.Linq.Expressions.Expression<System.Func<OutpatientRegistEntity, bool>> predicate);
        object RecordOutpId(object p0, object p1, object p2, object p3);
        object UpdateChargeMzghInfo(object p0, object p1, object p2, object p3, object p4, object p5, object p6);
        object UpdatePatPhone(object p0, object p1, object p2, object p3);
        object updatePatBrxzInfo(object p0, object p1, object p2);
        object updateYbghjzId(object p0, object p1, object p2, object p3);
            string SubmitForm(object p0, object p1, object p2, object p3, object p4, out string p5);
}

    public interface IOutpatientRegistScheduleRepo
    {
        object DeleteForm(object p0, object p1);
        object SubmitForm(object p0, object p1);
    }

    public interface IOutpatientSettlementGAYBFeeRepo
    {
        object InsertEntity(object p0);
    }

    public interface IOutpatientSettlementRepo
    {
        object UpdateSettedFph(object p0, object p1, object p2);
    }

    public interface IPatientBasicInfoDmnService
    {
        object GetQHDjzdjInfo(object p0, object p1);
    
        System.Collections.Generic.List<SysHosBasicInfoVO> GetSysBasicByZHY(string kh, string zyh, string orgid);
        System.DateTime? IFCQRQISJZSJ(string zyh, string orgId);
            object BoolRuQu(object p0, object p1);
        object CancelAdmission(object p0, object p1);
        bool CheckHasFee(object p0, object p1);
        S04RequestDTO ComposeS04par(object p0, object p1);
        S06RequestDTO ComposeS06par(object p0, object p1);
        IPatientBasicInfoDmnService_GetCQjzdjInfoDto GetCQjzdjInfo(object p0, object p1);
        object GetInPatientList(object p0, ref string p1, ref string p2, object zybz, object keyword, object pagination, object ryrqkssj, object ryrqjssj, object ksmc, object bqmc);
        List<IPatientBasicInfoDmnService_GetListDto> GetList(object p0, object p1, object p2);
        System.Collections.Generic.List<SysHosBasicInfoVO> GetPatBasicCardInfo(object p0, out bool p1, object p2);
        object GetPatSearchList(object p0, object p1, object p2, object p3, object p4);
        object GetSettlementList(object p0, object sczt, object tjzt, object keyword, object pagination, object cykssj, object cyjssj);
        object GetSyspatientFormJson(object p0, object p1, object p2);
        object GetYbCancelList(object p0, object p1, object p2, object p3);
        object GetksZzdList(object p0);
        bool InpatientZFchangetoXNH(object p0, object p1, object p2, object p3, out string p4);
        IPatientBasicInfoDmnService_S04submitDto S04submit(object p0, object p1);
        IPatientBasicInfoDmnService_S05submitDto S05submit(object p0, object p1);
        IPatientBasicInfoDmnService_S06submitDto S06submit(object p0, object p1);
        object SavePatBasicCardInfo(object p0, object p1, object p2);
        string SaveSysBasicAccountInfo(object p0, object p1, out string p2);
        object UpdateCqybOut02(object p0, object p1);
        object ValidateFirstVisit(object p0, object p1, object p2, object p3, object p4);
        object updateZybrxxkExpandBje(object p0, object p1, object p2);
        object validateTTentity(object p0, object p1);
        object GetSysBasicVagueByZHY(object p0, object p1);
}

    public interface IPayApp
    {
        int TradeRefund(object p0, object p1, object p2, object p3, out string p4);
    
        int TradeRefund(string outTradeNo, decimal refundAmount, string refundReason, string outRequestNo, out string errorMsg);
    }

    public interface IPhysicalexamDmnService
    {
        object SubmitPhysicalexamForm(object p0, object p1, object p2);
    }

    public interface IRefundApp
    {
        void DetailsUploadYb(string orgId, int jsnm, Dictionary<int, decimal> tjsxmDict, string newwjybjsh, out decimal tmxzje, out decimal yxzje);
        List<MZJSInfo> GetFPHByKh(string kh, string startTime, string endTime, string fph);
        List<GridViewMx> GetGridViewMx(int jsnm);
        List<MZJS> GetMZJSByJsnm(int jsnm);
        bool btnReturn(string kh, List<GridViewMx> RefundVOList, int jsnm, out bool isReturnAll);
        bool btnReturnInAcc(string blh, List<GridViewMx> GridViewMxList, int jsnm, out bool isReturnAll);
    }

    public interface IRefundDmnService
    {
        object GetBasicInfoSearchList(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8);
        object GetBasicInfoSearchListInRegister(object p0, object p1);
        object GetBasicInfoSearchListInRegister(object p0, object p1, object p2, object p3, object p4, object p5);
        List<RefundGridMxDto> GetGridViewMx(object p0, object p1);
        object GetjsInfoByblh(object p0, object p1, object p2, object p3);
        object RefundableChongQingQuery(object p0, object p1, object p2, object p3);
        List<IRefundDmnService_RefundableGuiAnJsQueryDto> RefundableGuiAnJsQuery(object p0, object p1, object p2, object p3);
        List<IRefundDmnService_RefundableJsQueryDto> RefundableJsQuery(object p0, object p1, object p2, object p3);
        List<OutPatientRefundableVO> RefundableQuery(object p0, object p1);
    
        object GetBasicInfoSearchListInRegister(string keyword, string orgId);
        object GetBasicInfoSearchList(string keyword, string orgId, string userCode, string p4, string from, string brzybzType);
            object GetZyMzYjjPatientSearch(object p0, object p1, object p2);
        object Getjzjsxx(object p0, object p1, object p2, object p3);
}

    public interface ISyncTreatmentServiceRecordDmnService
    {
        List<SyncTreatmentServiceRecordVO> GetList(object p0, object p1, object mzh, object zyh, object zlsgh);
    
        object GetList(string organizeId, int? clzt = null, string mzh = null, string zyh = null, string zlsgh = null);
    }

    public interface ISyncTreatmentServiceRecordRepo
    {
        List<SyncTreatmentUnitDto> GetUnitList(object p0, object p1, object p2, object p3, object p4);
            object GetPagintionList(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10, object p11, object p12, object p13);
        object SaveCancel(object p0, object p1);
}

    public interface ISysConfigRepo
    {
        bool? GetBoolValueByCode(object p0, object p1);
        bool? GetBoolValueByCode(object p0, object p1, object p2);
        SysConfigDto GetByCode(object p0, object p1);
        string GetValueByCode(object p0, object p1);
    
        SysConfigEntity GetByCode(string code, string orgId);
        string GetValueByCode(string code, string orgId);
        int GetIntValueByCode(string code, string orgId, int defaultValue = 0);
        bool? GetBoolValueByCode(string code, string orgId, bool? defaultValue = null);
            object GetIntValueByCode(object p0, object p1);
        ISysConfigRepo_GetFormDto GetForm(object p0);
        List<ISysConfigRepo_GetListDto> GetList(object p0, object p1);
        object SubmitForm(object p0, object p1);
}

    public interface ISysDepartmentRepo
    {
        IList<SysDepartmentVEntity> GetList(string orgId, string keyword = null);
        IList<SysDepartmentVEntity> GetList(string orgId, string zt, string keyword);
        SysDepartmentVEntity GetByCode(string code, string orgId);
        string GetCodeByName_FirstOrDefault(string name, string orgId);
        string GetNameByCode(string code, string orgId);
}

    public interface ISysPatientBasicInfoRepo
    {
        SysPatientBasicInfoEntity FindEntity(System.Linq.Expressions.Expression<System.Func<SysPatientBasicInfoEntity, bool>> predicate);
            object CardVoids(object p0, object p1, object p2);
        List<ISysPatientBasicInfoRepo_GetCardNoInfoDto> GetCardNoInfo(object p0, object p1, object p2);
        object GetDetailsData(object p0, object p1);
        object GetModifyLog(object p0, object p1, object p2);
        object GetPatOnlyBlhSearchList(object p0, object p1, object p2, object p3);
        System.Linq.IQueryable<SysPatientBasicInfoEntity> IQueryable();
        object SubmitCard(object p0, object p1);
        object UpdateZt(object p0, object p1, object p2, object p3);
        object UpdatelxrInfo(object p0, object p1, object p2, object p3, object p4, object p5);
        object Updateybxx(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7);
}

    public interface ISysRegistSpecialDiseaseRepo
    {
        object SelectSysChargeItemByghzbList(object p0);
    }

    public interface ISysUserDmnService
    {
        bool CheckStaffIsBelongDuty(object p0, object p1);
        List<StaffDutyVO> GetStaffByDutyCode(string orgId, string dutyCode);
        bool CheckStaffIsBelongDuty(string staffId, string duty);
        List<SysUserStaffVO> GetSatffVOListByOrg(string orgId, string keyword);
}

    public interface IprintOutpatientChargeBillDmnService
    {
        object GetCqmjzpjJson(object p0, object p1);
        object GetmjzpjJson(object p0, object p1);
    }

    public class ChargeTemplateDto
    {
    }

    public class CqDetailsYbTfhDto
    {
        public List<CqDetailsYbTfhItemDto> cflist { get; set; }
    }

    public class CqDetailsYbTfhItemDto
    {
        public string cfh { get; set; }
    }

    public class IRefundApp_GetGridViewMxDto
    {
        public string IS_RETURN { get; set; }
        public string czh { get; set; }
    }

    public class IOutBookScheduleRepo_GetPagintionListDto
    {
        public string czks { get; set; }
        public string ysgh { get; set; }
        public string ysxm { get; set; }
    }

    public class IOutPatChargeDmnService_GetChongQingGHMzjsDto
    {
        public int issc { get; set; }
        public decimal je { get; set; }
    }

    public class ICqybMedicalReg02Repo_FindEntityDto
    {
        public object blh { get; set; }
    }

    public class ICqybUpdateMedicalInput03Repo_FindEntityDto
    {
        public object blh { get; set; }
    }

    public class IOutBookDmnService_getArrangeInfoDto
    {
        public object ks { get; set; }
        public object mjzbz { get; set; }
        public object ys { get; set; }
        public object ysxm { get; set; }
    }

    public class IOutBookRepo_getStaffByKsDto
    {
    }

    public class IOutPatChargeApp_GetOutPatBasicInfoInAccDto
    {
        public object FirstOrDefault { get; set; }
    }

    public class IOutPatChargeApp_GetOutPatChargeInfoDto
    {
        public string brxz { get; set; }
        public List<string> brxzList { get; set; }
        public string patid { get; set; }
    }

    public class IOutPatChargeDmnService_GetCQjzdjInfoDto
    {
        public object mdtrt_id { get; set; }
        public object med_type { get; set; }
        public object psn_no { get; set; }
    }

    public class IOutPatChargeDmnService_GetNewUnSettedPrescriptionByMzhDto
    {
        public object ToJs { get; set; }
    }

    public class IOutPatChargeDmnService_GetOutpatChargePatInfoInAccDto
    {
        public object OutpatAccInfoDto { get; set; }
        public object OutpatAccListDto { get; set; }
        public object Where { get; set; }
        public object mzh { get; set; }
    }

    public class IOutPatChargeDmnService_GetOutpatientSettArrearageVOListDto
    {
        public string jsnm { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal zje { get; set; }
    }

    public class IOutPatChargeDmnService_GetPatientGridJsonDto
    {
        public object OrderByDescending { get; set; }
        public object Serialize { get; set; }
        public object blh { get; set; }
        public object mzh { get; set; }
        public object xnhgrbm { get; set; }
    }

    public class IOutPatientUniversalDmnService_GetSettPatientInfoDto
    {
        public object ybjsh { get; set; }
    }

    public class IOutPatientUniversalDmnService_pushApplicationformDto
    {
    }

    public class IOutPatientUniversalDmnService_retApplicationformDto
    {
    }

    public class IOutpatientPrescriptionRepo_GetByCfnmListDto
    {
        public string cfh { get; set; }
        public string cfnm { get; set; }
    }

    public class IOutpatientRegistRepo_IQueryableDto
    {
        public object CardType { get; set; }
        public object CreateTime { get; set; }
        public object kh { get; set; }
    }

    public class IRefundApp_GetMZJSByJsnmDto
    {
    }

    public class IRefundDmnService_RefundableGuiAnJsQueryDto
    {
        public object Where { get; set; }
        public object ghnm { get; set; }
        public object ztId { get; set; }
    }

    public class IRefundDmnService_RefundableJsQueryDto
    {
        public object Where { get; set; }
        public object ztId { get; set; }
    }

    public class IRefundDmnService_RefundableQueryDto
    {
        public object Key { get; set; }
        public object Where { get; set; }
        public object ghnm { get; set; }
        public object jsmxje { get; set; }
        public object jsmxnm { get; set; }
        public object ztId { get; set; }
    }

    public class ISysPatientBasicInfoRepo_FindEntityDto
    {
        public object blh { get; set; }
            public object Create { get; set; }
        public object Id { get; set; }
        public object OrganizeId { get; set; }
        public object inpId { get; set; }
        public object patid { get; set; }
        public object xnhgrbm { get; set; }
        public object xnhylzh { get; set; }
        public object zyh { get; set; }
}

    public class MzjsMxDto
    {
        public object jsnm { get; set; }
    }

    public class MzjsRecordDto
    {
        public object jsnm { get; set; }
        public object zffsmcstr { get; set; }
    }

    public class OutAccountInfoDto
    {
        public object jzsj { get; set; }
    }

    public class OutpatientRegistScheduleDto
    {
        public object Id { get; set; }
        public object ghpbId { get; set; }
    }

    public class RefundGridMxDto
    {
    }

    public class RefundZtDto
    {
        public int jsmxnm { get; set; }
        public decimal sl { get; set; }
        public string ztId { get; set; }
    }

    public class RelatedYpDetailDto
    {
        public string cfh { get; set; }
        public string ypdm { get; set; }
        public string Yfbm { get; set; }
        public int jsmxnm { get; set; }
    }

    public class SettZffsDto
    {
        public string jsnm { get; set; }
        public string xjzffsmc { get; set; }
    }

    public class SyncTreatmentUnitDto
    {
        public int hidden { get; set; }
    }

    public class SysConfigDto { public string Value { get; set; } public string Code { get; set; } }
    public interface IOutBookScheduleService { }
    public interface ICqybUploadInPres04Repo { }
    public interface IOutpatientItemRepo { }
    public interface IOrderPayInfoRepo {
        void SubmitInfo(OrderPayInfoEntity entity, string keyValue = null);
        OrderPayInfoEntity GetSuccessRecordByOutTradeNo(string outTradeNo);
    }

        public interface IAccountManageApp
    {
        HosPatAccInfoDto GetHosPatInfo(string zyh);
        string GetFinRepSJPZH();
        bool PayDepositPost(DeposDto dto, out string szId);
            bool CheckSJPZH(object p0, out FinanceReceiptEntity p1, out string p2);
        bool ExitDepositPost(object p0);
        ReserveDto GetHosPatAccInfo(object p0);
        System.Collections.Generic.List<PatAccPayVO> GetPayList(object p0, object p1);
        object GetZhyeByPatid(object p0);
        object GetZhyeByPatid(object p0, object p1);
        object PayDepositPost(object p0, out object p1);
        object PayDepositPostAcnt(object p0);
        object RefundAccount(object p0);
        ReserveDto GetHosPatAccInfo(object p0, object p1);
}

        public interface IBookingDmnService
    {
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
            System.Collections.Generic.IList<PendingExecutionPatientVO> GetOutPendingExecutionPatientVOList(object p0, object p1);
        List<IBookkeepInHosDmnService_GetPendingExecutionPatientVOListDto> GetPendingExecutionPatientVOList(object p0);
        System.Collections.Generic.IList<PendingExecutionPatientVO> GetPendingExecutionPatientVOList(object p0, object p1);
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
            System.Collections.Generic.IList<HospFeeChargeCategoryGroupVO> GetHospGroupFeeVOList(object p0, object p1, object p2);
        InpatientSettXnhPatInfoVO GetInpatientSettXnhPatInfo(object p0, object p1);
}

        public interface IHosPatAccDmnService
    {
    }

        public interface IHospAccountingPlanDetailRepo : IRepositoryBase<HospAccountingPlanDetailEntity>
    {
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

        public interface IHospFeeDmnService
    {
        void SyncPatFee(string orgId, string zyh, int lx);
        IList<HospItemFeeDetailVO> GetItemFeeDetailVOList(string zyh, string orgId);
        IList<HospItemFeeDetailVO> GetMedicinFeeDetailVOList(string zyh, string orgId);
    }

        public interface IHospItemBillingRepo : IRepositoryBase<HospItemBillingEntity>
    {
        IList<HospItemBillingEntity> GetItemFeeEntityListByTime(string zyh, string orgId, DateTime startTime, DateTime endTime);
        void Updatezyaddfee(string orgId, string jfbhs, string yfbm, string userCode);
    }

        public interface IHospItemFeeApp
    {
        void SubmitForm(HospItemBillingEntity entity, int? keyValue);
    }

        public interface IHospMedicinFeeApp
    {
        void SubmitForm(HospDrugBillingEntity entity, int? keyValue);
    }

        public interface IHospMultiDiagnosisRepo : IRepositoryBase<HospMultiDiagnosisEntity>
    {
        List<HospMultiDiagnosisEntity> SelectData(string zyh, string organizeId);
    }

        public interface IHospPatientBasicInfoRepo : IRepositoryBase<HospPatientBasicInfoEntity>
    {
        new HospPatientBasicInfoEntity FindEntity(Expression<Func<HospPatientBasicInfoEntity, bool>> predicate);
            string GetBlhByZyh(object p0, object p1);
        object GetInpatientList(object p0, object p1, object p2);
        object IQueryable();
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

        public interface IHospSettlementGAYBZYMXXRFeeRepo : IRepositoryBase<HospSettlementGAYBZYMXXRFeeEntity>
    {
        new void Insert(HospSettlementGAYBZYMXXRFeeEntity entity);
    }

        public interface IHospSettlementRepo : IRepositoryBase<HospSettlementEntity>
    {
        List<HospSettlementEntity> GetValidList(string zyh, string orgId);
    }

        public interface IHospSimulateSettlementGAYBFeeRepo : IRepositoryBase<HospSimulateSettlementGAYBFeeEntity>
    {
        new void Insert(HospSimulateSettlementGAYBFeeEntity entity);
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

        public interface IInpatientApp
    {
            object CQZFToYB_Step_6(object p0, object p1, object p2, object p3);
        object CqYBToZF_Step_3(object p0);
        S04RequestDTO GetZfToXnhPatInfo(object p0);
        object UpqdScData(object p0, object p1);
        object YBToZF_Step_1(object p0);
        object YBToZF_Step_3(object p0, object p1, object p2);
        object YBToZF_Step_4(object p0);
        object YBToZF_Step_5(object p0, object p1);
        object YBToZF_Step_7(object p0);
        bool ZFToXNH_Step_4(object p0, object p1, object p2, out string p3);
        object ZFToXNH_Step_8(object p0);
        object ZFToYB_Step_1(object p0);
        object ZFToYB_Step_2(object p0);
        object ZFToYB_Step_4(object p0, object p1, object p2);
        object ZFToYB_Step_6(object p0, object p1, object p2, object p3);
        object ZFToYB_Step_8(object p0);
}

        public interface IInpatientRefundApp
    {
        InpatientSettPatStatusDetailDto InpatientRefundQuery(string zyh, DateTime? kssj, DateTime? jssj, string ks, string xmlb, string xmmc);
    }

        public interface IInpatientRefundDmnService
    {
        string SaveRefund(List<HospItemBillingEntity> xmjfbList, List<HospDrugBillingEntity> ypjfbList, string zyh, string orgId);
    }

        public interface IItemDmnService
    {
        IList<SysItemsDetailVO> GetItemsDetailListByOrgIdAndItemCode(string orgId, string itemCode);
    }

        public interface IMoneyUpperLimitReminderDmnService
    {
        void GetStaffjfjeInfo(string orgId, string gh, string ks, out decimal jfjesx, out decimal yjfzje);
            List<MoneyUpperLimitReminderEntity> GetAllList(object p0, object p1, object p2, object p3);
        List<MoneyUpperLimitReminderEntity> GetAllList(object p0, object p1, object p2, object p3, object p4);
}

        public interface IOutPatientApp
    {
        void SaveoutpatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> xmList, string orgId);
    }

        public interface ISysChargeTemplateItemMappRepo : IRepositoryBase<SysChargeTemplateItemMappEntity>
    {
        IList<MbmxsfxmVO> getmbmx(string sfmb, string orgId);
    }

        public interface ISysChargeTemplateRepo
    {
    }

        public interface ISysPatiChargeAddApp
    {
            object DeleteForm(object p0);
        object GetSysPatiChargeAddVoList(object p0);
        List<SysPatiChargeAddVo> GetSysPatiChargeAddVoList(object p0, object p1);
        object GetfjsfdlList(object p0);
        object SubmitForm(object p0, object p1);
}

        public interface ISysPatiChargeWaiverApp
    {
            object DeleteForm(object p0);
        object GetEffectiveList(object p0);
        List<SysPatientChargeWaiverEntity> GetEffectiveList(object p0, object p1);
        object SubmitForm(object p0, object p1);
}

        public interface ISysStaffRepo
    {
        string GetNameByGh(string gh, string orgId);
    }

        public interface IOrderRefundInfoRepo
    {
        void SubmitInfo(OrderRefundInfoEntity entity, string keyValue);

        decimal GetRefundedSumByOutTradeNo(string outTradeNo);
    }

        public interface IPayDmnService
    {
        IList<MicroPayTradeInfoDTO> MicroPayTradeQuery(string ksrq, string jsrq, int? payType, int? payStatus,
            string keywords, string orgId);

        IList<MicroPayTradeInfoDTO> TradePayLsit(Pagination pagination, string ksrq, string jsrq, int? payType, int? payStatus,
            string keywords, string orgId);

        IList<OrderRefundInfoEntity> TradeRefundList(string OutTradeNo, string TradeNo);

        MicroPayTradeInfoDTO TradePayInfobyNo(string OutTradeNo, string TradeNo);
    }
}
