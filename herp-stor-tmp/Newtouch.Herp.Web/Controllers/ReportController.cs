using Microsoft.AspNetCore.Mvc;

namespace Newtouch.Herp.Web.Controllers
{
    /// <summary>
    /// Báo cáo SSRS: 13 báo cáo .rdl gốc (StockInventory, ProfitAndLoss,
    /// PSIStatistics, WarehouseStorageQuery, ...) không có bản tương đương
    /// .NET Core. Các trang báo cáo hiển thị thông báo rõ ràng thay vì 404
    /// hay dữ liệu giả; việc thay thế SSRS là hạng mục còn lại duy nhất.
    /// </summary>
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PrintReport(string type, string param)
        {
            ViewBag.ReportType = type;
            ViewBag.ReportParam = param;
            return View();
        }
    }
}
