namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// vàobántồn tạiThống kêĐiều kiện tìm kiếm
    /// </summary>
    public class PsiStatisticsDTO
    {
        /// <summary>
        /// Bắt đầukết chuyểnThời gian
        /// </summary>
        public string ksjzsj { get; set; }

        /// <summary>
        /// Kết thúckết chuyểnThời gian
        /// </summary>
        public string jsjzsj { get; set; }

        /// <summary>
        /// vật tưTrạng thái
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
        /// Hiển thịkhôngvàobántồn tạiThuốc
        /// </summary>
        public string noPSI { get; set; }
    }
}
