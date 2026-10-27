using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// mz_tfmx Chi tiết trả thuốc ngoại trú
    /// </summary>
    [Table("mz_tfmx")]
    [SugarTable("mz_tfmx")]
    public class OutpatientDrugRepercussionDetailEntity : IEntity<OutpatientDrugRepercussionDetailEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long tfmxId { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lượng đơn vị nhỏ nhất
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Nhân viên trả thuốc
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Trạng thái 0-Không hiệu lực 1-Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModiFierCode { get; set; }

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
