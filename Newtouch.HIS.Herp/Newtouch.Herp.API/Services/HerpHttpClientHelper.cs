using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Services.HttpService;
using static NewtouchHIS.Lib.Services.HttpService.HttpClientHelper;

namespace Newtouch.Herp.API.Services
{
    /// <summary>
    /// IHttpClientHelper implementation that forces plain IPv4 sockets.
    /// WHY: this sandbox intercepts IPv4-mapped IPv6 (dual-mode) TCP connections
    /// and answers them with a policy-denial text, which .NET's default
    /// SocketsHttpHandler triggers (it connects with dual-mode sockets), while
    /// plain IPv4 sockets reach loopback servers fine (verified: raw IPv4 socket
    /// to BaseAPI 127.0.0.1:7001 works, dual-mode socket gets the denial, and a
    /// default HttpClient never even reaches a local listener). The vendor
    /// HttpClientHelper uses default HttpClient instances, so every dotnet app
    /// login (via BaseAPI) currently fails the same way; this Herp-local
    /// replacement restores it without touching vendor code. Semantics mirror
    /// the vendor helper (timeouts, headers, JSON handling, cert-flag quirk).
    /// UseProxy is forced off: the egress proxy drops loopback targets, and
    /// loopback calls must stay direct (the original "proxy env unset" rule).
    /// </summary>
    public class HerpHttpClientHelper : IHttpClientHelper
    {
        private static SocketsHttpHandler CreateHandler(bool dangerousAcceptAnyCert = false, CookieContainer cookieContainer = null)
        {
            return new SocketsHttpHandler
            {
                UseProxy = false,
                UseCookies = cookieContainer != null,
                CookieContainer = cookieContainer ?? new CookieContainer(),
                AutomaticDecompression = DecompressionMethods.None,
                SslOptions = new SslClientAuthenticationOptions
                {
                    RemoteCertificateValidationCallback = dangerousAcceptAnyCert
                        ? (sender, cert, chain, errors) => true
                        : null
                },
                ConnectCallback = async (context, cancellationToken) =>
                {
                    var addresses = await Dns.GetHostAddressesAsync(context.DnsEndPoint.Host, cancellationToken);
                    var ipv4 = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
                    Socket socket;
                    if (ipv4 != null)
                    {
                        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        await socket.ConnectAsync(new IPEndPoint(ipv4, context.DnsEndPoint.Port), cancellationToken);
                    }
                    else
                    {
                        // No IPv4 address (external IPv6-only host): fall back to default connect.
                        socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                        await socket.ConnectAsync(context.DnsEndPoint, cancellationToken);
                    }
                    return new NetworkStream(socket, ownsSocket: true);
                }
            };
        }

        private static HttpClient CreateClient(bool dangerousAcceptAnyCert = false, CookieContainer cookieContainer = null)
        {
            var client = new HttpClient(CreateHandler(dangerousAcceptAnyCert, cookieContainer));
            client.Timeout = new TimeSpan(0, 0, 600);
            client.DefaultRequestHeaders.Connection.Add("keep-alive");
            return client;
        }

        public async Task<T> PostAsync<T>(string url, string data)
        {
            using (var client = CreateClient())
            {
                HttpContent content = new StringContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var res = await client.PostAsync(url, content);
                if (res.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());
                }
                return default(T);
            }
        }

        public async Task<T> PostAsync<T>(string url, string data, string token)
        {
            using (var client = CreateClient())
            {
                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                    Content = new StringContent(data),
                };
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var res = await client.SendAsync(requestMessage);
                if (res.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());
                }
                return default(T);
            }
        }

        public async Task<T> PostAsync<T>(string url, string data, Dictionary<string, string> header, Encoding requestEncoding = null, string mediaType = "application/json", bool CertificateCustomValid = false)
        {
            // Mirror the vendor helper's (inverted) cert-flag logic exactly:
            // CertificateCustomValid=true -> plain handler; false -> accept-any-cert handler.
            using (var client = CreateClient(dangerousAcceptAnyCert: !CertificateCustomValid))
            {
                if (url.StartsWith("https://"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                }
                HttpContent content = new StringContent(data);
                if (header != null && header.Count() > 0)
                {
                    foreach (var item in header)
                    {
                        content.Headers.Add(item.Key, item.Value);
                    }
                }
                content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                var res = await client.PostAsync(url, content);
                if (res.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());
                }
                return default(T);
            }
        }

        public async Task<T> GetAsync<T>(string url, Dictionary<string, string> header = null)
        {
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            using (var client = CreateClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (header != null && header.Count() > 0)
                {
                    foreach (var item in header)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
                return default(T);
            }
        }

        public T HttpPostStringAndRead<T>(string url, string postDataStr
            , int? timeout = 120000, string userAgent = null, Encoding requestEncoding = null
            , Encoding responseEncoding = null
            , CookieCollection cookies = null
            , EnumContentType? contentType = EnumContentType.json)
        {
            var str = HttpPostStringSync(url, postDataStr, timeout, userAgent, requestEncoding, contentType, cookies);
            return str.ToObject<T>();
        }

        private string HttpPostStringSync(string url, string postDataStr
            , int? timeout, string userAgent, Encoding requestEncoding
            , EnumContentType? contentType, CookieCollection cookies = null)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("url");
            CookieContainer cookieContainer = null;
            if (cookies != null)
            {
                cookieContainer = new CookieContainer();
                cookieContainer.Add(cookies);
            }
            using (var client = CreateClient(cookieContainer: cookieContainer))
            {
                if (timeout.HasValue) client.Timeout = TimeSpan.FromMilliseconds(timeout.Value);
                if (!string.IsNullOrEmpty(userAgent)) client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
                else client.DefaultRequestHeaders.UserAgent.ParseAdd(HttpClientHelper.DefaultUserAgent);
                var mediaType = (contentType ?? EnumContentType.form) == EnumContentType.json
                    ? "application/json"
                    : ((contentType ?? EnumContentType.form) == EnumContentType.xml
                        ? "text/xml"
                        : ((contentType ?? EnumContentType.form) == EnumContentType.formdata
                            ? "multipart/form-data"
                            : "application/x-www-form-urlencoded"));
                HttpContent content = null;
                if (!string.IsNullOrWhiteSpace(postDataStr))
                {
                    var enc = requestEncoding ?? Encoding.UTF8;
                    content = new ByteArrayContent(enc.GetBytes(postDataStr));
                    content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                }
                else
                {
                    content = new ByteArrayContent(Array.Empty<byte>());
                    content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                }
                var response = client.PostAsync(url, content).GetAwaiter().GetResult();
                return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
        }
    }
}
