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
    /// Xét nghiệmBáo cáo kiểm traTải lên
    /// </summary>
    [SugarTable("xt_JyjcFileUpload")]
    public class XtjyjcFileUploadEntity
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// vănmụcĐường dẫn
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// Số phiếu
        /// </summary>
        public string Sqdh { get; set; }
        /// <summary>
        /// vănmụcTên
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// vănmụcsauhậu tố
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// vănmụcLoại
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// vănmụclớnnhỏ
        /// </summary>
        public int FileSize { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
    }
}
