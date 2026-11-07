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
        /// GhiSession
        /// </summary>
        /// <param name="key">Tên khóa của Session</param>
        /// <param name="value">Sessioncủa giá trị khóa</param>
        public static void WriteSession(string key, string value)
        {
            WriteSession<string>(key, value);
        }

        /// <summary>
        /// GhiSession
        /// </summary>
        /// <typeparam name="T">Session Loại của giá trị khóa</typeparam>
        /// <param name="key">Tên khóa của Session</param>
        /// <param name="value">Sessioncủa giá trị khóa</param>
        public static void WriteSession<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                return;
            HttpCurrent.Session.SetString(key, value.ToJson());
        }

        /// <summary>
        /// Đọc giá trị của Session
        /// </summary>
        /// <param name="key">Tên khóa của Session</param>        
        public static string GetSession(string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;
            return HttpCurrent.Session.GetString(key);
        }

        /// <summary>
        /// Lấy giá trị đối tượng trong Session
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
        /// Xóa chỉ định Session
        /// </summary>
        /// <param name="key">Tên khóa của Session</param>
        public static void RemoveSession(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;
            HttpCurrent.Session.Remove(key);
        }

        #endregion

        #region Cookiethao tác

        /// <summary>
        /// Ghi giá trị cookie
        /// </summary>
        /// <param name="strName">tên</param>
        /// <param name="strValue">giá trị</param>
        public static void WriteCookie(string strName, string strValue)
        {
            // SECURITY (G7): cookie phien luon co HttpOnly + SameSite=Lax; Secure chi khi request la HTTPS
            // (hien tai chay HTTP nen Secure=false, neu set Secure=true mu se hong login).
            // FIX (C105): ghi de (xoa roi ghi lai, giong overload 3 tham so). Quirk cu "chi ghi khi
            // cookie chua ton tai" khien khoa phien moi vua tao khong bao gio toi duoc trinh duyet
            // khi trinh duyet dang giu cookie cu het han (vd: WriteLoginUserCookie nhan phien SSO
            // qua ?appId=), lam hong viec nhan phien SSO cua nguoi dung quay lai.
            RemoveCookie(strName);
            CookieOptions options = new CookieOptions();
            options.HttpOnly = true;
            options.SameSite = SameSiteMode.Lax;
            options.Secure = HttpCurrent.Request.IsHttps;

            HttpCurrent.Response.Cookies.Append(strName, strValue, options);

        }

        /// <summary>
        /// Ghi giá trị cookie
        /// </summary>
        /// <param name="strName">tên</param>
        /// <param name="strValue">giá trị</param>
        /// <param name="expires">Thời gian hết hạn(phút)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            RemoveCookie(strName);
            CookieOptions options = new CookieOptions();
            options.HttpOnly = true;
            options.SameSite = SameSiteMode.Lax;
            options.Secure = HttpCurrent.Request.IsHttps;
            options.Expires = DateTime.Now.AddMinutes(expires);//Thời gian hết hạn
            HttpCurrent.Response.Cookies.Append(strName, strValue, options);
        }
        /// <summary>
        /// Đọc giá trị cookie
        /// </summary>
        /// <param name="strName">tên</param>
        /// <returns>cookiegiá trị</returns>
        public static string? GetCookie(string strName)
        {
            return HttpCurrent.Request.Cookies[strName];
        }

        /// <summary>
        /// Xóa đối tượng Cookie
        /// </summary>
        /// <param name="CookiesName">Cookie Tên đối tượng</param>
        public static void RemoveCookie(string CookiesName)
        {
            HttpCurrent.Response.Cookies.Delete(CookiesName);
        }

        #endregion

        /// <summary>
        /// Lấy yêu cầu đầy đủ Url
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
