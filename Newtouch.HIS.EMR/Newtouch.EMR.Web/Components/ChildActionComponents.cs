using Microsoft.AspNetCore.Mvc;

namespace Newtouch.EMR.Web.Components
{
    // ASP.NET Core replacements for the Framework @Html.Action(...) child actions.
    // Each original child action only returned View() (RecordList also set ViewBag),
    // so each component renders the same partial view by path. The original
    // controller actions stay routable as normal endpoints.
    public class CtglListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Areas/MedicalRecordManage/Views/Blctgl/CTGLList.cshtml");
        }
    }

    public class YxfhListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Areas/MedicalRecordManage/Views/Blctgl/YXFHlist.cshtml");
        }
    }

    public class YsListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Areas/MedicalRecordManage/Views/Blys/YSList.cshtml");
        }
    }

    public class BlysFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Areas/MedicalRecordManage/Views/Blys/Form.cshtml");
        }
    }

    public class BljghFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Areas/MedicalRecordManage/Views/Bljgh/Form.cshtml");
        }
    }

    public class RecordListViewComponent : ViewComponent
    {
        // blxtmc_yj is accepted (one call site passes it) but unused, mirroring
        // the original RecordList action which only forwarded zyh/mzh to ViewBag.
        public IViewComponentResult Invoke(string zyh = null, string mzh = null, string blxtmc_yj = null)
        {
            ViewBag.zyh = zyh;
            ViewBag.mzh = mzh;
            return View("~/Areas/MedicalRecordManage/Views/MedicalRecord/RecordList.cshtml");
        }
    }
}
