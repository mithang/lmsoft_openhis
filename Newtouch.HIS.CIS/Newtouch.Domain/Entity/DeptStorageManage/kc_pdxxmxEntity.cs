using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.Domain.Entity.DeptStorageManage
{
    /// <summary>
    /// Chi tiết thông tin kiểm kê tồn kho
    /// </summary>
    [Table("dept_pdxxmx")]
    public class KcPdxxmxEntity : IEntity<KcPdxxmxEntity>
    {
        /// <summary>
        /// Khóa chính Chi tiếtID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Bảng chính kiểm kêID
        /// </summary>
        public long pdId { get; set; }

        /// <summary>
        /// Vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        ///  Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Số lượng lý thuyết
        /// </summary>
        public int llsl { get; set; }

        /// <summary>
        /// Số lượng thực tế
        /// </summary>
        public int sjsl { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsj { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
