
namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OrderMzVO
    {
        public string OrderNo { get; set; }
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// Số tiền đơn thuốc
        /// </summary>
        public decimal? cfje { get; set; }
        /// <summary>
        /// Chi tiết đơn thuốc
        /// </summary>
        public string cfxmmx { get; set; }
        /// <summary>
        /// Hạng mục thu phí đơn thuốc
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// Tên hạng mục thu phí đơn thuốc
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// Loại đơn thuốc EnumCflx
        /// </summary>
        public string cflx { get; set; }
        /// <summary>
        /// Có phải là đơn thuốc hay không
        /// </summary>
        public string isyp { get; set; }
        /// <summary>
        /// Giải thích đơn thuốc
        /// </summary>
        public string cfmc { get; set; }
    }
}
