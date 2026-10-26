using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tải lên báo cáo xét nghiệm/kiểm tra
    /// </summary>
    [Table("xt_JyjcFileUpload")]
    public class XtjyjcFileUploadEntity : IEntity<XtjyjcFileUploadEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Đường dẫn tệp
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// Số phiếu yêu cầu
        /// </summary>
        public string Sqdh { get; set; }
        /// <summary>
        /// Tên tệp
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Đuôi tệp
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// Loại tệp
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// Kích thước tệp
        /// </summary>
        public int FileSize { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
    }
}
