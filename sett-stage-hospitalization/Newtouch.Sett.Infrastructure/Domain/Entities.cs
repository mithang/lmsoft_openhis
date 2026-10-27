using SqlSugar;
using System;

namespace Newtouch.Sett.Domain
{
    [SugarTable("zy_xmjfb")]
    public class HospItemBillingEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int jfbbh { get; set; }
        public string OrganizeId { get; set; }
        public string zyh { get; set; }
        public string yzwym { get; set; }
        public int? zxid { get; set; }
        public int? mxid { get; set; }
        public DateTime tdrq { get; set; }
        public string sfxm { get; set; }
        public string dl { get; set; }
        public string ys { get; set; }
        public string ysmc { get; set; }
        public string ks { get; set; }
        public string ksmc { get; set; }
        public string cw { get; set; }
        public decimal dj { get; set; }
        public decimal? fwfdj { get; set; }
        public decimal sl { get; set; }
        public string jfdw { get; set; }
        public decimal? jmje { get; set; }
        public decimal? jmbl { get; set; }
        public decimal? zfbl { get; set; }
        public string zfxz { get; set; }
        public string ssbz { get; set; }
        public string ssry { get; set; }
        public DateTime? ssrq { get; set; }
        public string zxks { get; set; }
        public string yzxz { get; set; }
        public string yzzt { get; set; }
        public string cxry { get; set; }
        public DateTime? cxtdrq { get; set; }
        public int cxzyjfbbh { get; set; }
        public decimal? price { get; set; }
        public int? lb { get; set; }
        public string ybbm { get; set; }
        public string zltime { get; set; }
        public string bdzxid { get; set; }
        public int? clzhxmbh { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int? px { get; set; }
        public string bq { get; set; }
        public string zt { get; set; }
        public string jzjhmxId { get; set; }
        public string kflb { get; set; }
        public bool? ttbz { get; set; }
        public int? duration { get; set; }
        public int? zzll { get; set; }
        public string ztbh { get; set; }
        public decimal? ztsl { get; set; }
        public string dcztbs { get; set; }
        public int? zzfbz { get; set; }

        public HospItemBillingEntity Clone() { return (HospItemBillingEntity)this.MemberwiseClone(); }
        public void Create(bool isNew)
        {
            Create();
        }

        public void Create()
        {
            CreateTime = DateTime.Now;
        }
        public void Create(bool isNew, int keyValue) { jfbbh = keyValue; Create(); }
    }

    [SugarTable("zy_ypjfb")]
    public class HospDrugBillingEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int jfbbh { get; set; }
        public string OrganizeId { get; set; }
        public string zyh { get; set; }
        public string yzwym { get; set; }
        public int? zxid { get; set; }
        public int? mxid { get; set; }
        public DateTime tdrq { get; set; }
        public string yp { get; set; }
        public string yfdm { get; set; }
        public string bzdm { get; set; }
        public string dl { get; set; }
        public byte? ts { get; set; }
        public short? cls { get; set; }
        public string zlff { get; set; }
        public string ycqh { get; set; }
        public decimal? yl { get; set; }
        public string yldw { get; set; }
        public string ys { get; set; }
        public string ysmc { get; set; }
        public string ks { get; set; }
        public string ksmc { get; set; }
        public string cw { get; set; }
        public decimal dj { get; set; }
        public decimal? fwfdj { get; set; }
        public decimal sl { get; set; }
        public string jfdw { get; set; }
        public decimal? jmje { get; set; }
        public decimal? jmbl { get; set; }
        public decimal? zfbl { get; set; }
        public string zfxz { get; set; }
        public string lyyf { get; set; }
        public short? pyry { get; set; }
        public DateTime? pyrq { get; set; }
        public string fyry { get; set; }
        public DateTime? fyrq { get; set; }
        public string zxks { get; set; }
        public string yzxz { get; set; }
        public string yzzt { get; set; }
        public string cxry { get; set; }
        public DateTime? cxrq { get; set; }
        public int cxzyjfbbh { get; set; }
        public decimal? price { get; set; }
        public int? lb { get; set; }
        public string ybbm { get; set; }
        public string zltime { get; set; }
        public string bdzxid { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int? px { get; set; }
        public string bq { get; set; }
        public string zt { get; set; }
        public string jzjhmxId { get; set; }
        public string kflb { get; set; }
        public int? zzfbz { get; set; }

        public HospDrugBillingEntity Clone() { return (HospDrugBillingEntity)this.MemberwiseClone(); }
        public void Create(bool isNew)
        {
            Create();
        }

        public void Create()
        {
            CreateTime = DateTime.Now;
        }
        public void Create(bool isNew, int keyValue) { jfbbh = keyValue; Create(); }
    }

