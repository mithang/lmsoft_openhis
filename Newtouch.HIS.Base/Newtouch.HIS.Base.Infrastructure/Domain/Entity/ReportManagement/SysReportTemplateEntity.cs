using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("Sys_ReportTemplate")]
    public class SysReportTemplateEntity : EntityBase
    {
        /// <summary>
        /// Khóa chính vai trò
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public int TemplateID { get; set; }

        /// <summary>
        /// Tổ chứcId(tương ứng với khoaOrganizeId)
        /// </summary>
        public string HospitalCode { get; set; }
        /// <summary>
        /// Mã hệ thống
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// Báo cáo tương ứngCode
        /// </summary>
        public string ReportCode { get; set; }
        /// <summary>
        /// Tên báo cáo
        /// </summary>
        public string ReportNameDes { get; set; }
        /// <summary>
        /// xmlNội dung báo cáo
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Có bật không 1Kích hoạt 0Vô hiệu hóa
        /// </summary>
        public int ReportStatus { get; set; }
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
        public int zt { get; set; }

    }
}
