namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Điều kiện truy vấn thống kê nhập xuất tồn
    /// </summary>
    public class PsiStatisticsParam
    {
        /// <summary>
        /// Thời gian bắt đầu kết chuyển
        /// </summary>
        public string ksjzsj { get; set; }

        /// <summary>
        /// Thời gian kết thúc kết chuyển
        /// </summary>
        public string jsjzsj { get; set; }

        /// <summary>
        /// Trạng thái thuốc
        /// </summary>
        public string ypzt { get; set; }

        /// <summary>
        /// Mã nhập/Mã thuốc
        /// </summary>
        public string srm { get; set; }

        /// <summary>
        /// Loại lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Dạng bào chế
        /// </summary>
        public string jx { get; set; }

        /// <summary>
        /// Tỷ lệ chênh lệch bằng 0
        /// </summary>
        public string rate { get; set; }

        /// <summary>
        /// Hiển thị thuốc không có nhập xuất tồn
        /// </summary>
        public string noPSI { get; set; }
    }

}
