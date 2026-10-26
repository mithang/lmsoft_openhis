using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.PayDto
{
    public class MicroPayTradeInfoDTO
    {
        /// <summary>
        /// Nguồn thanh toán Alipay1 WeChat2
        /// </summary>
        public int? PayType { get; set; }

        /// <summary>
        /// Số đơn hàng yêu cầu thanh toán
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// Số giao dịch bên thứ ba
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// Thời gian giao dịch
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public decimal? PayAmount { get; set; }


        /// <summary>
        /// Thanh toán có thành công hay không 0 không 1 là
        /// </summary>
        public int? PayStatus { get; set; }

        public string PayMemo { get; set; }

        /// <summary>
        /// Tài khoản thanh toán
        /// </summary>
        public string PayAccount { get; set; }

        /// <summary>
        /// Mô tả giao dịch
        /// </summary>
        public string TradeDesc { get; set; }

        /// <summary>
        /// Thời gian yêu cầu thanh toán
        /// </summary>
        public DateTime? OutTradeTime { get; set; }

        /// <summary>
        /// Số tiền hoàn tiền
        /// </summary>
        public decimal? RefundAmount { get; set; }

        /// <summary>
        /// Thời gian giao dịch hoàn tiền trên nền tảng
        /// </summary>
        public DateTime? RefundDate { get; set; }

        /// <summary>
        /// Lý do hoàn tiền
        /// </summary>
        public string RefundReason { get; set; }

        public int? RefundStatus { get; set; }
        public string RefundMemo { get; set; }

        /// <summary>
        /// Thời gian yêu cầu hoàn tiền
        /// </summary>
        public DateTime? OutRefundTime { get; set; }

        /// <summary>
        /// Số yêu cầu hoàn tiền
        /// </summary>
        public string OutRequestNo { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Số khám ngoại trú/Số nhập viện
        /// </summary>
        public string patno { get; set; }
        /// <summary>
        /// Trạng thái giao dịch
        /// </summary>
        public string TradeStatus { get; set; }
    }
}
