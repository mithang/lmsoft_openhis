using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Application.Interface
{
    public interface IPayApp
    {
        /// <summary>
        /// Hoàn tiền(đường cũ trả lại lần)
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <param name="refundAmount">Số tiền</param>
        /// <param name="refundReason">Hoàn tiền nguyên nhân </param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        int TradeRefund(string outTradeNo, decimal refundAmount, string refundReason,string outRequestNo
            , out string errorMsg);

    }
}
