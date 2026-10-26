using System;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IRepository;

namespace Newtouch.MR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Trang chủ
    /// </summary>
    public class HomeController : FrameworkBase.MultiOrg.Web.Controllers.HomeController
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        
        /**
         * Đồng bộ cấu hình tham số hệ thống
         */ 
        public ActionResult SyncSysConfigParams(string orgId)
        {
            var sysConfigEntities = _sysConfigRepo.GetList("", "*").ToList();
            foreach (var item in sysConfigEntities)
            {
                item.Id = Guid.NewGuid().ToString();
                item.OrganizeId = orgId;
            }

            var insert = _sysConfigRepo.Insert(sysConfigEntities);
            return Success("",insert);
        }

    }
}