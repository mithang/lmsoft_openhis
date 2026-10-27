using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.DRGManage.Controllers
{

    [Area("DRGManage")]

    public class DRGTradUploadController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IDRGTradUploadDmnService _DRGTradUploadDmnService;
        public DRGTradUploadController(IDRGTradUploadDmnService dRGTradUploadDmnService)
        {
            _DRGTradUploadDmnService = dRGTradUploadDmnService;
        }

		// GET: DRGManage/DRGTradUpload

		public IActionResult From()
		{
			return View();
		}

		[HttpGet]

        public IActionResult GetGridJson(string kssj, string jssj, string scqk, string zyh,string tradiNumber)
        {
            var data = new
            {
                rows = _DRGTradUploadDmnService.GetList(this.OrganizeId, kssj, jssj, scqk, zyh, tradiNumber)
            };
            return Content(data.ToJson());
        }


        //DRGtải lên
        public IActionResult DRGUpload(List<DRGUploadDto> list,string tradiNumber)
        {
            var data = _DRGTradUploadDmnService.DRGUpload(this.OrganizeId, list, tradiNumber);
            return Content(data.ToJson());
        }

        public IActionResult GetGridJsonYp(string kssj, string jssj, string scqk, string tradiNumber)
        {
            var data = new
            {
                rows = _DRGTradUploadDmnService.GetListYp(this.OrganizeId, kssj, jssj, scqk, tradiNumber)
            };
            return Content(data.ToJson());
        }
        public IActionResult GetGridJsonsqsz(string kssj, string jssj, string scqk, string zyh, string tradiNumber)
        {
            var data = new
            {
                rows = _DRGTradUploadDmnService.GetListsqsz(this.OrganizeId, kssj, jssj, scqk, zyh, tradiNumber)
            };
            return Content(data.ToJson());
        }

		public IActionResult GetGridyzList(string zyh,string types)
		{
			var data = new
			{
				rows = _DRGTradUploadDmnService.Getmxlist(this.OrganizeId, zyh, types)
			};
			return Content(data.ToJson());
		}
	}
}
