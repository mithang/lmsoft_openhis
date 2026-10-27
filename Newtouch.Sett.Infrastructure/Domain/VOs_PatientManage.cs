using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    // Source-true: Newtouch.Domain/ReportTemplateVO/PatientListOutPutVO.cs.
    // (PatientListInputVO already exists in Types_PatientManage.cs via gen-types.)
    // Source-true: Newtouch.Domain/DTO/OutputDto/SysPatientMedicalRecordDTO.cs,
    // SysPatientMedicalRecordTreeItemDTO.cs,
    // ValueObjects/PatientManage/SysPatientMedicalRecordDetailVO.cs.
    // (The empty gen-types TreeItemDTO in Types_PatientManage.cs is deleted;
    // gen-types skips types defined in VOs_* files.)
    public class SysPatientMedicalRecordDTO
    {
        public string Id { get; set; }
        public string blh { get; set; }
        public string rq { get; set; }
        public string bz { get; set; }
        public string CreatorCode { get; set; }
        public string CreatorUserName { get; set; }
        public string CreateTime { get; set; }
        public System.Collections.Generic.IList<SysPatientMedicalRecordDetailVO> Details { get; set; }
        public string zt { get; set; }
    }

    public class SysPatientMedicalRecordDetailVO
    {
        public string Id { get; set; }
        public int attachType { get; set; }
        public string attachName { get; set; }
        public string attachPath { get; set; }
        public string attachUrl { get; set; }
        public string CreateTime { get; set; }
        public string CreatorUserName { get; set; }
    }

    public class SysPatientMedicalRecordTreeItemDTO
    {
        public string Id { get; set; }
        public string blh { get; set; }
        public string rq { get; set; }
        public string bz { get; set; }
        public string CreatorCode { get; set; }
        public string CreatorUserName { get; set; }
        public string CreateTime { get; set; }
        public string ParentId { get; set; }
        public int attachType { get; set; }
        public string attachName { get; set; }
        public string attachPath { get; set; }
        public string attachUrl { get; set; }
        public string DetailCreateTime { get; set; }
        public string DetailCreatorUserName { get; set; }
        public string zt { get; set; }
    }

    public class PatientListOutPutVO
    {
        public string blh { get; set; }
        public short nl { get; set; }
        public string ryzd { get; set; }
        public DateTime ryrq { get; set; }
        public DateTime cyrq { get; set; }
        public string zyh { get; set; }
        public string xm { get; set; }
        public string zjh { get; set; }
        public string xb { get; set; }
        public DateTime csrq { get; set; }
        public string cw { get; set; }
    }

    public class SysAccountEntity
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public int zhCode { get; set; }
        public int patid { get; set; }
        public decimal zhye { get; set; }
        public int? zhxz { get; set; }
        public string zt { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public void Create() { }
        public void Create(bool _) { }
    }

    public class GuianXnhS04InfoEntity
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string xnhgrbm { get; set; }
        public string xnhylzh { get; set; }
        public string zyh { get; set; }
        public string inpId { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string zt { get; set; }
        public void Create() { }
        public void Create(bool _) { }
    }

    public class GuianRybl21OutInfoEntity
    {
        public string prm_ykc010 { get; set; }
        public string prm_akc190 { get; set; }
        public string prm_yab003 { get; set; }
        public string prm_aka130 { get; set; }
        public string prm_aac001 { get; set; }
        public string prm_aac003 { get; set; }
        public string prm_aac004 { get; set; }
        public string prm_aac002 { get; set; }
        public string prm_aab001 { get; set; }
        public string prm_aab004 { get; set; }
        public string prm_akc021 { get; set; }
        public int prm_akc023 { get; set; }
        public string prm_ykb065 { get; set; }
        public string OrganizeId { get; set; }
        public string jylsh { get; set; }
        public string jyyzm { get; set; }
    }

    public class SysPatientMedicalRecordDetailEntity
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public int attachType { get; set; }
        public string blId { get; set; }
        public string attachName { get; set; }
        public string attachPath { get; set; }
        public int? px { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
    }

    public class SysDiagnosisVEntity
    {
        public int zdId { get; set; }
        public string zdCode { get; set; }
        public string icd10 { get; set; }
        public string icd10fjm { get; set; }
        public string zdmc { get; set; }
        public string py { get; set; }
        public string wb { get; set; }
        public string OrganizeId { get; set; }
        public string zt { get; set; }
    }

    public class S04RequestDTO
    {
        public string memberId { get; set; }
        public string inpatientNo { get; set; }
        public string admissionDate { get; set; }
        public string admissionDepartments { get; set; }
        public string treatingPhysician { get; set; }
        public string admissionStatus { get; set; }
        public string diseaseCode { get; set; }
        public string initialDiagnosis { get; set; }
        public string surgeryCode { get; set; }
        public string wardArea { get; set; }
        public string wardNo { get; set; }
        public string berthNo { get; set; }
        public string isIncrease { get; set; }
        public string isReferra { get; set; }
        public string inpatientTypeOflocal { get; set; }
        public string bxAccount { get; set; }
        public string tel { get; set; }
        public string bankCardNo { get; set; }
        public string accountHolder { get; set; }
        public string holderRelation { get; set; }
        public string remark { get; set; }
        public string bigDiseaseNo { get; set; }
        public string isReferraNo { get; set; }
        public string uploadType { get; set; }
        public string familySysno { get; set; }
        public string Stature { get; set; }
        public string Weight { get; set; }
        public string secondIcdNo { get; set; }
        public string threeIcdNo { get; set; }
        public string opsId { get; set; }
        public string treatCode { get; set; }
        public string cureId { get; set; }
        public string complication { get; set; }
        public string turnMode { get; set; }
        public string turnCode { get; set; }
        public string turnDate { get; set; }
        public string ticketNo { get; set; }
        public string ministerNotice { get; set; }
        public string procreateNotice { get; set; }
        public string isNewborn { get; set; }
        public string newbornBirthday { get; set; }
        public string newbornName { get; set; }
        public string newbornSex { get; set; }
        public string registerID { get; set; }
        public string inpatientSn { get; set; }
        public string areaCode { get; set; }
        public string isTransProvincial { get; set; }
        public string majorDiseaseICD { get; set; }
        public string secondMajorDiseaseICD { get; set; }
        public string threeMajorDiseaseICD { get; set; }
        public string secondTreatCode { get; set; }
        public string threeTreatCode { get; set; }
        public List<valueList> disList { get; set; }
    }

    public class S04ResponseDTO
    {
        public string inpId { get; set; }
    }

    public class S05RequestDTO
    {
        public string inpId { get; set; }
        public string cancelCause { get; set; }
        public string areaCode { get; set; }
        public string isTransProvincial { get; set; }
    }

    public class S06RequestDTO
    {
        public string inpId { get; set; }
        public string inpatientNo { get; set; }
        public string admissionDate { get; set; }
        public string admissionDepartments { get; set; }
        public string treatingPhysician { get; set; }
        public string admissionStatus { get; set; }
        public string diseaseCode { get; set; }
        public string initialDiagnosis { get; set; }
        public string surgeryCode { get; set; }
        public string wardArea { get; set; }
        public string wardNo { get; set; }
        public string berthNo { get; set; }
        public string isIncrease { get; set; }
        public string isReferra { get; set; }
        public string inpatientTypeOflocal { get; set; }
        public string bxAccount { get; set; }
        public string tel { get; set; }
        public string bankCardNo { get; set; }
        public string accountHolder { get; set; }
        public string holderRelation { get; set; }
        public string remark { get; set; }
    }

    public class SysPatientNatureEntity
    {
        public int brxzbh { get; set; }
        public string OrganizeId { get; set; }
        public string brxz { get; set; }
        public string brxzmc { get; set; }
        public string py { get; set; }
        public string brxzlb { get; set; }
        public string ybjylx { get; set; }
        public string ybtsdy { get; set; }
        public string jyfyfw { get; set; }
        public string pzbz { get; set; }
        public string mzzybz { get; set; }
        public int? bzs { get; set; }
        public string sqjmbz { get; set; }
        public string zhxz { get; set; }
        public string zt { get; set; }
        public string syybbf { get; set; }
        public string brxxpz { get; set; }
        public string fpdymb { get; set; }
        public string fpdyghmb { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int? px { get; set; }
        public string bz { get; set; }
        public int? ParentId { get; set; }
        public string insutype { get; set; }
    }

    public class valueList
    {
        public string disCode { get; set; }
    }

    public class InpatientSettPatInfoVO
    {
        public string zyh { get; set; }
        public string zybz { get; set; }
        public int patId { get; set; }
        public string xm { get; set; }
        public string zjh { get; set; }
        public string xb { get; set; }
        public string brxz { get; set; }
        public int? brxzbh { get; set; }
        public string ybjylx { get; set; }
        public string brxzlb { get; set; }
        public string brxzmc { get; set; }
        public string blh { get; set; }
        public DateTime? ryrq { get; set; }
        public int? zyyjjzh { get; set; }
        public string ksmc { get; set; }
        public decimal? zyts { get; set; }
        public DateTime? cyrq { get; set; }
        public string ksCode { get; set; }
        public DateTime? csny { get; set; }
        public string ryzdmc { get; set; }
        public string ryzdicd10 { get; set; }
        public string cyzd { get; set; }
        public string cyzdicd10 { get; set; }
        public string cw { get; set; }
        public string kh { get; set; }
        public string CardType { get; set; }
        public string CardTypeName { get; set; }
        public decimal? zhye { get; set; }
        public string cblb { get; set; }
    }

    public class HospFeeChargeCategoryGroupVO
    {
        public string dlCode { get; set; }
        public string dlmc { get; set; }
        public decimal je { get; set; }
    }

    public class InpatIentFeeSumVo
    {
        public decimal zje { get; set; }
    }

    public class HosPatZFToXNHVO
    {
        public string xnhgrbm { get; set; }
        public string xnhylzh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string brxzmc { get; set; }
        public string brxzcode { get; set; }
        public string zjh { get; set; }
        public string xian_dz { get; set; }
        public string zhye { get; set; }
    }

    public class WDInfoVO
    {
        public string patid { get; set; }
        public string xm { get; set; }
        public string csrq { get; set; }
        public string ksmc { get; set; }
        public string bqmc { get; set; }
        public string zyh { get; set; }
        public string xb { get; set; }
        public string Flag { get; set; }
    }

    public class SysCardEntity
    {
        public string CardId { get; set; }
        public string OrganizeId { get; set; }
        public string CardNo { get; set; }
        public string CardType { get; set; }
        public string CardTypeName { get; set; }
        public int patid { get; set; }
        public string hzxm { get; set; }
        public string grbh { get; set; }
        public string cblb { get; set; }
        public string cbdbm { get; set; }
        public string xzlx { get; set; }
        public string brxz { get; set; }
        public string accountattr { get; set; }
        public string zt { get; set; }
        public int? px { get; set; }
        public string Memo { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
