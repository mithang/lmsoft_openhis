using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Tồn kho _ Kết chuyển tồn kho
    /// </summary>
    [Table("kc_kcjz")]
    public class KcKcjzEntity : IEntity<KcKcjzEntity>
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
        /// Mã kho
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// Vật tư ID(Bắt buộc nhập)
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Thời gian hiệu lực
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Số lượng tồn kho,Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Giá bán lẻ khoa phòng,Đơn vị tương ứng với hệ số chuyển đổi
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal bmlsj { get; set; }

        /// <summary>
        /// Giá nhập,Đơn vị tương ứng với hệ số chuyển đổi
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal jj { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi(Bắt buộc nhập)
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Thời gian kết chuyển
        /// </summary>
        public DateTime jzsj { get; set; }

        /// <summary>
        /// Trạng thái  0:Hủy bỏ;1.Hiệu lực
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
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
