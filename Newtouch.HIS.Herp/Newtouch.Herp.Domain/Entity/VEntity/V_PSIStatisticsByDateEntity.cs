namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Thống kê nhập-xuất-tồn
    /// </summary>
    public class VPsiStatisticsByDateEntity
    {
        private string _statisticsDate = "";
        /// <summary>
        /// Thời gian thống kê
        /// </summary>
        public string statisticsDate
        {
            get { return _statisticsDate; }
            set { _statisticsDate = value; }
        }

        /// <summary>
        /// Số lượng
        /// </summary>
        public long sl { get; set; }

        private string _itemName = "";
        /// <summary>
        /// Tên dự án
        /// </summary>
        public string itemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }
    }

    /// <summary>
    /// Thống kê trang chủ highcharts Chuyên dụng
    /// </summary>
    public class ClassificationStatisticsEntity
    {
        private string _name = "";
        /// <summary>
        /// Tên dự án
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Số lượng
        /// </summary>
        public long y { get; set; }
    }
}
