namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// Thông tin sơ bộ chi tiết đơn thuốc giao diện trả lại
    /// </summary>
    public class fyDetailListRequest
    {
        /// <summary>
        /// Số đ ơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Chi tiết đ ơn thuốcID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// ThuốcTên
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// ThuốcMã
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Đ ơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Đ ơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Giá
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// Mã nội bộ đ ơn thuốc
        /// </summary>
        public int cfnm { get; set; }

        /// <summary>
        /// Tên hãng dược
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// Số lư ợng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// liều lư ợng 
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// liều lư ợng Đ ơn vị
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// cách dùng tên
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// Thời gian sắp xếp (Tần suất)
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// dùng lư ợng 
        /// </summary>
        public decimal yl { get; set; }

        /// <summary>
        /// dùng lư ợng Đ ơn vị
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yszt { get; set; }

        /// <summary>
        /// Tần suất
        /// </summary>
        public short plh { get; set; }
    }
}
