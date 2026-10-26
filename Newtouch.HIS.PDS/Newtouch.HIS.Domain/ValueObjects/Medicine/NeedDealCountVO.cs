namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Tổng hợp sự kiện chờ xử lý
    /// </summary>
    public class NeedDealCountVO
    {
        /// <summary>
        /// điều chỉnh giá chờ duyệt
        /// </summary>
        public long? tjshCount { get; set; }

        /// <summary>
        /// thuốc điều chỉnh giá
        /// </summary>
        public long? tjypCount { get; set; }

        /// <summary>
        /// Ngoại trú chờ xếp
        /// </summary>
        public long? mzdpCount { get; set; }

        /// <summary>
        /// Ngoại trú chờ phát thuốc
        /// </summary>
        public long? mzdfCount { get; set; }

        /// <summary>
        /// Nội trú chờ xếp
        /// </summary>
        public long? zydpCount { get; set; }

        /// <summary>
        /// Nội trú chờ phát thuốc
        /// </summary>
        public long? zydfCount { get; set; }

        /// <summary>
        /// Nội trú chờ trả thuốc
        /// </summary>
        public long? zydtCount { get; set; }

        /// <summary>
        /// Lĩnh chờ duyệt
        /// </summary>
        public long? sldshCount { get; set; }

        /// <summary>
        /// xuất kho chờ duyệt
        /// </summary>
        public long? ckdshCount { get; set; }

        /// <summary>
        /// nhập kho chờ duyệt
        /// </summary>
        public long? rkdshCount { get; set; }

        /// <summary>
        /// hết hạn thuốc số lượng
        /// </summary>
        public long? expiryDrugCount { get; set; }

        /// <summary>
        /// khi ngày hết hạn thuốc số lượng
        /// </summary>
        public long? todayDrugCount { get; set; }

    }
}
