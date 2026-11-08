namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// vào bán tồn tại Thống kê Điều kiện tìm kiếm
    /// </summary>
    public class PsiStatisticsDTO
    {
        /// <summary>
        /// Bắt đầu kết chuyển Thời gian
        /// </summary>
        public string ksjzsj { get; set; }

        /// <summary>
        /// Kết thúc kết chuyển Thời gian
        /// </summary>
        public string jsjzsj { get; set; }

        /// <summary>
        /// vật tư Trạng thái
        /// </summary>
        public string wzzt { get; set; }

        /// <summary>
        /// nhập mã/Mã thuốc
        /// </summary>
        public string srm { get; set; }

        /// <summary>
        /// Nhóm lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// không sai tỷ lệ
        /// </summary>
        public string rate { get; set; }

        /// <summary>
        /// Hiển thị không vào bán tồn tại Thuốc
        /// </summary>
        public string noPSI { get; set; }
    }
}
