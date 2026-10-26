using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_Report")]
    public class SysReportEntity : IEntity<SysReportEntity>
    {
        /// <summary>
        /// Khóa chính vai trò
        /// </summary>
        [Key]
        public int ReportID { get; set; }

        /// <summary>
        /// Id tổ chức (OrganizeId tương ứng với khoa)
        /// </summary>
        public string HospitalCode { get; set; }
        /// <summary>
        /// Mã hệ thống
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// Tên hệ thống
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// Code tương ứng của báo cáo
        /// </summary>
        public string ReportCode { get; set; }
        /// <summary>
        /// Tên báo cáo
        /// </summary>
        public string ReportName { get; set; }
        /// <summary>
        /// Pinyin
        /// </summary>
        public string PinYin { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string ReportDes { get; set; }
        /// <summary>
        /// 1 Báo cáo chức năng 2 Báo cáo truy vấn
        /// </summary>
        public int? ReportType { get; set; }
        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? StatusTemplateID { get; set; }
        /// <summary>
        /// Id cấp cha
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// Có phải hệ thống hay không 1 Có 0 Không
        /// </summary>
        public int? DirectoryFlag { get; set; }
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
        public int zt { get; set; }

    }
}
