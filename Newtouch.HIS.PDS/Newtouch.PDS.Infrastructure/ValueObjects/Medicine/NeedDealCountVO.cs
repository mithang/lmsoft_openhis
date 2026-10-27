namespace Newtouch.HIS.Domain.ValueObjects
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
        /// Thuốc điều chỉnh giá
        /// </summary>
        public long? tjypCount { get; set; }

        /// <summary>
        /// Chờ xếp ngoại trú
        /// </summary>
        public long? mzdpCount { get; set; }

        /// <summary>
        /// Thuốc ngoại trú chờ phát
        /// </summary>
        public long? mzdfCount { get; set; }

        /// <summary>
        /// Chờ xếp nội trú
        /// </summary>
        public long? zydpCount { get; set; }

        /// <summary>
        /// Chờ phát thuốc nội trú
        /// </summary>
        public long? zydfCount { get; set; }

        /// <summary>
        /// Trả thuốc nội trú chờ
        /// </summary>
        public long? zydtCount { get; set; }

        /// <summary>
        /// Lĩnh chờ duyệt
        /// </summary>
        public long? sldshCount { get; set; }

        /// <summary>
        /// Xuất kho chờ duyệt
        /// </summary>
        public long? ckdshCount { get; set; }

        /// <summary>
        /// Nhập kho chờ duyệt
        /// </summary>
        public long? rkdshCount { get; set; }

        /// <summary>
        /// Số lượng thuốc hết hạn
        /// </summary>
        public long? expiryDrugCount { get; set; }

        /// <summary>
        /// Số lượng thuốc hết hạn trong ngày
        /// </summary>
        public long? todayDrugCount { get; set; }

    }
}
