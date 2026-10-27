using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtonsoft.Json.Linq;
using System;

namespace Newtouch.Sett.Web.Controllers
{
    /// <summary>
    /// Port of the original Framework TreatmentController (parity audit 2026-10-27).
    /// The controller was missing from the port although live pages call it:
    /// OutBook/RosterCalendarForm calls Tjchaxun / deletemc / tianjia / rowdata,
    /// and Treatment/Index calls loginfy / deleteid / AForm / Codecx.
    /// Backed by the new SqlSugar TreatmentportfolioRepo (mz_gh_zlxmzh).
    /// Login() is kept for action parity: like the original, it has no
    /// Login.cshtml view in either tree, and no live caller (the RosterCalendarForm
    /// reference to it is commented out).
    /// rowdata field reads are null-guarded vs the original's direct ToString()
    /// calls, so a partial grid row cannot throw a NullReferenceException.
    /// </summary>
    public class TreatmentController : ControllerBase
    {
        private readonly ITreatmentportfolioRepo _treatmentportfolio;

        public TreatmentController(ITreatmentportfolioRepo treatmentportfolio)
        {
            _treatmentportfolio = treatmentportfolio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AForm()
        {
            return View();
        }

        public IActionResult Login(string keyword)
        {
            return View();
        }

        public IActionResult tianjia(TreatmentportfolioEntity entity, string keyValue)
        {
            _treatmentportfolio.ADDInsert(entity, keyValue);
            return Success("Thao tác thành công.");
        }

        public IActionResult loginfy(Pagination pagination, string keyword, string OrganizeId)
        {
            var data = _treatmentportfolio.Keyword(pagination, keyword, OrganizeId);
            return Content(data.ToJson());
        }

        public IActionResult deleteid(string keyValue, string orgId)
        {
            _treatmentportfolio.deleteid(keyValue, orgId);
            return Success("Thao tác thành công.");
        }

        public IActionResult deletemc(string keyValue, string zhcodemc, string orgId = null)
        {
            _treatmentportfolio.deletemc(keyValue, zhcodemc, orgId ?? OrganizeId);
            return Success("Thao tác thành công.");
        }

        [HttpPost]
        public IActionResult rowdata(string dataset, string orgId = null)
        {
            var jar = JArray.Parse(dataset);
            foreach (var token in jar)
            {
                var jobjar = JObject.Parse(token.ToString());
                var zhcodetj = Str(jobjar, "zhcode");
                var sfxmmc = Str(jobjar, "zlxmmc");
                if (_treatmentportfolio.TJchaxun(zhcodetj, sfxmmc, orgId ?? OrganizeId).Count > 0)
                {
                    continue;
                }
                var addrow = new TreatmentportfolioEntity
                {
                    zhmc = Str(jobjar, "zhmc"),
                    zhcode = zhcodetj,
                    ord = int.TryParse(Str(jobjar, "ord"), out var ord) ? ord : (int?)null,
                    zlxm = Str(jobjar, "zlxm"),
                    zlxmmc = sfxmmc,
                    price = decimal.TryParse(Str(jobjar, "price"), out var price) ? price : 0m,
                    zlxmpy = Str(jobjar, "zlxmpy"),
                    OrganizeId = orgId ?? OrganizeId,
                    sfdl = Str(jobjar, "sfdlcode"),
                };
                _treatmentportfolio.ADDrowdata(addrow);
            }
            return Success("Thao tác thành công.");
        }

        [HttpPost]
        public IActionResult Tjchaxun(string zhcodetj, string sfxmmc, string orgId = null)
        {
            var data = _treatmentportfolio.TJchaxun(zhcodetj, sfxmmc, orgId ?? OrganizeId);
            return Content(data.Count > 0 ? "1" : "0");
        }

        public IActionResult Codecx(TreatmentportfolioEntity entity, string zhcode, string orgId)
        {
            var data = _treatmentportfolio.Codecx(orgId ?? OrganizeId, zhcode);
            return Content(data.ToJson());
        }

        private static string Str(JObject obj, string name)
        {
            return obj[name]?.ToString() ?? string.Empty;
        }
    }
}
