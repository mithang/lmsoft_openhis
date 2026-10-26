using System.Web;
using System.Web.Optimization;

namespace Newtouch.PDS.API
{
    public class BundleConfig
    {
        // Để biết thông tin chi tiết về gói, vui lòng truy cập https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Sử dụng cho việc phát triển và học tập Modernizr Phiên bản phát triển. Sau đó, khi bạn đã chuẩn bị xong
            // Sản xuất đã sẵn sàng, vui lòng dùng https://modernizr.com Công cụ tạo trên chỉ chọn kiểm thử cần thiết。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
