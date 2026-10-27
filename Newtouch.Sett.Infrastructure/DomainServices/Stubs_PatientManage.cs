using Newtouch.Sett.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.Sett.DomainServices
{
    public class BaseGaXnhS30Repo : IBaseGaXnhS30Repo
    {
        public object Insert(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class Cache : ICache
    {
        public T Get<T>(string key, System.Func<T> func)
        {
            return func();
        }
        public T Get<T>(string key, System.Func<T> func, int cacheTime)
        {
            return func();
        }
}

    public class G_yb_daysrg_trt_list_bRepo : IG_yb_daysrg_trt_list_bRepo
    {
        public object Get_G_yb_mluCommon_Info(object p0, object p1, object p2)
        {
            return null;
        }

        public string Header(object p0)
        {
            return null;
        }

    }

    public class GuianRybl21OutInfoRepo : IGuianRybl21OutInfoRepo
    {
        public GuianRybl21OutInfoEntity FindEntity(System.Func<GuianRybl21OutInfoEntity, bool> predicate)
        {
            return null;
        }

        public object GetInfoByZyh(object p0, object p1)
        {
            return null;
        }

        public int Insert(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public int Update(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class GuianXnhS04InfoRepo : IGuianXnhS04InfoRepo
    {
        public GuianXnhS04InfoEntity FindEntity(System.Func<GuianXnhS04InfoEntity, bool> predicate)
        {
            return null;
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class PatientCenterDmnService : IPatientCenterDmnService
    {
        public object GetAdvancePayment(object p0, object p1)
        {
            return null;
        }

        public object GetDiagLsit(object p0, object p1)
        {
            return null;
        }

        public object GetPatHisZDInfo(object p0, object p1, object p2)
        {
            return null;
        }

        public object GetPatjfbInfo(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
        {
            return null;
        }

        public object GetPatjfbInfo(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7)
        {
            return null;
        }

        public object InHospitalDoctorInfo(object p0, object p1, ref IPatientCenterDmnService_InHospitalHistoryDto p2)
        {
            return null;
        }

        public System.Collections.Generic.List<IPatientCenterDmnService_InHospitalHistoryDto> InHospitalHistory(object p0, object p1, object p2)
        {
            return new System.Collections.Generic.List<IPatientCenterDmnService_InHospitalHistoryDto>();
        }

        public object InHospitalSett(object p0, object p1, object p2)
        {
            return null;
        }

        public object InHospitalSettbyJsnm(object p0, object p1, object p2)
        {
            return null;
        }

        public object MedInsurPreSettList(object p0, object p1)
        {
            return null;
        }

        public object MedInsurSettbyId(object p0)
        {
            return null;
        }

        public IPatientCenterDmnService_PatientBasicDto PatientBasic(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            return new IPatientCenterDmnService_PatientBasicDto();
        }

        public object PreSettbyId(object p0)
        {
            return null;
        }

    }

    public class SysAccountRepo : ISysAccountRepo
    {
        public System.Linq.IQueryable<SysAccountEntity> IQueryable()
        {
            return new System.Collections.Generic.List<SysAccountEntity>().AsQueryable();
        }

        public object Insert(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysCardApp : ISysCardApp
    {
        public string GetCardSerialNo()
        {
            return null;
        }

        public string GetPatidByCardNo(object p0)
        {
            return null;
        }

    }

    public class SysCardRepo : ISysCardRepo
    {
        public SysCardEntity FindEntity(System.Func<SysCardEntity, bool> predicate)
        {
            return null;
        }

        public SysCardEntity GetCardEntity(object p0, object p1, object p2)
        {
            return null;
        }

    }

    public class SysDiagnosisRepo : ISysDiagnosisRepo
    {
        public System.Collections.Generic.List<SysDiagnosisVEntity> GetList(object p0, object p1, object p2, object p3)
        {
            return new System.Collections.Generic.List<SysDiagnosisVEntity>();
        }

    }

    public class SysPatAccApp : ISysPatAccApp
    {
    }

    public class SysPatBasicInfoApp : ISysPatBasicInfoApp
    {
        public ISysPatBasicInfoApp_GetInfoByPatidDto GetInfoByPatid(object p0)
        {
            return new ISysPatBasicInfoApp_GetInfoByPatidDto();
        }

        public string Getblh()
        {
            return null;
        }

        public object PrinZYInfo(object p0)
        {
            return null;
        }

        public object PrintWDInfo(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysPatientMedicalRecordDmnService : ISysPatientMedicalRecordDmnService
    {
        public object GetHealthUpload(object p0, object p1, object p2, object p3, object p4)
        {
            return null;
        }

        public object GetMedicalRecordById(object p0)
        {
            return null;
        }

        public System.Collections.Generic.IList<SysPatientMedicalRecordDetailEntity> GetMedicalRecordDetailListByMainId(object p0)
        {
            return new System.Collections.Generic.List<SysPatientMedicalRecordDetailEntity>();
        }

        public List<SysPatientMedicalRecordDTO> GetMedicalRecordList(object p0, object p1)
        {
            return new List<SysPatientMedicalRecordDTO>();
        }

        public object SubmitForm(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysPatientNatureRepo : ISysPatientNatureRepo
    {
        public System.Linq.IQueryable<SysPatientNatureEntity> IQueryable(System.Linq.Expressions.Expression<System.Func<SysPatientNatureEntity, bool>> predicate)
        {
            return new System.Collections.Generic.List<SysPatientNatureEntity>().AsQueryable();
        }

            public object DeleteForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
        public ISysPatientNatureRepo_GetFormDto GetForm(object p0)
        {
            return new ISysPatientNatureRepo_GetFormDto();
        }
        public List<SysPatientNatureEntity> GetbxzcBySearch(object p0, object p1)
        {
            return new List<SysPatientNatureEntity>();
        }
        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
}

    public class SysWardRepo : ISysWardRepo
    {
        public List<ISysWardRepo_GetbqListDto> GetbqList(object p0)
        {
            return new List<ISysWardRepo_GetbqListDto>();
        }

    }
    public class PatientListPrintDmnServiceStub : IPatientListPrintDmnService
    {
        public System.Collections.Generic.List<PatientListOutPutVO> PatientList(PatientListInputVO patientListVO)
        {
            return new System.Collections.Generic.List<PatientListOutPutVO>();
        }
    }
}
