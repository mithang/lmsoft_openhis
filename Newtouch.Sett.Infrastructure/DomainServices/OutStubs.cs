using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using SqlSugar;

namespace Newtouch.Sett.DomainServices
{
    public class CancelOutpatientRegDmnService : ICancelOutpatientRegDmnService
    {
        public object SaveCancelRegister(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object SelectRegisterlist(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            return null;
        }

    }

    public class CommonDmnService : ICommonDmnService
    {
        public string Gzyb_GetJyyzmByJylsh(object p0, object p1)
        {
            return null;
        }

            public object GetWardbyDept(object p0, object p1, object p2)
        {
            return null;
        }
}

    public class CqybMedicalInPut02Repo : ICqybMedicalInPut02Repo
    {
        public object SaveCqybMedicalS02InReg(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }

    public class CqybMedicalReg02Repo : ICqybMedicalReg02Repo
    {
        public CqybMedicalReg02Entity FindEntity(System.Linq.Expressions.Expression<System.Func<CqybMedicalReg02Entity, bool>> predicate)
        {
            return null;
        }

        public object SaveCqybMedicalReg(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }


    public class CqybSett23Repo : ICqybSett23Repo
    {
        public object SaveCqybS23Sett(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }

    public class CqybUpdateMedicalInput03Repo : ICqybUpdateMedicalInput03Repo
    {
        public ICqybUpdateMedicalInput03Repo_FindEntityDto FindEntity(object p0)
        {
            return new ICqybUpdateMedicalInput03Repo_FindEntityDto();
        }

        public object SaveCqybUpdateMedicalS03InReg(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }

    public class CqybUploadPresList04Repo : ICqybUploadPresList04Repo
    {
        public object SaveCqybUploadPresList(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }

