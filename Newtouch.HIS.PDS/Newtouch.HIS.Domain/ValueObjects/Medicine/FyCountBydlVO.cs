namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// y lệnh lần|đơn thuốc thuốc lần
    /// </summary>
    public class FyCountBydlVO
    {
        /// <summary>
        /// thuốc phát thuốc lần tổng hợp
        /// </summary>
        public long ypCount { get; set; }

        /// <summary>
        /// nhóm lớn mã
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// tên nhóm lớn
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// cờ ngoại trú/nội trú
        /// </summary>
        public string mzzybz { get; set; }
    }

    /// <summary>
    /// theo nhóm lớn thống kê phát thuốc số  trang chủ thống kê highcharts专用
    /// </summary>
    public class FyClassificationStatistics
    {
        /// <summary>
        /// tên nhóm lớn
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// số lượng
        /// </summary>
        public long y { get; set; }
    }
}
