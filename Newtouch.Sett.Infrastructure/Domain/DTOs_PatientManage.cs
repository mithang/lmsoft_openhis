using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public class IBookkeepInHosDmnService_GetPendingExecutionPatientVOListDto
    {
        public object Where { get; set; }
        public object bqCode { get; set; }
    }

    public class IDischargeSettleDmnService_GetHospGroupFeeVOListDto
    {
        public object GroupFeeVOList { get; set; }
    }

    public class IDischargeSettleDmnService_GetInpatientSettXnhPatInfoDto
    {
        public object inpId { get; set; }
        public object zybz { get; set; }
    }

    public class IGuianRybl21OutInfoRepo_FindEntityDto
    {
        public object Create { get; set; }
        public object Id { get; set; }
        public object OrganizeId { get; set; }
        public object inpId { get; set; }
        public object patid { get; set; }
        public object xnhgrbm { get; set; }
        public object xnhylzh { get; set; }
        public object zyh { get; set; }
    }

    public class IGuianRybl21OutInfoRepo_UpdateDto
    {
        public object OrganizeId { get; set; }
        public object prm_ykc010 { get; set; }
        public object zjh { get; set; }
        public object zjlx { get; set; }
    }

    public class IGuianXnhS04InfoRepo_FindEntityDto
    {
        public object Create { get; set; }
        public object Id { get; set; }
        public object OrganizeId { get; set; }
        public object inpId { get; set; }
        public object patid { get; set; }
        public object xnhgrbm { get; set; }
        public object xnhylzh { get; set; }
        public object zyh { get; set; }
    }

    public class IHospPatientBasicInfoRepo_FindEntityDto
    {
        public object Create { get; set; }
        public object Id { get; set; }
        public object OrganizeId { get; set; }
        public object inpId { get; set; }
        public object patid { get; set; }
        public object xnhgrbm { get; set; }
        public object xnhylzh { get; set; }
        public object zyh { get; set; }
    }

    public class IInpatientApp_GetZfToXnhPatInfoDto
    {
        public object admissionDate { get; set; }
        public object admissionDepartments { get; set; }
        public object inpatientNo { get; set; }
        public object memberId { get; set; }
    }

    public class IPatientBasicInfoDmnService_GetCQjzdjInfoDto
    {
        public object operatorId { get; set; }
        public object operatorName { get; set; }
        public object orgId { get; set; }
    }

    public class IPatientBasicInfoDmnService_GetListDto
    {
    }

    public class IPatientBasicInfoDmnService_GetPatBasicCardInfoDto
    {
        public object inpId { get; set; }
        public object zyh { get; set; }
    }

    public class IPatientBasicInfoDmnService_S04submitDto
    {
        public S04ResponseDTO data { get; set; }
        public bool state { get; set; }
    }

    public class IPatientBasicInfoDmnService_S05submitDto
    {
        public string message { get; set; }
        public bool state { get; set; }
    }

    public class IPatientBasicInfoDmnService_S06submitDto
    {
        public string message { get; set; }
        public bool state { get; set; }
    }

    public class IPatientCenterDmnService_InHospitalHistoryDto
    {
        public object cqsj { get; set; }
        public object rysj { get; set; }
        public object zybz { get; set; }
        public object zyts { get; set; }
    }

    public class IPatientCenterDmnService_PatientBasicDto
    {
        public System.Collections.Generic.List<object> basic { get; set; }
        public System.Collections.Generic.List<object> mzinfolist { get; set; }
        public System.Collections.Generic.List<object> zyinfolist { get; set; }
    }

    public class ISysAccountRepo_IQueryableDto
    {
        public object patid { get; set; }
    }

    public class ISysCardRepo_FindEntityDto
    {
        public object Create { get; set; }
        public object Id { get; set; }
        public object OrganizeId { get; set; }
        public object inpId { get; set; }
        public object patid { get; set; }
        public object xnhgrbm { get; set; }
        public object xnhylzh { get; set; }
        public object zyh { get; set; }
    }

    public class ISysDiagnosisRepo_GetListDto
    {
    }

    public class ISysOrganizeDmnService_GetValidListByParentOrgDto
    {
    }

    public class ISysPatBasicInfoApp_GetInfoByPatidDto
    {
        public object email { get; set; }
        public object phone { get; set; }
        public object wechat { get; set; }
        public object zjlxfs { get; set; }
    }

    public class ISysPatientBasicInfoRepo_GetCardNoInfoDto
    {
        public object CardType { get; set; }
    }

    public class ISysPatientBasicInfoRepo_IQueryableDto
    {
        public object sbbh { get; set; }
    }

    public class ISysPatientMedicalRecordDmnService_GetMedicalRecordDetailListByMainIdDto
    {
        public object Select { get; set; }
    }

    public class ISysPatientMedicalRecordDmnService_GetMedicalRecordListDto
    {
    }

    public class ISysPatientNatureRepo_IQueryableDto
    {
    }

    public class ISysWardRepo_GetbqListDto
    {
    }

}