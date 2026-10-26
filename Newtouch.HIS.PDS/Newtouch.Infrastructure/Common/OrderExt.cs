using System;

namespace Newtouch.Infrastructure.Common
{
    /// <summary>
    /// Mở rộng phiếu
    /// </summary>
    public static class OrderExt
    {
        /// <summary>
        /// Tạo số phiếu đơn giản, thời điểm+Số ngẫu nhiên 3 chữ số
        /// </summary>
        /// <returns></returns>
        public static string GennerateSimpleOrderNo()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(100, 1000);
        }
    }
}