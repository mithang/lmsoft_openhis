namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chi tiết phát thuốc
    /// </summary>
    public class fydisplayDetailInfo
    {
        /// <summary>
        /// Chi tiết đơn thuốcID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// mz_cfmxph Khóa chính
        /// </summary>
        public string cfmxphId { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Số lượng tồn kho
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// Mã nội bộ đơn thuốc
        /// </summary>
        public int cfnm { get; set; }

        /// <summary>
        /// Tên hãng thuốc
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// Đơn vị liều lượng
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// Tên cách dùng
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// Sắp xếp thời gian（Tần suất）
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// Liều dùng
        /// </summary>
        public decimal yl { get; set; }

        /// <summary>
        /// Đơn vị liều dùng
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

        /// <summary>
        /// 0:Đã phát thuốc  1：Đã trả thuốc  -1：Tất cả
        /// </summary>
        public string gjzt { get; set; }
    }
}
