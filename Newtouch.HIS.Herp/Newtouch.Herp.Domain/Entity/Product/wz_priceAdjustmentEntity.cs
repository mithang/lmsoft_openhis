using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Vật tư _ Điều chỉnh giá
    /// </summary>
    [Table("wz_priceAdjustment")]
    public class WzPriceAdjustmentEntity : IEntity<WzPriceAdjustmentEntity>
    {
        /// <summary>
        /// Khóa chính Điều chỉnh giá vật tư Id
        /// </summary>
        [Key]
        public string wztjId { get; set; }

        /// <summary>
        /// Tổ chức Id(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Kho ID    (Gửi kho điều chỉnh giá)
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Giá bán lẻ Tương ứng với hệ số chuyển đổi,Thông thường là giá đơn vị khoa phòng
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsj { get; set; }

        /// <summary>
        /// Giá bán lẻ gốc Tương ứng với hệ số chuyển đổi,Thông thường là giá đơn vị khoa phòng
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal ylsj { get; set; }

        /// <summary>
        /// Đơn vị Và zhyz Tương ứng,Thông thường là đơn vị khoa phòng
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// Trạng thái duyệt   0:Chưa duyệt 1:Đã duyệt 2:Đã từ chối 3.Đã hủy
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// Sửa loại
        /// </summary>
        public string xglx { get; set; }

        /// <summary>
        /// Thời gian điều chỉnh
        /// </summary>
        public DateTime tzsj { get; set; }

        /// <summary>
        /// Điều chỉnh tệp
        /// </summary>
        public string tzwj { get; set; }

        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public DateTime zxsj { get; set; }

        /// <summary>
        /// Điều chỉnh người thao tác
        /// </summary>
        public string tzczy { get; set; }

        /// <summary>
        /// Người thao tác duyệt
        /// </summary>
        public string shczy { get; set; }

        /// <summary>
        /// Người thực hiện thao tác
        /// </summary>
        public string zxczy { get; set; }

        /// <summary>
        /// Cờ thực hiện   0:Chưa thực hiện 1:Đã thực hiện
        /// </summary>
        public string zxbz { get; set; }

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
