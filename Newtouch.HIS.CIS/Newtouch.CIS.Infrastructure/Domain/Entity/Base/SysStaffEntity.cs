using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Hệ thống ngườiviên
    /// </summary>
    [SugarTable("Sys_Staff")]
    public class SysStaffEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chứcId（Khoasởtương ứngcủaOrganizeId）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tổ chứcId（đỉnhcấp，Hệ thốngcấu hình）
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
        /// chínhquảnKhóa chính
        /// </summary>
        public string ManagerCode { get; set; }

        /// <summary>
        /// Thứ tựmã
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
        /// tạoNgười dùng
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửa cuốiNgười dùng
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
        /// góiQuyền（TheoLiệt kê）
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
