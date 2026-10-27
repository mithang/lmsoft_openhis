using System.Runtime.CompilerServices;

namespace NewtouchHIS.Lib.Base.Model
{
    public class Request<T> : RequestBase
    {
        public Request() { }
        /// <summary>
        /// Dữ liệu yêu cầu nghiệp vụ
        /// </summary>
        public T? Data { get; set; }


    }

    public class RequestBus<T> : BusRequest
    {
        /// <summary>
        /// Dữ liệu yêu cầu nghiệp vụ
        /// </summary>
        public T? Data { get; set; }
    }

    /// <summary>
    /// Tự động đóng góiRequest
    /// </summary>
    public static class RequestApiGen
    {

        public static Request<T> GetRequest<T>(this Request<T> request)
        {
            request.AppId = request.AppId ?? ConfigInitHelper.SysConfig?.AppId;
            request.Timestamp = DateTime.Now;
            return request;
        }

    }
}
