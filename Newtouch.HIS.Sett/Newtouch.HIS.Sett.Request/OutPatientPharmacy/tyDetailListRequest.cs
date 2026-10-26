namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// Ngoại trú trả thuốc hiển thị thuốc thông tin chi tiết
    /// </summary>
    public class tyDetailListRequest
    {
        /// <summary>
        /// Chi tiết đ ơn thuốcID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// Số đ ơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// ThuốcMã
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// ThuốcTên
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 频 số 
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Số lư ợng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Đ ơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Đ ơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// liều lư ợng 
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// liều lư ợng Đ ơn vị
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// cách dùng 
        /// </summary>
        public string yf { get; set; }

        /// <summary>
        /// Tần suất
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// dùng lư ợng 
        /// </summary>
        public decimal yl { get; set; }

        /// <summary>
        /// dùng Đ ơn vị
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yszt { get; set; }

        /// <summary>
        /// xếp cột số 
        /// </summary>
        public short plh { get; set; }

        /// <summary>
        /// Mã nội bộ đ ơn thuốc
        /// </summary>
        public int cfnm { get; set; }
    }
}
