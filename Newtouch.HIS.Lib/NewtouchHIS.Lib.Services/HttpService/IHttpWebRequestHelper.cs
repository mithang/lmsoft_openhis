namespace NewtouchHIS.Lib.Services.HttpService
{
    public interface IHttpWebRequestHelper
    {
        /// <summary>
        /// Phương thức chung
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method">mặc địnhPOST</param>
        /// <returns></returns>
        T Request<T>(string url, string data, string method = "POST");
    }


}
