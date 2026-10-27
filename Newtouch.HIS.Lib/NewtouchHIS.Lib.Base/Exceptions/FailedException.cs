using NewtouchHIS.Lib.Base.Model;
using System.DirectoryServices.Protocols;
using System.Runtime.CompilerServices;

namespace NewtouchHIS.Lib.Base.Exceptions
{
    /// <summary>
    /// Ngoại lệ có thể dự đoán khi xử lý nghiệp vụ thất bại
    /// </summary>
    public class FailedException : Exception
    {
        /// <summary>
        /// mã lỗi
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Mô tả lỗi
        /// </summary>
        public string? Msg { get; set; }

        /// <summary>
        /// Có ghi vào nhật ký không, mặc định là false, không ghi vào nhật ký
        /// </summary>
        public bool Log { get; set; } = false;
        public Exception? exception { get; set; } = null;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public FailedException()
        {

        }

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="msg">Mô tả lỗi</param>
        public FailedException(string msg)
        {
            this.Msg = msg;
        }

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="code">mã lỗi</param>
        /// <param name="msg">Mô tả lỗi</param>
        public FailedException(string code, string msg)
        {
            this.Code = code;
            this.Msg = msg;
        }
        public FailedException(ResponseResultCode code, string msg)
        {
            this.Code = ResultType.error.ToString();
            switch (code)
            {
                case ResponseResultCode.FAILOfConfigInit:
                    this.Msg = $"Khởi tạo mục cấu hình ngoại lệ: {msg}";
                    break;
                case ResponseResultCode.FAILOfEmpty:
                    this.Msg = $"Mục không được để trống: {msg}";
                    break;
                case ResponseResultCode.FAILOfExists:
                    this.Msg = $"Giá trị trùng lặp: {msg}";
                    break;                   
            }
            if (string.IsNullOrWhiteSpace(this.Msg))
            {
                this.Msg = msg;
            }
        }
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="code">mã lỗi</param>
        /// <param name="msg">Mô tả lỗi</param>
        /// <param name="log">Có ghi vào nhật ký không</param>
        public FailedException(string code, string msg, bool log)
        {
            this.Code = code;
            this.Msg = msg;
            this.Log = log;
        }
        public FailedException(string code, string msg, Exception ex, bool log = true)
        {
            this.Code = code;
            this.Msg = msg;
            this.Log = log;
            this.exception = ex;
        }


    }


    /// <summary>
    /// Ngoại lệ có thể dự đoán khi xử lý nghiệp vụ thất bại (nội dung nhắc nhở được khởi tạo trước từ cơ sở dữ liệu)
    /// </summary>
    public class FailedCodeException : FailedException
    {
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="code">mã lỗi</param>
        public FailedCodeException(string code)
        {
            this.Code = code;
        }

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="code">mã lỗi</param>
        /// <param name="log">Loại ngoại lệ này có ghi vào nhật ký không (thông thường không ghi)</param>
        public FailedCodeException(string code, bool log)
        {
            this.Code = code;
            this.Log = log;
        }
    }
}
