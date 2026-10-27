using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Newtouch.Sett.Web.Areas.OutpatientManage.Controllers
{
    [Area("OutpatientManage")]

    public class DealGuiAnNotConfirmController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ICommonDmnService _commonDmnService;

        public DealGuiAnNotConfirmController(ICommonDmnService commonDmnService)
        {
            _commonDmnService = commonDmnService;
        }
        // xử lýkhung nhìn giao dịch chưa xác định
        public IActionResult DealGuiAnNotConfirm()
        {
            return View();
        }

        public IActionResult GetJyyzmByJylsh(string jylsh)
        {
            return Content(_commonDmnService.Gzyb_GetJyyzmByJylsh(jylsh, this.OrganizeId ));
        }
    }
}
