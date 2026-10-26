using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.SystemManage
{
    /// <summary>
    /// GRSThông tin thu nhập
    /// </summary>
    [Table("jgss_srxx")]
   public class jgss_EarningInfoEntity : IEntity<jgss_EarningInfoEntity>
    {
        [Key]
        public string Id { get; set; }
        public string siteId { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        /// <summary>
        /// Thực thu của cơ sở
        /// </summary>
        public decimal jgss { get; set; }
        /// <summary>
        /// grsThực thu
        /// </summary>
        public decimal grsss { get; set; }
        /// <summary>
        /// Tên tệp đính kèm
        /// </summary>
        public string fjmc { get; set; }
        /// <summary>
        /// Loại tệp đính kèm
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// Loại tệp đính kèm
        /// </summary>
        public string fjPath { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string zt { get; set; }
        public string shzt { get; set; }
        public decimal mzsfzje { get; set; }
        public decimal zysfzje { get; set; }
        /// <summary>
        /// Tổng thu nhập
        /// </summary>
        public decimal zsr { get; set; }
        public string shr { get; set; }
        public DateTime? shsj { get; set; }
    }
}
