using Microsoft.AspNetCore.Mvc;
using Newtouch.EMR.Domain.IDomainServices;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Extension;
using System.Linq;

namespace Newtouch.EMR.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// T-44/E-11 (bo sung theo nhu cau view, KHONG phai port trung thuc):
    /// ban goc EMR KHONG co controller nay (chi co Common/YzPreView trong
    /// area SystemManage; action nay o ban goc nam trong binary FrameworkBase
    /// khong co source), nhung 3 view PatInfo (Index, PatHistoricalMedRecord,
    /// PatMedRecordList) goi /SystemManage/BaseData/GetAuthedWardSelectJson
    /// de nap dropdown buong #sel_inpatientArea, nen endpoint 404 va dropdown
    /// chi co placeholder. Tien le Sett T-12 da them BaseDataController
    /// tuong tu cho Sett (xt_bq, theo org). O day dung lai service san co
    /// cua EMR: ICommonDmnService.GetWardList(orgId, null) doc view
    /// V_S_xt_bq cua Base DB (buong dang hoat dong, zt='1', theo org),
    /// map Code/Name sang {id, text} ma framework bindSelect doi hoi.
    /// GIOI HAN "Authed" (giong Sett T-12, ghi ro de khong hieu lam):
    /// ban port khong co bang phan quyen buong-theo-user nao duoc noi day,
    /// nen tra ve TOAN BO buong dang hoat dong cua org; gioi han theo
    /// quyen tung user KHONG duoc thuc thi o day va can quyet rieng neu
    /// benh vien yeu cau (E-11 von la quyet dinh mo).
    /// </summary>
    [Area("SystemManage")]
    public class BaseDataController : OrgControllerBase
    {
        private readonly ICommonDmnService _commonDmnService;

        public BaseDataController(ICommonDmnService commonDmnService)
        {
            _commonDmnService = commonDmnService;
        }

        /// <summary>Ward dropdown cho PatInfo (bindSelect doi {id, text}).</summary>
        public IActionResult GetAuthedWardSelectJson()
        {
            var data = _commonDmnService.GetWardList(this.OrganizeId, null)
                .Select(w => new { id = w.Code, text = w.Name })
                .ToList();
            return Content(data.ToJson());
        }
    }
}
