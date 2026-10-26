using NewtouchHIS.Lib.Base.Model;
using System.DirectoryServices.Protocols;
using System.Runtime.CompilerServices;

namespace NewtouchHIS.Lib.Base.Exceptions
{
    /// <summary>
    /// ngoại lệ có thể dự đoán khi xử lý nghiệp vụ thất bại
    /// </summary>
    public class FailedException : Exception
    {
        /// <summary>
        /// mã lỗi
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// mô tả lỗi
        /// </summary>
        public string? Msg { get; set; }

        /// <summary>
        /// có ghi vào nhật ký hay không，mặc địnhfalse，không ghi vào nhật ký
        /// </summary>
        public bool Log { get; set; } = false;
        public Exception? exception { get; set; } = null;

        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        public FailedException()
        {

        }

        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        /// <param name="msg">mô tả lỗi</param>
        public FailedException(string msg)
        {
            this.Msg = msg;
        }

        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        /// <param name="code">mã lỗi</param>
        /// <param name="msg">mô tả lỗi</param>
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
                    this.Msg = $"配置项初始化异常：{msg}";
                    break;
                case ResponseResultCode.FAILOfEmpty:
                    this.Msg = $"不可为空项：{msg}";
                    break;
                case ResponseResultCode.FAILOfExists:
                    this.Msg = $"值重复：{msg}";
                    break;                   
            }
            if (string.IsNullOrWhiteSpace(this.Msg))
            {
                this.Msg = msg;
            }
        }
        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        /// <param name="code">mã lỗi</param>
        /// <param name="msg">mô tả lỗi</param>
        /// <param name="log">có ghi vào nhật ký hay không</param>
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
    /// ngoại lệ có thể dự đoán khi xử lý nghiệp vụ thất bại（nội dung nhắc được khởi tạo trước từ cơ sở dữ liệu）
    /// </summary>
    public class FailedCodeException : FailedException
    {
        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        /// <param name="code">mã lỗi</param>
        public FailedCodeException(string code)
        {
            this.Code = code;
        }

        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        /// <param name="code">mã lỗi</param>
        /// <param name="log">ngoại lệ kiểu này có ghi vào nhật ký hay không（trường hợp thông thường không ghi）</param>
        public FailedCodeException(string code, bool log)
        {
            this.Code = code;
            this.Log = log;
        }
    }
}
