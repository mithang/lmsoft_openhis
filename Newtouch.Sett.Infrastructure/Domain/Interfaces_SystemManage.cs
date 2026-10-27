using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public interface IAreaApp
    {
        object DeleteForm(object p0);
        IAreaApp_GetFormDto GetForm(object p0);
        List<AreaEntity> GetList();
        object SubmitForm(object p0, object p1);
    }

    public interface IDutyApp
    {
        object DeleteForm(object p0);
        IDutyApp_GetFormDto GetForm(object p0);
        List<IDutyApp_GetListDto> GetList();
        List<IDutyApp_GetListDto> GetList(object p0, object p1);
        object SubmitForm(object p0, object p1);
    }

    public interface IFinanceReceiptRepo
    {
        object DeleteForm(object p0, object p1);
        object GetFinancialInvoiceEntity(object p0, object p1);
        object SubmitForm(object p0, object p1);
        object VlidateRepeat(object p0, object p1, object p2);
    }

    public interface IFinancialInvoiceRepo
    {
        object DeleteForm(object p0, object p1);
        object GetFinancialInvoiceEntity(object p0, object p1);
        object GetFinancialInvoiceList(object p0, object p1);
        object SubmitForm(object p0, object p1);
        object VlidateRepeat(object p0, object p1, object p2);
    }

    public interface IInpatientAccountManageApp
    {
        InpatientReserveDto GetHosPatAccInfo(object p0, object p1);
        List<InpatientPatAccPayVO> GetPayList(object p0, object p1);
        bool PayDepositPost(object p0, out string p1);
        bool RefundAccount(object p0);
        bool ReturnDepositPost(object p0);
    }

    public interface IInpatientAccountRepo
    {
        InpatientAccountEntity FindEntity(System.Func<InpatientAccountEntity, bool> predicate);
        System.Linq.IQueryable<InpatientAccountEntity> IQueryable();
        object Insert(object p0);
    }

    public interface IItemsApp
    {
        object DeleteForm(object p0);
        IItemsApp_GetFormDto GetForm(object p0);
        List<ItemsEntity> GetList();
        object SubmitForm(object p0, object p1);
    }

    public interface IItemsDetailApp
    {
        object DeleteForm(object p0);
        IItemsDetailApp_GetFormDto GetForm(object p0);
        List<ItemsDetailEntity> GetItemList(object p0);
        List<IItemsDetailApp_GetListDto> GetList(object p0, object p1);
        object SubmitForm(object p0, object p1);
    }

    public interface IModuleApp
    {
        // no calls observed
    }

    public interface IModuleButtonApp
    {
        object DeleteForm(object p0);
        IModuleButtonApp_GetFormDto GetForm(object p0);
    }

    public interface IMoneyUpperLimitReminderRepo
    {
        object DeleteForm(object p0, object p1);
        object SubmitForm(object p0, object p1, object p2);
    }

    public interface IRoleApp
    {
        object DeleteForm(object p0);
        IRoleApp_GetFormDto GetForm(object p0);
        object GetPagintionList(object p0, object p1, object p2);
        object SubmitForm(object p0, object p1, object p2);
    }

    public interface IRoleAuthorizeApp
    {
        List<SysRoleAuthorizeEntity> GetValidList(object p0);
    }

    public interface ISysAgInsurChargeCategApp
    {
        object DeleteForm(object p0);
        object GetEffectiveList(object p0);
        ISysAgInsurChargeCategApp_GetFormDto GetForm(object p0);
        object SubmitForm(object p0, object p1);
    }

    public interface ISysCIRemindContentApp
    {
        object DeleteForm(object p0);
        ISysCIRemindContentApp_GetFormDto GetForm(object p0);
        List<ISysCIRemindContentApp_GetListBySearchDto> GetListBySearch(object p0, object p1);
        object SubmitForm(object p0, object p1);
    }

    public interface ISysCISpecialMarkApp
    {
        object DeleteForm(object p0);
        ISysCISpecialMarkApp_GetFormDto GetForm(object p0);
        List<ISysCISpecialMarkApp_GetListBySearchDto> GetListBySearch(object p0, object p1);
        object SubmitForm(object p0, object p1);
    }

    public interface ISysChargeAddCategApp
    {
        object DeleteForm(object p0);
        object GetEffectiveList(object p0);
        string GetSysChargeAddCategSelect();
        object SubmitForm(object p0, object p1);
    }

    public interface ISysChargeCategoryRepo
    {
        List<SysChargeCategoryVEntity> GetLazyList(object p0);
        IList<SysChargeCategoryVEntity> GetList(string orgId, string dllbs = "", string zt = "");
        
        List<ISysChargeCategoryRepo_GetListBySearchDto> GetListBySearch(object p0, object p1);
    }

    public interface ISysChargeClassifyApp
    {
        object DeleteForm(object p0);
        SysChargeClassificationEntity GetForm(object p0);
        List<ISysChargeClassifyApp_GetListBySearchDto> GetListBySearch(object p0, object p1);
        object SubmitForm(object p0, object p1);
    }

    public interface ISysChargeItemApp
    {
        // no calls observed
    }

    public interface ISysChargeItemDmnService
    {
        List<SysChargeCategoryVEntity> GetSFDLList(object p0);
        object GetYF();
        object Search(object p0, object p1, object p2, object p3);
        object SelectItemAndMedicineSearch(object p0, object p1, object p2, object p3);
        object SelectSearch(object p0, object p1, object p2, object p3);
    }

    public interface ISysChargeItemRepo
    {
        // no calls observed
    }

    public interface ISysChargeMajorClassApp
    {
        object DeleteForm(object p0);
        SysChargeCategoryEntity GetForm(object p0);
        string GetsfdlTreeSelectJson();
        object SubmitForm(object p0, object p1);
    }

    public interface ISysChargeProjPriceAdjApp
    {
        object DeleteForm(object p0);
        object GetEffectiveList(object p0);
        ISysChargeProjPriceAdjApp_GetFormDto GetForm(object p0);
        object SubmitForm(object p0, object p1);
    }

    public interface ISysDepartmentBindingDmnService
    {
        SysDepartmentBindingVO GetSaffEntity(object p0, object p1);
    }

    public interface ISysDepartmentBindingRepo
    {
        object DeleteForm(object p0, object p1);
        SysDepartmentBindingEntity FindEntity(System.Func<SysDepartmentBindingEntity, bool> predicate);
        object GetSysDepartmentBindingEntity(object p0, object p1);
        object GetSysDepartmentBindingList(object p0, object p1);
        object SubmitForm(object p0, object p1);
    }

    public interface ISysFailedCodeMessageMappRepo
    {
        object DeleteForm(object p0);
        ISysFailedCodeMessageMappRepo_FindEntityDto FindEntity(object p0);
        object GetPagintionList(object p0, object p1);
        object SubmitForm(object p0, object p1);
    }

    public interface ISysFeeDmnService
    {
        object DeleteById(object p0, object p1);
    }

    public interface ISysFinancialDmnService
    {
        object GetCwfpList(object p0, object p1);
        object GetFinancialInvoiceList(object p0, object p1);
    }

    public interface ISysForCashPayApp
    {
        object DeleteForm(object p0);
        ISysForCashPayApp_GetFormDto GetForm(object p0);
        List<ISysForCashPayApp_GetListDto> GetList();
        List<ISysForCashPayApp_GetListBySearchDto> GetListBySearch(object p0);
        object SubmitForm(object p0, object p1);
    }

    public interface ISysMRChargeClassApp
    {
        object DeleteForm(object p0);
        object GetEffectiveList(object p0);
        ISysMRChargeClassApp_GetFormDto GetForm(object p0);
        object SubmitForm(object p0, object p1);
    }

    public interface ISysMSItemComparedApp
    {
        object DeleteForm(object p0);
        ISysMSItemComparedApp_GetFormDto GetForm(object p0);
        List<ISysMSItemComparedApp_GetListBySearchDto> GetListBySearch(object p0);
        object SubmitForm(object p0, object p1);
    }

    public interface ISysMedicinApp
    {
        object GetYpList(object p0);
    }

    public interface ISysModuleDmnService
    {
        object AuthAllOrganize(object p0, object p1);
        object AuthCancelAllOrganize(object p0, object p1);
        IList<SysOrganizeVEntity> GetAuthedOrgListByModuleId(object p0, object p1);
        object GetMenuButtonListByTopOrg(object p0, object p1);
        List<SysModuleEntity> GetMenuList(object p0, object p1, object p2, object p3);
        List<SysModuleButtonEntity> GetOpenMenuButtonListByTopOrg(object p0);
        IList<SysModuleEntity> GetOpenMenuListByTopOrg(object p0);
        object ModuleButtonSubmitForm(object p0, object p1, object p2);
        object UpdateAuthOrganizeList(object p0, object p1, object p2);
    }

    public interface ISysModuleExRepo
    {
        List<SysModuleEntity> GetModuleTreeJsonByName(object p0);
    }

    public interface ISysParameterConfigApp
    {
        object ChargeTemplate_SubmitForm(object p0, object p1);
        SysChargeTemplateInfoVM GetSysChargeTemplateInfo(object p0);
    }

    public interface ISysPatiChargeAddDmnService
    {
        // no calls observed
    }

    public interface ISysPatiChargeLogicApp
    {
        object DeleteForm(object p0);
        ISysPatiChargeLogicApp_GetFormDto GetForm(object p0);
        List<ISysPatiChargeLogicApp_GetListDto> GetList(object p0, object p1, object p2);
        object GetSFXMItemInfoByDlCode(object p0, object p1, object p2);
        object SubmitForm(object p0, object p1, object p2);
    }

    public interface ISysPatiChargeRangeApp
    {
        object DeleteForm(object p0);
        object GetEffectiveList(object p0);
        List<SysPatientChargeRangeEntity> GetEffectiveList(object p0, object p1);
        object SubmitForm(object p0, object p1);
    }

    public interface ISysPatiNatureApp
    {
        string GetbrxzSelect(object p0);
    }

    public interface ISysRoleRepo
    {
        System.Linq.IQueryable<SysRoleEntity> IQueryable(System.Linq.Expressions.Expression<System.Func<SysRoleEntity, bool>> predicate);
    }

    public interface ISysRoleShortcutMenuDmnService
    {
        List<string> GetAuthedRoleIdList(object p0, object p1);
        object UpdateAuthRoleList(object p0, object p1, object p2);
    }

    public interface ISysShortcutMenuRepo
    {
        object DeleteForm(object p0);
        ISysShortcutMenuRepo_FindEntityDto FindEntity(object p0);
        List<ISysShortcutMenuRepo_GetListDto> GetList(object p0);
        object SubmitForm(object p0, object p1);
    }

    public interface ISysUserRoleRepo
    {
        List<string> GetRoleIdListByUserId(object p0);
    }

    public interface ITTCataloguesComparisonDetailRepo
    {
        object Delete(object p0);
        ITTCataloguesComparisonDetailRepo_FindEntityDto FindEntity(object p0);
        object GetListByMainId(object p0, object p1);
        object SubmitForm(object p0, object p1);
    }

    public interface ITTCataloguesComparisonDmnService
    {
        List<TTCataloguesComparisonMainEntity> GetValidMainList(object p0);
    }

    public interface ITTCataloguesComparisonMainRepo
    {
        object Delete(object p0);
        ITTCataloguesComparisonMainRepo_FindEntityDto FindEntity(object p0);
        List<ITTCataloguesComparisonMainRepo_GetListDto> GetList(object p0);
        object SubmitForm(object p0, object p1);
    }

    public interface IUserRoleAuthDmnService
    {
        object submitRoleUser(object p0, object p1);
    }
}
