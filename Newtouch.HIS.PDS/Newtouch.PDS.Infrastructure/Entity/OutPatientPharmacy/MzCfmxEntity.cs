using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chi tiết đơn ngoại trú
    /// </summary>
    [Table("mz_cfmx")]
    [SugarTable("mz_cfmx")]
    public class MzCfmxEntity : IEntity<MzCfmxEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết đơn thuốc
        /// </summary>
        public long Cfmxnm { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Số lượng đơn vị
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Đơn giá đơn vị nhỏ nhất
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? dj { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// Số tiền sl*dj
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// Đơn vị liều lượng
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// Tên cách dùng
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// Dặn dò của bác sĩ(Ghi chú)
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Nhóm thuốc
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// Tổ chứcId
        /// </summary>
        public string OrganizeId { get; set; }

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
