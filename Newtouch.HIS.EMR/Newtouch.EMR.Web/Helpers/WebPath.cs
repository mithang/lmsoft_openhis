using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Newtouch.EMR.Web
{
    /// <summary>
    /// Replaces Framework Server.MapPath and Request[...] indexing on ASP.NET Core.
    /// Content root is the project directory when launched via run-emr.sh (cwd = project dir).
    /// </summary>
    public static class WebPath
    {
        public static string Map(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return path;
            var p = path.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
            if (p.StartsWith("~" + Path.DirectorySeparatorChar))
                return Path.Combine(Directory.GetCurrentDirectory(), p.Substring(2));
            if (p == "~") return Directory.GetCurrentDirectory();
            if (Path.IsPathRooted(p)) return p;
            return Path.Combine(Directory.GetCurrentDirectory(), p);
        }
    }

    public static class ReqExt
    {
        public static string ReqVal(this HttpRequest request, string key)
        {
            var q = request.Query[key].ToString();
            if (!string.IsNullOrEmpty(q)) return q;
            return request.HasFormContentType ? request.Form[key].ToString() : null;
        }
    }
}
