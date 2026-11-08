using System;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Xét nghiệm Báo cáo kiểm tra Tải lên
    /// </summary>
    [SugarTable("xt_JyjcFileUpload")]
    public class XtjyjcFileUploadEntity
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// văn mục Đường dẫn
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// Số phiếu
        /// </summary>
        public string Sqdh { get; set; }
        /// <summary>
        /// văn mục Tên
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// văn mục sau hậu tố
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// văn mục Loại
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// văn mục lớn nhỏ
        /// </summary>
        public int FileSize { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
    }
}
