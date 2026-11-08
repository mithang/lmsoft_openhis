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
    /// trí có thể Phê duyệt ngày chí
    /// </summary>
    [SugarTable("qhd_znshsqtx")]
    public class QhdZnshSqtxEntity
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// giao dịch Mã
        /// </summary>
        public string jydm { get; set; }
        /// <summary>
        /// Ngoại trú Số nhập viện
        /// </summary>
        public string mzzyh { get; set; }
        /// <summary>
        /// 1:Ngoại trú 2:Nhập viện
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Tham số đầu vào xml
        /// </summary>
        public string XmlRequest { get; set; }
        /// <summary>
        /// ratham số xml
        /// </summary>
        public string XmlResponse { get; set; }
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
