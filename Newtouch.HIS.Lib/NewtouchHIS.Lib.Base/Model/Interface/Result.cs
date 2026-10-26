using Newtonsoft.Json;
using System.Net;

namespace NewtouchHIS.Lib.Base.Model
{
    public class BusResult<T>
    {
        /// <summary>
        /// mã trả về nghiệp vụ
        /// </summary>
        public ResponseResultCode code { get; set; }

        /// <summary>
        /// mô tả mã trả về nghiệp vụ
        /// </summary>
        public string? msg { get; set; }

        /// <summary>
        /// trả về dữ liệu
        /// </summary>
        [JsonProperty("data")]
        public T? Data { get; set; }

        public BusResult()
        {
            code = ResponseResultCode.Default;
            msg = "";
        }
        public BusResult(bool isSuccess = false, string? msg = null)
        {
            code = isSuccess ? ResponseResultCode.SUCCESS : ResponseResultCode.FAIL;
            msg = msg ?? "";
        }
    }

    public class BusResultPage<T> : BusResult<T>
    {
        public Pagination page { get; set; }
        public BusResultPage()
        {
            page = new Pagination() { page = 1, rows = 20 };
        }
    }

    /// <summary>
    /// lớp cơ sở mô hình trả về dữ liệu
    /// </summary>
    public class Result
    {
        /// <summary>
        /// trả về mã trạng thái
        /// </summary>
        public virtual HttpStatusCode HttpStatus { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// lấy nội dung tin nhắn
        /// </summary>
        public virtual string? Message { get; set; } = "OK";

        /// <summary>
        /// trả về dữ liệu
        /// </summary>
        public virtual object? BusData { get; set; }

        /// <summary>
        /// thời lượng thực hiện
        /// </summary>
        public virtual long TimeOut { get; set; }

        public static Result BuildResult(object? data, string? errormsg, long elapsedMilliseconds, ResponseResultCode responseResultCode, int httpStatusCode = 200)
        {
            return new Result
            {
                HttpStatus = (HttpStatusCode)httpStatusCode,
                BusData = new ResponseBase
                {
                    code = responseResultCode,
                    msg = errormsg,
                    data = data
                },
                TimeOut = elapsedMilliseconds
            };
        }

        public static Result ResultException(string msg, object? data = null, long elapsedMilliseconds = 0)
        {
            return new Result
            {
                HttpStatus = HttpStatusCode.OK,
                BusData = new ResponseBase
                {
                    code = ResponseResultCode.FAIL,
                    msg = msg,
                    data = data
                },
                TimeOut = elapsedMilliseconds
            };
        }


    }

    public class ApiManageResult<T>
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// thời lượng thực hiện
        /// </summary>
        public virtual long TimeOut { get; set; }
        public BusResult<T> BusData { get; set; }
    }

}
