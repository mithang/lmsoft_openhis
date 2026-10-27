using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>
    /// 字典 — bản gốc KHÔNG có action nào (chỉ ctor). Port giữ nguyên: controller
    /// tồn tại nhưng không expose endpoint (không phải stub do port bịa ra).
    /// </summary>
    [Route("api/Items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
    }
}
