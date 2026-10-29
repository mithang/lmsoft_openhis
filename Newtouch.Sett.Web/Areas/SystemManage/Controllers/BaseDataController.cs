using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.DomainServices;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// T-12 (minimal): the Sett port never had this controller — the
    /// original FrameworkBase binary held it — but 8+ views call
    /// /SystemManage/BaseData/SelectSfxmYp and HealthInsuranceReport calls
    /// GetAuthedWardSelectJson here. Only those two actions exist;
    /// GetSfxmZxksSelectJson lives on the root (non-area) BaseDataController
    /// because TherapistAdvice calls it as /basedata/... (no area segment).
    /// </summary>
    [Area("SystemManage")]
    public class BaseDataController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly BaseDataDmnService _baseData;

        public BaseDataController(BaseDataDmnService baseData)
        {
            _baseData = baseData;
        }

        /// <summary>
        /// Charge-item/drug floating selector. The plugin expects a plain
        /// JSON array (it checks data.length), NOT a jqGrid envelope.
        /// Callers pass keyword/mzzybz/dllb/sfdllx and optionally
        /// containyp0ck; topCount defaults to 50 when the caller sends none.
        /// </summary>
        public IActionResult SelectSfxmYp(string keyword, string mzzybz, string dllb,
            string sfdllx, string containyp0ck, int? topCount)
        {
            // containyp0ck absent => include zero-stock drugs (broadest
            // result, matching the callers that omit it); only an explicit
            // 0/false narrows it.
            bool contain = string.IsNullOrEmpty(containyp0ck)
                || containyp0ck == "1"
                || containyp0ck.Equals("true", System.StringComparison.OrdinalIgnoreCase);
            var data = _baseData.SelectSfxmYp(this.OrganizeId, keyword, mzzybz, dllb,
                sfdllx, contain, topCount ?? 50);
            return Content(data.ToJson());
        }

        /// <summary>Ward dropdown for HealthInsuranceReport (bindSelect
        /// expects {id, text} array).</summary>
        public IActionResult GetAuthedWardSelectJson()
        {
            var data = _baseData.GetAuthedWardSelectJson(this.OrganizeId);
            return Content(data.ToJson());
        }
    }
}
