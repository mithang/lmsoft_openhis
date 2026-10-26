namespace NewtouchHIS.Base.Domain.Model
{
    /// <summary>
    /// phản hồi phân trang Model
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
        /// truy vấn theo từ khóa
        /// </summary>
        public string? keyword { get; set; }
        /// <summary>
        /// cơ quanId
        /// </summary>
        public string? orgId { get; set; }
        /// <summary>
        /// mã
        /// </summary>
        public string? code { get;set; }
        public List<string>? stringList { get; set; }
        /// <summary>
        /// mã khoa
        /// </summary>
        public string? kscode { get; set; }
        /// <summary>
        /// thời gian bắt đầu phát thuốc
        /// </summary>
        public DateTime? fykssj { get; set; }
        /// <summary>
        /// thời gian kết thúc phát thuốc
        /// </summary>
        public DateTime? fyjssj { get; set; }
        /// <summary>
        /// mã thuốc
        /// </summary>
        public string? ypdm { get; set; }
        /// <summary>
        /// chi tiết xuất nhập khoID
        /// </summary>
        public string? crkmxId { get; set; }
        /// <summary>
        /// 1 đã đồng bộ phần còn lại chưa đồng bộ
        /// </summary>
        public string? zstbzt { get; set; }
        public string? isyfy { get; set; }
    }


}
