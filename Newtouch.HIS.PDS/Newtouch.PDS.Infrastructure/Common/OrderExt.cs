using System;

namespace Newtouch.Infrastructure.Common
{
    /// <summary>
    /// Mở rộng chứng từ
    /// </summary>
    public static class OrderExt
    {
        /// <summary>
        /// Tạo số đơn giản  Thời điểm+Số ngẫu nhiên 3 chữ số
        /// </summary>
        /// <returns></returns>
        public static string GennerateSimpleOrderNo()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(100, 1000);
        }
    }
}