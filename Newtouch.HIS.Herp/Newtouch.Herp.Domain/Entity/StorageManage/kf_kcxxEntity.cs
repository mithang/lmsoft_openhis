using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Bảng tồn kho
    /// </summary>
    [Table("kf_kcxx")]
    public class KfKcxxEntity : IEntity<KfKcxxEntity>
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
        /// Kho ID
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        ///  Vật tư ID
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
        /// Hạn sử dụng
        /// </summary>
        public DateTime yxq { get; set; }

        /// <summary>
        /// Số lượng tồn kho Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Số lượng đóng băng Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// Chi tiết nhập/xuất kho ID
        /// </summary>
        public long crkmxId { get; set; }

        /// <summary>
        /// Giá nhập Đơn vị tương ứng với hệ số chuyển đổi
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal jj { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi,Chuyển đổi thành đơn vị kho hiện tại
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Khóa tồn kho 0/null:Chưa khóa      >0:Đã khóa
        /// </summary>
        public int? locked { get; set; }

        /// <summary>
        /// Trạng thái
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
