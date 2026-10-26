using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuốc đơn chứng duyệt
    /// </summary>
    [Table("xt_yp_djsh")]
    public class SysMedicineReceiptApprovalEntity : IEntity<SysMedicineReceiptApprovalEntity>
    {
        /// <summary>
        /// đơn chứng duyệt ID
        /// </summary>
        [Key]
        public string djshId { get; set; }

        /// <summary>
        /// cơ cấu tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// chứng từ nhập/xuất kho ID
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// người thao tác công số
        /// </summary>
        public string Shczy { get; set; }

        /// <summary>
        /// thời gian duyệt
        /// </summary>
        public DateTime Shsj { get; set; }

        /// <summary>
        /// người thao tác công số
        /// </summary>
        public string Qxczy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Qxsj { get; set; }

        /// <summary>
        /// trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

    }
}
