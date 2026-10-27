namespace NewtouchHIS.Base.Domain.Model
{
    /// <summary>
    /// Phân trangPhản hồi Model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageRespModel<T>
    {
        public int total { get; set; }
        public T? rows { get; set; }
    }

    public class QueryParamsBase
    {
        /// <summary>
        /// Truy vấn từ khóa
        /// </summary>
        public string? keyword { get; set; }
        /// <summary>
        /// Tổ chứcId
        /// </summary>
        public string? orgId { get; set; }
        /// <summary>
        /// Mã
        /// </summary>
        public string? code { get;set; }
        public List<string>? stringList { get; set; }
        /// <summary>
        /// KhoaMã
        /// </summary>
        public string? kscode { get; set; }
        /// <summary>
        /// Thời gian bắt đầu phát thuốc
        /// </summary>
        public DateTime? fykssj { get; set; }
        /// <summary>
        /// Thời gian kết thúc phát thuốc
        /// </summary>
        public DateTime? fyjssj { get; set; }
        /// <summary>
        /// ThuốcMã
        /// </summary>
        public string? ypdm { get; set; }
        /// <summary>
        /// Chi tiết nhập/xuất khoID
        /// </summary>
        public string? crkmxId { get; set; }
        /// <summary>
        /// 1 Đã đồng bộ còn lại chưa đồng bộ
        /// </summary>
        public string? zstbzt { get; set; }
        public string? isyfy { get; set; }
    }


}
