using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
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
        // NX23-002 (D-NX23-002): port thật GetList() — vendor Sett KHÔNG CÓ
        // implementation gốc trong source (không có IItemsApp.cs /
        // ItemsApp.cs trong lmsoft_openhis; ItemsTypeController chỉ tham
        // chiếu interface từ assembly biên dịch sẵn) và bảng Sys_Items
        // KHÔNG TỒN TẠI trong Sett DB (NewtouchHIS_Sett: 0 bảng
        // Sys_Items/Sys_ItemsDetail, 0 view Items). Dữ liệu từ điển sống
        // ở Base DB: dbo.Sys_Items 73 rows (72 zt='1', 1 zt='0'
        // BodyTempMed), đọc qua SettDb.GetBaseConnection() như
        // ComController (V_S_Sys_Items) + vendor Base ItemDmnService
        // (Sys_Items zt='1'). Map cột Base (Id/ParentId/Name/Code/
        // Description) -> ItemsEntity F_ (F_ParentId/F_EnCode/F_FullName/
        // F_Description); F_IsTree/F_Layers không có cột tương ứng -> null.
        // Lọc zt='1' như DutyApp.GetList sibling + Base ItemDmnService +
        // ItemsDetailRepository (d.zt=1); ORDER BY px, Name (px NULL trên
        // cả 72 row nên cần sort phụ để cây ổn định). Chỉ đọc;
        // GetForm/SubmitForm/DeleteForm giữ stub (đường ghi cần STG,
        // ngoài scope NX23-002).
        public List<ItemsEntity> GetList()
        {
            return SettDb.GetBaseConnection().SqlQueryable<ItemsEntity>(
                    @"select Id, ParentId as F_ParentId, Code as F_EnCode,
Name as F_FullName, Description as F_Description,
CreateTime, CreatorCode, LastModifyTime, LastModifierCode,
px, zt from dbo.Sys_Items where zt = '1' order by px, Name")
                .ToList();
        }

        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public IItemsApp_GetFormDto GetForm(object p0)
        {
            return new IItemsApp_GetFormDto();
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
        // NX14-001 (D-NX14-001): port thật từ PDS ModuleApp — trước đây class
        // rỗng, interface rỗng; ModuleController.GetFormJson/DeleteForm/
        // GetTreeSelectJson cần 3 method này. DB dịch: Sett DB (SettDb),
        // SQL thô theo mẫu NX8-002/NX9-002 (không dùng EF như PDS).
        public void DeleteForm(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                throw new FailedException("Vui lòng chọn một dòng thông tin.");
            }
            var childCount = SettDb.GetConnection().Ado.GetInt(
                "select count(1) from Sys_Module where ParentId = @id",
                new SugarParameter[] { new SugarParameter("@id", keyValue) });
            if (childCount > 0)
            {
                throw new FailedException("Xóa thất bại! Đối tượng thao tác chứa dữ liệu cấp dưới.");
            }
            SettDb.GetConnection().Ado.ExecuteCommand(
                "delete from Sys_Module where Id = @id",
                new SugarParameter[] { new SugarParameter("@id", keyValue) });
        }

        public SysModuleEntity GetForm(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return null;
            }
            return SettDb.GetConnection().SqlQueryable<SysModuleEntity>(
                    "select * from Sys_Module where Id = @id")
                .AddParameters(new SugarParameter[] { new SugarParameter("@id", keyValue) })
                .First();
        }

        public List<SysModuleEntity> GetValidList()
        {
            return SettDb.GetConnection().SqlQueryable<SysModuleEntity>(
                    "select * from Sys_Module where zt = '1' order by px")
                .ToList();
        }
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
        // NX8-002: 4 method phân quyền cơ sở (Organizes) — port thật theo mẫu
        // PDS SysModuleDmnService. DB dịch: Sett DB (SettDb.GetConnection() =
        // NewtouchHIS_Sett) — xem D-NX8-002: Union không có bảng
        // Sys_OrganizeAuthorize; Sett đã có 319 row (199 ItemId khớp Sett
        // Sys_Module, 0 khớp Union); dbo.SplitToTable tồn tại ở Sett.
        // Đọc đúng kiểu PDS (join [NewtouchHIS_Base]..V_S_Sys_Organize) vì
        // Sett không có bảng/view Sys_Organize local — kiểu Base.Infrastructure
        // (join Sys_Organize local) sẽ lỗi ở Sett DB.
        // Lưu ý: trước NX9, chưa có đường chạy nào của Sett đọc
        // Sys_OrganizeAuthorize (menu sidebar đọc Sys_RoleAuthorize trên
        // Union) — màn Organizes tự nhất quán (ghi -> đọc lại thấy) và sửa
        // luôn luồng ModuleButton type-2 đang có. NX9-002 port consumer:
        // GetOpenMenuListByTopOrg + GetOpenMenuButtonListByTopOrg (cây phân
        // quyền vai trò), GetMenuList (lọc cây Module theo vai trò),
        // GetMenuButtonListByTopOrg (grid nút ModuleButton), xem D-NX9-002.
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

        // NX9-002 (D-NX9-002): 5 method đọc menu/cây — port thật theo mẫu PDS
        // SysModuleDmnService + vendor Sett gốc
        // (lmsoft_openhis/Newtouch.HIS.Sett/.../SysModuleDmnService.cs +
        // SysModuleRepo.cs). DB dịch: Sett DB (SettDb.GetConnection()),
        // như NX8-002 Organizes. ĐỘ LỆCH vs PDS (ghi rõ trong từng method):
        // (1) GetMenuList viết bằng SQL thô (stub class không có field repo
        // IQueryable như PDS); (2) nhánh vai trò KHÔNG lọc ItemType=1 trên
        // Sys_RoleAuthorize vì cột này không tồn tại ở Sett DB (9 cột — cùng
        // drift như DDL NX2 Role, chưa chạy); (3) roleIdList null -> [] thay
        // vì null như PDS (để caller khỏi NRE); (4) nhánh isAdministrator lấy
        // đủ 4 tên của vendor Sett (PDS port thiếu 系统用户).
        public IList<SysModuleButtonVO> GetMenuButtonListByTopOrg(string topOrgId, string moduleId)
        {
            // PDS GetMenuButtonListByTopOrg verbatim (Sys_OrganizeAuthorize
            // CÓ cột ItemType ở Sett DB). Grid ModuleButton/Index.cshtml:48
            // cần cột IsOpenToOrg.
            var sql = @"select * from
(
select a.*, cast(1 as bit) IsOpenToOrg
from Sys_ModuleButton a
left join Sys_OrganizeAuthorize b
on a.Id = b.ItemId
where b.ItemType = 2 and a.ModuleId = @moduleId
and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)
union
(
select a.*, cast(0 as bit) IsOpenToOrg
from Sys_ModuleButton a
where a.ModuleId = @moduleId
and Id not in
(
    select a.Id IsOpenToOrg
    from Sys_ModuleButton a
    left join Sys_OrganizeAuthorize b
    on a.Id = b.ItemId
    where b.ItemType = 2 and a.ModuleId = @moduleId
    and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)
))
) as t";
            return SettDb.GetConnection().SqlQueryable<SysModuleButtonVO>(sql)
                .AddParameters(new SugarParameter[] {
                    new SugarParameter("@topOrganizeId", (object)topOrgId ?? DBNull.Value),
                    new SugarParameter("@moduleId", (object)moduleId ?? DBNull.Value)
                }).ToList();
        }

        public List<SysModuleEntity> GetMenuList(string topOrgId, IList<string> roleIdList, bool isRoot, bool isAdministrator)
        {
            // PDS GetMenuListByTopOrg, viết lại bằng SQL thô qua SettDb.
            if (isRoot)
            {
                // Vendor gốc: 系统管理/系统菜单/菜单管理/FailMsg配置/快捷菜单
                // (tên đã dịch; "Quan ly menu" hiện không có row nào trong
                // Sett Sys_Module nên nhánh này khớp 4/5 tên — vô hại).
                return GetMenuListByNames(new[] { "Quan ly he thong", "Menu he thong", "Quan ly menu", "Cau hinh FailMsg", "Menu tac vu" });
            }
            if (isAdministrator)
            {
                // Vendor gốc: 系统管理/角色管理/系统角色/系统用户 ("Vai tro
                // he thong" hiện không có row nào trong Sett Sys_Module).
                return GetMenuListByNames(new[] { "Quan ly he thong", "Quan ly vai tro", "Vai tro he thong", "Nguoi dung he thong" });
            }
            // Nhánh vai trò: roleIdList null/rỗng -> [] (PDS trả null; trả []
            // để caller ModuleController.GetModuleTreeJsonByName khỏi NRE ở
            // authedList.Any — kết quả hiển thị như nhau: 0 node).
            if (roleIdList == null || roleIdList.Count == 0)
            {
                return new List<SysModuleEntity>();
            }
            var pars = new List<SugarParameter> {
                new SugarParameter("@topOrgId", (object)topOrgId ?? DBNull.Value)
            };
            var inList = new List<string>();
            for (int i = 0; i < roleIdList.Count; i++)
            {
                inList.Add("@r" + i);
                pars.Add(new SugarParameter("@r" + i, roleIdList[i]));
            }
            // ĐỘ LỆCH vs PDS (ghi rõ): Sys_RoleAuthorize ở Sett DB KHÔNG CÓ
            // cột ItemType (9 cột — cùng drift như DDL NX2 Role, chưa chạy)
            // nên KHÔNG lọc ItemType=1 như PDS; an toàn vì Id module vs Id
            // nút là 2 không gian GUID không trùng nhau (khớp ItemId vào
            // Sys_Module.Id thì chỉ module mới khớp).
            var sql = $@"
SELECT a.* FROM Sys_Module a WHERE a.zt = '1'
AND a.Id IN (SELECT ItemId FROM Sys_OrganizeAuthorize WHERE zt = '1' AND ItemType = 1 AND (TopOrganizeId = '*' OR TopOrganizeId = @topOrgId))
AND a.Id IN (SELECT ItemId FROM Sys_RoleAuthorize WHERE zt = '1' AND RoleId IN ({string.Join(",", inList)}))
ORDER BY a.px";
            return SettDb.GetConnection().SqlQueryable<SysModuleEntity>(sql)
                .AddParameters(pars.ToArray()).ToList();
        }

        private List<SysModuleEntity> GetMenuListByNames(string[] names)
        {
            var pars = new List<SugarParameter>();
            var inList = new List<string>();
            for (int i = 0; i < names.Length; i++)
            {
                inList.Add("@n" + i);
                pars.Add(new SugarParameter("@n" + i, names[i]));
            }
            var sql = $"SELECT * FROM Sys_Module WHERE zt = '1' AND Name IN ({string.Join(",", inList)}) ORDER BY px";
            return SettDb.GetConnection().SqlQueryable<SysModuleEntity>(sql)
                .AddParameters(pars.ToArray()).ToList();
        }

        public List<SysModuleButtonEntity> GetOpenMenuButtonListByTopOrg(string topOrgId)
        {
            // PDS GetOpenMenuButtonListByTopOrg verbatim.
            return SettDb.GetConnection().SqlQueryable<SysModuleButtonEntity>(@"select a.*
from Sys_ModuleButton a
left join Sys_OrganizeAuthorize b
on a.Id = b.ItemId
where b.ItemType = 2
and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)")
                .AddParameters(new SugarParameter[] {
                    new SugarParameter("@topOrganizeId", (object)topOrgId ?? DBNull.Value)
                }).ToList();
        }

        public IList<SysModuleEntity> GetOpenMenuListByTopOrg(string topOrgId)
        {
            // PDS GetOpenMenuListByTopOrg verbatim. Dùng bởi
            // RoleAuthorizeController.GetPermissionTree (cây phân quyền vai
            // trò) — đây là consumer đầu tiên của Organizes (NX8-002).
            return SettDb.GetConnection().SqlQueryable<SysModuleEntity>(@"select a.*
from Sys_Module a
left join Sys_OrganizeAuthorize b
on a.Id = b.ItemId
where b.ItemType = 1
and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)")
                .AddParameters(new SugarParameter[] {
                    new SugarParameter("@topOrganizeId", (object)topOrgId ?? DBNull.Value)
                }).ToList();
        }

        // NX14-001 (D-NX14-001): port từ PDS SysModuleDmnService.SubmitForm.
        // PDS dùng EF (Modify/Create + EFDbTransaction); Sett dùng SQL thô
        // qua SettDb (mẫu NX8-002). Update 0 row -> FailedException (không
        // trả success giả — bài học BA-005/UseTranStrict). Insert mới: Id
        // Guid mới + CreateTime/CreatorCode; nếu có topOrgId thì tự động
        // chèn 1 row ủy quyền tổ chức (ItemType=1) như PDS.
        // D-NX24-001: 2 insert (Sys_Module + Sys_OrganizeAuthorize) bọc
        // trong 1 UseTranStrict trên CÙNG 1 connection (var db lấy 1 lần —
        // SettDb.GetConnection() mỗi lần có thể trả provider khác, gọi lại
        // trong lambda sẽ rơi khỏi transaction). Insert 2 lỗi -> rollback
        // cả insert 1 (không còn row module mồ côi), lỗi ném lại cho caller
        // thay vì success giả (gặp D-NX23-002b). Nhánh update (1 lệnh, đã
        // ném FailedException khi 0 row) giữ nguyên, không cần transaction.
        public void SubmitForm(SysModuleEntity moduleEntity, string keyValue, string topOrgId = null)
        {
            if (moduleEntity == null)
            {
                throw new FailedException("Dữ liệu menu trống.");
            }
            var creatorCode = OperatorProvider.GetCurrent() == null
                ? string.Empty
                : OperatorProvider.GetCurrent().UserCode;
            var pars = new SugarParameter[]
            {
                new SugarParameter("@parentId", (object)moduleEntity.ParentId ?? DBNull.Value),
                new SugarParameter("@name", (object)moduleEntity.Name ?? DBNull.Value),
                new SugarParameter("@enName", (object)moduleEntity.EnName ?? DBNull.Value),
                new SugarParameter("@code", (object)moduleEntity.Code ?? DBNull.Value),
                new SugarParameter("@icon", (object)moduleEntity.Icon ?? DBNull.Value),
                new SugarParameter("@urlAddress", (object)moduleEntity.UrlAddress ?? DBNull.Value),
                new SugarParameter("@target", (object)moduleEntity.Target ?? DBNull.Value),
                new SugarParameter("@px", (object)moduleEntity.px ?? DBNull.Value),
                new SugarParameter("@description", (object)moduleEntity.Description ?? DBNull.Value),
                new SugarParameter("@zt", (object)moduleEntity.zt ?? DBNull.Value),
                new SugarParameter("@organizeId", (object)moduleEntity.OrganizeId ?? DBNull.Value),
                new SugarParameter("@appId", (object)moduleEntity.AppId ?? DBNull.Value),
                new SugarParameter("@creatorCode", (object)creatorCode ?? DBNull.Value),
            };
            if (!string.IsNullOrEmpty(keyValue))
            {
                var allPars = pars.Concat(new SugarParameter[]
                {
                    new SugarParameter("@id", keyValue),
                }).ToArray();
                var affected = SettDb.GetConnection().Ado.ExecuteCommand(
                    @"update Sys_Module set ParentId = @parentId, Name = @name,
EnName = @enName, Code = @code, Icon = @icon, UrlAddress = @urlAddress,
Target = @target, px = @px, Description = @description, zt = @zt,
OrganizeId = @organizeId, AppId = @appId,
LastModifyTime = GETDATE(), LastModifierCode = @creatorCode
where Id = @id", allPars);
                if (affected == 0)
                {
                    throw new FailedException("Không tìm thấy menu cần sửa.");
                }
            }
            else
            {
                var newId = Guid.NewGuid().ToString();
                var insertPars = pars.Concat(new SugarParameter[]
                {
                    new SugarParameter("@id", newId),
                }).ToArray();
                var db = SettDb.GetConnection();
                db.Ado.UseTranStrict(() =>
                {
                    db.Ado.ExecuteCommand(
                        @"insert into Sys_Module(Id, ParentId, Name, EnName, Code, Icon,
UrlAddress, Target, px, Description, CreateTime, CreatorCode, zt, OrganizeId, AppId)
values(@id, @parentId, @name, @enName, @code, @icon, @urlAddress, @target,
@px, @description, GETDATE(), @creatorCode, @zt, @organizeId, @appId)", insertPars);
                    if (!string.IsNullOrWhiteSpace(topOrgId))
                    {
                        db.Ado.ExecuteCommand(
                            @"insert into Sys_OrganizeAuthorize(Id, TopOrganizeId, ItemId, ItemType, CreateTime, CreatorCode, zt)
values(newid(), @topOrganizeId, @moduleId, 1, GETDATE(), @creatorCode, '1')",
                            new SugarParameter[]
                            {
                                new SugarParameter("@topOrganizeId", topOrgId),
                                new SugarParameter("@moduleId", newId),
                                new SugarParameter("@creatorCode", (object)creatorCode ?? DBNull.Value),
                            });
                    }
                });
            }
        }

        // NX14-001 (D-NX14-001): grid Module/Index.cshtml cần toàn bộ module +
        // cột IsOpenToOrg. Mẫu UNION của GetMenuButtonListByTopOrg (NX9-002),
        // áp dụng cho Sys_Module, ItemType=1; lọc keyword theo Name như PDS.
        public IList<SysModuleVO> GetTreeGridList(string topOrgId, string keyword)
        {
            var sql = @"select * from
(
select a.*, cast(1 as bit) IsOpenToOrg
from Sys_Module a
left join Sys_OrganizeAuthorize b
on a.Id = b.ItemId
where b.ItemType = 1
and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)
union
select a.*, cast(0 as bit) IsOpenToOrg
from Sys_Module a
where a.Id not in
(
    select a.Id
    from Sys_Module a
    left join Sys_OrganizeAuthorize b
    on a.Id = b.ItemId
    where b.ItemType = 1
    and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)
)
) as t order by px";
            var data = SettDb.GetConnection().SqlQueryable<SysModuleVO>(sql)
                .AddParameters(new SugarParameter[] {
                    new SugarParameter("@topOrganizeId", (object)topOrgId ?? DBNull.Value)
                }).ToList();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(t => (t.Name ?? "").Contains(keyword)).ToList();
            }
            return data;
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
            // NX8-002: orgList null coi như rỗng (xóa hết) thay vì Trim(',') ném
            // NRE như gốc PDS — dùng ngữ nghĩa nhánh rỗng đã có trong code
            // vendor (chọn 0 tổ chức = thu hồi hết).
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
        // NX9-002 (D-NX9-002): port thật theo vendor Sett gốc
        // (lmsoft_openhis/Newtouch.HIS.Sett/Newtouch.Repository/SystemManage/
        // SysModuleRepo.cs:29) verbatim — module có tên @name + các con của
        // nó (zt='1'). Dùng bởi ModuleController.GetModuleTreeJsonByName.
        public List<SysModuleEntity> GetModuleTreeJsonByName(string name)
        {
            var sql = @"SELECT *
                            FROM Sys_Module
                            WHERE (parentId = (SELECT Id
                                                FROM Sys_Module
                                                WHERE Name = @name
                                                 )
                                or Name = @name)
and zt = '1'";
            return SettDb.GetConnection().SqlQueryable<SysModuleEntity>(sql)
                .AddParameters(new SugarParameter[] {
                    new SugarParameter("@name", (object)name ?? DBNull.Value)
                }).ToList();
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

        // SHELL-A: port thật từ vendor
        // Newtouch.HIS.DomainServices/SystemManage/SysRoleShortcutMenuDmnService.cs
        // — SQL verbatim, chỉ đổi FindList EF thành SettDb SqlQueryable; chỉ
        // đọc. Được Home/Default gọi qua ViewBag.ShortcutMenuList.
        public IList<SysShortcutMenuEntity> GetAuthedSCMList(IList<string> roleIdList, string orgId)
        {
            if (roleIdList == null || roleIdList.Count == 0 || string.IsNullOrWhiteSpace(orgId))
            {
                return new List<SysShortcutMenuEntity>();
            }
            var sql = @"select * from (select distinct a.* from Sys_ShortcutMenu a
left join Sys_RoleShortcutMenu b
on a.Id = b.SCMId
where a.zt = '1' and b.zt = '1' and b.OrganizeId = @orgId
and b.RoleId in (select * from dbo.f_split(@jsnmStr, ','))
) as sc
order by isnull(px, 99999), CreateTime desc";
            return SettDb.GetConnection().SqlQueryable<SysShortcutMenuEntity>(sql)
                .AddParameters(new SugarParameter[]
                {
                    new SugarParameter("@orgId", orgId),
                    new SugarParameter("@jsnmStr", string.Join(",", roleIdList)),
                }).ToList();
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

        public IList<SysRoleEntity> GetUserRoleList(string userId)
        {
            // NX10-002 (D-NX10-002): port thật từ vendor Sett
            // UserRoleAuthDmnService.GetUserRoleList, SQL gốc verbatim trừ
            // điều kiện a.OrganizeId = @orgId (cột không tồn tại trong Sett
            // Sys_UserRole — 9 cột; PDS port cùng bỏ tham số org). DB dịch:
            // Sett DB qua SettDb (Sys_UserRole + Sys_Role local; join
            // [NewtouchHIS_Base]..V_C_Sys_User chỉ như vendor, không lọc).
            // userId null/rỗng -> [] (không trả null, tránh NRE ở caller).
            if (string.IsNullOrWhiteSpace(userId))
            {
                return new List<SysRoleEntity>();
            }
            var sql = @"select distinct c.* from Sys_UserRole a
left join [NewtouchHIS_Base].[dbo].V_C_Sys_User b
on a.UserId = b.Id
left join Sys_Role c
on a.RoleId = c.Id
where a.UserId = @userId and a.zt = '1' and c.zt = '1'";
            return SettDb.GetConnection().SqlQueryable<SysRoleEntity>(sql)
                .AddParameters(new SugarParameter[] {
                    new SugarParameter("@userId", userId)
                }).ToList();
        }

    }
}
