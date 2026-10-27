using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public interface IBaseGaXnhS30Repo
    {
        object Insert(object p0);
    }

    public interface ICache
    {
        T Get<T>(string key, System.Func<T> func);
        T Get<T>(string key, System.Func<T> func, int cacheTime);
}

    public interface IG_yb_daysrg_trt_list_bRepo
    {
        object Get_G_yb_mluCommon_Info(object p0, object p1, object p2);
        string Header(object p0);
    }

    public interface IGuianRybl21OutInfoRepo
    {
        GuianRybl21OutInfoEntity FindEntity(System.Func<GuianRybl21OutInfoEntity, bool> predicate);
        object GetInfoByZyh(object p0, object p1);
        int Insert(object p0);
        int Update(object p0);
    }

    public interface IGuianXnhS04InfoRepo
    {
        GuianXnhS04InfoEntity FindEntity(System.Func<GuianXnhS04InfoEntity, bool> predicate);
        object SubmitForm(object p0, object p1);
    }

    public interface IPatientCenterDmnService
    {
        object GetAdvancePayment(object p0, object p1);
        object GetDiagLsit(object p0, object p1);
        object GetPatHisZDInfo(object p0, object p1, object p2);
        object GetPatjfbInfo(object p0, object p1, object p2, object p3, object p4, object p5, object p6);
        object GetPatjfbInfo(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7);
        object InHospitalDoctorInfo(object p0, object p1, ref IPatientCenterDmnService_InHospitalHistoryDto p2);
        System.Collections.Generic.List<IPatientCenterDmnService_InHospitalHistoryDto> InHospitalHistory(object p0, object p1, object p2);
        object InHospitalSett(object p0, object p1, object p2);
        object InHospitalSettbyJsnm(object p0, object p1, object p2);
        object MedInsurPreSettList(object p0, object p1);
        object MedInsurSettbyId(object p0);
        IPatientCenterDmnService_PatientBasicDto PatientBasic(object p0, object p1, object p2, object p3, object p4, object p5);
        object PreSettbyId(object p0);
    }

    public interface ISysAccountRepo
    {
        System.Linq.IQueryable<SysAccountEntity> IQueryable();
        object Insert(object p0);
    }

    public interface ISysCardApp
    {
        string GetCardSerialNo();
        string GetPatidByCardNo(object p0);
    }

    public interface ISysCardRepo
    {
        SysCardEntity FindEntity(System.Func<SysCardEntity, bool> predicate);
        SysCardEntity GetCardEntity(object p0, object p1, object p2);
    }

    public interface ISysDiagnosisRepo
    {
        System.Collections.Generic.List<SysDiagnosisVEntity> GetList(object p0, object p1, object p2, object p3);
    }

    public interface ISysPatAccApp
    {
        // no calls observed
    }

    public interface ISysPatBasicInfoApp
    {
        ISysPatBasicInfoApp_GetInfoByPatidDto GetInfoByPatid(object p0);
        string Getblh();
        object PrinZYInfo(object p0);
        object PrintWDInfo(object p0);
    }

    public interface ISysPatientMedicalRecordDmnService
    {
        object GetHealthUpload(object p0, object p1, object p2, object p3, object p4);
        object GetMedicalRecordById(object p0);
        System.Collections.Generic.IList<SysPatientMedicalRecordDetailEntity> GetMedicalRecordDetailListByMainId(object p0);
        System.Collections.Generic.List<SysPatientMedicalRecordDTO> GetMedicalRecordList(object p0, object p1);
        object SubmitForm(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8);
    }

    public interface ISysPatientNatureRepo
    {
        System.Linq.IQueryable<SysPatientNatureEntity> IQueryable(System.Linq.Expressions.Expression<System.Func<SysPatientNatureEntity, bool>> predicate);
            object DeleteForm(object p0, object p1);
        ISysPatientNatureRepo_GetFormDto GetForm(object p0);
        List<SysPatientNatureEntity> GetbxzcBySearch(object p0, object p1);
        object SubmitForm(object p0, object p1);
}

    public interface ISysWardRepo
    {
        List<ISysWardRepo_GetbqListDto> GetbqList(object p0);
    }
    public interface IPatientListPrintDmnService
    {
        // Source-true: Newtouch.Domain/IDomainServices/PatientManage/
        // IPatientListPrintDmnService.cs (called by LoadData; stub returns empty).
        System.Collections.Generic.List<PatientListOutPutVO> PatientList(PatientListInputVO patientListVO);
    }
}
