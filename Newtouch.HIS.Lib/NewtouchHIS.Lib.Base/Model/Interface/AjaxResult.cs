namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// AjaxNội dung phản hồi Model
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// Loại kết quả thao tác
        /// </summary>
        public object state { get; set; }

        /// <summary>
        /// Lấy nội dung tin nhắn
        /// </summary>
        public string? message { get; set; }

        /// <summary>
        /// Lấy dữ liệu trả về
        /// </summary>
        public object? data { get; set; }

        /// <summary>
        /// mã lỗi
        /// </summary>
        public string? code { get; set; }

        /// <summary>
        /// Dấu vết ngăn xếp của ngoại lệ
        /// </summary>
        public string? exStackTrace { get; set; }

        /// <summary>
        /// InnerException
        /// </summary>
        public Exception? InnerException { get; set; }
    }

    /// <summary>
    /// Enum biểu thị loại kết quả thao tác ajax
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// Loại kết quả tin nhắn
        /// </summary>
        info,
        /// <summary>
        /// Loại kết quả thành công
        /// </summary>
        success,
        /// <summary>
        /// Loại kết quả cảnh báo
        /// </summary>
        warning,
        /// <summary>
        /// Loại kết quả ngoại lệ
        /// </summary>
        error
    }
}
