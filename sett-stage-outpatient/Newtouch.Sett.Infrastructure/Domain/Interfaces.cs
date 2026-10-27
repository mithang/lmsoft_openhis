using System;
using System.Collections.Generic;

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

    public interface ICqybSett05Repo
    {
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
    }

    public interface IOutPatientChargeQueryDmnService
    {
        List<SettZffsDto> GetSettZffsResultList(object p0, object p1);
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
    }

    public interface IOutpatientRegistRepo
    {
        object AllowRegh(object p0, object p1);
        List<IOutpatientRegistRepo_IQueryableDto> IQueryable(object p0);
        object RecordOutpId(object p0, object p1, object p2, object p3);
        object UpdateChargeMzghInfo(object p0, object p1, object p2, object p3, object p4, object p5, object p6);
        object UpdatePatPhone(object p0, object p1, object p2, object p3);
        object updatePatBrxzInfo(object p0, object p1, object p2);
        object updateYbghjzId(object p0, object p1, object p2, object p3);
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
    }

    public interface IPayApp
    {
        int TradeRefund(object p0, object p1, object p2, object p3, out string p4);
    }

    public interface IPhysicalexamDmnService
    {
        object SubmitPhysicalexamForm(object p0, object p1, object p2);
    }

    public interface IRefundApp
    {
        object DetailsUploadYb(object p0, object p1, object p2, object p3, out decimal p4, out decimal p5);
        object GetFPHByKh(object p0, object p1, object p2, object p3);
        List<IRefundApp_GetGridViewMxDto> GetGridViewMx(object p0);
        List<IRefundApp_GetMZJSByJsnmDto> GetMZJSByJsnm(object p0);
        bool btnReturn(object p0, object p1, object p2, out bool p3);
        bool btnReturnInAcc(object p0, object p1, object p2, out bool p3);
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
    }

    public interface ISyncTreatmentServiceRecordDmnService
    {
        List<SyncTreatmentServiceRecordVO> GetList(object p0, object p1, object mzh, object zyh, object zlsgh);
    }

    public interface ISyncTreatmentServiceRecordRepo
    {
        List<SyncTreatmentUnitDto> GetUnitList(object p0, object p1, object p2, object p3, object p4);
    }

    public interface ISysConfigRepo
    {
        bool? GetBoolValueByCode(object p0, object p1);
        bool? GetBoolValueByCode(object p0, object p1, object p2);
        SysConfigDto GetByCode(object p0, object p1);
        string GetValueByCode(object p0, object p1);
    }

    public interface ISysDepartmentRepo
    {
        object GetList(object p0, object p1);
    }

    public interface ISysPatientBasicInfoRepo
    {
        SysPatientBasicInfoEntity FindEntity(System.Linq.Expressions.Expression<System.Func<SysPatientBasicInfoEntity, bool>> predicate);
    }

    public interface ISysRegistSpecialDiseaseRepo
    {
        object SelectSysChargeItemByghzbList(object p0);
    }

    public interface ISysUserDmnService
    {
        bool CheckStaffIsBelongDuty(object p0, object p1);
        object GetStaffByDutyCode(object p0, object p1);
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
    public interface IOrderPayInfoRepo { }
}
