using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Bản ghi thao tác thuốc đơn ngoại trú
    /// </summary>
    [Table("mz_cfypczjl")]
    [SugarTable("mz_cfypczjl")]
    public class MzCfypczjlEntity : IEntity<MzCfypczjlEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Chi tiết đơn ngoại trúID mz_cfmxKhóa chính bảng
        /// </summary>
        public long mzcfmxId { get; set; }

        /// <summary>
        /// Loại thao tác 1：Phát thuốc 2：Trả thuốc
        /// </summary>
        public string operateType { get; set; }

        /// <summary>
        /// Số hiệu thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Số lượng thao tác  Đơn vị nhỏ nhất
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }

    }
}
