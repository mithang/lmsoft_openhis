using Microsoft.AspNetCore.Mvc;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Trang chủ của host API. Bản gốc trả View HelpPage (MVC); host API Core không có
    /// view nên chỉ trả chuỗi trạng thái — không giả vờ có trang HelpPage.
    /// </summary>
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Content("PDS API (.NET 6) đang chạy. Các endpoint nghiệp vụ nằm dưới /api/* (xem README-PDS-API.md).", "text/plain");
        }
    }
}
