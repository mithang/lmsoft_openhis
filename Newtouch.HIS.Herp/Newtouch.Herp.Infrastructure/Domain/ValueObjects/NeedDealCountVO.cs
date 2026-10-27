namespace Newtouch.Herp.Domain.ValueObjects
{
    /// <summary>
    /// Tổng hợp sự kiện chờ xử lý
    /// </summary>
    public class NeedDealCountVO
    {
        /// <summary>
        /// Điều chỉnh giá chờ duyệt
        /// </summary>
        public long? tjshCount { get; set; }

        /// <summary>
        /// Vật tư điều chỉnh giá
        /// </summary>
        public long? tjwzCount { get; set; }

        /// <summary>
        /// Phiếu lĩnh chờ duyệt
        /// </summary>
        public long? sldshCount { get; set; }

        /// <summary>
        /// Xuất kho chờ duyệt
        /// </summary>
        public long? ckdshCount { get; set; }

        /// <summary>
        /// Nhập kho ngoài chờ duyệt
        /// </summary>
        public long? wbrkCount { get; set; }

        /// <summary>
        /// Trả hàng nội bộ chờ duyệt
        /// </summary>
        public long? nbthCount { get; set; }

        /// <summary>
        /// Nhập kho chờ duyệt
        /// </summary>
        public long? rkdshCount { get; set; }

        /// <summary>
        /// Số lượng thuốc quá hạn
        /// </summary>
        public long? expriedWzCount { get; set; }
    }
}
