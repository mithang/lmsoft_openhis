using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Extension;
using System.Text;

namespace NewtouchHIS.Lib.Base.Utilities
{
    public class HttpWebHelper
    {
        public static IServiceCollection? servicesCollection;
        public static HttpContext HttpCurrent
        {
            get
            {
                object factory = servicesCollection.BuildServiceProvider().GetService(typeof(IHttpContextAccessor));
                HttpContext context = ((IHttpContextAccessor)factory).HttpContext;
                return context;
            }
        }

        #region Sessionthao tác

        /// <summary>
        /// ghiSession
        /// </summary>
        /// <param name="key">Sessiontên khóa của</param>
        /// <param name="value">Sessionkhóa-giá trị của</param>
        public static void WriteSession(string key, string value)
        {
            WriteSession<string>(key, value);
        }

        /// <summary>
        /// ghiSession
        /// </summary>
        /// <typeparam name="T">Sessionkiểu khóa-giá trị</typeparam>
        /// <param name="key">Sessiontên khóa của</param>
        /// <param name="value">Sessionkhóa-giá trị của</param>
        public static void WriteSession<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                return;
            HttpCurrent.Session.SetString(key, value.ToJson());
        }

        /// <summary>
        /// đọcSessiongiá trị của
        /// </summary>
        /// <param name="key">Sessiontên khóa của</param>        
        public static string GetSession(string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;
            return HttpCurrent.Session.GetString(key);
        }

        /// <summary>
        /// lấySessiongiá trị đối tượng trong
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetSessionObjVal<T>(string key)
            where T : class
        {
            if (string.IsNullOrEmpty(key))
                return null;
            var val = HttpCurrent.Session.GetString(key);
            if (!string.IsNullOrEmpty(val))
            {
                return JsonConvert.DeserializeObject<T>(val);
            }
            return null;
        }

        /// <summary>
        /// xóa chỉ địnhSession
        /// </summary>
        /// <param name="key">Sessiontên khóa của</param>
        public static void RemoveSession(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;
            HttpCurrent.Session.Remove(key);
        }

        #endregion

        #region Cookiethao tác

        /// <summary>
        /// ghicookiegiá trị
        /// </summary>
        /// <param name="strName">tên</param>
        /// <param name="strValue">giá trị</param>
        public static void WriteCookie(string strName, string strValue)
        {
            CookieOptions options = new CookieOptions();

            var cookie = HttpCurrent.Request.Cookies[strName];
            if (cookie == null)
            {
                HttpCurrent.Response.Cookies.Append(strName, strValue);
            }
            else
            {

            }

        }

        /// <summary>
        /// ghicookiegiá trị
        /// </summary>
        /// <param name="strName">tên</param>
        /// <param name="strValue">giá trị</param>
        /// <param name="expires">thời gian hết hạn(phút)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            RemoveCookie(strName);
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(expires);//thời gian hết hạn
            HttpCurrent.Response.Cookies.Append(strName, strValue, options);
        }
        /// <summary>
        /// đọccookiegiá trị
        /// </summary>
        /// <param name="strName">tên</param>
        /// <returns>cookiegiá trị</returns>
        public static string? GetCookie(string strName)
        {
            return HttpCurrent.Request.Cookies[strName];
        }

        /// <summary>
        /// xóaCookieđối tượng
        /// </summary>
        /// <param name="CookiesName">Cookietên đối tượng</param>
        public static void RemoveCookie(string CookiesName)
        {
            HttpCurrent.Response.Cookies.Delete(CookiesName);
        }

        #endregion

        /// <summary>
        /// lấy yêu cầu đầy đủUrl
        /// </summary>
        /// <returns></returns>
        public static string GetCompleteUrl()
        {
            if (HttpCurrent != null && HttpCurrent.Request != null)
            {
                return new StringBuilder()
               .Append(HttpCurrent.Request.Scheme)
               .Append("://")
               .Append(HttpCurrent.Request.Host)
               .Append(HttpCurrent.Request.PathBase)
               .Append(HttpCurrent.Request.Path)
               .Append(HttpCurrent.Request.QueryString)
               .ToString();
            }
            return string.Empty;
        }

    }
}
