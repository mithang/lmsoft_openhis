using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_ModuleButton")]
    public class SysModuleButtonEntity : IEntity<SysModuleButtonEntity>
    {
        /// <summary>
        /// nút khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// khóa chính mô-đun
        /// </summary>
        public string ModuleId { get; set; }

        /// <summary>
        /// tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// kết nối
        /// </summary>
        public string UrlAddress { get; set; }

        /// <summary>
        /// mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// khóa chính người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// mã sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
