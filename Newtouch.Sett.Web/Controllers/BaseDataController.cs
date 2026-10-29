using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.DomainServices;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.Sett.Web.Controllers
{
    /// <summary>
    /// T-12 (minimal, root route): TherapistAdvice calls this action as
    /// /basedata/GetSfxmZxksSelectJson (no area segment), so it must live
    /// on a NON-area controller — the area route {area:exists}/... would
    /// never match a bare /basedata/... URL. Only this one action exists
    /// here; SelectSfxmYp/GetAuthedWardSelectJson live on the
    /// SystemManage-area BaseDataController.
    /// </summary>
    public class BaseDataController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly BaseDataDmnService _baseData;

        public BaseDataController(BaseDataDmnService baseData)
        {
            _baseData = baseData;
        }

        /// <summary>Executing-department selector. sfxmCode is accepted
        /// (the view sends it) but, like the CIS port, the result is the
        /// org's zxks=1 departments — no per-item mapping data exists.</summary>
        public IActionResult GetSfxmZxksSelectJson(string sfxmCode)
        {
            var data = _baseData.GetSfxmZxksSelectJson(this.OrganizeId);
            return Content(data.ToJson());
        }
    }
}
