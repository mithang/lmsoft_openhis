using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class MedicineDjandZhyzVO
    {
        /// <summary>
        /// Đơn giá thuốc
        /// </summary>
        public decimal Dj { get; set; }
        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public decimal Zhyz { get; set; }
    }
}
