namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// vàobántồn tại Thống kê Điều kiện tìm kiếm
    /// </summary>
    public class PsiStatisticsDTO
    {
        /// <summary>
        /// Bắt đầukết chuyển Thời gian
        /// </summary>
        public string ksjzsj { get; set; }

        /// <summary>
        /// Kết thúckết chuyển Thời gian
        /// </summary>
        public string jsjzsj { get; set; }

        /// <summary>
        /// vật tư Trạng thái
        /// </summary>
        public string wzzt { get; set; }

        /// <summary>
        /// nhậpmã/Mã thuốc
        /// </summary>
        public string srm { get; set; }

        /// <summary>
        /// Nhóm lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// khôngsaitỷ lệ
        /// </summary>
        public string rate { get; set; }

        /// <summary>
        /// Hiển thịkhôngvàobántồn tại Thuốc
        /// </summary>
        public string noPSI { get; set; }
    }
}
