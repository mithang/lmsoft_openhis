namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Giao diện trả về thông tin sơ bộ chi tiết đơn thuốc
    /// </summary>
    public class fyDetailListRequest
    {
        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// chi tiết đơn thuốc ID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// đơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// giá
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// mã nội bộ đơn thuốc
        /// </summary>
        public int cfnm { get; set; }

        /// <summary>
        /// tên nhà máy dược
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// liều dùng
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// đơn vị liều dùng
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// cách dùng tên
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// sắp xếp thời gian  (tần suất)
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// liều lượng
        /// </summary>
        public decimal yl { get; set; }

        /// <summary>
        /// đơn vị liều lượng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yszt { get; set; }

        /// <summary>
        /// tần suất
        /// </summary>
        public short plh { get; set; }
    }
}
