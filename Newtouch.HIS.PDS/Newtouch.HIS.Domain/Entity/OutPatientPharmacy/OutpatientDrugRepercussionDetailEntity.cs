using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// mz_tfmx trả thuốc ngoại trú chi tiết
    /// </summary>
    [Table("mz_tfmx")]
    public class OutpatientDrugRepercussionDetailEntity : IEntity<OutpatientDrugRepercussionDetailEntity>
    {
        /// <summary>
        /// khóa chính ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long tfmxId { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số lượng đơn vị nhỏ nhất
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// trả thuốc nhân viên
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// trạng thái 0-không hiệu lực 1-còn hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// người sửa đổi
        /// </summary>
        public string LastModiFierCode { get; set; }

        /// <summary>
        /// ngày sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
