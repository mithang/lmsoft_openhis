using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_RoleAuthorize")]
    [SugarTable("Sys_RoleAuthorize")]
    public class SysRoleAuthorizeEntity : IEntity<SysRoleAuthorizeEntity>
    {
        /// <summary>
        /// Khóa chính ủy quyền vai trò
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Khóa chính đối tượng
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// Khóa chính hạng mục
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Loại hạng mục1-Mô-đun2-Nút3-Danh sách
        /// </summary>
        // NX30-ROLEDDL (D-NX30-ROLE-002): bo [SugarColumn(IsIgnore = true)]
        // cua NX20-004 — cot ItemType DA TON TAI tren *_STG sau DDL
        // nx30-role-ddl-STG.sql (backfill theo JOIN Sys_Module/Sys_ModuleButton).
        // IsIgnore khien SqlSugar khong bao gio doc/ghi cot nay (doc luon tra
        // default(0), INSERT authorize row luon ghi 0). GIOI HAN NX20-004 ve
        // SysModuleDmnService (D-NX21-002) khong doi. CHI chay tren DB co cot
        // nay — KHONG redeploy DLL nay sang carrier chay DB demo (demo chua co
        // cot) cho toi khi DDL tuong ung duoc quyet dinh cho demo.
        public int ItemType { get; set; }

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
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
