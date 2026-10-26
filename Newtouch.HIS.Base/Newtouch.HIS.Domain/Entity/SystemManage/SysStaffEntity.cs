using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Nhân viên hệ thống
    /// </summary>
    [Table("Sys_Staff")]
    public class SysStaffEntity : IEntity<SysStaffEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Id tổ chức (OrganizeId tương ứng với khoa)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Id tổ chức (cấp cao nhất, cấu hình hệ thống)
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gh { get; set; }

        public string zjlx { get; set; }

        public string zjh { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Chữ đầu pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Code phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string zc { get; set; }

        /// <summary>
        /// Ảnh đại diện
        /// </summary>
        public string HeadIcon { get; set; }

        /// <summary>
        /// Giới tính
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
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// WeChat
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// Khóa chính quản lý
        /// </summary>
        public string ManagerCode { get; set; }

        /// <summary>
        /// Mã sắp xếp
        /// </summary>
        public int? px { get; set; }

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
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Danh mục phục hồi chức năng
        /// </summary>
        public string kflb { get; set; }


        /// <summary>
        /// Quyền gói (theo enum)
        /// </summary>
        public string mbqx { get; set; }
        /// <summary>
        /// Mã bác sĩ quốc gia
        /// </summary>
        public string gjybdm { get; set; }

        /// <summary>
        /// Email Microsoft
        /// </summary>

        public string MsEmail { get; set; }

		public string yszydm { get; set; }

	}
}
