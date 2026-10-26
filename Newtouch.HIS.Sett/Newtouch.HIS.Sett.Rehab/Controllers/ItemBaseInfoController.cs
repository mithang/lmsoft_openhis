using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Sett.Rehab.Controllers
{
    public class ItemBaseInfoController : Controller
    {
        /// <summary>
        /// Thêm
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Sửa
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }
    }
}