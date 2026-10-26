using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// ngoại trú đơn thuốc thuốc thao tác bản ghi
    /// </summary>
    [Table("mz_cfypczjl")]
    public class MzCfypczjlEntity : IEntity<MzCfypczjlEntity>
    {
        /// <summary>
        /// khóa chính ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// ngoại trú chi tiết đơn thuốc ID mz_cfmx bảng khóa chính
        /// </summary>
        public long mzcfmxId { get; set; }

        /// <summary>
        /// loại thao tác 1: phát thuốc 2: trả thuốc
        /// </summary>
        public string operateType { get; set; }

        /// <summary>
        /// số hiệu thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// thao tác số lượng  đơn vị nhỏ nhất
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// ghi chú
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }

    }
}
