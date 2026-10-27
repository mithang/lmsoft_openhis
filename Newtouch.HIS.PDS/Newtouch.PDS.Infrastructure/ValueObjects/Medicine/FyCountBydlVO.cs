namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Lần y lệnh|Lần thuốc trong đơn
    /// </summary>
    public class FyCountBydlVO
    {
        /// <summary>
        /// Tổng hợp lần phát thuốc
        /// </summary>
        public long ypCount { get; set; }

        /// <summary>
        /// Mã loại lớn
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// Tên loại lớn
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Cờ ngoại trú/nội trú
        /// </summary>
        public string mzzybz { get; set; }
    }

    /// <summary>
    /// Thống kê Số phát thuốc theo loại lớn  Thống kê trang chủhighchartsChuyên dùng
    /// </summary>
    public class FyClassificationStatistics
    {
        /// <summary>
        /// Tên loại lớn
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public long y { get; set; }
    }
}
