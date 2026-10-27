using System.IO;

namespace Newtouch.Sett.Web.Helpers
{
    /// <summary>Core replacement for Server.MapPath: resolves "~/" paths against
    /// the web content root (set in Program.cs from IWebHostEnvironment).</summary>
    public static class SettPathHelper
    {
        public static string ContentRoot { get; set; } = System.AppContext.BaseDirectory;

        public static string MapPath(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath)) return ContentRoot;
            var rel = virtualPath.Replace("~/", "").Replace("~", "").TrimStart('/', '\\')
                .Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
            return Path.Combine(ContentRoot, rel);
        }
    }
}
