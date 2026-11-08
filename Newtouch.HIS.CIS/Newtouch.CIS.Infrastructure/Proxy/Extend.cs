using System;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Proxy
{
    /// <summary>
    /// mở rộng triển
    /// </summary>
    public static class Extend
    {

        /// <summary>
        /// html Mã chuyển thành html thực thể
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string HtmlCodingParsing(this string source)
        {
            return string.IsNullOrWhiteSpace(source) ? "" : source.Replace("&lt;", "<").Replace("&#60;", "<").Replace("&gt;", ">").Replace("&#62;", ">");
        }

        /// <summary>
        /// nhắc lấy soapXml báo văn return trong của nội dung
        /// </summary>
        /// <param name="envelope"></param>
        /// <returns></returns>
        public static string ExtractReturnContent(this string envelope)
        {
            if (string.IsNullOrWhiteSpace(envelope)) return "";
            if (envelope.IndexOf("<return>", StringComparison.Ordinal) <= -1) return envelope;
            var tmpStr = envelope.Substring(envelope.IndexOf("<return>", StringComparison.Ordinal) + 8);
            if (tmpStr.Length <= 0) return tmpStr;
            return tmpStr.IndexOf("</return>", StringComparison.Ordinal) > -1 ? tmpStr.Substring(0, tmpStr.IndexOf("</return>", StringComparison.Ordinal)) : "";
        }

        /// <summary>
        /// nhắc lấy soapXml báo văn return trong của nội dung
        /// </summary>
        /// <param name="envelope"></param>
        /// <returns></returns>
        public static string ExtractReturnContentAndParsing(this string envelope)
        {
            return envelope.ExtractReturnContent().HtmlCodingParsing();
        }

        /// <summary>
        /// kiểm tra Quay lại là Không đang xác,kiểm tra theo cứ là là Không chứa có"<return>"
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool IsReturnSuccess(this string response)
        {
            if (string.IsNullOrWhiteSpace(response)) return false;
            var tmp = response.Replace(" ", "");
            if (string.IsNullOrWhiteSpace(tmp)) return false;
            tmp = tmp.ToLower();
            return tmp.IndexOf("<return>", StringComparison.Ordinal) > -1;
        }
    }
}