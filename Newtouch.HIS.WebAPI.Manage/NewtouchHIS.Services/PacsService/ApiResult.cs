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
        /// giá trị mặc định
        /// </summary>
        Default = -1,
        /// <summary>
        /// thành công
        /// </summary>
        SUCCESS = 0,
        /// <summary>
        /// thất bại
        /// </summary>
        FAIL = 1,



    }
}
