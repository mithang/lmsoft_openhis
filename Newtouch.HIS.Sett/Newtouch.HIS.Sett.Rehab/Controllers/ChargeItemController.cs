using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Sett.Rehab.Controllers
{
    /// <summary>
    /// Hạng mục thu phí
    /// </summary>
    public class ChargeItemController : ControllerBase
    {
        public ActionResult ShowChargeItem()
        {
            return View();
        }
    }
}