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
    
        public int ghnm { get; set; }
        public string OrganizeId { get; set; }
        public int patid { get; set; }
        public string brxz { get; set; }
        public string ghly { get; set; }
        public string mjzbz { get; set; }
        public string ks { get; set; }
        public string ys { get; set; }
        public Int16? jzxh { get; set; }
        public string ghlx { get; set; }
        public decimal ghf { get; set; }
        public decimal zlf { get; set; }
        public decimal ckf { get; set; }
        public decimal gbf { get; set; }
        public string ghzt { get; set; }
        public DateTime? jsrq { get; set; }
        public string jzbz { get; set; }
        public DateTime? jzrq { get; set; }
        public string ghxz { get; set; }
        public byte? fzbz { get; set; }
        public decimal? tz { get; set; }
        public string dbxm { get; set; }
        public string gsrdh { get; set; }
        public int? ghzbbh { get; set; }
        public string ghzb { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int? px { get; set; }
        public bool zzjm { get; set; }
        public string zdicd10 { get; set; }
        public string zdmc { get; set; }
        public string kh { get; set; }
        public string zt { get; set; }
        public string mzh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public DateTime? csny { get; set; }
        public string blh { get; set; }
        public string zjlx { get; set; }
        public string zjh { get; set; }
        public long OrderId { get; set; }
        public string CardType { get; set; }
        public string CardTypeName { get; set; }
        public string brly { get; set; }
        public bool? xlbrbz { get; set; }
        public DateTime? ghrq { get; set; }
        public string lxr { get; set; }
        public string lxrgx { get; set; }
        public string lxrdh { get; set; }
        public string ybjsh { get; set; }
        public string jzyy { get; set; }
        public string tjr { get; set; }
        public string yyghId { get; set; }
        public string nlshow { get; set; }
        public string outpId { get; set; }
        public string ghlybz { get; set; }
        public decimal? ScheduId { get; set; }
        public string jzid { get; set; }
        public string jzpzlx { get; set; }
        public string bzbm { get; set; }
        public string bzmc { get; set; }
        public string ecToken { get; set; }
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
    
        public int patid { get; set; }
        public string xm { get; set; }
        public string py { get; set; }
        public string xb { get; set; }
        public DateTime? csny { get; set; }
        public string zjlx { get; set; }
        public string zjh { get; set; }
        public string cs_sheng { get; set; }
        public string cs_shi { get; set; }
        public string cs_xian { get; set; }
        public string cs_dz { get; set; }
        public string cs_yb { get; set; }
        public string hu_sheng { get; set; }
        public string hu_shi { get; set; }
        public string hu_xian { get; set; }
        public string hu_dz { get; set; }
        public string hu_yb { get; set; }
        public string xian_sheng { get; set; }
        public string xian_shi { get; set; }
        public string xian_xian { get; set; }
        public string xian_dz { get; set; }
        public string xian_yb { get; set; }
        public string jjlxr_sheng { get; set; }
        public string jjlxr_shi { get; set; }
        public string jjlxr_xian { get; set; }
        public string jjlxr_dz { get; set; }
        public string jjllrgx { get; set; }
        public string jjllr { get; set; }
        public string jjlldh { get; set; }
        public int? dybh { get; set; }
        public string dwmc { get; set; }
        public string dwdz { get; set; }
        public string dwdh { get; set; }
        public string dwyb { get; set; }
        public string xl { get; set; }
        public string gj { get; set; }
        public byte? hf { get; set; }
        public string mz { get; set; }
        public string jg { get; set; }
        public string zy { get; set; }
        public string zjlxfs { get; set; }
        public string dh { get; set; }
        public string phone { get; set; }
        public string wechat { get; set; }
        public string email { get; set; }
        public string brly { get; set; }
        public string gms { get; set; }
        public string jbs { get; set; }
        public string bz { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int? px { get; set; }
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