    [SugarTable("zy_js")]
    public class HospSettlementEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int jsnm { get; set; }
        public string OrganizeId { get; set; }
        public string zyh { get; set; }
        public string brxz { get; set; }
        public decimal zyts { get; set; }
        public decimal zje { get; set; }
        public decimal zlfy { get; set; }
        public decimal zffy { get; set; }
        public decimal flzffy { get; set; }
        public decimal jzfy { get; set; }
        public decimal xjzf { get; set; }
        public decimal xjwc { get; set; }
        public decimal zhjz { get; set; }
        public string xjzffs { get; set; }
        public string fph { get; set; }
        public string jszt { get; set; }
        public int cxjsnm { get; set; }
        public string cxjsyy { get; set; }
        public int? zh { get; set; }
        public string jsxz { get; set; }
        public DateTime? jsksrq { get; set; }
        public DateTime? jsjsrq { get; set; }
        public string fpdm { get; set; }
        public decimal jmje { get; set; }
        public string jylx { get; set; }
        public decimal? ysk { get; set; }
        public decimal? zl { get; set; }
        public string OutTradeNo { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int? px { get; set; }
        public string zt { get; set; }
        public DateTime? sfrq { get; set; }
        public decimal? zkbl { get; set; }
        public string ybjslsh { get; set; }

        public HospSettlementEntity Clone() { return (HospSettlementEntity)this.MemberwiseClone(); }
        public void Create(bool isNew)
        {
            Create();
        }

        public void Create()
        {
            CreateTime = DateTime.Now;
        }
        public void Create(bool isNew, int keyValue) { jsnm = keyValue; Create(); }
    }

