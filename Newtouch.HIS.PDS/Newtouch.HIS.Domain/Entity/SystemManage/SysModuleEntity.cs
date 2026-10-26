using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_Module")]
    public class SysModuleEntity : IEntity<SysModuleEntity>
    {
        /// <summary>
        /// khóa chính mô-đun
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// cấp cha
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// tên tiếng Anh
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// biểu tượng
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// kết nối
        /// </summary>
        public string UrlAddress { get; set; }

        /// <summary>
        /// mục tiêu
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// mã sắp xếp
        /// </summary>
        public int? px { get; set; }

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
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
