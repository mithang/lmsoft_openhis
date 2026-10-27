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
            /// báo cáo Thống kê Nhóm thu phí lớn
            /// </summary>
            public string reportdlcode { get; set; }
    
            /// <summary>
            /// lớnloạiloạikhác，đốiphảicáinêu EnumSfdlDllb 1Thuốc 2Khoản mục trị liệu 3Khoản mục không điều trị
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
            /// tài chínhvụthudữ liệuID
            /// </summary>
            public string cwsjId { get; set; }
    
            /// <summary>
            /// Nhân viên
            /// </summary>
            public string ry { get; set; }
    
            /// <summary>
            /// Tổ chứcId（cócụthểnghiệpvụcủay tếviện）
            /// </summary>
            public string OrganizeId { get; set; }
    
            /// <summary>
            /// đầuchữmẹ
            /// </summary>
            public string szm { get; set; }
    
            /// <summary>
            /// Số biên lai bắt đầu
            /// </summary>
            public long qssjh { get; set; }
    
            /// <summary>
            /// khitrướcthudữ liệusố
            /// </summary>
            public long dqsjh { get; set; }
    
            /// <summary>
            /// kếtbóthudữ liệusố
            /// </summary>
            public long? jssjh { get; set; }
    
            /// <summary>
            /// tạolậpdùnghộID
            /// </summary>
            public string CreatorCode { get; set; }
    
            /// <summary>
            /// Thời gian tạo mới
            /// </summary>
            public DateTime CreateTime { get; set; }
    
            /// <summary>
            /// nhấtsausửasửathời giangiữa
            /// </summary>
            public DateTime? LastModifyTime { get; set; }
    
            /// <summary>
            /// nhấtsausửasửadùnghộID
            /// </summary>
            public string LastModifierCode { get; set; }
    
            /// <summary>
            /// Sắp xếp
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
            /// bệnhngườithuphíkèmthêmmãsố（xt_brsffj）
            /// </summary>
            public int brsffjbh { get; set; }
    
            /// <summary>
            /// Tính chất bệnh nhân（xt_brxz）
            /// </summary>
            public string brxzmc { get; set; }
    
            public string brxz { get; set; }
    
            /// <summary>
            /// nhóm lớn（xt_sfdl）
            /// </summary>
            public string dlmc { get; set; }
    
            public string dl { get; set; }
    
            /// <summary>
            /// kèmthêmhiển thịhiển thịlớnloạitêngọi（xt_fjsfdl）
            /// </summary>
            public string fjxsdlmc { get; set; }
    
            public string fjxsdl { get; set; }
    
            /// <summary>
            /// Khoản thu phí（xt_fjsfdl）
            /// </summary>
            public string sfxmmc { get; set; }
    
            public string sfxm { get; set; }
    
            /// <summary>
            /// phục vụvụphísoví dụ（xt_brsffj）
            /// </summary>
            public decimal fwfbl { get; set; }
    
            /// <summary>
            /// Trạng thái（xt_brsffj）
            /// </summary>
            public string zt { get; set; }
            /// <summary>
            /// Người lập hồ sơ（xt_brsffj）
            /// </summary>
            public string CreatorCode { get; set; }
            /// <summary>
            /// Ngày lập hồ sơ（xt_brsffj）
            /// </summary>
            public DateTime CreateTime { get; set; }
        }
    public class InpatientHosPatAccPayVO
        {
            /// <summary>
            /// Số tiền thu chi xt_brzhszjl
            /// </summary>
            public decimal szje { get; set; }
            /// <summary>
            /// Số dư tài khoản
            /// </summary>
            public decimal zhye { get; set; }
            /// <summary>
            /// Số chứng từ
            /// </summary>
            public string pzh { get; set; }
            /// <summary>
            /// Nhân viên thu chi
            /// </summary>
            public string CreatorUserName { get; set; }
    
            /// <summary>
            /// Ngày thu chi-tức là ngày tạo
            /// </summary>
            public DateTime CreateTime { get; set; }
            /// <summary>
            /// Tính chất thu chi
            /// </summary>
            public string szxz { get; set; }
    
            /// <summary>
            /// Phương thức thanh toán  xt_xjzffs
            /// </summary>
            public string xjzffsmc { get; set; }
    
            /// <summary>
            /// Phương thức thanh toán
            /// </summary>
            public string xjzffs { get; set; }
        }
    
        /// <summary>
        /// Bản ghi thu chi tài khoản
        /// </summary>
        public class InpatientPatAccPayVO
        {
            public string Id { get; set; }
            /// <summary>
            /// Số tiền thu chi xt_brzhszjl
            /// </summary>
            public decimal szje { get; set; }
            /// <summary>
            /// Số dư tài khoản
            /// </summary>
            public decimal zhye { get; set; }
            /// <summary>
            /// Số chứng từ
            /// </summary>
            public string pzh { get; set; }
            /// <summary>
            /// Nhân viên thu chi
            /// </summary>
            public string Creator { get; set; }
    
            /// <summary>
            /// Ngày thu chi-tức là ngày tạo
            /// </summary>
            public DateTime CreateTime { get; set; }
            /// <summary>
            /// Tính chất thu chi
            /// </summary>
            public int szxz { get; set; }
    
            public string szxzmc { get; set; }
    
            /// <summary>
            /// Phương thức thanh toán  xt_xjzffs
            /// </summary>
            public string xjzffsmc { get; set; }
    
            /// <summary>
            /// Phương thức thanh toán
            /// </summary>
            public string xjzffs { get; set; }
            /// <summary>
            /// Ghi chú
            /// </summary>
            public string memo { get; set; }
            /// <summary>
            /// Mã đơn hàng
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