    public class GuiAnOutpatientXnhApp : IGuiAnOutpatientXnhApp
    {
        public object CancelSett(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public string Sett(object p0, object p1, out object p2)
        {
            p2 = null;
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public string Sett(object p0, object p1, object p2, object p3, out S25ResponseDTO p4)
        {
            p4 = null;
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public string Sett(object p0, object p1, object p2, out S25ResponseDTO p3, out string p4)
        {
            p3 = null;
            p4 = null;
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public string SimulationSettWholeProcess(object p0, object p1, object p2, out S24ResponseDTO p3)
        {
            p3 = null;
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public string XnhTuiFeiProcess(object p0, object p1, object p2, object p3, object p4, object p5, out TFS21RequestDTO p6)
        {
            p6 = null;
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }

    public class MzghbookRepo : IMzghbookRepo
    {
        public object FZsjTB(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object SigninAppointment(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object UpdateMzGhAppointment(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }

    public class OutBookDateRepo : IOutBookDateRepo
    {
        public object UpdateDate(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }

    public class OutBookDmnService : IOutBookDmnService
    {
        public object CancalBook(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object DeleteArrange(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object GetMzpbZlxmzh(object p0, object p1, object p2)
        {
            return null;
        }

        public List<IOutBookScheduleRepo_GetPagintionListDto> GetPagintionList(object p0, object p1, object p2)
        {
            return null;
        }

        public object GetStaffList(object p0)
        {
            return null;
        }

        public object InsertArrange(object p0, object p1, object p2, object p3, object p4)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object InsertghpbTime(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object PatBookGh(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object SaveDatapb(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object SaveDatatzcz(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object UpdateArrange(object p0, object p1, object p2, object p3, object p4)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public IOutBookDmnService_getArrangeInfoDto getArrangeInfo(object p0, object p1)
        {
            return new IOutBookDmnService_getArrangeInfoDto();
        }

        public object getDateInfo(object p0, object p1)
        {
            return null;
        }

        public List<string> getDateInfosjd(object p0)
        {
            return null;
        }

        public List<string> getDateInfosjdcount(object p0, object p1)
        {
            return null;
        }

        public object getDateTimeInfo(object p0, object p1, object p2)
        {
            return null;
        }

        public List<string> getStaffListByKs(object p0, object p1)
        {
            return null;
        }

        public string getStaffName(object p0)
        {
            return null;
        }

        public object getghpbId()
        {
            return null;
        }

    }

    public class OutBookRepo : IOutBookRepo
    {
        public object SubmitForm(object p0, object p1, object p2, object p3, object p4)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object UpdateForm(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public List<string> getStaffByKs(object p0)
        {
            return new List<string>();
        }

        public List<string> getStaffByKs(object p0, object p1)
        {
            return new List<string>();
        }

        public object updateZt(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }

    public class OutBookScheduleRepo : IOutBookScheduleRepo
    {
        public object ExecSchedule()
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object ExecSchedulebyGroup()
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public List<IOutBookScheduleRepo_GetPagintionListDto> GetPagintionList(object p0, object p1, object p2)
        {
            return null;
        }

        public object GetPagintionListTime(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7)
        {
            return null;
        }

    }

    public class OutPatChargeApp : IOutPatChargeApp
    {
        public object DetailsUploadYb(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public CqDetailsYbTfhDto GetCQDetailsMzjsYbTfh(object p0, object p1, object p2, out decimal p3, out decimal p4, out decimal p5)
        {
            p3 = 0;
            p4 = 0;
            p5 = 0;
            return new CqDetailsYbTfhDto();
        }

        public string GetCfh(object p0)
        {
            return null;
        }

        public object GetCfhAndFjfMoney(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
        {
            return null;
        }

        public List<ChargeTemplateDto> GetChargeTemplateAll(object p0)
        {
            return new List<ChargeTemplateDto>();
        }

        public object GetChongQingMainOfMzjs(object p0, object p1, out decimal p2, out decimal p3)
        {
            p2 = 0;
            p3 = 0;
            return null;
        }

        public object GetCqyb10Data(object p0, object p1)
        {
            return null;
        }

        public object GetCqybMxCxData(object p0, object p1, object p2)
        {
            return null;
        }

        public guianMainAllOfMzjs GetGuiAnDetailsMzjsYbTfh(object p0, object p1, object p2)
        {
            return null;
        }

        public guianMainAllOfMzjs GetGuianMainOfMzjs(object p0)
        {
            return null;
        }

        public List<OutAccountInfoDto> GetInfo(object p0)
        {
            return null;
        }

        public List<OutPatChargeItemVO> GetOutGhInfo(object p0, object p1, out OutPatChargeDoctorVO p2) { p2 = new OutPatChargeDoctorVO(); return new List<OutPatChargeItemVO>(); }

        public List<OutpatAccInfoDto> GetOutPatBasicInfoInAcc(object p0, object p1)
        {
            return new List<OutpatAccInfoDto>();
        }

        public IOutPatChargeApp_GetOutPatChargeInfoDto GetOutPatChargeInfo(object p0, object p1)
        {
            return new IOutPatChargeApp_GetOutPatChargeInfoDto();
        }

        public object GetYpItemInfo(object p0)
        {
            return null;
        }

        public object OutPatChargeSett(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7)
        {
            return null;
        }

        public object SaveCqybUploadInPres(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object SaveCqybUploadInPres(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object UpDateUploadPch(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object UpdateCqyb04InPres(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object ZFToYB_Step_1(object p0)
        {
            return null;
        }

        public object ZFToYB_Step_3(object p0, object p1, object p2)
        {
            return null;
        }

        public object ZFToYB_Step_6(object p0, object p1, object p2)
        {
            return null;
        }

        public object ZFToYB_Step_8(object p0)
        {
            return null;
        }

        public object submitOutpatAdditional(object p0, object p1, object p2)
        {
            return null;
        }

        public bool submitOutpatCharge(object p0, object p1, object p2, object p3, object p4, object p5, object p6, out IList<int> p7, object p8, object p9)
        {
            p7 = null;
            return false;
        }

        public object togetherData(object p0)
        {
            return null;
        }

    
        public System.Collections.Generic.List<ChargeItemDetailVO> GetYpItemInfo(string keyword)
        {
            throw new NotSupportedException("Chức năng tra cứu thuốc/mục thu phí này chưa được triển khai trong bản .NET Core.");
        }
            public object GetOutPatBasicInfoInRegister(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            return null;
        }
}

    public class OutPatChargeDmnService : IOutPatChargeDmnService
    {
        public object GetAllUnSettedListListByMzh(object p0, object p1)
        {
            return null;
        }

        public object GetAllUnSettedSingleItemListByMzh(object p0, object p1)
        {
            return null;
        }

        public Input_2203A GetCQjzdjInfo(object p0, object p1)
        {
            return new Input_2203A();
        }

        public List<IOutPatChargeDmnService_GetChongQingGHMzjsDto> GetChongQingGHMzjs(object p0, object p1, object p2, object p3, object p4)
        {
            return null;
        }

        public object GetMzbzml(object p0)
        {
            return null;
        }

        public object GetNewAllUnSettedListByMzh(object p0, object p1, object p2)
        {
            return null;
        }

        public IOutPatChargeDmnService_GetNewUnSettedPrescriptionByMzhDto GetNewUnSettedPrescriptionByMzh(object p0, object p1)
        {
            return new IOutPatChargeDmnService_GetNewUnSettedPrescriptionByMzhDto();
        }

        public List<OutAccountInfoDto> GetOutAccountInfo(object p0, object p1, object p2, object p3, object p4)
        {
            return new List<OutAccountInfoDto>();
        }

        public List<OutpatAccInfoDto> GetOutpatChargePatInfoInAcc(object p0, object p1, object p2, object p3, object p4)
        {
            return new List<OutpatAccInfoDto>();
        }

        public List<IOutPatChargeDmnService_GetOutpatientSettArrearageVOListDto> GetOutpatientSettArrearageVOList(object p0, object p1)
        {
            return new List<IOutPatChargeDmnService_GetOutpatientSettArrearageVOListDto>();
        }

        public object GetOutpatientSettDetailList(object p0, object p1)
        {
            return null;
        }

        public OutpatAccInfoDto GetPatInfoInOptima(object p0, object p1, object p2)
        {
            return null;
        }

        public string GetPatientGridJson(object p0)
        {
            return null;
        }

        public object GetPrescriptionBymzh(object p0, object p1)
        {
            return null;
        }

        public object GetPrescriptionDetailBycfnm(object p0, object p1)
        {
            return null;
        }

        public object GetRegisterGridJson(object p0, object p1, object p2, object p3)
        {
            return null;
        }

        public List<OutpatientSettlementVO> GetSettlementList(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            return null;
        }

        public List<OutAccountInfoDto> GetintAccountInfo(object p0, object p1, object p2, object p3, object p4)
        {
            return new List<OutAccountInfoDto>();
        }

        public List<OutAccountInfoDto> GetpatientAccountInfoV4(object p0, object p1)
        {
            return null;
        }

        public object GetpatientAccountList(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            return null;
        }

    }

    public class OutPatienChargeQueryApp : IOutPatienChargeQueryApp
    {
        public object ChargeRecordsQuery(object p0)
        {
            return null;
        }

        public object CheckPintInfo(object p0)
        {
            return null;
        }

        public OutPatientReprintOrSuppPrintSettleDetailDto LoadMzjsMXRecords(object p0)
        {
            return new OutPatientReprintOrSuppPrintSettleDetailDto();
        }

        public List<MzjsRecordDto> LoadMzjsRecords(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            return new List<MzjsRecordDto>();
        }

        public object PrintInvoice(object p0, object p1)
        {
            return null;
        }

        public object PrintMxData(object p0)
        {
            return null;
        }

        public object RePrint(object p0, object p1, object p2)
        {
            return null;
        }

        public object SupplementPrint(object p0, object p1)
        {
            return null;
        }

            public object GetRecordsByJsnm(object p0)
        {
            return null;
        }
        public object SelectRegChargeQuery(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            return null;
        }
}

    public class OutPatientChargeQueryDmnService : IOutPatientChargeQueryDmnService
    {
        public List<SettZffsDto> GetSettZffsResultList(object p0, object p1)
        {
            return new List<SettZffsDto>();
        }

        public List<OutPatientRegChargeMVO> RegChargeQuery(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10)
        {
            return null;
        }

        public List<OutPatientRegChargeMVO> ZFRegChargeQuery(object p0, object p1, object p2, object p3)
        {
            return null;
        }
        public IList<RoleUnionUser> GetCurUserIdListRoleId(string roleId)
        {
            return new List<RoleUnionUser>();
        }

    }

    public class OutPatientDmnService : IOutPatientDmnService
    {
        public object CommitAccounting(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object GetIsMzghBookSchedule(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10)
        {
            return null;
        }

        public object GetNewRegScheduleList(object p0, object p1, object p2)
        {
            return null;
        }

        public List<OutpatientRegistScheduleDto> GetOutpatientRegistScheduleList(object p0, object p1)
        {
            return new List<OutpatientRegistScheduleDto>();
        }

        public List<OutpatientRegistScheduleDto> GetOutpatientRegistScheduleList(object p0, object p1, object p2)
        {
            return new List<OutpatientRegistScheduleDto>();
        }

        public object GetRegScheduleListbyGroup(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            return null;
        }

        public List<RelatedYpDetailDto> GetRelatedYpDetailListByMzh(object p0, object p1, object p2)
        {
            return new List<RelatedYpDetailDto>();
        }

        public object SaveOutpatientAccountInfo(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object SavepatientAccountInfo(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    
        public object SelectAccountingPlanList(NewtouchHIS.Lib.Base.Model.Pagination pagination, string keyword, System.DateTime? kssj, System.DateTime? jssj, int? zxzt, string orgId, int? zsftx, int? sycstx, string sfzt)
        {
            throw new NotSupportedException("Chức năng truy vấn kế hoạch ghisổ ngoại trú chưa được triển khai trong bản .NET Core.");
        }
        public void overAccountingPlan(string mxId, string orgId, string rygh)
        {
            throw new NotSupportedException("Chức năng kết thúc kế hoạch ghisổ ngoại trú chưa được triển khai trong bản .NET Core.");
        }
    }

    public class OutPatientSettleDmnService : IOutPatientSettleDmnService
    {
        public Tuple<short, string> GetCQMzhJzxh(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8)
        {
            return new Tuple<short, string>(0, "");
        }

        public List<OutPatientRegInfoVO> GetCurrentDayRegList(object p0, object p1)
        {
            // Port of OutPatientSettleDmnService.GetCurrentDayRegList; explicit column list replaces
            // the original `select ..., *` (SqlSugar maps by name; `*` over several joins is ambiguous).
            var orgId = (string)p0;
            var creatorCode = (string)p1;
            var cfg = SettDb.GetUnionConnection().SqlQueryable<SysConfigDto>(
                "select Code, Value from Sys_Config where Code=@code and OrganizeId=@org and zt='1'")
                .AddParameters(new SugarParameter[] { new SugarParameter("@code", "GhSelectOnlyCreator"), new SugarParameter("@org", orgId) })
                .First();
            var sql = @"select staff.Name ysmc,dept.Name ksmc,brxz.brxzmc,gh.ghnm,gh.mzh,gh.blh,gh.xm,gh.kh,gh.CardType,gh.patid,gh.ks,gh.ys,gh.ghrq,gh.ghzt,gh.jzbz,gh.brxz,gh.xb,gh.zjh,gh.CreateTime,gh.CreatorCode,gh.OrganizeId,jbxx.py
from mz_gh(nolock) gh
left join [NewtouchHIS_Base]..V_S_Sys_Staff staff on staff.gh = gh.ys and staff.Organizeid = gh.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Department dept on dept.Code = gh.ks and dept.Organizeid = gh.OrganizeId
inner join xt_card kxx on kxx.CardNo=gh.kh and kxx.CardType=gh.CardType and kxx.OrganizeId=gh.OrganizeId and kxx.zt=1
left join xt_brxz brxz on brxz.brxz = kxx.brxz and brxz.OrganizeId = kxx.OrganizeId and brxz.zt=1
left join xt_brjbxx jbxx on jbxx.patid = gh.patid and jbxx.zt = '1' and jbxx.OrganizeId = gh.OrganizeId
where gh.OrganizeId = @orgId and gh.zt = '1' and gh.CreateTime >= CONVERT(varchar(100), GETDATE(), 23) ";
            var pars = new List<SugarParameter> { new SugarParameter("@orgId", orgId) };
            if (!string.IsNullOrEmpty(creatorCode) && cfg != null && cfg.Value == "ON")
            {
                sql += " and gh.CreatorCode = @creatorCode ";
                pars.Add(new SugarParameter("@creatorCode", creatorCode));
            }
            sql += " order by gh.CreateTime desc ";
            return SettDb.GetConnection().SqlQueryable<OutPatientRegInfoVO>(sql).AddParameters(pars.ToArray()).ToList();
        }

        public CqybGjbmInfoVo GetDepartmentDoctorIdC(object p0, object p1, object p2)
        {
            return new CqybGjbmInfoVo();
        }

        public Tuple<short, string> GetMzhJzxh(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            return new Tuple<short, string>(0, "");
        }

        public string GetOrgXzqh(object p0)
        {
            var orgId = (string)p0;
            return SettDb.GetBaseConnection().Ado.SqlQuery<string>(
                "select ybxzqhCode from sys_organize where id = @orgId and zt = '1'",
                new SugarParameter("@orgId", orgId)).FirstOrDefault();
        }

        public object GetOutpatientFees(object p0, object p1, object p2, object p3)
        {
            return null;
        }

        public object GetOutpatientFeesbyGroup(object p0, object p1, object p2, object p3)
        {
            return null;
        }

        public List<GhJzInfo> GetRegListJson(object p0, object p1)
        {
            // Faithful port of OutPatientSettleDmnService.GetRegListJson (original raw SQL, verbatim
            // table/column names; ORDER BY is safe here because this is a plain ToList, not ToPageList).
            var inparameter = (Inparameter)p0;
            var orgid = (string)p1;
            var sql = @"select gh.ghnm,gh.mzh,gh.blh,gh.xm,gh.zjh,gh.xb,gh.brxz,dept.Name ghksmc,gh.ks ghks,staff.Name ghysmc,gh.ys ghys,gh.ghrq,gh.ghzt,
gh.jzbz jzzt,jz.createtime jzrq,jzdept.Name jzksmc,jz.jzks,jz.jzys,jz.jzysmc,js.fph,
case when gh.ghzt='1' then 'Không' else 'Có' end syth,
case when gh.ghzt='1' then null else thys.Name end thys,
case when gh.ghzt='1' then null else convert(varchar(30), gh.LastModifyTime,120) end thrq
from mz_gh(nolock) gh
left join mz_js (nolock) js on gh.ghnm=js.ghnm and gh.OrganizeId=js.OrganizeId and js.jslx='0' and js.jszt='1' and js.zt='1'
left join Newtouch_CIS..xt_jz jz (nolock) on gh.mzh=jz.mzh and gh.OrganizeId=jz.OrganizeId and jz.zt='1'
left join [NewtouchHIS_Base]..V_S_Sys_Staff staff on staff.gh = gh.ys and staff.Organizeid = gh.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Staff thys on thys.gh=gh.LastModifierCode and thys.OrganizeId=gh.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Department dept on dept.Code = gh.ks and dept.Organizeid = gh.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Department jzdept on jzdept.Code = jz.jzks and jzdept.Organizeid = jz.OrganizeId
left join xt_brjbxx jbxx on jbxx.patid = gh.patid and jbxx.zt = '1' and jbxx.OrganizeId = gh.OrganizeId
where gh.OrganizeId = @orgId and gh.zt = '1'";
            var pars = new List<SugarParameter> { new SugarParameter("@orgId", orgid) };
            if (inparameter != null && inparameter.ksrq != null && inparameter.jsrq != null)
            {
                sql += " and CONVERT(varchar(100), gh.ghrq, 23) >= CONVERT(varchar(100), @ksrq, 23) ";
                sql += " and CONVERT(varchar(100), gh.ghrq, 23) <= CONVERT(varchar(100), @jsrq, 23) ";
                pars.Add(new SugarParameter("@ksrq", inparameter.ksrq));
                pars.Add(new SugarParameter("@jsrq", inparameter.jsrq));
            }
            if (inparameter != null && !string.IsNullOrWhiteSpace(inparameter.keywordo))
            {
                sql += " and gh.ks = @kscode ";
                pars.Add(new SugarParameter("@kscode", inparameter.keywordo));
            }
            sql += " order by gh.ghrq desc ";
            return SettDb.GetConnection().SqlQueryable<GhJzInfo>(sql).AddParameters(pars.ToArray()).ToList();
        }

        public object GetYbjzdjVo(object p0, object p1)
        {
            return null;
        }

        public object UnSettedRegBackNum(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }

    public class OutPatientUniversalDmnService : IOutPatientUniversalDmnService
    {
        public object AccountPlanRefundAll(object p0, object p1)
        {
            return null;
        }

        public IOutPatientUniversalDmnService_GetSettPatientInfoDto GetSettPatientInfo(object p0, object p1)
        {
            return new IOutPatientUniversalDmnService_GetSettPatientInfoDto();
        }

        public object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out string p6, out decimal p7, object p8, object p9, object p10, object p11, object p12, object p13)
        {
            p5 = null;
            p6 = null;
            p7 = 0;
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out string p6, out decimal p7, object p8, object p9, object p10, object p11, object p12, object p13, object p14, object p15)
        {
            p5 = null;
            p6 = null;
            p7 = 0;
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public List<RefundZtDto> RefundZtDetail(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object Sumwzdj(object p0, object p1, object p2)
        {
            return null;
        }

        public object TuiHuiwzdj(object p0, object p1, object p2)
        {
            return null;
        }

        public object UpdateArrearageSettlement(object p0, object p1, object p2, object p3, object p4)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object UpdateYPcfxm(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public List<IOutPatientUniversalDmnService_pushApplicationformDto> pushApplicationform(object p0, object p1, object p2, object p3)
        {
            return new List<IOutPatientUniversalDmnService_pushApplicationformDto>();
        }

        public object pushApplicationformRef(object p0, object p1, object p2)
        {
            return null;
        }

        public List<IOutPatientUniversalDmnService_retApplicationformDto> retApplicationform(object p0, object p1, object p2, object p3)
        {
            return new List<IOutPatientUniversalDmnService_retApplicationformDto>();
        }

    }

    public class OutpatientPrescriptionRepo : IOutpatientPrescriptionRepo
    {

        public object GeneratePresNo(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public List<IOutpatientPrescriptionRepo_GetByCfnmListDto> GetByCfnmList(object p0, object p1)
        {
            return new List<IOutpatientPrescriptionRepo_GetByCfnmListDto>();
        }

        public OutpatientPrescriptionEntity GetValidEntityByCfnm(object p0, object p1)
        {
            return new OutpatientPrescriptionEntity();
        }

    }

    public class OutpatientRegApp : IOutpatientRegApp
    {
        public object Book(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object CheckBrxz(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object CheckInvoice(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public string GetInvoice()
        {
            return null;
        }

        public object GetRegScheduleList(object p0, object p1)
        {
            return null;
        }

        public object Save(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10, object p11, object p12, object p13, object p14, object p15, object p16, object p17, object p18, ref short? p19, ref string p20, object p21, object p22, object p23, object p24, object p25, object p26, out object p27)
        {
            p27 = null;
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

            public object BackNum(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
        public object GetJZXH(object p0, object p1, object p2, object p3)
        {
            return null;
        }
        public List<LastSettleInfoVO> GetLastSettleInfo(string userCode)
        {
            return new List<LastSettleInfoVO>();
        }
        public object GetOutpatientFees(object p0, object p1, object p2, object p3)
        {
            return null;
        }
        public object GetSettledFeesList(object p0, object p1)
        {
            return null;
        }
        public object GetSettledFeesList(object p0, object p1, object p2)
        {
            return null;
        }
        public OutPatientBasicInfoVO LoadPatientInfo(string kh)
        {
            return null;
        }
        public object Save(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
        public object SaveRegAbandRecord(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
        public object SelectZdList(object p0)
        {
            return null;
        }
}

    public class OutpatientRegistRepo : IOutpatientRegistRepo
    {
        public object AllowRegh(object p0, object p1)
        {
            return null;
        }

        public System.Linq.IQueryable<OutpatientRegistEntity> IQueryable(System.Linq.Expressions.Expression<System.Func<OutpatientRegistEntity, bool>> predicate)
        {
            return new System.Collections.Generic.List<OutpatientRegistEntity>().AsQueryable();
        }

        public object RecordOutpId(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object UpdateChargeMzghInfo(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object UpdatePatPhone(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object updatePatBrxzInfo(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object updateYbghjzId(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

            public string SubmitForm(object p0, object p1, object p2, object p3, object p4, out string p5)
        {
            p5 = default;
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
}

    public class OutpatientRegistScheduleRepo : IOutpatientRegistScheduleRepo
    {
        public object DeleteForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }

    public class OutpatientSettlementGAYBFeeRepo : IOutpatientSettlementGAYBFeeRepo
    {
        public object InsertEntity(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }

    public class OutpatientSettlementRepo : IOutpatientSettlementRepo
    {
        public object UpdateSettedFph(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }



    public class PhysicalexamDmnService : IPhysicalexamDmnService
    {
        public object SubmitPhysicalexamForm(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }

    public class RefundApp : IRefundApp
    {
        public void DetailsUploadYb(string orgId, int jsnm, Dictionary<int, decimal> tjsxmDict, string newwjybjsh, out decimal tmxzje, out decimal yxzje)
        {
            tmxzje = 0;
            yxzje = 0;
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public List<MZJSInfo> GetFPHByKh(string kh, string startTime, string endTime, string fph)
        {
            return new List<MZJSInfo>();
        }

        public List<GridViewMx> GetGridViewMx(int jsnm)
        {
            return new List<GridViewMx>();
        }

        public List<MZJS> GetMZJSByJsnm(int jsnm)
        {
            return new List<MZJS>();
        }

        public bool btnReturn(string kh, List<GridViewMx> RefundVOList, int jsnm, out bool isReturnAll)
        {
            isReturnAll = false;
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public bool btnReturnInAcc(string blh, List<GridViewMx> GridViewMxList, int jsnm, out bool isReturnAll)
        {
            isReturnAll = false;
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    }

    public class RefundDmnService : IRefundDmnService
    {
        public object GetBasicInfoSearchList(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8)
        {
            return null;
        }

        public object GetBasicInfoSearchListInRegister(object p0, object p1)
        {
            return null;
        }

        public object GetBasicInfoSearchListInRegister(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            return null;
        }

        public List<RefundGridMxDto> GetGridViewMx(object p0, object p1)
        {
            return new List<RefundGridMxDto>();
        }

        public object GetjsInfoByblh(object p0, object p1, object p2, object p3)
        {
            return null;
        }

        public object RefundableChongQingQuery(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public List<IRefundDmnService_RefundableGuiAnJsQueryDto> RefundableGuiAnJsQuery(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public List<IRefundDmnService_RefundableJsQueryDto> RefundableJsQuery(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

        public List<OutPatientRefundableVO> RefundableQuery(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }

    
        public object GetBasicInfoSearchListInRegister(string keyword, string orgId)
        {
            throw new NotSupportedException("Chức năng tìm kiếm bệnh nhân đăng ký chưa được triển khai trong bản .NET Core.");
        }
        public object GetBasicInfoSearchList(string keyword, string orgId, string userCode, string p4, string from, string brzybzType)
        {
            throw new NotSupportedException("Chức năng tìm kiếm bệnh nhân ghisổ chưa được triển khai trong bản .NET Core.");
        }
            public object GetZyMzYjjPatientSearch(object p0, object p1, object p2)
        {
            return null;
        }
        public object Getjzjsxx(object p0, object p1, object p2, object p3)
        {
            return null;
        }
}

    public class SyncTreatmentServiceRecordDmnService : ISyncTreatmentServiceRecordDmnService
    {
        public List<SyncTreatmentServiceRecordVO> GetList(object p0, object p1, object mzh, object zyh, object zlsgh)
        {
            return null;
        }

    
        public object GetList(string organizeId, int? clzt = null, string mzh = null, string zyh = null, string zlsgh = null)
        {
            // Merged-interface overload: delegate to the object overload (OUT stub semantics: null).
            return GetList((object)organizeId, (object)clzt, (object)mzh, (object)zyh, (object)zlsgh);
        }
    }

    public class SyncTreatmentServiceRecordRepo : ISyncTreatmentServiceRecordRepo
    {
        public List<SyncTreatmentUnitDto> GetUnitList(object p0, object p1, object p2, object p3, object p4)
        {
            return new List<SyncTreatmentUnitDto>();
        }

            public object GetPagintionList(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10, object p11, object p12, object p13)
        {
            return null;
        }
        public object SaveCancel(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
}


    public class SysDepartmentRepo : ISysDepartmentRepo
    {
        public IList<SysDepartmentVEntity> GetList(string orgId, string keyword = null)
        {
            return new List<SysDepartmentVEntity>();
        }
        public IList<SysDepartmentVEntity> GetList(string orgId, string zt, string keyword)
        {
            return new List<SysDepartmentVEntity>();
        }
        public SysDepartmentVEntity GetByCode(string code, string orgId)
        {
            return null;
        }
        public string GetCodeByName_FirstOrDefault(string name, string orgId)
        {
            return null;
        }
        public string GetNameByCode(string code, string orgId)
        {
            return null;
        }
}

    public class SysPatientBasicInfoRepo : ISysPatientBasicInfoRepo
    {
        public SysPatientBasicInfoEntity FindEntity(System.Linq.Expressions.Expression<System.Func<SysPatientBasicInfoEntity, bool>> predicate)
        {
            return null;
        }

            public object CardVoids(object p0, object p1, object p2)
        {
            return null;
        }
        public List<ISysPatientBasicInfoRepo_GetCardNoInfoDto> GetCardNoInfo(object p0, object p1, object p2)
        {
            return new List<ISysPatientBasicInfoRepo_GetCardNoInfoDto>();
        }
        public object GetDetailsData(object p0, object p1)
        {
            return null;
        }
        public object GetModifyLog(object p0, object p1, object p2)
        {
            return null;
        }
        public object GetPatOnlyBlhSearchList(object p0, object p1, object p2, object p3)
        {
            return null;
        }
        public System.Linq.IQueryable<SysPatientBasicInfoEntity> IQueryable()
        {
            return new System.Collections.Generic.List<SysPatientBasicInfoEntity>().AsQueryable();
        }
        public object SubmitCard(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
        public object UpdateZt(object p0, object p1, object p2, object p3)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
        public object UpdatelxrInfo(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
        public object Updateybxx(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }
}

    public class SysRegistSpecialDiseaseRepo : ISysRegistSpecialDiseaseRepo
    {
        public object SelectSysChargeItemByghzbList(object p0)
        {
            return null;
        }

    }

    public class SysUserDmnService : ISysUserDmnService
    {
        public bool CheckStaffIsBelongDuty(object p0, object p1)
        {
            return false;
        }

        public List<StaffDutyVO> GetStaffByDutyCode(string orgId, string dutyCode)
        {
            return new List<StaffDutyVO>();
        }
        public bool CheckStaffIsBelongDuty(string staffId, string duty)
        {
            return CheckStaffIsBelongDuty((object)staffId, (object)duty);
        }
        public List<SysUserStaffVO> GetSatffVOListByOrg(string orgId, string keyword)
        {
            return new List<SysUserStaffVO>();
        }
}

    public class printOutpatientChargeBillDmnService : IprintOutpatientChargeBillDmnService
    {
        public object GetCqmjzpjJson(object p0, object p1)
        {
            return null;
        }

        public object GetmjzpjJson(object p0, object p1)
        {
            return null;
        }

    }

    public class OutBookScheduleService : IOutBookScheduleService { }
    public class CqybUploadInPres04Repo : ICqybUploadInPres04Repo { }
    public class OutpatientItemRepo : IOutpatientItemRepo { }
}
