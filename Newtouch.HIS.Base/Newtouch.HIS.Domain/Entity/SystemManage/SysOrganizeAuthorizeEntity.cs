using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Cấu hình quyền mở của cơ sở (menu, nút bấm)
    /// </summary>
    [Table("Sys_OrganizeAuthorize")]
    public class SysOrganizeAuthorizeEntity : IEntity<SysOrganizeAuthorizeEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Khóa chính hạng mục
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Loại hạng mục 1 - Module 2 - Nút bấm 3 - Danh sách
        /// </summary>
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
