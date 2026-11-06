using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_Role")]
    [SugarTable("Sys_Role")]
    public class SysRoleEntity : IEntity<SysRoleEntity>
    {
        /// <summary>
        /// Khóa chính vai trò
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        ///
        /// </summary>
        // NX20-004 (D-NX20-004): cot nay KHONG ton tai trong bang
        // Sys_Role (live + scratch deu 11 cot, khong co TopOrganizeId).
        // SqlSugar map moi property mac dinh -> moi SELECT/INSERT/UPDATE
        // deu loi "Invalid column name 'TopOrganizeId'" (harness NX20
        // chung minh: ca guard lan insert that deu SqlException). Ban
        // goc EF chi map cot da cau hinh nen property thua bi bo qua —
        // khoi phuc dung hanh vi do bang IsIgnore. Khong DDL (cam).
        [SugarColumn(IsIgnore = true)]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Số hiệu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Mã sắp xếp
        /// </summary>
        public int? px { get; set; }

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

    }
}
