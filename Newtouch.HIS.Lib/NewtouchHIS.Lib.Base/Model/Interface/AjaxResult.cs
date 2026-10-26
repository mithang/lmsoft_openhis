namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// Ajaxnội dung phản hồi Model
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// kiểu kết quả thao tác
        /// </summary>
        public object state { get; set; }

        /// <summary>
        /// lấy nội dung tin nhắn
        /// </summary>
        public string? message { get; set; }

        /// <summary>
        /// lấy trả về dữ liệu
        /// </summary>
        public object? data { get; set; }

        /// <summary>
        /// mã lỗi
        /// </summary>
        public string? code { get; set; }

        /// <summary>
        /// dấu vết ngăn xếp của ngoại lệ
        /// </summary>
        public string? exStackTrace { get; set; }

        /// <summary>
        /// InnerException
        /// </summary>
        public Exception? InnerException { get; set; }
    }

    /// <summary>
    /// biểu thị ajax enum của kiểu kết quả thao tác
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// kiểu kết quả tin nhắn
        /// </summary>
        info,
        /// <summary>
        /// kiểu kết quả thành công
        /// </summary>
        success,
        /// <summary>
        /// kiểu kết quả cảnh báo
        /// </summary>
        warning,
        /// <summary>
        /// kiểu kết quả ngoại lệ
        /// </summary>
        error
    }
}
