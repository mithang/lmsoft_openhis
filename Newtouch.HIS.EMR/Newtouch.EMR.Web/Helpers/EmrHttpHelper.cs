using System;
using System.Net.Http;
using System.Text;

namespace Newtouch.EMR.Web.Helpers
{
    /// <summary>
    /// Minimal synchronous JSON POST helper replacing the Framework-era
    /// Tools.Net.HttpClientHelper.HttpPostStringAndRead used by the QHD SmartCheck
    /// upload actions (medical-record upload to the external check service).
    /// Returns the response body as a string; throws on transport failure, same as
    /// the original helper. The upload host (MRUploadURL, 127.0.0.1:33333) does not
    /// exist in this deployment, so these actions fail fast with a clear error
    /// instead of hanging — the surrounding actions already surface that message.
    /// </summary>
    public static class EmrHttpHelper
    {
        private static readonly HttpClient Client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        public static string PostJsonString(string url, string jsonBody)
        {
            using var content = new StringContent(jsonBody ?? "", Encoding.UTF8, "application/json");
            using var response = Client.PostAsync(url, content).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }
    }
}
