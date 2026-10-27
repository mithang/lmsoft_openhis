using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("Sys_Report")]
    public class SysReportEntity : EntityBase
    {
        /// <summary>
        /// Khóa chính vai trò
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public int ReportID { get; set; }

        /// <summary>
        /// Tổ chứcId（tương ứng với khoaOrganizeId）
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
        /// Báo cáo tương ứngCode
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
        /// 1 Báo cáo chức năng 2Báo cáo truy vấn
        /// </summary>
        public int? ReportType { get; set; }
        /// <summary>
        /// Thứ tự
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? StatusTemplateID { get; set; }
        /// <summary>
        /// Cấp chaid
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// Có phải hệ thống không 1Có 0Không phải
        /// </summary>
        public int? DirectoryFlag { get; set; }
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
