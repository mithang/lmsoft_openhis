using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace NewtouchHIS.Lib.Services.HttpService
{
    public class HttpWebRequestHelper : IHttpWebRequestHelper
    {
        readonly ILogger<HttpWebRequestHelper> _logger;

        public HttpWebRequestHelper(ILogger<HttpWebRequestHelper> logger)
        {
            _logger = logger;
        }
        public T Request<T>(string url, string data, string method = "POST")
        {
            var result = default(T);
            HttpWebRequest request = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                var byteData = Encoding.UTF8.GetBytes(data);
                request.Method = method;
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                request.Timeout = 10000; // Thời gian chờ, đơn vị mili giây
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse(); // Gửi yêu cầu
                using (var resStream = response.GetResponseStream()) // Đọc dữ liệu
                {
                    using (var reader = new StreamReader(resStream, Encoding.UTF8))
                    {
                        result = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Sử dụng HttpWebRequest truy cập phương thức webapi ngoại lệ:\r url:{url} \r data:{data} \r Thông tin ngoại lệ:{ex.Message}");
            }
            finally
            {
                if (request != null)
                {
                    request.Abort(); // Giải phóng tài nguyên
                }
            }
            return result;
        }
    }


}
