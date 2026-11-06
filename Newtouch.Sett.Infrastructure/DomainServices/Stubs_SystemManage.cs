using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using NewtouchHIS.Lib.Framework.Operator;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.Sett.DomainServices
{
    public class AreaApp : IAreaApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public IAreaApp_GetFormDto GetForm(object p0)
        {
            return new IAreaApp_GetFormDto();
        }

        public List<AreaEntity> GetList()
        {
            return new List<AreaEntity>();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class DutyApp : IDutyApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public IDutyApp_GetFormDto GetForm(object p0)
        {
            return new IDutyApp_GetFormDto();
        }

        public List<IDutyApp_GetListDto> GetList()
        {
            return new List<IDutyApp_GetListDto>();
        }

        public List<IDutyApp_GetListDto> GetList(object p0, object p1)
        {
            return new List<IDutyApp_GetListDto>();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class FinanceReceiptRepo : IFinanceReceiptRepo
    {
        public object DeleteForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object GetFinancialInvoiceEntity(object p0, object p1)
        {
            return null;
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object VlidateRepeat(object p0, object p1, object p2)
        {
            return null;
        }

    }

    public class FinancialInvoiceRepo : IFinancialInvoiceRepo
    {
        public object DeleteForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object GetFinancialInvoiceEntity(object p0, object p1)
        {
            return null;
        }

        public object GetFinancialInvoiceList(object p0, object p1)
        {
            return null;
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object VlidateRepeat(object p0, object p1, object p2)
        {
            return null;
        }

    }

    public class InpatientAccountManageApp : IInpatientAccountManageApp
    {
        public InpatientReserveDto GetHosPatAccInfo(object p0, object p1)
        {
            return null;
        }

        public List<InpatientPatAccPayVO> GetPayList(object p0, object p1)
        {
            return new List<InpatientPatAccPayVO>();
        }

        public bool PayDepositPost(object p0, out string p1)
        {
            p1 = null;
            return false;
        }

        public bool RefundAccount(object p0)
        {
            return false;
        }

        public bool ReturnDepositPost(object p0)
        {
            return false;
        }

    }

    public class InpatientAccountRepo : IInpatientAccountRepo
    {
        public InpatientAccountEntity FindEntity(System.Func<InpatientAccountEntity, bool> predicate)
        {
            return null;
        }

        public System.Linq.IQueryable<InpatientAccountEntity> IQueryable()
        {
            return new List<InpatientAccountEntity>().AsQueryable();
        }

        public object Insert(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class ItemsApp : IItemsApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public IItemsApp_GetFormDto GetForm(object p0)
        {
            return new IItemsApp_GetFormDto();
        }

        public List<ItemsEntity> GetList()
        {
            return new List<ItemsEntity>();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class ItemsDetailApp : IItemsDetailApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public IItemsDetailApp_GetFormDto GetForm(object p0)
        {
            return new IItemsDetailApp_GetFormDto();
        }

        public List<ItemsDetailEntity> GetItemList(object p0)
        {
            return new List<ItemsDetailEntity>();
        }

        public List<IItemsDetailApp_GetListDto> GetList(object p0, object p1)
        {
            return new List<IItemsDetailApp_GetListDto>();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class ModuleApp : IModuleApp
    {
    }

    public class ModuleButtonApp : IModuleButtonApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public IModuleButtonApp_GetFormDto GetForm(object p0)
        {
            return new IModuleButtonApp_GetFormDto();
        }

    }

    public class MoneyUpperLimitReminderRepo : IMoneyUpperLimitReminderRepo
    {
        public object DeleteForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object SubmitForm(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class RoleApp : IRoleApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public IRoleApp_GetFormDto GetForm(object p0)
        {
            return new IRoleApp_GetFormDto();
        }

        public object GetPagintionList(object p0, object p1, object p2)
        {
            return null;
        }

        public object SubmitForm(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class RoleAuthorizeApp : IRoleAuthorizeApp
    {
        public List<SysRoleAuthorizeEntity> GetValidList(object p0)
        {
            return new List<SysRoleAuthorizeEntity>();
        }

    }

    public class SysAgInsurChargeCategApp : ISysAgInsurChargeCategApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object GetEffectiveList(object p0)
        {
            return null;
        }

        public ISysAgInsurChargeCategApp_GetFormDto GetForm(object p0)
        {
            return new ISysAgInsurChargeCategApp_GetFormDto();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysCIRemindContentApp : ISysCIRemindContentApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public ISysCIRemindContentApp_GetFormDto GetForm(object p0)
        {
            return new ISysCIRemindContentApp_GetFormDto();
        }

        public List<ISysCIRemindContentApp_GetListBySearchDto> GetListBySearch(object p0, object p1)
        {
            return new List<ISysCIRemindContentApp_GetListBySearchDto>();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysCISpecialMarkApp : ISysCISpecialMarkApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public ISysCISpecialMarkApp_GetFormDto GetForm(object p0)
        {
            return new ISysCISpecialMarkApp_GetFormDto();
        }

        public List<ISysCISpecialMarkApp_GetListBySearchDto> GetListBySearch(object p0, object p1)
        {
            return new List<ISysCISpecialMarkApp_GetListBySearchDto>();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysChargeAddCategApp : ISysChargeAddCategApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object GetEffectiveList(object p0)
        {
            return null;
        }

        public string GetSysChargeAddCategSelect()
        {
            return null;
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysChargeCategoryRepo : ISysChargeCategoryRepo
    {
        public List<SysChargeCategoryVEntity> GetLazyList(object p0)
        {
            return new List<SysChargeCategoryVEntity>();
        }

        public IList<SysChargeCategoryVEntity> GetList(string orgId, string dllbs = "", string zt = "")
        {
            return new List<SysChargeCategoryVEntity>();
        }


        public List<ISysChargeCategoryRepo_GetListBySearchDto> GetListBySearch(object p0, object p1)
        {
            return new List<ISysChargeCategoryRepo_GetListBySearchDto>();
        }

    }

    public class SysChargeClassifyApp : ISysChargeClassifyApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public SysChargeClassificationEntity GetForm(object p0)
        {
            return null;
        }

        public List<ISysChargeClassifyApp_GetListBySearchDto> GetListBySearch(object p0, object p1)
        {
            return new List<ISysChargeClassifyApp_GetListBySearchDto>();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysChargeItemApp : ISysChargeItemApp
    {
    }

    public class SysChargeItemDmnService : ISysChargeItemDmnService
    {
        public List<SysChargeCategoryVEntity> GetSFDLList(object p0)
        {
            return new List<SysChargeCategoryVEntity>();
        }

        public object GetYF()
        {
            return null;
        }

        public object Search(object p0, object p1, object p2, object p3)
        {
            return null;
        }

        public object SelectItemAndMedicineSearch(object p0, object p1, object p2, object p3)
        {
            return null;
        }

        public object SelectSearch(object p0, object p1, object p2, object p3)
        {
            return null;
        }

    }

    public class SysChargeItemRepo : ISysChargeItemRepo
    {
    }

    public class SysChargeMajorClassApp : ISysChargeMajorClassApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public SysChargeCategoryEntity GetForm(object p0)
        {
            return null;
        }

        public string GetsfdlTreeSelectJson()
        {
            return "[]";
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysChargeProjPriceAdjApp : ISysChargeProjPriceAdjApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object GetEffectiveList(object p0)
        {
            return null;
        }

        public ISysChargeProjPriceAdjApp_GetFormDto GetForm(object p0)
        {
            return new ISysChargeProjPriceAdjApp_GetFormDto();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysDepartmentBindingDmnService : ISysDepartmentBindingDmnService
    {
        public SysDepartmentBindingVO GetSaffEntity(object p0, object p1)
        {
            return null;
        }

    }

    public class SysDepartmentBindingRepo : ISysDepartmentBindingRepo
    {
        public object DeleteForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public SysDepartmentBindingEntity FindEntity(System.Func<SysDepartmentBindingEntity, bool> predicate)
        {
            return null;
        }

        public object GetSysDepartmentBindingEntity(object p0, object p1)
        {
            return null;
        }

        public object GetSysDepartmentBindingList(object p0, object p1)
        {
            return null;
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysFailedCodeMessageMappRepo : ISysFailedCodeMessageMappRepo
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public ISysFailedCodeMessageMappRepo_FindEntityDto FindEntity(object p0)
        {
            return new ISysFailedCodeMessageMappRepo_FindEntityDto();
        }

        public object GetPagintionList(object p0, object p1)
        {
            return null;
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysFeeDmnService : ISysFeeDmnService
    {
        public object DeleteById(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysFinancialDmnService : ISysFinancialDmnService
    {
        public object GetCwfpList(object p0, object p1)
        {
            return null;
        }

        public object GetFinancialInvoiceList(object p0, object p1)
        {
            return null;
        }

    }

    public class SysForCashPayApp : ISysForCashPayApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public ISysForCashPayApp_GetFormDto GetForm(object p0)
        {
            return new ISysForCashPayApp_GetFormDto();
        }

        public List<ISysForCashPayApp_GetListDto> GetList()
        {
            return new List<ISysForCashPayApp_GetListDto>();
        }

        public List<ISysForCashPayApp_GetListBySearchDto> GetListBySearch(object p0)
        {
            return new List<ISysForCashPayApp_GetListBySearchDto>();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysMRChargeClassApp : ISysMRChargeClassApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object GetEffectiveList(object p0)
        {
            return null;
        }

        public ISysMRChargeClassApp_GetFormDto GetForm(object p0)
        {
            return new ISysMRChargeClassApp_GetFormDto();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysMSItemComparedApp : ISysMSItemComparedApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public ISysMSItemComparedApp_GetFormDto GetForm(object p0)
        {
            return new ISysMSItemComparedApp_GetFormDto();
        }

        public List<ISysMSItemComparedApp_GetListBySearchDto> GetListBySearch(object p0)
        {
            return new List<ISysMSItemComparedApp_GetListBySearchDto>();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysMedicinApp : ISysMedicinApp
    {
        public object GetYpList(object p0)
        {
            return null;
        }

    }

    public class SysModuleDmnService : ISysModuleDmnService
    {
        // NX8-002: 4 method phan quyen co so (Organizes) — port that theo mau
        // PDS SysModuleDmnService. DB dich: Sett DB (SettDb.GetConnection() =
        // NewtouchHIS_Sett) — xem D-NX8-002: Union khong co bang
        // Sys_OrganizeAuthorize; Sett da co 319 row (199 ItemId khop Sett
        // Sys_Module, 0 khop Union); dbo.SplitToTable ton tai o Sett.
        // Doc dung kieu PDS (join [NewtouchHIS_Base]..V_S_Sys_Organize) vi
        // Sett khong co bang/view Sys_Organize local — kieu Base.Infrastructure
        // (join Sys_Organize local) se loi o Sett DB.
        // Luu y: chua co duong chay nao cua Sett doc Sys_OrganizeAuthorize
        // (menu sidebar doc Sys_RoleAuthorize tren Union) — man Organizes tu
        // nhat quan (ghi -> doc lai thay) va sua luon luong ModuleButton
        // type-2 dang co (truoc day doc tra [], ghi nem NotSupportedException);
        // consumer GetOpenMenuListByTopOrg van stub, de NX9.
        public void AuthAllOrganize(string moduleId, int itemType)
        {
            if (string.IsNullOrWhiteSpace(moduleId))
            {
                return;
            }
            var sql = @"
if exists (select 1 from Sys_Module where Id = @moduleId and @itemType = 1
union select 1 from Sys_ModuleButton where Id = @moduleId and @itemType = 2)
begin
    delete from Sys_OrganizeAuthorize where ItemId = @moduleId and ItemType = @itemType
    insert into Sys_OrganizeAuthorize(Id, TopOrganizeId, ItemId, ItemType, CreateTime, CreatorCode,zt)
    values(newid(), '*', @moduleId, @itemType, GETDATE(), @creatorCode, '1')
end";
            SettDb.GetConnection().Ado.ExecuteCommand(sql, new SugarParameter[] {
                    new SugarParameter("@moduleId", moduleId),
                    new SugarParameter("@itemType", itemType),
                    new SugarParameter("@creatorCode", OperatorProvider.GetCurrent().UserCode)
                });
        }

        public void AuthCancelAllOrganize(string moduleId, int itemType)
        {
            if (string.IsNullOrWhiteSpace(moduleId))
            {
                return;
            }
            var sql = @"delete from Sys_OrganizeAuthorize where ItemId = @moduleId and ItemType = @itemType";
            SettDb.GetConnection().Ado.ExecuteCommand(sql, new SugarParameter[] {
                    new SugarParameter("@moduleId", moduleId),
                    new SugarParameter("@itemType", itemType)
                });
        }

        public IList<SysOrganizeVEntity> GetAuthedOrgListByModuleId(string itemId, int itemType)
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                return new List<SysOrganizeVEntity>();
            }
            var sql = @"if exists (select 1 from Sys_OrganizeAuthorize where ItemId = @itemId and ItemType = @itemType and TopOrganizeId = '*')
begin 
    --Tất cả tổ chức cấp cao nhất
	select * from [NewtouchHIS_Base]..V_S_Sys_Organize where ParentId is null
end
else
	select distinct b.* from Sys_OrganizeAuthorize a
	left join [NewtouchHIS_Base]..V_S_Sys_Organize b
	on a.TopOrganizeId = b.Id
	where a.ItemId = @itemId and a.ItemType = @itemType";
            return SettDb.GetConnection().SqlQueryable<SysOrganizeVEntity>(sql)
                .AddParameters(new SugarParameter[] {
                    new SugarParameter("@itemId", itemId),
                    new SugarParameter("@itemType", itemType)
                }).ToList();
        }

        public object GetMenuButtonListByTopOrg(object p0, object p1)
        {
            return null;
        }

        public List<SysModuleEntity> GetMenuList(object p0, object p1, object p2, object p3)
        {
            return new List<SysModuleEntity>();
        }

        public List<SysModuleButtonEntity> GetOpenMenuButtonListByTopOrg(object p0)
        {
            return new List<SysModuleButtonEntity>();
        }

        public IList<SysModuleEntity> GetOpenMenuListByTopOrg(object p0)
        {
            return new List<SysModuleEntity>();
        }

        public object ModuleButtonSubmitForm(object p0, object p1, object p2)
        {
            return null;
        }

        public void UpdateAuthOrganizeList(string moduleId, string orgList, int itemType)
        {
            if (string.IsNullOrWhiteSpace(moduleId))
            {
                return;
            }
            // NX8-002: orgList null coi nhu rong (xoa het) thay vi Trim(',') nem
            // NRE nhu goc PDS — dung ngu nghia nhanh rong da co trong code
            // vendor (chon 0 to chuc = thu hoi het).
            var orgIdArr = (orgList ?? "").Trim(',');
            if (string.IsNullOrWhiteSpace(orgIdArr))
            {
                var sql = @"
delete from Sys_OrganizeAuthorize where ItemId = @moduleId and ItemType = @itemType
";
                SettDb.GetConnection().Ado.ExecuteCommand(sql, new SugarParameter[] {
                    new SugarParameter("@moduleId", moduleId),
                    new SugarParameter("@itemType", itemType),
                });
            }
            else
            {
                var sql = @"
delete from Sys_OrganizeAuthorize where ItemId = @moduleId and ItemType = @itemType
insert into Sys_OrganizeAuthorize(Id, TopOrganizeId, ItemId, ItemType, CreateTime, CreatorCode,zt)
select newid(), Id, @moduleId, @itemType, GETDATE(), @creatorCode, '1'
from [NewtouchHIS_Base]..V_S_Sys_Organize where Id in (
    select value from dbo.SplitToTable(@orgIdArr, ',')
)";
                SettDb.GetConnection().Ado.ExecuteCommand(sql, new SugarParameter[] {
                    new SugarParameter("@moduleId", moduleId),
                    new SugarParameter("@itemType", itemType),
                    new SugarParameter("@creatorCode", OperatorProvider.GetCurrent().UserCode),
                    new SugarParameter("@orgIdArr", "") { DbType = System.Data.DbType.AnsiString, Size = -1, Value = orgIdArr }
                });
            }
        }

    }

    public class SysModuleExRepo : ISysModuleExRepo
    {
        public List<SysModuleEntity> GetModuleTreeJsonByName(object p0)
        {
            return new List<SysModuleEntity>();
        }

    }

    public class SysParameterConfigApp : ISysParameterConfigApp
    {
        public object ChargeTemplate_SubmitForm(object p0, object p1)
        {
            return null;
        }

        public SysChargeTemplateInfoVM GetSysChargeTemplateInfo(object p0)
        {
            return null;
        }

    }

    public class SysPatiChargeAddDmnService : ISysPatiChargeAddDmnService
    {
    }

    public class SysPatiChargeLogicApp : ISysPatiChargeLogicApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public ISysPatiChargeLogicApp_GetFormDto GetForm(object p0)
        {
            return new ISysPatiChargeLogicApp_GetFormDto();
        }

        public List<ISysPatiChargeLogicApp_GetListDto> GetList(object p0, object p1, object p2)
        {
            return new List<ISysPatiChargeLogicApp_GetListDto>();
        }

        public object GetSFXMItemInfoByDlCode(object p0, object p1, object p2)
        {
            return null;
        }

        public object SubmitForm(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysPatiChargeRangeApp : ISysPatiChargeRangeApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object GetEffectiveList(object p0)
        {
            return null;
        }

        public List<SysPatientChargeRangeEntity> GetEffectiveList(object p0, object p1)
        {
            return new List<SysPatientChargeRangeEntity>();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysPatiNatureApp : ISysPatiNatureApp
    {
        public string GetbrxzSelect(object p0)
        {
            return null;
        }

    }

    public class SysRoleRepo : ISysRoleRepo
    {
        public System.Linq.IQueryable<SysRoleEntity> IQueryable(System.Linq.Expressions.Expression<System.Func<SysRoleEntity, bool>> predicate)
        {
            return new List<SysRoleEntity>().AsQueryable();
        }

    }

    public class SysRoleShortcutMenuDmnService : ISysRoleShortcutMenuDmnService
    {
        public List<string> GetAuthedRoleIdList(object p0, object p1)
        {
            return new List<string>();
        }

        public object UpdateAuthRoleList(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysShortcutMenuRepo : ISysShortcutMenuRepo
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public ISysShortcutMenuRepo_FindEntityDto FindEntity(object p0)
        {
            return new ISysShortcutMenuRepo_FindEntityDto();
        }

        public List<ISysShortcutMenuRepo_GetListDto> GetList(object p0)
        {
            return new List<ISysShortcutMenuRepo_GetListDto>();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysUserRoleRepo : ISysUserRoleRepo
    {
        public List<string> GetRoleIdListByUserId(object p0)
        {
            return new List<string>();
        }

    }

    public class TTCataloguesComparisonDetailRepo : ITTCataloguesComparisonDetailRepo
    {
        public object Delete(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public ITTCataloguesComparisonDetailRepo_FindEntityDto FindEntity(object p0)
        {
            return new ITTCataloguesComparisonDetailRepo_FindEntityDto();
        }

        public object GetListByMainId(object p0, object p1)
        {
            return null;
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class TTCataloguesComparisonDmnService : ITTCataloguesComparisonDmnService
    {
        public List<TTCataloguesComparisonMainEntity> GetValidMainList(object p0)
        {
            return new List<TTCataloguesComparisonMainEntity>();
        }

    }

    public class TTCataloguesComparisonMainRepo : ITTCataloguesComparisonMainRepo
    {
        public object Delete(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public ITTCataloguesComparisonMainRepo_FindEntityDto FindEntity(object p0)
        {
            return new ITTCataloguesComparisonMainRepo_FindEntityDto();
        }

        public List<ITTCataloguesComparisonMainRepo_GetListDto> GetList(object p0)
        {
            return new List<ITTCataloguesComparisonMainRepo_GetListDto>();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class UserRoleAuthDmnService : IUserRoleAuthDmnService
    {
        public object submitRoleUser(object p0, object p1)
        {
            return null;
        }

    }
}
