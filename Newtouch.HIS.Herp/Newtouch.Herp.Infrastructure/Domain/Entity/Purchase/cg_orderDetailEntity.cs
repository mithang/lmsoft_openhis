using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Mua sắm_Chi tiết đơn hàng
    /// </summary>
    [SugarTable("cg_orderDetail")]
    public class CgOrderDetailEntity : IEntity<CgOrderDetailEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Đơn mua sắm Id
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
        /// Chi tiết kế hoạch mua sắm ID,cg_purchaseOrderDetail Khóa chính của bảng
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
        /// Hệ số chuyển đổi,tương ứng với đơn vị
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Giá nhập,tương ứng với đơn vị
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
        /// Quy trình xử lý đơn mua sắm -1:Từ chối xử lý; 0:Chờ xử lý; 1:Chuẩn bị hàng; 2:Phân phối; 3:Ký nhận; 4:Hoàn thành; 5:Từ chối ký nhận 
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
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}