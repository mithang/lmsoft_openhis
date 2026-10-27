using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Duyệt chứng từ thuốc hệ thống
    /// </summary>
    [Table("xt_yp_djsh")]
    [SugarTable("xt_yp_djsh")]
    public class SysMedicineReceiptApprovalEntity : IEntity<SysMedicineReceiptApprovalEntity>
    {
        /// <summary>
        /// Duyệt chứng từID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string djshId { get; set; }

        /// <summary>
        /// Cơ cấu tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Chứng từ xuất nhập khoID
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// Số hiệu nhân viên thao tác
        /// </summary>
        public string Shczy { get; set; }

        /// <summary>
        /// Thời gian duyệt
        /// </summary>
        public DateTime Shsj { get; set; }

        /// <summary>
        /// Số hiệu nhân viên thao tác
        /// </summary>
        public string Qxczy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Qxsj { get; set; }

        /// <summary>
        /// Trạng thái
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
