using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.SystemManage
{
    /// <summary>
    /// GRSBảng chi tiết thông tin thu nhập
    /// </summary>
    [Table("jgss_srxxdetail")]
    public class jgss_EarningDetailInfoEntity : IEntity<jgss_EarningDetailInfoEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string srxxId { get; set; }
        /// <summary>
        /// Số tiền thu phí ngoại trú
        /// </summary>
        public decimal mzsfje { get; set; }
        /// <summary>
        /// Số tiền thực hiện ngoại trú
        /// </summary>
        public decimal mzzxje { get; set; }
        /// <summary>
        /// Số tiền thu phí nội trú
        /// </summary>
        public decimal zysfje { get; set; }
        /// <summary>
        /// Số tiền thực hiện nội trú
        /// </summary>
        public decimal zyzxje { get; set; }
        /// <summary>
        /// Tổng thu nhập
        /// </summary>
        public decimal zsr { get; set; }
        /// <summary>
        /// Loại lớncode
        /// </summary>
        public string dlCode { get; set; }
        /// <summary>
        /// Tên loại lớn
        /// </summary>
        public string dlmc { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string zt { get; set; }
        /// <summary>
        /// Xác minh thu nhập
        /// </summary>
        public decimal hssr { get; set; }
        /// <summary>
        /// Chênh lệch
        /// </summary>
        public decimal ce { get; set; }
        /// <summary>
        /// Giải thích điều chỉnh
        /// </summary>
        public string tzsm { get; set; }
        /// <summary>
        /// Tỷ lệ chia
        /// </summary>
        public decimal fcbl { get; set; }
    }
}
