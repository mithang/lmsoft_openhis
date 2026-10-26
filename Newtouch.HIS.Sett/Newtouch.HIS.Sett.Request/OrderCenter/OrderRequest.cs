using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.HIS.Sett.Request
{
    public class OrderRequest : RequestBase
    {
        /// <summary>
        /// đ ơn hàng số 
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// Khám bệnhSố thẻ
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// lần này khám bệnh dấu hiệu ngoại trú:mzh;nhập viện:zyh
        /// </summary>
        public string VisitNo { get; set; }
        /// <summary>
        /// đ ơn hàng số tiền
        /// </summary>
        public decimal OrderAmt { get; set; }
        /// <summary>
        /// phư ơng thức thanh toán 
        /// </summary>
        public string PayWay { get; set; }
        /// <summary>
        /// thanh toán số tiền
        /// </summary>
        public decimal? PayFee { get; set; }
        /// <summary>
        /// thanh toán số thứ tự 
        /// </summary>
        public string PayLsh { get; set; }
        /// <summary>
        /// Thông tin thân phận người thanh toán
        /// </summary>
        public string PayerId { get; set; }
        /// <summary>
        /// HIS nội bộ số quyết toán
        /// </summary>
        public string SettTradeNo { get; set; }
        /// <summary>
        /// đ ơn hàng thanh toán thời gian
        /// </summary>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// đ ơn hàng loại enumOrderType
        /// </summary>
        public int? OrderType { get; set; }
        /// <summary>
        /// đ ơn hàng trạng thái enumOrderStatus
        /// </summary>
        public int? OrderStu { get; set; }
    }

    
}
