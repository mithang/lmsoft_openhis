namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 待处理事件汇总
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
        /// 门诊待排
        /// </summary>
        public long? mzdpCount { get; set; }

        /// <summary>
        /// 门诊待发药
        /// </summary>
        public long? mzdfCount { get; set; }

        /// <summary>
        /// 住院待排
        /// </summary>
        public long? zydpCount { get; set; }

        /// <summary>
        /// 住院待发药
        /// </summary>
        public long? zydfCount { get; set; }

        /// <summary>
        /// 住院待退药
        /// </summary>
        public long? zydtCount { get; set; }

        /// <summary>
        /// 申领待审核
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
