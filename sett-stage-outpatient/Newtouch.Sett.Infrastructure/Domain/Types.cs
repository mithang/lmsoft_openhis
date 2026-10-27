using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public class AddPhysicalexamDto
    {
    }

    public class BasicInfoDto2018
    {
        public string fph { get; set; }
        public bool isQfyj { get; set; }
        public string mzh { get; set; }
        public DateTime? sfrq { get; set; }
    }

    public class CQMzjs05Dto
    {
        public bool IsNull() { return false; }
        public string ybdlx { get; set; }
    }

    public class CqybMedicalInPut02Entity
    {
        public string OrganizeId { get; set; }
        public string jytype { get; set; }
        public string zt { get; set; }
    }

    public class CqybMedicalReg02Entity
    {
        public string OrganizeId { get; set; }
        public string jytype { get; set; }
        public string zt { get; set; }
        public string zymzh { get; set; }
    }

    public class CqybSett05Entity
    {
        public string OrganizeId { get; set; }
        public string cqtczf { get; set; }
        public string cqxjzf { get; set; }
        public string dbzddyljgdz { get; set; }
        public string delpje { get; set; }
        public string gwybz { get; set; }
        public string jslb { get; set; }
        public string jsnm { get; set; }
        public string jylsh { get; set; }
        public string pch { get; set; }
        public string yllb { get; set; }
        public string zhye { get; set; }
        public string zhzf { get; set; }
    }

    public class CqybSett23Entity
    {
        public string OrganizeId { get; set; }
        public string acct_pay { get; set; }
        public string balc { get; set; }
        public string blh { get; set; }
        public string cvlserv_pay { get; set; }
        public string hifob_pay { get; set; }
        public string hifp_pay { get; set; }
        public string hosp_part_amt { get; set; }
        public string jsnm { get; set; }
        public string pch { get; set; }
        public string psn_cash_pay { get; set; }
        public string setl_id { get; set; }
        public string yllb { get; set; }
        public string zt { get; set; }
    }

    public class CqybUpdateMedicalInput03Entity
    {
        public string OrganizeId { get; set; }
        public string zt { get; set; }
    }

    public class CqybUploadPresList04Entity
    {
    }

    public class GridViewMx
    {
        public string IS_RETURN { get; set; }
        public string czh { get; set; }
    }

    public class GuiAnMztfProDto
    {
    }

    public class OutBookArrangeVO
    {
    }

    public class OutBookScheduleEntity
    {
    }

    public class OutBookScheduleVO
    {
    }

    public class OutGridInfoDto2018
    {
    }

    public class OutPatArrearageSettedUpdateDTO
    {
        public string fph { get; set; }
        public string jsnm { get; set; }
        public string sfrq { get; set; }
    }

    public class OutPatChargeDoctorVO
    {
    }

    public class OutPatChargeDto
    {
        public IOutPatChargeApp_GetOutPatChargeInfoDto patInfo { get; set; }
        public List<OutPatChargeItemVO> ghItemList { get; set; }
        public OutPatChargeDoctorVO ghDocVo { get; set; }
    }

    public class OutPatChargeItemVO
    {
    }

    public class OutPatientRefundableVO
    {
        public string cfh { get; set; }
        public string cflxmc { get; set; }
        public string cfmxId { get; set; }
        public string cfnm { get; set; }
        public string czh { get; set; }
        public decimal dj { get; set; }
        public string dlmc { get; set; }
        public string dw { get; set; }
        public string feeType { get; set; }
        public string ghnm { get; set; }
        public string jslx { get; set; }
        public decimal jsmxje { get; set; }
        public int jsmxnm { get; set; }
        public DateTime klsj { get; set; }
        public string ks { get; set; }
        public int ktsl { get; set; }
        public string m { get; set; }
        public string mc { get; set; }
        public string sfdlCode { get; set; }
        public string sfmb { get; set; }
        public string sfxmCode { get; set; }
        public int sl { get; set; }
        public string sqdzt { get; set; }
        public int tsl { get; set; }
        public string xmnm { get; set; }
        public string ys { get; set; }
        public string ysmc { get; set; }
        public string zfbl { get; set; }
        public string zfxz { get; set; }
        public string zljhwzx { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }
        public string ztsl { get; set; }
    }

    public class OutPatientRegChargeMVO
    {
        public string jsnm { get; set; }
        public string zffsmcstr { get; set; }
    }

    public class OutPatientRegVO
    {
        public string xnhgrbm { get; set; }
        public string mzh { get; set; }
        public string blh { get; set; }
    }

    public class OutpatAccBasicInfoDto
    {
        public string mzh { get; set; }
        public string fph { get; set; }
        public bool isQfyj { get; set; }
        public DateTime? sfrq { get; set; }
    }

    public class OutpatAccGridInfoDto
    {
    }

    public class OutpatAccInfoDto
    {
        public string mzh { get; set; }
        public string blh { get; set; }
        public string xm { get; set; }
        public string ghrq { get; set; }
        public string ghzt { get; set; }
        public string patid { get; set; }
        public string brxz { get; set; }
    }

    public class OutpatChargeDto
    {
        public IOutPatChargeApp_GetOutPatChargeInfoDto patInfo { get; set; }
        public List<OutPatChargeItemVO> ghItemList { get; set; }
        public OutPatChargeDoctorVO ghDocVo { get; set; }
    }

    public class OutpatOptimAccInfoDto
    {
        public OutpatAccInfoDto OutpatAccInfoDto { get; set; }
        public List<OutpatAccInfoDto> OutpatAccListDto { get; set; }
        public List<OutAccountInfoDto> OptimAccInfoDto { get; set; }
    }

    public class OutpatOptimInfoDto
    {
        public OutpatAccInfoDto OutpatAccInfoDto { get; set; }
        public List<SyncTreatmentServiceRecordVO> SyncTreatmentServiceRecordVO { get; set; }
    }

    public class OutpatientRegistEntity
    {
    }

    public class OutpatientRegistScheduleEntity
    {
        public string zt { get; set; }
    }

    public class OutpatientSettFeeRelatedDTO
    {
        public decimal zje { get; set; }
    }

    public class OutpatientSettYbFeeRelatedDTO
    {
    }

    public class OutpatientSettlementGAYBFeeEntity
    {
        public string OrganizeId { get; set; }
    }

    public class OutpatientSettlementVO
    {
        public string jsnm { get; set; }
        public string zffsmcstr { get; set; }
    }

    public class Output_2207VO
    {
        public string setl_id { get; set; }
        public string jsnm { get; set; }
        public string hifp_pay { get; set; }
        public string acct_pay { get; set; }
        public string psn_cash_pay { get; set; }
        public string cvlserv_pay { get; set; }
        public string hifob_pay { get; set; }
        public string hosp_part_amt { get; set; }
        public string balc { get; set; }
        public string pch { get; set; }
        public string yllb { get; set; }
    }

    public class SaveCancelRegisterGhnmVO
    {
    }

    public class SysPatientBasicInfoEntity
    {
        public string OrganizeId { get; set; }
        public string zt { get; set; }
        public string blh { get; set; }
    }

    public class UploadPrescriptionsListInPut
    {
        public decimal Count { get; set; }
    }

    public class ZYToYBDto
    {
    }


    public class guianMainAllOfMzjs
    {
    }

    public class Input_2203A
    {
        public string mdtrt_id { get; set; }
        public string med_type { get; set; }
        public string psn_no { get; set; }
    }

    public class TFS21RequestDTO
    {
    }

    public class OutPatientReprintOrSuppPrintSettleDetailDto
    {
    }

    public class SyncTreatmentServiceRecordVO
    {
    }

}