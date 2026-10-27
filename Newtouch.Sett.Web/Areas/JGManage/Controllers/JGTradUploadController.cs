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

namespace Newtouch.Sett.Web.Areas.JGManage.Controllers
{

    [Area("JGManage")]

    public class JGTradUploadController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IJGTradUploadDmnService _JGTradUploadDmnService;
        public JGTradUploadController(IJGTradUploadDmnService jGTradUploadDmnService)
        {
            _JGTradUploadDmnService = jGTradUploadDmnService;
        }

		// GET: JGManage/JGTradUpload

		public IActionResult From()
		{
			return View();
		}

		[HttpGet]

        public IActionResult GetGridJson(string kssj, string jssj, string scqk, string zyh,string tradiNumber)
        {
            var data = new
            {
                rows = _JGTradUploadDmnService.GetList(this.OrganizeId, kssj, jssj, scqk, zyh, tradiNumber)
            };
            return Content(data.ToJson());
        }

        

        //JG上传
        public IActionResult JGUpload(List<JGUploadDto> list,string tradiNumber)
        {
            var data = _JGTradUploadDmnService.JGUpload(this.OrganizeId, list, tradiNumber);
            return Content(data.ToJson());
        }

        public IActionResult GetGridJsonYp(string kssj, string jssj, string scqk, string tradiNumber)
        {
            var data = new
            {
                rows = _JGTradUploadDmnService.GetListYp(this.OrganizeId, kssj, jssj, scqk, tradiNumber)
            };
            return Content(data.ToJson());
        }
        public IActionResult GetGridJsonsqsz(string kssj, string jssj, string scqk, string zyh, string tradiNumber)
        {
            var data = new
            {
                rows = _JGTradUploadDmnService.GetListsqsz(this.OrganizeId, kssj, jssj, scqk, zyh, tradiNumber)
            };
            return Content(data.ToJson());
        }

		public IActionResult GetGridyzList(string zyh,string types)
		{
			var data = new
			{
				rows = _JGTradUploadDmnService.Getmxlist(this.OrganizeId, zyh, types)
			};
			return Content(data.ToJson());
		}
        
        public IActionResult ReUpload(string zyh,string types)
        {
            var data = new
            {
                rows = _JGTradUploadDmnService.Getmxlist(this.OrganizeId, zyh, types)
            };
            return Content(data.ToJson());
        }
        
        
	}
}
