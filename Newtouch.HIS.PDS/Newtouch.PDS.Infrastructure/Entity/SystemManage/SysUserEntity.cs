using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_User")]
    [SugarTable("Sys_User")]
    [Obsolete("please use the view")]
    public class SysUserEntity : IEntity<SysUserEntity>
    {
        /// <summary>
        /// Khóa chính người dùng
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        // NX21-003 (D-NX21-003): khong co cot 'OrganizeId' trong bang Sys_User
        // (kiem chung INFORMATION_SCHEMA tren NewtouchHIS_Base); SqlSugar map moi
        // property mac dinh -> SELECT/INSERT/UPDATE deu loi "Invalid column
        // name 'OrganizeId'". Khoi phuc hanh vi EF goc bang IsIgnore. Khong DDL (cam).
        [SugarColumn(IsIgnore = true)]
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // NX21-003 (D-NX21-003): khong co cot 'StaffId' trong bang Sys_User
        // (kiem chung INFORMATION_SCHEMA tren NewtouchHIS_Base); SqlSugar map moi
        // property mac dinh -> SELECT/INSERT/UPDATE deu loi "Invalid column
        // name 'StaffId'". Khoi phuc hanh vi EF goc bang IsIgnore. Khong DDL (cam).
        [SugarColumn(IsIgnore = true)]
        public string StaffId { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // NX21-003 (D-NX21-003): khong co cot 'AllowEdit' trong bang Sys_User
        // (kiem chung INFORMATION_SCHEMA tren NewtouchHIS_Base); SqlSugar map moi
        // property mac dinh -> SELECT/INSERT/UPDATE deu loi "Invalid column
        // name 'AllowEdit'". Khoi phuc hanh vi EF goc bang IsIgnore. Khong DDL (cam).
        [SugarColumn(IsIgnore = true)]
        public bool AllowEdit { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người dùng tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
