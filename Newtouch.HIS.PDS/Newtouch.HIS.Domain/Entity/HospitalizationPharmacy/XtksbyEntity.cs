using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// bản ghi thao tác y lệnh thuốc nội trú
    /// </summary>
    [Table("xt_bqksby")]
    public class XtksbyEntity : IEntity<XtksbyEntity>
    {
        /// <summary>
        /// khóa chính ID
        /// </summary>
        [Key]
        public string Id { get; set; }
      
        public string OrganizeId { get; set; }
        public string yfbm { get; set; }
        public string bqbm { get; set; }
        public string ksbm { get; set; }
        public string djh { get; set; }
        public string shzt { get; set; }
        /// <summary>
        /// trạng thái
        /// </summary>
        public string zt { get; set; }


        /// <summary>
        /// người sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// thời gian sửa đổi
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
