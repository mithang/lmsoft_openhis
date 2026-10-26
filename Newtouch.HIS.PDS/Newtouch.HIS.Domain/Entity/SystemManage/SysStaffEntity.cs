using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_Staff")]
    [Obsolete("please use the view")]
    public class SysStaffEntity : IEntity<SysStaffEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// họ tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// bộ phận Code
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte ysbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zc { get; set; }

        /// <summary>
        /// Ảnh đại diện
        /// </summary>
        public string HeadIcon { get; set; }

        /// <summary>
        /// giới tính
        /// </summary>
        public bool? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// WeChat
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// Khóa chính của cấp quản lý
        /// </summary>
        public string ManagerCode { get; set; }

        /// <summary>
        /// mã sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// mô tả
        /// </summary>
        public string Description { get; set; }

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
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
