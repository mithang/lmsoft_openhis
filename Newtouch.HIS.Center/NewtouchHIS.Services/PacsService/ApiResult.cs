namespace NewtouchHIS.Lib.Base.Model.PACS
{
    public class ApiResult<T>
    {
        public ApiResponseResultCode code { get; set; }

        public string? msg { get; set; }
        public T? data { get; set; }
        public T? errList { get; set; }

        public ApiResult()
        {
            code = ApiResponseResultCode.Default;
            msg = "";
        }

        public ApiResult(bool isSuccess = false, string? msg = null)
        {
            code = (isSuccess ? ApiResponseResultCode.SUCCESS : ApiResponseResultCode.FAIL);
            msg = msg ?? "";
        }
    }

    public enum ApiResponseResultCode
    {
        /// <summary>
        /// Giá trị mặc định
        /// </summary>
        Default = -1,
        /// <summary>
        /// Thành công
        /// </summary>
        SUCCESS = 0,
        /// <summary>
        /// Thất bại
        /// </summary>
        FAIL = 1,



    }
}
