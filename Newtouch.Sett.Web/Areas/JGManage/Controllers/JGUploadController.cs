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

    public class JGUploadController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        
        private readonly IJGUploadDmnService _JGUploadDmnService;
        public JGUploadController(IJGUploadDmnService jGUploadDmnService)
        {
            _JGUploadDmnService = jGUploadDmnService;
        }

        // GET: JGManage/JGUpload
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]

        public IActionResult GetGridJson(Pagination pagination, string kssj, string jssj,string scqk,string zyh)
        {
            var data = new
            {
                rows = _JGUploadDmnService.GetList(pagination, this.OrganizeId, kssj, jssj,scqk,zyh),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }


        //JG上传
        public IActionResult JGUpload(List<JGUploadDto> list)
        {
            var data = _JGUploadDmnService.JGUpload(this.OrganizeId,list);
            return Content(data.ToJson());
        }

    }
}
