using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    [Obsolete("please use the view")]
    public class ItemsEntity
    {
        public string Id { get; set; }
        public string F_ParentId { get; set; }
        public string F_EnCode { get; set; }
        public string F_FullName { get; set; }
        public bool? F_IsTree { get; set; }
        public int? F_Layers { get; set; }
        public string F_Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public int? px { get; set; }
    }

    [Obsolete("please use the view")]
    public class ItemsDetailEntity
    {
        public string Id { get; set; }
        public string F_ItemId { get; set; }
        public string F_ParentId { get; set; }
        public string F_ItemCode { get; set; }
        public string F_ItemName { get; set; }
        public string F_SimpleSpelling { get; set; }
        public bool? F_IsDefault { get; set; }
        public int? F_Layers { get; set; }
        public string F_Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public int? px { get; set; }
    }

    public class SysDepartmentVEntity
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool yjbz { get; set; }
        public byte mzzybz { get; set; }
        public string py { get; set; }
        public string zt { get; set; }
    }

    public class SysUserStaffVO
    {
        public string StaffId { get; set; }
        public string UserId { get; set; }
        public string DepartmentCode { get; set; }
        public string Name { get; set; }
        public string gh { get; set; }
        public string UserCode { get; set; }
    }

    // Source StaffDutyVO + ks: both source StaffController and the Framework
    // service filter on a.ks, but shipped StaffDutyVO has no ks prop (source/DLL drift).
    public class StaffDutyVO
    {
        public string StaffName { get; set; }
        public string StaffPY { get; set; }
        public string StaffGh { get; set; }
        public string OrganizeId { get; set; }
        public string DutyCode { get; set; }
        public string ks { get; set; }
    }

    public class RoleUnionUser
    {
        public string First { get; set; }
        public string Second { get; set; }
    }

    public class AreaEntity
    {
        public string Id { get; set; }
        public string F_FullName { get; set; }
        public string F_ParentId { get; set; }
    }

    // FrameworkBase.MultiOrg binary entity (Sys_Module table); props from
    // ModuleController + RoleAuthorizeController usage only.
    public class SysModuleEntity
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string UrlAddress { get; set; }
        public string Icon { get; set; }
    }

    public class SysModuleButtonEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ModuleId { get; set; }
        public string zt { get; set; }
    }

    public class SysRoleAuthorizeEntity
    {
        public string ItemId { get; set; }
    }

    public class SysRoleEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string OrganizeId { get; set; }
        public string TopOrganizeId { get; set; }
        public string zt { get; set; }
    }

    public class InpatientAccountEntity
    {
        public string Id { get; set; }
        public int? patid { get; set; }
        public string zyh { get; set; }
        public string OrganizeId { get; set; }
        public decimal zhye { get; set; }
        public int? zhxz { get; set; }
        public string zt { get; set; }
        public int zhCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public void Create(bool isNew)
        {
            if (isNew) { CreateTime = DateTime.Now; }
        }
    }

    public class InpatientReservePatientInfoDto
    {
        public int zhCode { get; set; }
        public int? zhxz { get; set; }
    }

    public class InpatientReserveDto
    {
        public InpatientReservePatientInfoDto patInfo;
        public List<InpatientPatAccPayVO> accPayInfoList;
    }
    public class SysChargeCategoryVEntity
        {
            /// <summary>
            /// 
            /// </summary>
            public int dlId { get; set; }
    
            /// <summary>
            /// 
            /// </summary>
            public string dlCode { get; set; }
    
            /// <summary>
            /// 
            /// </summary>
            public string dlmc { get; set; }
    
            /// <summary>
            /// 
            /// </summary>
            public string OrganizeId { get; set; }
    
            /// <summary>
            /// 
            /// </summary>
            public string py { get; set; }
    
            /// <summary>
            /// 
            /// </summary>
            public string mzprintreportcode { get; set; }
    
            /// <summary>
            /// 
            /// </summary>
            public string mzprintbillcode { get; set; }
    
            /// <summary>
            /// 
            /// </summary>
            public int? ParentId { get; set; }
    
            /// <summary>
            /// 报表 统计 收费大类
            /// </summary>
            public string reportdlcode { get; set; }
    
            /// <summary>
            /// 大类类别，对应枚举 EnumSfdlDllb 1药品 2治疗项目 3非治疗项目
            /// </summary>
            public int? dllb { get; set; }
    
            /// <summary>
            /// 
            /// </summary>
            public string zt { get; set; }
    
        }
    public class FinanceReceiptEntity
        {
            /// <summary>
            /// 财务收据ID
            /// </summary>
            public string cwsjId { get; set; }
    
            /// <summary>
            /// 人员
            /// </summary>
            public string ry { get; set; }
    
            /// <summary>
            /// 组织机构Id（有具体业务的医院）
            /// </summary>
            public string OrganizeId { get; set; }
    
            /// <summary>
            /// 首字母
            /// </summary>
            public string szm { get; set; }
    
            /// <summary>
            /// 起始收据号
            /// </summary>
            public long qssjh { get; set; }
    
            /// <summary>
            /// 当前收据号
            /// </summary>
            public long dqsjh { get; set; }
    
            /// <summary>
            /// 结束收据号
            /// </summary>
            public long? jssjh { get; set; }
    
            /// <summary>
            /// 创建用户ID
            /// </summary>
            public string CreatorCode { get; set; }
    
            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreateTime { get; set; }
    
            /// <summary>
            /// 最后修改时间
            /// </summary>
            public DateTime? LastModifyTime { get; set; }
    
            /// <summary>
            /// 最后修改用户ID
            /// </summary>
            public string LastModifierCode { get; set; }
    
            /// <summary>
            /// 排序
            /// </summary>
            public int? px { get; set; }
    
            /// <summary>
            /// 
            /// </summary>
            public string zt { get; set; }
    
        }
    public class SysDepartmentBindingVO
        {
            public bool xb { get; set; }
    
            public string zjlx { get; set; }
    
            public string zjh { get; set; }
        }
    public class SysPatiChargeAddVo
        {
    
            /// <summary>
            /// 病人收费附加编号（xt_brsffj）
            /// </summary>
            public int brsffjbh { get; set; }
    
            /// <summary>
            /// 病人性质（xt_brxz）
            /// </summary>
            public string brxzmc { get; set; }
    
            public string brxz { get; set; }
    
            /// <summary>
            /// 大类（xt_sfdl）
            /// </summary>
            public string dlmc { get; set; }
    
            public string dl { get; set; }
    
            /// <summary>
            /// 附加显示大类名称（xt_fjsfdl）
            /// </summary>
            public string fjxsdlmc { get; set; }
    
            public string fjxsdl { get; set; }
    
            /// <summary>
            /// 收费项目（xt_fjsfdl）
            /// </summary>
            public string sfxmmc { get; set; }
    
            public string sfxm { get; set; }
    
            /// <summary>
            /// 服务费比例（xt_brsffj）
            /// </summary>
            public decimal fwfbl { get; set; }
    
            /// <summary>
            /// 状态（xt_brsffj）
            /// </summary>
            public string zt { get; set; }
            /// <summary>
            /// 建档人员（xt_brsffj）
            /// </summary>
            public string CreatorCode { get; set; }
            /// <summary>
            /// 建档日期（xt_brsffj）
            /// </summary>
            public DateTime CreateTime { get; set; }
        }
    public class InpatientHosPatAccPayVO
        {
            /// <summary>
            /// 收支金额 xt_brzhszjl
            /// </summary>
            public decimal szje { get; set; }
            /// <summary>
            /// 账户余额
            /// </summary>
            public decimal zhye { get; set; }
            /// <summary>
            /// 凭证号
            /// </summary>
            public string pzh { get; set; }
            /// <summary>
            /// 收支人员
            /// </summary>
            public string CreatorUserName { get; set; }
    
            /// <summary>
            /// 收支日期-即创建日期
            /// </summary>
            public DateTime CreateTime { get; set; }
            /// <summary>
            /// 收支性质
            /// </summary>
            public string szxz { get; set; }
    
            /// <summary>
            /// 支付方式  xt_xjzffs
            /// </summary>
            public string xjzffsmc { get; set; }
    
            /// <summary>
            /// 支付方式
            /// </summary>
            public string xjzffs { get; set; }
        }
    
        /// <summary>
        /// 账户收支记录
        /// </summary>
        public class InpatientPatAccPayVO
        {
            public string Id { get; set; }
            /// <summary>
            /// 收支金额 xt_brzhszjl
            /// </summary>
            public decimal szje { get; set; }
            /// <summary>
            /// 账户余额
            /// </summary>
            public decimal zhye { get; set; }
            /// <summary>
            /// 凭证号
            /// </summary>
            public string pzh { get; set; }
            /// <summary>
            /// 收支人员
            /// </summary>
            public string Creator { get; set; }
    
            /// <summary>
            /// 收支日期-即创建日期
            /// </summary>
            public DateTime CreateTime { get; set; }
            /// <summary>
            /// 收支性质
            /// </summary>
            public int szxz { get; set; }
    
            public string szxzmc { get; set; }
    
            /// <summary>
            /// 支付方式  xt_xjzffs
            /// </summary>
            public string xjzffsmc { get; set; }
    
            /// <summary>
            /// 支付方式
            /// </summary>
            public string xjzffs { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string memo { get; set; }
            /// <summary>
            /// 订单号
            /// </summary>
            public string outTradeNo { get; set; }
        }

    public class TTCataloguesComparisonMainEntity
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string TTCode { get; set; }
        public string TTMark { get; set; }
        public string OrganizeId { get; set; }
        public string zt { get; set; }
    }
}
