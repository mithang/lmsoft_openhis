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
    /// Nhật ký kiểm duyệt thông minh
    /// </summary>
    [Table("shjk_log")]
    public class shjkldrzEntity : IEntity<shjkldrzEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Mã giao dịch
        /// </summary>
        public string jydm { get; set; }
        /// <summary>
        /// Số ngoại trú/nội trú
        /// </summary>
        public string mzzyh { get; set; }
        /// <summary>
        /// 1:Ngoại trú 2:Nội trú
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Tên viết tắt giao diện
        /// </summary>
        public string jkType { get; set; }
        public string yzh { get; set; }
        /// <summary>
        /// Số thứ tự tham số trả về
        /// </summary>
        public string outlsh { get; set; }
        /// <summary>
        /// Cấu hình tham số đầu vào
        /// </summary>
        public string param { get; set; }
        /// <summary>
        /// Tham số đầu vàoxml
        /// </summary>
        public string XmlRequest { get; set; }
        /// <summary>
        /// Tham số đầu raxml
        /// </summary>
        public string XmlResponse { get; set; }
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
