using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// ngoại trú chi tiết đơn thuốc
    /// </summary>
    [Table("mz_cfmx")]
    public class MzCfmxEntity : IEntity<MzCfmxEntity>
    {
        /// <summary>
        /// khóa chính ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// chi tiết đơn thuốc trong mã
        /// </summary>
        public long Cfmxnm { get; set; }

        /// <summary>
        /// quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// đơn vị số lượng
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// tên đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// đơn vị nhỏ nhất đơn giá
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? dj { get; set; }

        /// <summary>
        /// nhà sản xuất
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// số tiền sl*dj
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// liều dùng
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// đơn vị liều dùng
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// cách dùng tên
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// lời dặn của bác sĩ(ghi chú)
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// nhóm thuốc
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// tổ chức Id
        /// </summary>
        public string OrganizeId { get; set; }

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
