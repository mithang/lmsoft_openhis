namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// thống kê nhập-xuất-tồn điều kiện tra cứu
    /// </summary>
    public class PsiStatisticsParam
    {
        /// <summary>
        /// bắt đầu thời gian kết chuyển
        /// </summary>
        public string ksjzsj { get; set; }

        /// <summary>
        /// kết thúc thời gian kết chuyển
        /// </summary>
        public string jsjzsj { get; set; }

        /// <summary>
        /// trạng thái thuốc
        /// </summary>
        public string ypzt { get; set; }

        /// <summary>
        /// mã nhập/mã thuốc
        /// </summary>
        public string srm { get; set; }

        /// <summary>
        /// nhóm lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// dạng bào chế
        /// </summary>
        public string jx { get; set; }

        /// <summary>
        /// tỷ lệ chênh lệch bằng 0
        /// </summary>
        public string rate { get; set; }

        /// <summary>
        /// hiển thị không có nhập-xuất-tồn thuốc
        /// </summary>
        public string noPSI { get; set; }
    }

}
