using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_ReportTemplate")]
    public class SysReportTemplateEntity : IEntity<SysReportTemplateEntity>
    {
        /// <summary>
        /// Khóa chính vai trò
        /// </summary>
        [Key]
        public int TemplateID { get; set; }

        /// <summary>
        /// Id tổ chức (OrganizeId tương ứng với khoa)
        /// </summary>
        public string HospitalCode { get; set; }
        /// <summary>
        /// Mã hệ thống
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// Code tương ứng của báo cáo
        /// </summary>
        public string ReportCode { get; set; }
        /// <summary>
        /// Tên báo cáo
        /// </summary>
        public string ReportNameDes { get; set; }
        /// <summary>
        /// Nội dung báo cáo xml
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Có kích hoạt hay không 1 Kích hoạt 0 Ngừng sử dụng
        /// </summary>
        public int ReportStatus { get; set; }
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
