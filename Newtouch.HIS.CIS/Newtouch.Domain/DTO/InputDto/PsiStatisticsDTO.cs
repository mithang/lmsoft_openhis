namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// Điều kiện tra cứu thống kê nhập xuất tồn
    /// </summary>
    public class PsiStatisticsDTO
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
        /// Trạng thái vật tư
        /// </summary>
        public string wzzt { get; set; }

        /// <summary>
        /// Mã nhập/Mã thuốc
        /// </summary>
        public string srm { get; set; }

        /// <summary>
        /// Nhóm lớn
        /// </summary>
        public string dl { get; set; }

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
