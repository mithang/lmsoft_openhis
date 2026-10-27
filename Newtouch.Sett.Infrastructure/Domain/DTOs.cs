using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    // DTO/VO/BO classes ported for HospitalizationManage controllers.
    // Only properties actually used by the ported controllers/views are guaranteed;
    // service stubs return null/empty for the rest (see Stubs.cs).

    public class ItemFeeVO
    {
        public DateTime tdrq { get; set; }
        public DateTime ryrq { get; set; }
        public DateTime cyrq { get; set; }
        public string yzlx { get; set; }
        public string sfxm { get; set; }
        public string dl { get; set; }
        public string ys { get; set; }
        public string ks { get; set; }
        public string ysmc { get; set; }
        public string ksmc { get; set; }
        public decimal dj { get; set; }
        public decimal sl { get; set; }
        public string dw { get; set; }
        public string zfxz { get; set; }
        public decimal? zfbl { get; set; }
        public string ybbm { get; set; }
        public string yfdm { get; set; }
        public int? cls { get; set; }
        public decimal? ztsl { get; set; }
    }

    public class ChargeItemDetailVO
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class InpatientAccountingPlanItemDto
    {
        public string sfxm { get; set; }
        public decimal? sl { get; set; }
    }

    public class InpatientAccountingItemDto
    {
        public string sfxm { get; set; }
        public decimal? sl { get; set; }
    }

    public class zxGirdDto
    {
        public string jzjhmxId { get; set; }
    }

    public class cxzxGirdDto
    {
        public string jzjhmxId { get; set; }
        public string zxbh { get; set; }
    }

    public class OutpatAccBasicInfoDto
    {
        public string zyh { get; set; }
    
        public string mzh { get; set; }
        public string fph { get; set; }
        public bool isQfyj { get; set; }
        public DateTime? sfrq { get; set; }
    }

    public class OutpatAccGridInfoDto
    {
        public string sfxm { get; set; }
    }

    public class HospFeeClassifyWithDLBO
    {
        public string dl { get; set; }
        public string dlmc { get; set; }
        public decimal jsje { get; set; }
        public IList<HospItemFeeDetailVO> ItemFeeList { get; set; }
        public IList<HospMedicinFeeDetailVO> MedicinFeeList { get; set; }
    }

    public class HospItemFeeDetailVO
    {
        public int jfbbh { get; set; }
        public decimal dj { get; set; }
        public decimal fwfdj { get; set; }
        public decimal sl { get; set; }
        public string sfxmmc { get; set; }
        public string dl { get; set; }
        public string dlmc { get; set; }
        public DateTime? CreateTime { get; set; }
        public decimal jmje { get; set; }
        public decimal jmbl { get; set; }
        public decimal? zfbl { get; set; }
        public string zfxz { get; set; }
        public string ybbm { get; set; }
        public decimal je { get; set; }
    }

    public class HospMedicinFeeDetailVO : HospItemFeeDetailVO
    {
        public string ypmc { get; set; }
    }

    public class SysHosBasicInfoVO
    {
        public string zyh { get; set; }
        public int patid { get; set; }
        public string zybz { get; set; }
        public string ryrq { get; set; }
        public string rybq { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string csny { get; set; }
        public string kh { get; set; }
        public string brxzbh { get; set; }
        public string brxz { get; set; }
        public string brxzmc { get; set; }
        public string ks { get; set; }
        public string ksmc { get; set; }
        public string bq { get; set; }
        public string bqmc { get; set; }
        public string cw { get; set; }
        public string cwmc { get; set; }
        public string zh { get; set; }
        public decimal? zhye { get; set; }
        public string zdCode { get; set; }
        public string icd10 { get; set; }
        public string zdmc { get; set; }
        public decimal wjje { get; set; }
    
        public string cardtype { get; set; }
        public string CardTypeName { get; set; }
        public string zyh2 { get; set; }
        public string db { get; set; }
        public string dbzd { get; set; }
        public string zjh { get; set; }
        public short? nl { get; set; }
        public string zjlx { get; set; }
        public string blh { get; set; }
        public string dh { get; set; }
        public string dy { get; set; }
        public int? dybh { get; set; }
        public string dymc { get; set; }
        public byte? hf { get; set; }
        public string wechat { get; set; }
        public string zjlxfs { get; set; }
        public string yddh { get; set; }
        public string email { get; set; }
        public string py { get; set; }
        public string xl { get; set; }
        public string gj2 { get; set; }
        public string mz2 { get; set; }
        public string jsr { get; set; }
        public string phone { get; set; }
        public string brly { get; set; }
        public string jjlldh { get; set; }
        public string jjllr { get; set; }
        public string jjllrgx { get; set; }
        public string cs_yb { get; set; }
        public string hu_yb { get; set; }
        public string xian_yb { get; set; }
        public string dwyb { get; set; }
        public string jbs { get; set; }
        public string jg { get; set; }
        public string jkjl { get; set; }
        public string jkjlmc { get; set; }
        public string doctor { get; set; }
        public string doctormc { get; set; }
        public string cyrq { get; set; }
        public string rytj { get; set; }
        public string zy { get; set; }
        public string mzmc { get; set; }
        public int? mz { get; set; }
        public string mzCode { get; set; }
        public string gjCode { get; set; }
        public int? gj { get; set; }
        public string gjmc { get; set; }
        public string cs_sheng { get; set; }
        public string cs_shi { get; set; }
        public string cs_xian { get; set; }
        public string cs_dz { get; set; }
        public string hu_sheng { get; set; }
        public string hu_shi { get; set; }
        public string hu_xian { get; set; }
        public string hu_dz { get; set; }
        public string xian_sheng { get; set; }
        public string xian_shi { get; set; }
        public string xian_xian { get; set; }
        public string xian_dz { get; set; }
        public string jjlxr_sheng { get; set; }
        public string jjlxr_shi { get; set; }
        public string jjlxr_xian { get; set; }
        public string jjlxr_dz { get; set; }
        public int? hy { get; set; }
        public decimal? bje { get; set; }
        public string lxr { get; set; }
        public string lxrgx { get; set; }
        public string lxrdh { get; set; }
        public string lxrdz { get; set; }
        public string lxrjtdh { get; set; }
        public string lxrWebchat { get; set; }
        public string lxrEmail { get; set; }
        public string lxr2 { get; set; }
        public string lxrgx2 { get; set; }
        public string lxryddh2 { get; set; }
        public string lxrjtdh2 { get; set; }
        public string lxrWebchat2 { get; set; }
        public string lxrEmail2 { get; set; }
        public string lxrdz2 { get; set; }
        public string gms { get; set; }
        public string ys { get; set; }
        public string zdmc1 { get; set; }
        public string zdmc2 { get; set; }
        public string zdmc3 { get; set; }
        public string ryzd1 { get; set; }
        public string ryzd2 { get; set; }
        public string ryzd3 { get; set; }
        public string dwmc { get; set; }
        public string dwdz { get; set; }
        public string dwdh { get; set; }
        public string bz { get; set; }
        public string sbbh { get; set; }
        public string cbdbm { get; set; }
        public string zxsbbf { get; set; }
        public string xnhgrbm { get; set; }
        public string xnhylzh { get; set; }
        public string inpId { get; set; }
        public string cblb { get; set; }
        public string zcyy { get; set; }
        public string dwbh { get; set; }
        public string grbh { get; set; }
        public string xzlx { get; set; }
        public string jzlx { get; set; }
        public string bzbm { get; set; }
        public string bzmc { get; set; }
        public string ssczdm { get; set; }
        public string ssczmc { get; set; }
        public string syfwzh { get; set; }
        public string sylb { get; set; }
        public string sysslb { get; set; }
        public string wybz { get; set; }
        public string yzs { get; set; }
        public string tc { get; set; }
        public string tes { get; set; }
        public string zcbz { get; set; }
        public DateTime? syrq { get; set; }
        public string yktbz { get; set; }
        public string isdsfly { get; set; }
        public string accountattr { get; set; }
    }

    public class TreatmentItemFeeDetailVO
    {
        public string isYP { get; set; }
        public int jfbbh { get; set; }
        public string yzwym { get; set; }
        public string dw { get; set; }
        public decimal dj { get; set; }
        public string sfxm { get; set; }
        public string dl { get; set; }
        public string dlmc { get; set; }
        public string sfxmmc { get; set; }
        public string sfmbmc { get; set; }
        public DateTime? CreateTime { get; set; }
        public decimal jmje { get; set; }
        public decimal jmbl { get; set; }
        public decimal zfbl { get; set; }
        public string zfxz { get; set; }
        public string ybbm { get; set; }
        public DateTime? tdrq { get; set; }
        public decimal sl { get; set; }
        public decimal tsl { get; set; }
        public decimal je { get; set; }
        public decimal ylsl { get; set; }
        public string ztbh { get; set; }
        public string dcztbs { get; set; }
        public decimal? ztsl { get; set; }
        public decimal? ztylsl { get; set; }
        public string jfbbhs { get; set; }
    }

    public class InpatientSettleItemBO
    {
        public List<TreatmentItemFeeDetailVO> TreatmentItemList { get; set; }
    }

    public class InpatientSettPatStatusDetailDto
    {
        public InpatientSettleItemBO InpatientSettleItemBO { get; set; }
    
        public InpatientSettPatInfoVO InpatientSettPatInfo { get; set; }
        public IList<HospFeeChargeCategoryGroupVO> GroupFeeVOList { get; set; }
        public InpatIentFeeSumVo InpatIentFeeSum { get; set; }
    }

    public class InpatientRefundJsonVO
    {
        public string isYp { get; set; }
        public string jfbbh { get; set; }
        public string jfbbhs { get; set; }
        public decimal? tsl { get; set; }
    }

    public class HosAccountingPatInfo
    {
        public DateTime? cqrq { get; set; }
    }

    public class HosAccountingPatStatusDetailDto
    {
        public HosAccountingPatInfo patInfo { get; set; }
    }

    public class InpatientHosAccountingPatStatusDetailDto
    {
        public HosAccountingPatInfo patInfo { get; set; }
    }

    public class HospAccountingPlanWaitingExeVO
    {
        public string zyh { get; set; }
    }

    public class HospSettQueryReq
    {
        public string keyword { get; set; }
        public string zyh { get; set; }
    }

    public class DeposDto
    {
        public int? patid { get; set; }
        public string pzh { get; set; }
        public int szxz { get; set; }
        public int? zffsbh { get; set; }
        public string zffsmc { get; set; }
        public decimal zfje { get; set; }
        public string zh { get; set; }
        public int? zhxz { get; set; }
        public string outTradeNo { get; set; }
    }

    public class HosPatAccInfoDto
    {
        public int? patid { get; set; }
        public string zh { get; set; }
        public string zhxz { get; set; }
    }

    public class HosSettPatInfoVO
    {
        public string zyh { get; set; }
        public string kh { get; set; }
        public string zybz { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string brxz { get; set; }
        public string brxzmc { get; set; }
        public string ybjylx { get; set; }
        public DateTime? ryrq { get; set; }
        public DateTime? cyrq { get; set; }
        public string pzh { get; set; }
        public string blh { get; set; }
        public int? patid { get; set; }
        public string ksCode { get; set; }
        public string ksmc { get; set; }
        public int? zyyjjzh { get; set; }
        public int? zyyjjzhbh { get; set; }
        public string zyyjjzhzhxz { get; set; }
        public string ryzdCode { get; set; }
        public string ryzdicd10 { get; set; }
        public decimal? zyts { get; set; }
        public DateTime? csny { get; set; }
    }

    public class HosSettPatClassifyChargePreviewDto
    {
        public string zyh { get; set; }
        public decimal jzfy { get; set; }
        public decimal flzf { get; set; }
        public decimal zifei { get; set; }
        public decimal zifu { get; set; }
        public decimal jmje { get; set; }
        public decimal yingshoukuan { get; set; }
        public decimal zhaoling { get; set; }
        public decimal srce { get; set; }
        public decimal ssk { get; set; }
        public decimal zhye { get; set; }
        public string fph { get; set; }
        public string fprq { get; set; }
        public HosSettPatInfoVO HospSettPatInfo { get; set; }
        public decimal expectedjsje { get; set; }
        public DateTime expectedcyrq { get; set; }
    }

    public class SettSaveSuccessResultDto
    {
        public int jsnm { get; set; }
        public string message { get; set; }
    }

    public class InpatientSettFeeRelatedDTO
    {
        public decimal? jsje { get; set; }
    }

    public class CQZyjs05Dto
    {
        public string zyh { get; set; }
    }

    public class S13OtherResponseDTO
    {
        public decimal? salvaQTCost { get; set; }
        public decimal? nhzje { get; set; }
        public decimal? nhxjzf { get; set; }
    }

    public class InpatientSettXnhPatInfoVO
    {
        public string inpId { get; set; }
        public string zyh { get; set; }
    
        public int patid { get; set; }
        public string doctor { get; set; }
        public string name { get; set; }
        public string bookNo { get; set; }
        public string familyNo { get; set; }
        public string memberNo { get; set; }
        public string zybz { get; set; }
    }

    public class InpatientPatientInfoDTO
    {
        public string zyh { get; set; }
        public string zybz { get; set; }
    }

    public class MbmxsfxmVO
    {
        public string sfxmcode { get; set; }
        public string sfdlcode { get; set; }
        public decimal dj { get; set; }
        public int? sl { get; set; }
        public string dw { get; set; }
        public string sfmbbh { get; set; }
    }

    public class GridZyXmYpjfb
    {
        public string zyh { get; set; }
    }

    public class gridZyXmYpItemsjfb
    {
        public string zyh { get; set; }
    }

    public class SysItemsDetailVO
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class VerificationVO
    {
        public string productCode { get; set; }
        public decimal kykc { get; set; }
    }

    public class UnSettedFeeItemVO
    {
        public string sfxmmc { get; set; }
        public decimal sl { get; set; }
        public decimal dj { get; set; }
        public decimal je { get; set; }
    }

    public class CQAlreadyUploadFeeDetailsVO
    {
        public decimal zje { get; set; }
        public DateTime cyrq { get; set; }
    }

    public class CancelSettInfoDto
    {
        public string operatorId { get; set; }
        public string operatorName { get; set; }
    }

    public class HospPreInfoDto
    {
        public decimal zje { get; set; }
        public DateTime cyrq { get; set; }
        public decimal ybzje { get; set; }
    }

    public class HospFeeSearchMainModel
    {
        public string Sfrq { get; set; }
        public string CWDlCode { get; set; }
        public decimal CWJe { get; set; }
        public string HLDlCode { get; set; }
        public decimal HLJe { get; set; }
        public string ZLDlCode { get; set; }
        public decimal ZLJe { get; set; }
        public string QTDlCode { get; set; }
        public decimal QTJe { get; set; }
        public string XYDlCode { get; set; }
        public decimal XYJe { get; set; }
        public string ZCYDlCode { get; set; }
        public decimal ZCYJe { get; set; }
        public string HYDlCode { get; set; }
        public decimal HYJe { get; set; }
        public string JCDlCode { get; set; }
        public decimal JCJe { get; set; }
        public string CLDlCode { get; set; }
        public decimal CLJe { get; set; }
        public decimal HJ { get; set; }
    }
}
