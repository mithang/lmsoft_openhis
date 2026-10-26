using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Mua hàng _ Đơn hàng
    /// </summary>
    [Table("cg_order")]
    public class CgOrderEntity : IEntity<CgOrderEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chức Id(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số đơn mua hàng
        /// </summary>
        public string orderNo { get; set; }

        /// <summary>
        /// Loại đơn hàng 0:Phiếu lưu tạm;1:Phiếu chính thức
        /// </summary>
        public int orderType { get; set; }

        /// <summary>
        /// Quy trình xử lý đơn đặt mua hàng -1:Từ chối xử lý; 0:Chờ xử lý; 1:Chuẩn bị hàng; 2:Giao hàng; 3:Ký nhận; 4:Hoàn thành; 5:Từ chối ký 
        /// </summary>
        public int orderProcess { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Trạng thái 0:Hủy bỏ;1.Hiệu lực
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