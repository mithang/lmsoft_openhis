using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OrderInfoVO : OrderInfoBaseVO
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public decimal? PayFee { get; set; }
        /// <summary>
        /// Số giao dịch thanh toán
        /// </summary>
        public string PayLsh { get; set; }
        /// <summary>
        /// Phương thức thanh toán
        /// </summary>
        public int? PayWay { get; set; }
        /// <summary>
        /// Thông tin thân phận người thanh toán
        /// </summary>
        public string PayerId { get; set; }
        /// <summary>
        /// Thời gian thanh toán đơn hàng
        /// </summary>
        public DateTime? PayTime { get; set; }
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

        ///// <summary>
        ///// 
        ///// </summary>
        //public string zt { get; set; }
        public int patId { get; set; }
        public string dzId { get; set; }
    }

    public class OrderInfoBaseVO
    {
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
        public int? OrderStu { get; set; }
        /// <summary>
        /// Số tiền đơn hàng
        /// </summary>
        public decimal? OrderAmt { get; set; }
        /// <summary>
        /// Thời gian quá hạn khóa
        /// </summary>
        public DateTime? LockExpTime { get; set; }
    }
    /// <summary>
    /// Thông tin đơn hàng đầy đủ
    /// </summary>
    public class OutpOrderVO
    {
        /// <summary>
        /// Thông tin cơ bản
        /// </summary>
        public OrderInfoVO info { get; set; }
        /// <summary>
        /// Chi tiết đơn hàng
        /// </summary>
        public List<OrderMzVO> mx { get; set; }
        /// <summary>
        /// Loại đơn thuốc 0 Toàn bộ EnumYiZhuXZ
        /// </summary>
        public int? cflx { get; set; } 
    }

    public class InHosOrderVO
    {
        /// <summary>
        /// Thông tin cơ bản
        /// </summary>
        public OrderInfoVO info { get; set; }
        public HosPatientVo patient { get; set; }
        /// <summary>
        /// Ngày xuất viện(Mặc định cùng thời gian ra khỏi khu)
        /// </summary>
        public DateTime cyrq { get; set; }
    }

    public class OrderDetailVO
    {
        /// <summary>
        /// Chi tiết đơn hàng ngoại trú
        /// </summary>
        public List<OrderMzVO> mzList { get; set; }
        /// <summary>
        /// Chi tiết đơn hàng nội trú
        /// </summary>
        public List<OrderZyBaseVO> zyList { get; set; }
    }
}
