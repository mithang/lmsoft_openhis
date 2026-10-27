using System.IO;

namespace Newtouch.CIS.Web.Helpers
{
    /// <summary>Port of MVC5 Server.MapPath for area controllers
    /// (which do not derive from the root ControllerBase).</summary>
    public static class WebPath
    {
        public static string MapPath(string virtualPath)
        {
            var rel = virtualPath.TrimStart('~', '/', '\\').Replace('/', Path.DirectorySeparatorChar);
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", rel);
        }
    }
}
