using System.Web.Mvc;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.Web.Areas.OutpatientManage.Controllers
{
    public class DealGuiAnNotConfirmController : ControllerBase
    {
        private readonly ICommonDmnService _commonDmnService;
        // Xử lý chế độ xem giao dịch không chắc chắn
        public ActionResult DealGuiAnNotConfirm()
        {
            return View();
        }

        public ActionResult GetJyyzmByJylsh(string jylsh)
        {
            return Content(_commonDmnService.Gzyb_GetJyyzmByJylsh(jylsh, this.OrganizeId ));
        }
    }
}