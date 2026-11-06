using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public class DutyEntity
    {
        public object zt { get; set; }
    }

    public class FinancialInvoiceEntity
    {
        public object OrganizeId { get; set; }
        public object zt { get; set; }
    }

    public class FirstSecond
    {
    }

    public class MoneyUpperLimitReminderEntity
    {
        public object OrganizeId { get; set; }
        public object zt { get; set; }
    }

    public class PatiChargeLogicVO
    {
    }

    public class SysAgriInsuranceChargeCategoryEntity
    {
    }

    public class SysCashPaymentModelEntity
    {
    }

    public class SysChargeAdditionalCategoryEntity
    {
    }

    public class SysChargeCategoryEntity
    {
        public DateTime CreateTime { get; set; }
        public object CreatorCode { get; set; }
        public object dlCode { get; set; }
    }

    public class SysChargeClassificationEntity
    {
        public DateTime CreateTime { get; set; }
        public object CreatorCode { get; set; }
        public object fl { get; set; }
    }

    public class SysChargeItemPriceAdjustEntity
    {
    }

    public class SysChargeItemSpecialMarkEntity
    {
    }

    public class SysChargeItemWarningContentEntity
    {
    }

    public class SysChargeTemplateEntity
    {
        public object OrganizeId { get; set; }
        public object zt { get; set; }
    }

    public class SysChargeTemplateInfoVM
    {
    }

    public class SysDepartmentBindingEntity
    {
        public object OrganizeId { get; set; }
        public string bddm { get; set; }
        public object gh { get; set; }
        public object ks { get; set; }
        public object xb { get; set; }
        public object zjh { get; set; }
        public object zjlx { get; set; }
        public object zt { get; set; }
    }

    public class SysFailedCodeMessageMappEntity
    {
    }

    public class SysMedicalRecordChargeCategoryEntity
    {
        public object zt { get; set; }
    }

    public class SysMedicalTechItemMappEntity
    {
    }

    public class SysPatientChargeAdditionalEntity
    {
        public object zt { get; set; }
    }

    public class SysPatientChargeRangeEntity
    {
        public object zt { get; set; }
    }

    public class SysPatientChargeWaiverEntity
    {
        public object zt { get; set; }
    }

    public class SysShortcutMenuEntity
    {
        // SHELL-A: source-true fields tu vendor
        // Newtouch.Domain/Entity/SystemManage/SysShortcutMenuEntity.cs. Stub
        // cu chi co zt (object) nen Default.cshtml khong doc duoc
        // Code/Name/UrlAddress/ImgPath.
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string UrlAddress { get; set; }
        public string ImgPath { get; set; }
        public int? px { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
    }

    public class TTCataloguesComparisonDetailEntity
    {
        public object OrganizeId { get; set; }
        public object zt { get; set; }
    }

    public enum EnumZT
    {
        Invalid = 0,
        Valid = 1,
    }

    public enum Item
    {
        dlCode = 0,
        dlmc = 1,
    }

    public enum Screen
    {
        bddm = 0,
    }

    public enum SysPatiChargeAddEntity
    {
        zt = 0,
    }

}