using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain
{
    public class QRBarPayOutput
    {
        /// <summary>
        ///     Kết quả
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        ///     Tin nhắn
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Giá trị trả về
        /// </summary>
        public string ReturnValue { get; set; }

        /// <summary>
        /// Số đơn hàng bên ngoài
        /// </summary>
        public string outTradeNo { get; set; }

        /// <summary>
        /// Số đơn hàng 
        /// </summary>
        public string tradeNo { get; set; }

        /// <summary>
        /// Người muaID
        /// </summary> 
        public string OpenId { get; set; }

        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public string TotalAmount { get; set; }
        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public string PaymentAmount { get; set; }

        /// <summary>
        /// Trạng thái đơn hàng
        /// </summary>
        public string TradeStatus { get; set; }

        /// <summary>
        /// Giao dịch thành công,Giao dịch thất bại
        /// </summary>
        public bool TradeSuccess { get; set; }
    }

    public class RefundFeeOut
    {
        /// <summary>
        ///     Kết quả
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        ///     Tin nhắn
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Giá trị trả về
        /// </summary>
        public string ReturnValue { get; set; }
    }

}
