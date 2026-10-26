using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_Organize")]
    [Obsolete("please use the view")]
    public class OrganizeEntity : IEntity<OrganizeEntity>
    {
        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// cấp cha
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// phần loại
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// Người phụ trách
        /// </summary>
        public string ManagerCode { get; set; }

        /// <summary>
        /// điện thoại
        /// </summary>
        public string TelePhone { get; set; }

        /// <summary>
        /// WeChat
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// Fax
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// liên hệ địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AllowEdit { get; set; }

        /// <summary>
        /// thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// người tạo
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
