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
        // NX30-ROLEDDL (D-NX30-ROLE-002): bo [SugarColumn(IsIgnore = true)]
        // cua NX20-004 — cot ItemType DA TON TAI tren *_STG sau DDL
        // nx30-role-ddl-STG.sql (backfill theo JOIN Sys_Module/Sys_ModuleButton).
        // IsIgnore khien SqlSugar khong bao gio doc/ghi cot nay: INSERT authorize
        // row luon de ItemType=0, doc qua entity luon tra default(0). CHI chay
        // tren DB co cot nay — KHONG redeploy DLL nay sang carrier chay DB demo
        // (demo chua co cot) cho toi khi DDL tuong ung duoc quyet dinh cho demo.
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
