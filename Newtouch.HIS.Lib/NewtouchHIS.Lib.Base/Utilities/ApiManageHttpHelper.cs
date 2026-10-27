using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Model;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace NewtouchHIS.Lib.Base.Utilities
{
    public class ApiManageHttpHelper
    {
        #region async
        public static async Task<BusResult<T>> PostAsync<T>(string url, string data)
        {
            var result = new ApiManageResult<T>();
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, 600); // 10là số giây, dùng để thiết lập thời gian chờ
                HttpContent content = new StringContent(data);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Connection.Add("keep-alive");
                var res = await client.PostAsync(url, content);
                if (res.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<ApiManageResult<T>>(res.Content.ReadAsStringAsync().Result);
                }
                client.Dispose();
            }
            if (result != null && result.HttpStatus == (int)HttpStatusCode.OK)
            {
                return result.BusData;
            }
            return new BusResult<T> { code = ResponseResultCode.ERROR, msg = $"Giao diện trả về ngoại lệ: {result?.Message}" };
        }
        /// <summary>
        /// Hỗ trợ yêu cầu Https 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="header"></param>
        /// <param name="requestEncoding"></param>
        /// <param name="mediaType"></param>
        /// <param name="CertificateCustomValid">Trang web https được truy cập sử dụng chứng chỉ tự ký hoặc chứng chỉ không đáng tin cậy, cần sử dụng đoạn mã sau để vô hiệu hóa kiểm tra SSL/TLS</param>
        /// <returns></returns>
        public static async Task<BusResult<T>> PostAsync<T>(string url, string data, Dictionary<string, string> header, Encoding? requestEncoding = null, string mediaType = "application/json", bool CertificateCustomValid = false)
        {
            var result = new ApiManageResult<T>();
            using (HttpClient client = CertificateCustomValid ? new HttpClient()
                : new HttpClient(new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                }))
            {
                if (url.StartsWith("https://"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                }
                client.Timeout = new TimeSpan(0, 0, 600); // 10là số giây, dùng để thiết lập thời gian chờ
                HttpContent content = new StringContent(data);
                if (header != null && header.Count() > 0)
                {
                    foreach (var item in header)
                    {
                        content.Headers.Add(item.Key, item.Value);
                    }
                }
                content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                client.DefaultRequestHeaders.Connection.Add("keep-alive");
                var res = await client.PostAsync(url, content);
                if (res.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<ApiManageResult<T>>(res.Content.ReadAsStringAsync().Result);
                }
                client.Dispose();
            }
            if (result != null && result.HttpStatus == (int)HttpStatusCode.OK)
            {
                return result.BusData;
            }
            return new BusResult<T> { code = ResponseResultCode.ERROR, msg = $"Giao diện trả về ngoại lệ: {result?.Message}" };
        }
        #endregion

        #region get
        /// <summary>
        /// getyêu cầu
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url, out string statusCode, Dictionary<string, string>? header = null)
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(
              new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            statusCode = response.StatusCode.ToString();
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }


        public static async Task<T> GetAsync<T>(string url, Dictionary<string, string>? header = null)
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            if (header != null && header.Count() > 0)
            {
                foreach (var item in header)
                {
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            HttpResponseMessage response = await httpClient.GetAsync(url);

            T result = default(T);

            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                string s = t.Result;

                result = JsonConvert.DeserializeObject<T>(s);
            }
            return result;
        }
        #endregion


    }
}
