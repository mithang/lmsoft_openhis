using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Extension;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace NewtouchHIS.Lib.Services.HttpService
{
    public class HttpClientHelper : IHttpClientHelper
    {
        readonly ILogger<HttpClientHelper> _logger;

        public HttpClientHelper(ILogger<HttpClientHelper> logger)
        {
            _logger = logger;
        }
        #region async
        public async Task<T> PostAsync<T>(string url, string data)
        {
            var result = default(T);
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, 600); // 10là số giây, dùng để thiết lập thời gian chờ
                HttpContent content = new StringContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Connection.Add("keep-alive");
                var res = await client.PostAsync(url, content);
                if (res.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(res.Content.ReadAsStringAsync().Result);
                }
                client.Dispose();
            }
            return result;
        }
        public async Task<T> PostAsync<T>(string url, string data,string token)
        {
            var result = default(T);
            using (HttpClient client = new HttpClient())
            {
                // Xây dựng thông điệp yêu cầu HTTP
                var requestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                    Content = new StringContent(data),
                    
                };
                client.Timeout = new TimeSpan(0, 0, 600); // 10là số giây, dùng để thiết lập thời gian chờ
                client.DefaultRequestHeaders.Connection.Add("keep-alive");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer",token);
                requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                 
                var res = await client.SendAsync(requestMessage);
                if (res.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(res.Content.ReadAsStringAsync().Result);
                }
                client.Dispose();
            }
            return result;
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
        public async Task<T> PostAsync<T>(string url, string data, Dictionary<string, string> header, Encoding? requestEncoding = null, string mediaType = "application/json", bool CertificateCustomValid = false)
        {
            var result = default(T);
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
                    result = JsonConvert.DeserializeObject<T>(res.Content.ReadAsStringAsync().Result);
                }
                client.Dispose();
            }
            return result;
        }
        #endregion

        #region get
        /// <summary>
        /// getyêu cầu
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string Get(string url, out string statusCode, Dictionary<string, string>? header = null)
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


        public async Task<T> GetAsync<T>(string url, Dictionary<string, string>? header = null)
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

        #region Newtouch
        /// <summary>
        /// Phương thức Post thông thường, gửi dữ liệu json, trả về ví dụ generic
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="timeout"></param>
        /// <param name="userAgent"></param>
        /// <param name="requestEncoding"></param>
        /// <param name="responseEncoding"></param>
        /// <param name="cookies"></param>
        /// <param name="contentType">mặc địnhjson</param>
        /// <returns></returns>
        public T HttpPostStringAndRead<T>(string url, string postDataStr
            , int? timeout = 120000, string? userAgent = null, Encoding? requestEncoding = null
            , Encoding? responseEncoding = null
            , CookieCollection? cookies = null
            , EnumContentType? contentType =
            EnumContentType.json)
        {
            var str = HttpPostString(url, postDataStr, timeout, userAgent, requestEncoding, responseEncoding
                , cookies, contentType);
            return str.ToObject<T>();
        }

        /// <summary>
        /// HttpProxy mặc định của yêu cầu:’Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)‘
        /// </summary>
        public static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        /// <summary>
        /// Loại tài liệu yêu cầu
        /// </summary>
        public enum EnumContentType
        {
            /// <summary>
            /// json
            /// </summary>
            json,
            /// <summary>
            /// form
            /// </summary>
            form,
            /// <summary>
            /// formdata
            /// </summary>
            formdata,
            /// <summary>
            /// xml
            /// </summary>
            xml
        }

        #region GET

        /// <summary>
        /// Phương thức Get thông thường, trả về string (tham số out là mã trạng thái http)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="responseStatusCode">HTTPmã trạng thái</param>
        /// <param name="timeout">Thời gian chờ yêu cầu (tính bằng mili giây)</param>
        /// <param name="userAgent"></param>
        /// <param name="responseEncoding">Mã hóa phản hồi, mặc định khi không chỉ định làUTF8</param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static string? HttpGetString(string url, out int responseStatusCode
            , int? timeout = 120000, string? userAgent = null
            , Encoding? responseEncoding = null, CookieCollection? cookies = null)
        {
            try
            {
                HttpWebResponse response;
                var resp = HttpGetString(url, out response, timeout: timeout, userAgent: userAgent, responseEncoding: responseEncoding, cookies: cookies);

                responseStatusCode = (int)response.StatusCode;

                return resp;
            }
            catch (WebException wex)
            {
                responseStatusCode = (int)((((HttpWebResponse)wex.Response)).StatusCode);
                return string.Empty;
            }
        }

        /// <summary>
        /// Phương thức Get thông thường, trả vềstring
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout">Thời gian chờ yêu cầu (tính bằng mili giây)</param>
        /// <param name="userAgent"></param>
        /// <param name="responseEncoding">Mã hóa phản hồi, mặc định khi không chỉ định làUTF8</param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static string HttpGetString(string url
            , int? timeout = 120000, string? userAgent = null
            , Encoding? responseEncoding = null, CookieCollection? cookies = null)
        {
            HttpWebResponse response;
            var resp = HttpGetString(url, out response, timeout: timeout, userAgent: userAgent, responseEncoding: responseEncoding, cookies: cookies);
            return resp;
        }

        #endregion GET end

        #region POST

        /// <summary>
        /// Phương thức Post thông thường, gửi dữ liệu json, trả về String (tham số out là mã trạng thái http)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="responseStatusCode">HTTPmã trạng thái</param>
        /// <param name="timeout">Thời gian chờ yêu cầu (tính bằng mili giây)</param>
        /// <param name="userAgent"></param>
        /// <param name="requestEncoding">Mã hóa nội dung yêu cầu, mặc định khi không chỉ định làUTF8</param>
        /// <param name="responseEncoding">Mã hóa phản hồi, mặc định khi không chỉ định làUTF8</param>
        /// <param name="cookies"></param>
        /// <param name="contentType">Mặc định gửi theo dạng biểu mẫu form</param>
        /// <returns></returns>
        public string HttpPostString(string url, string postDataStr
            , out int responseStatusCode
            , int? timeout = 120000, string? userAgent = null, Encoding? requestEncoding = null
            , Encoding? responseEncoding = null
            , CookieCollection? cookies = null
            , EnumContentType? contentType = null)
        {
            try
            {
                HttpWebResponse response;
                var resp = HttpPostString(url, postDataStr, out response, timeout: timeout
                    , userAgent: userAgent
                    , requestEncoding: requestEncoding
                    , responseEncoding: responseEncoding, cookies: cookies
                    , contentType: contentType);

                responseStatusCode = (int)response.StatusCode;

                return resp;
            }
            catch (WebException wex)
            {
                responseStatusCode = (int)((((HttpWebResponse)wex.Response)).StatusCode);
                return string.Empty;
            }
        }

        /// <summary>
        /// Phương thức Post thông thường, gửi dữ liệu json, trả vềString
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="timeout">Thời gian chờ yêu cầu (tính bằng mili giây)</param>
        /// <param name="userAgent"></param>
        /// <param name="requestEncoding">Mã hóa nội dung yêu cầu, mặc định khi không chỉ định làUTF8</param>
        /// <param name="responseEncoding">Mã hóa phản hồi, mặc định khi không chỉ định làUTF8</param>
        /// <param name="cookies"></param>
        /// <param name="contentType">Mặc định gửi theo dạng biểu mẫu form</param>
        /// <returns></returns>
        public string HttpPostString(string url, string postDataStr
            , int? timeout = 120000, string? userAgent = null, Encoding? requestEncoding = null
            , Encoding? responseEncoding = null
            , CookieCollection? cookies = null
            , EnumContentType? contentType = null)
        {
            HttpWebResponse response;
            var resp = HttpPostString(url, postDataStr, out response, timeout: timeout
                , userAgent: userAgent
                , requestEncoding: requestEncoding
                , responseEncoding: responseEncoding, cookies: cookies
                , contentType: contentType);
            return resp;
        }

        #endregion POST end

        #region private mthods

        /// <summary>
        /// Phương thức Get thông thường, trả vềstring
        /// </summary>
        /// <param name="url"></param>
        /// <param name="response">HttpWebResponse</param>
        /// <param name="timeout">Thời gian chờ yêu cầu (tính bằng mili giây)</param>
        /// <param name="userAgent"></param>
        /// <param name="responseEncoding">Mã hóa phản hồi, mặc định khi không chỉ định làUTF8</param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        private static string HttpGetString(string url
            , out HttpWebResponse response
            , int? timeout = 120000, string? userAgent = null
            , Encoding? responseEncoding = null, CookieCollection? cookies = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request!.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }

            response = request.GetResponse() as HttpWebResponse;
            Stream myResponseStream = response!.GetResponseStream();

            if (responseEncoding == null)
            {
                responseEncoding = Encoding.UTF8;
            }
            StreamReader myStreamReader = new StreamReader(myResponseStream, responseEncoding);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        /// <summary>
        /// Phương thức Post thông thường, gửi dữ liệu json, trả vềString
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="response">HttpWebResponse</param>
        /// <param name="timeout">Thời gian chờ yêu cầu (tính bằng mili giây)</param>
        /// <param name="userAgent"></param>
        /// <param name="requestEncoding">Mã hóa nội dung yêu cầu, mặc định khi không chỉ định làUTF8</param>
        /// <param name="responseEncoding">Mã hóa phản hồi, mặc định khi không chỉ định làUTF8</param>
        /// <param name="cookies"></param>
        /// <param name="contentType">Mặc định gửi theo dạng biểu mẫu form</param>
        /// <returns></returns>
        public string HttpPostString(string url, string postDataStr
            , out HttpWebResponse response
            , int? timeout = 120000, string? userAgent = null, Encoding? requestEncoding = null
            , Encoding? responseEncoding = null
            , CookieCollection? cookies = null
            , EnumContentType? contentType = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = null;
            //Nếu gửi yêu cầu HTTPS  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request!.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }

            request!.Method = "POST";
            request.ContentType = (contentType ?? EnumContentType.form) == EnumContentType.json
                ? "application/json"
                : (
                    (contentType ?? EnumContentType.form) == EnumContentType.xml
                    ? "text/xml"
                    : (
                        (contentType ?? EnumContentType.form) == EnumContentType.formdata
                        ? "multipart/form-data"
                        : "application/x-www-form-urlencoded"
                    )
                );

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }

            //Nếu cần dữ liệu POST  
            if (!string.IsNullOrWhiteSpace(postDataStr))
            {
                if (requestEncoding == null)
                {
                    requestEncoding = Encoding.UTF8;
                }
                byte[] data = requestEncoding.GetBytes(postDataStr.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            response = request.GetResponse() as HttpWebResponse;
            Stream myResponseStream = response!.GetResponseStream();
            if (responseEncoding == null)
            {
                responseEncoding = Encoding.UTF8;
            }
            StreamReader myStreamReader = new StreamReader(myResponseStream, responseEncoding);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        /// <summary>
        /// https
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //Luôn chấp nhận  
        }

        #endregion
        #endregion

    }


}
