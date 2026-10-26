
using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Bảng thông tin đơn hàng 2023-9-8 chl
    /// </summary>
    [Table("order_info")]
    public class OrderInfoEntity : IEntity<OrderInfoEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// Loại đơn hàng EnumOrderType
        /// </summary>
        public int? OrderType { get; set; }
        /// <summary>
        /// Số thẻ khám bệnh
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// Dấu khám bệnh lần này  Khám ngoại trú:mzh;Nhập viện:zyh
        /// </summary>
        public string VisitNo { get; set; }
        /// <summary>
        /// Trạng thái đơn hàng EnumOrderStatus
        /// </summary>
        public int OrderStu { get; set; }
        /// <summary>
        /// Số tiền đơn hàng
        /// </summary>
        public decimal OrderAmt { get; set; }
        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public decimal? PayFee { get; set; }
        /// <summary>
        /// Số giao dịch thanh toán
        /// </summary>
        public string PayLsh { get; set; }
        /// <summary>
        /// Thông tin thân phận người thanh toán
        /// </summary>
        public string PayerId { get; set; }        
        /// <summary>
        /// Thời gian thanh toán đơn hàng
        /// </summary>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// Phương thức thanh toán
        /// </summary>
        public int? PayWay { get; set; }
        /// <summary>
        /// HISSố quyết toán nội bộ
        /// </summary>
        public string SettTradeNo { get; set; }
        public string Memo { get; set; }
        /// <summary>
        /// Người tạoID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        public int? patid { get; set; }
        public string dzId { get; set; }
    }
}