    [SugarTable("zy_brjbxx")]
    public class HospPatientBasicInfoEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int syxh { get; set; }
        public string OrganizeId { get; set; }
        public string zyh { get; set; }
        public int patid { get; set; }
        public string brxz { get; set; }
        public string zybz { get; set; }
        public string ks { get; set; }
        public string bq { get; set; }
        public DateTime ryrq { get; set; }
        public string rytj { get; set; }
        public string rqry { get; set; }
        public DateTime? rqrq { get; set; }
        public string zy { get; set; }
        public string mz { get; set; }
        public string gj { get; set; }
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
        public int? hy { get; set; }
        public decimal? bje { get; set; }
        public string lxr { get; set; }
        public string lxrgx { get; set; }
        public string lxrdh { get; set; }
        public string lxrdz { get; set; }
        public string cyjdry { get; set; }
        public DateTime? cyjdrq { get; set; }
        public DateTime? cyrq { get; set; }
        public string cyzd { get; set; }
        public string cybq { get; set; }
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
        public string doctor { get; set; }
        public int? px { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string kh { get; set; }
        public string CardType { get; set; }
        public string CardTypeName { get; set; }
        public string jkjl { get; set; }
        public string cw { get; set; }
        public string rybq { get; set; }
        public short? ryzd { get; set; }
        public string zt { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string blh { get; set; }
        public DateTime? csny { get; set; }
        public string zjh { get; set; }
        public string zjlx { get; set; }
        public short? nl { get; set; }
        public string brly { get; set; }
        public string nlshow { get; set; }
        public string zcyy { get; set; }
        public string jjlxr_sheng { get; set; }
        public string jjlxr_shi { get; set; }
        public string jjlxr_xian { get; set; }
        public string jjlxr_dz { get; set; }
        public string jzid { get; set; }
        public string jzlx { get; set; }
        public string bzbm { get; set; }
        public string bzmc { get; set; }
        public string jzh { get; set; }
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
        public string BedNo { get; set; }

        public HospPatientBasicInfoEntity Clone() { return (HospPatientBasicInfoEntity)this.MemberwiseClone(); }
        public void Create(bool isNew)
        {
            Create();
        }

        public void Create()
        {
            CreateTime = DateTime.Now;
        }
        public void Create(bool isNew, int keyValue) { syxh = keyValue; Create(); }
    }

    [SugarTable("zy_mxxr_gayb")]
    public class HospSettlementGAYBZYMXXRFeeEntity
    {
        public int Id { get; set; }
        public string OrganizeId { get; set; }
        public string zyh { get; set; }
        public string akc190 { get; set; }
        public string yka105 { get; set; }
        public string yka317 { get; set; }
        public string yka318 { get; set; }
        public string yka319 { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }

        public HospSettlementGAYBZYMXXRFeeEntity Clone() { return (HospSettlementGAYBZYMXXRFeeEntity)this.MemberwiseClone(); }
        public void Create(bool isNew)
        {
            Create();
        }

        public void Create()
        {
            CreateTime = DateTime.Now;
        }
    }

    [SugarTable("zy_mnjs_gayb")]
    public class HospSimulateSettlementGAYBFeeEntity
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string prm_akc190 { get; set; }
        public string prm_aac001 { get; set; }
        public string prm_yab003 { get; set; }
        public string prm_aka130 { get; set; }
        public string prm_ykb065 { get; set; }
        public decimal prm_hisfyze { get; set; }
        public DateTime? prm_aae036 { get; set; }
        public decimal prm_yka055 { get; set; }
        public decimal prm_yka056 { get; set; }
        public decimal prm_yka057 { get; set; }
        public decimal prm_yka111 { get; set; }
        public decimal? prm_yka058 { get; set; }
        public decimal? prm_yka248 { get; set; }
        public decimal? prm_yka062 { get; set; }
        public decimal prm_yke030 { get; set; }
        public decimal prm_ake032 { get; set; }
        public decimal prm_ake181 { get; set; }
        public decimal prm_ake173 { get; set; }
        public decimal? prm_zhkzf { get; set; }
        public string prm_ykc299 { get; set; }
        public string prm_ykb037 { get; set; }
        public string prm_yka316 { get; set; }
        public string prm_yka054 { get; set; }
        public string prm_yae366 { get; set; }
        public decimal? prm_akc090 { get; set; }
        public decimal? prm_yka120 { get; set; }
        public decimal? prm_yka122 { get; set; }
        public decimal? prm_yka368 { get; set; }
        public decimal? prm_yka025 { get; set; }
        public decimal? prm_yka900 { get; set; }
        public decimal? prm_aae001 { get; set; }
        public string prm_yka089 { get; set; }
        public string prm_yka027 { get; set; }
        public decimal? prm_yka028 { get; set; }
        public decimal? prm_yka345 { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public string zt { get; set; }

        public HospSimulateSettlementGAYBFeeEntity Clone() { return (HospSimulateSettlementGAYBFeeEntity)this.MemberwiseClone(); }
        public void Create(bool isNew)
        {
            Create();
        }

        public void Create()
        {
            CreateTime = DateTime.Now;
        }
    }

    [SugarTable("cqyb_OutPut05")]
    public class CqybSett05Entity
    {
        public int Id { get; set; }
        public string OrganizeId { get; set; }
        public int jsnm { get; set; }
        public string jylsh { get; set; }
        public string jslb { get; set; }
        public decimal? cqtczf { get; set; }
        public decimal? zhzf { get; set; }
        public decimal? gwybz { get; set; }
        public decimal? cqxjzf { get; set; }
        public decimal? delpje { get; set; }
        public decimal? lsqfxgwyff { get; set; }
        public decimal? zhye { get; set; }
        public decimal? dbzddyljgdz { get; set; }
        public decimal? mzjzje { get; set; }
        public decimal? mzjzmzye { get; set; }
        public decimal? ndyxmzfje { get; set; }
        public decimal? ybzlzfs { get; set; }
        public decimal? shjzjjzfs { get; set; }
        public decimal? bntczflj { get; set; }
        public decimal? bndezflj { get; set; }
        public decimal? tbqfxzflj { get; set; }
        public decimal? ndyxmlj { get; set; }
        public decimal? bnmzjzzyzflj { get; set; }
        public DateTime? zxjssj { get; set; }
        public decimal? bcqfxzfje { get; set; }
        public decimal? bcjrybfwfy { get; set; }
        public decimal? ysfwzfs { get; set; }
        public decimal? yycbkkje { get; set; }
        public decimal? syjjzf { get; set; }
        public decimal? syxjzf { get; set; }
        public decimal? gsjjzf { get; set; }
        public decimal? gsxjzf { get; set; }
        public decimal? gsdbzjgdz { get; set; }
        public string gsqzfyy { get; set; }
        public decimal? qtbz { get; set; }
        public decimal? syzhzf { get; set; }
        public decimal? gszhzf { get; set; }
        public string bcjssfjkfpry { get; set; }
        public decimal? jkfpyljj { get; set; }
        public decimal? jztpbxje { get; set; }
        public decimal? zgjjzf { get; set; }
        public decimal? jxxmje { get; set; }
        public decimal? zfkzyl2 { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int px { get; set; }
        public string zt { get; set; }
        public decimal? qtfpbxje { get; set; }
        public string pch { get; set; }
        public string bzbm { get; set; }
        public string bzmc { get; set; }
        public string yllb { get; set; }
        public string jzlx { get; set; }
        public string jzpzm { get; set; }

        public CqybSett05Entity Clone() { return (CqybSett05Entity)this.MemberwiseClone(); }
        public void Create(bool isNew)
        {
            Create();
        }

        public void Create()
        {
            CreateTime = DateTime.Now;
        }
    }

    [SugarTable("Drjk_prejs_output")]
    public class CqybSett2303Entity
    {
        public string prejs_id { get; set; }
        public string mdtrt_id { get; set; }
        public string setl_id { get; set; }
        public string psn_no { get; set; }
        public string psn_name { get; set; }
        public string psn_cert_type { get; set; }
        public string certno { get; set; }
        public string gend { get; set; }
        public string naty { get; set; }
        public string brdy { get; set; }
        public string age { get; set; }
        public string insutype { get; set; }
        public string psn_type { get; set; }
        public string cvlserv_flag { get; set; }
        public string setl_time { get; set; }
        public string mdtrt_cert_type { get; set; }
        public string med_type { get; set; }
        public string medfee_sumamt { get; set; }
        public string fulamt_ownpay_amt { get; set; }
        public string overlmt_selfpay { get; set; }
        public string preselfpay_amt { get; set; }
        public string inscp_scp_amt { get; set; }
        public string act_pay_dedc { get; set; }
        public string hifp_pay { get; set; }
        public string pool_prop_selfpay { get; set; }
        public string cvlserv_pay { get; set; }
        public string hifes_pay { get; set; }
        public string hifmi_pay { get; set; }
        public string hifob_pay { get; set; }
        public string maf_pay { get; set; }
        public string hosp_part_amt { get; set; }
        public string oth_pay { get; set; }
        public string fund_pay_sumamt { get; set; }
        public string psn_part_amt { get; set; }
        public string acct_pay { get; set; }
        public string psn_cash_pay { get; set; }
        public string balc { get; set; }
        public string acct_mulaid_pay { get; set; }
        public string medins_setl_id { get; set; }
        public string clr_optins { get; set; }
        public string clr_way { get; set; }
        public string clr_type { get; set; }
        public string zyh { get; set; }
        public string czydm { get; set; }
        public DateTime? czrq { get; set; }
        public int zt { get; set; }
        public string zt_czy { get; set; }
        public DateTime? zt_rq { get; set; }

        public CqybSett2303Entity Clone() { return (CqybSett2303Entity)this.MemberwiseClone(); }
        public void Create(bool isNew)
        {
            Create();
        }

        public void Create()
        {
        }
    }

    [SugarTable("zy_jzjhmx")]
    public class HospAccountingPlanDetailEntity
    {
        public string jzjhmxId { get; set; }
        public string OrganizeId { get; set; }
        public string jzjhId { get; set; }
        public string zyh { get; set; }
        public string yzxz { get; set; }
        public string yzlx { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string sfxmCode { get; set; }
        public int? duration { get; set; }
        public decimal? dj { get; set; }
        public int sl { get; set; }
        public DateTime? LastEexcutionTime { get; set; }
        public int zxzt { get; set; }
        public string bz { get; set; }
        public string ys { get; set; }
        public string ysmc { get; set; }
        public string ks { get; set; }
        public string ksmc { get; set; }
        public bool? ttbz { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public int? px { get; set; }
        public string zt { get; set; }
        public DateTime? PreStopDate { get; set; }
        public string kflb { get; set; }
        public int? zll { get; set; }
        public int? zcs { get; set; }
        public int? yzxcs { get; set; }
        public string yzId { get; set; }
        public string zxks { get; set; }

        public HospAccountingPlanDetailEntity Clone() { return (HospAccountingPlanDetailEntity)this.MemberwiseClone(); }
        public void Create(bool isNew)
        {
            Create();
        }

        public void Create()
        {
            CreateTime = DateTime.Now;
        }
    }

    [SugarTable("xt_sfmbxm")]
    public class SysChargeTemplateItemMappEntity
    {
        public string sfmbxmId { get; set; }
        public string OrganizeId { get; set; }
        public string sfmbbh { get; set; }
        public string sfxm { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int? px { get; set; }
        public string zt { get; set; }
        public int? sl { get; set; }
        public int? duration { get; set; }
        public string kflb { get; set; }
        public int? zll { get; set; }
        public int? zxcs { get; set; }
        public string zxks { get; set; }
        public string yzpc { get; set; }
        public string bw { get; set; }
        public decimal? dj { get; set; }

        public SysChargeTemplateItemMappEntity Clone() { return (SysChargeTemplateItemMappEntity)this.MemberwiseClone(); }
        public void Create(bool isNew)
        {
            Create();
        }

        public void Create()
        {
            CreateTime = DateTime.Now;
        }
    }

    [SugarTable("zy_rydzd")]
    public class HospMultiDiagnosisEntity
    {
        public string rydzdId { get; set; }
        public string OrganizeId { get; set; }
        public string zyh { get; set; }
        public string zdCode { get; set; }
        public string icd10 { get; set; }
        public string zdmc { get; set; }
        public string zdpx { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int? px { get; set; }
        public string zt { get; set; }

        public HospMultiDiagnosisEntity Clone() { return (HospMultiDiagnosisEntity)this.MemberwiseClone(); }
        public void Create(bool isNew)
        {
            Create();
        }

        public void Create()
        {
            CreateTime = DateTime.Now;
        }
    }

    [SugarTable("Sys_Config")]
    public class SysConfigEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Memo { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public int? px { get; set; }

        public SysConfigEntity Clone() { return (SysConfigEntity)this.MemberwiseClone(); }
        public void Create(bool isNew)
        {
            Create();
        }

        public void Create()
        {
            CreateTime = DateTime.Now;
        }
    }

}