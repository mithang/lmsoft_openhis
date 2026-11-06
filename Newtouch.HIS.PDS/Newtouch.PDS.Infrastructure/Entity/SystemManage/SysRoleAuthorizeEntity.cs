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
        // NX20-004 (D-NX20-004): cot nay KHONG ton tai trong bang
        // Sys_RoleAuthorize (live + scratch deu 9 cot, khong co
        // ItemType). Ly do nhu TopOrganizeId o SysRoleEntity: SqlSugar
        // map moi property -> INSERT authorize row luon loi
        // "Invalid column name 'ItemType'". Khoi phuc hanh vi EF goc
        // (bo qua property thua) bang IsIgnore. Khong DDL (cam).
        // GIOI HAN cu da ghi (NX20-004) da duoc xu ly o NX21-002
        // (D-NX21-002): SysModuleDmnService khong loc ItemType tren
        // repo nay nua ma suy loai tu tap Id cua Sys_Module /
        // Sys_ModuleButton (dung cach RoleApp gan ItemType); doc
        // ItemType qua entity nay van tra default(0).
        [SugarColumn(IsIgnore = true)]
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
