using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class MedicineDjandZhyzVO
    {
        /// <summary>
        /// thuốc đơn giá
        /// </summary>
        public decimal Dj { get; set; }
        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public decimal Zhyz { get; set; }
    }
}
