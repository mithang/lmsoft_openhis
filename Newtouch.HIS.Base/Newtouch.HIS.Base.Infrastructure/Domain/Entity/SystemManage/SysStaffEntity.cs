using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Nhân viên hệ thống
    /// </summary>
    [SugarTable("Sys_Staff")]
    public class SysStaffEntity : EntityBase
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chứcId（tương ứng với khoaOrganizeId）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tổ chứcId（Cấp cao nhất，Cấu hình hệ thống）
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
        /// Mã đầu chữ
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Khoa phòngCode
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
        /// Điện thoại
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
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối cùng
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Loại phục hồi chức năng
        /// </summary>
        public string kflb { get; set; }


        /// <summary>
        /// Quyền gói（Theo enum）
        /// </summary>
        public string mbqx { get; set; }
        /// <summary>
        /// Mã bác sĩ quốc gia
        /// </summary>
        public string gjybdm { get; set; }

        /// <summary>
        /// Hộp thư Microsoft
        /// </summary>

        public string MsEmail { get; set; }

		public string yszydm { get; set; }

	}
}
