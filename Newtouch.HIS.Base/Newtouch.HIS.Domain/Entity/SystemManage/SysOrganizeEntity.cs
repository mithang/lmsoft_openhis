using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_Organize")]
    public class SysOrganizeEntity : IEntity<SysOrganizeEntity>
    {
        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Cấp cha
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Id tổ chức (cấp cao nhất, cấu hình hệ thống)
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Phân loại
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// Người phụ trách
        /// </summary>
        public string ManagerCode { get; set; }

        /// <summary>
        /// Điện thoại
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
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// Địa chỉ liên hệ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người dùng tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối cùng
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người dùng sửa đổi cuối cùng
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Mã sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Mã cơ sở quốc gia
        /// </summary>
        public string gjjgdm { get; set; }
        

        public string ybxzqhCode { get; set; }
        
        public string ybxzqhName { get; set; }
        
    }
}
