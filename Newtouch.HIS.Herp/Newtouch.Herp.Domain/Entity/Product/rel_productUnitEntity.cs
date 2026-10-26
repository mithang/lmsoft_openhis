using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Đơn vị vật tư
    /// </summary>
    [Table("rel_productUnit")]
    public class RelProductUnitEntity : IEntity<RelProductUnitEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chức Id(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Vật tư ID(Bắt buộc nhập)
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Đơn vị ID,wz_untiI Khóa chính bảng(Bắt buộc nhập)
        /// </summary>
        public string unitId { get; set; }

        /// <summary>
        /// Tên đơn vị(Bắt buộc nhập)
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi(Bắt buộc nhập)
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Trạng thái  0:Hủy bỏ;1.Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
