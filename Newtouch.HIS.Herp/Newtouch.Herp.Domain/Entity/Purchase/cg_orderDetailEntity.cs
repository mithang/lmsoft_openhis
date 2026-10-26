using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Mua hàng _ Chi tiết đơn hàng
    /// </summary>
    [Table("cg_orderDetail")]
    public class CgOrderDetailEntity : IEntity<CgOrderDetailEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Đơn mua hàng Id
        /// </summary>
        public long orderId { get; set; }

        /// <summary>
        /// Nhà cung cấp ID
        /// </summary>
        public string supplierId { get; set; }

        /// <summary>
        /// Số đơn hàng con
        /// </summary>
        public string subOrderNo { get; set; }

        /// <summary>
        /// Chi tiết kế hoạch mua hàng ID,cg_purchaseOrderDetail Khóa chính bảng
        /// </summary>
        public long pdId { get; set; }

        /// <summary>
        /// Vật tư Id
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Số lượng,sl*zhyz=Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Chuyển đổi thành,Tương ứng với đơn vị
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Giá nhập,Tương ứng với đơn vị
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal jj { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// Đơn vị ID
        /// </summary>
        public string unitId { get; set; }

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