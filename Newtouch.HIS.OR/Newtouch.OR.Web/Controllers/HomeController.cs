using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Trang chủ. The original portal frame views depended on Framework-only helpers
    /// and are replaced by a minimal landing view (the portal frame is served
    /// by SSO in production, as in the MRQC pilot).
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ISysConfigRepo _sysConfigRepo;

        public HomeController(ISysConfigRepo sysConfigRepo)
        {
            _sysConfigRepo = sysConfigRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Đồng bộ cấu hình tham số hệ thống. NOTE: in this port SysConfigRepo reads AND writes
        /// NewtouchHIS_Base..Sys_GlobalConfig (the Framework repo's separate
        /// target database is not available here). The demo Base table is
        /// empty (0 rows), so this is currently a no-op returning 0.
        /// </summary>
        public IActionResult SyncSysConfigParams(string orgId)
        {
            var sysConfigEntities = _sysConfigRepo.GetList("", "*");
            foreach (var item in sysConfigEntities)
            {
                item.Id = Guid.NewGuid().ToString();
            }

            var insert = _sysConfigRepo.Insert(sysConfigEntities);
            return Content(new AjaxResult
            {
                state = ResultType.success.ToString(),
                data = insert,
            }.ToJson());
        }
    }
}
