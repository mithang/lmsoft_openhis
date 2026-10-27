

using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.DomainServices;
using Newtouch.EMR.Infrastructure;
using Newtouch.EMR.Repository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.EMR.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
   
    public class YzPreViewController : OrgControllerBase//Controller
    {
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IBlmblbRepo _blmbRepo;

        public YzPreViewController(ICommonDmnService commonDmnService, IBlmblbRepo blmbRepo)
        {
            this._CommonDmnService = commonDmnService;
            this._blmbRepo = blmbRepo;
        }

        // DCSoft.Writer has no .NET Core build: this service endpoint reports the
        // limitation explicitly instead of 404. A replacement web editor is the
        // remaining EMR item.
        public IActionResult MoreHandleDCWriterServicePage()
        {
            return Content(new AjaxResult
            {
                state = ResultType.error.ToString(),
                message = "Trình soạn thảo văn bản DCSoft chưa khả dụng trên .NET Core (không có bản build .NET Core). Cần thay bằng trình soạn thảo web khác."
            }.ToJson());
        }


        public IActionResult YzPreView()
        {
            //DCwriterServer dcControl = new DCwriterServer();
            //dcControl.Init();
            //WebWriterControlEngine eng = dcControl.DCWControl;//这是上一步创建的引擎 
            //ViewBag.WriterControlHtml = eng.GetAllContentHtml();
            //eng.Dispose();
            return View();
        }

        #region 医嘱续打
        public IActionResult GetZyCqYzData(string zyh, string orgId, string isSign)
        {
            var data = _CommonDmnService.GetPrintCqyzData(zyh, orgId, isSign);
            return Content(data.ToJson());
        }

        public IActionResult GetZyLsYzData(string zyh, string orgId, string isSign)
        {
            var data = _CommonDmnService.GetPrintLsyzData(zyh, orgId, isSign);
            return Content(data.ToJson());
        }

        public IActionResult GetInpatInfo(string zyh, string orgId)
        {
            orgId= orgId==null? this.OrganizeId:orgId;
            var data = _CommonDmnService.GetInpatInfo(zyh, orgId);
            return Content(data.ToJson());
        }
        #endregion

        #region 护理单打印
    
        public IActionResult GetHljldData(string zyh,string hljb)
        {
            var data = _CommonDmnService.GetPrintHljlData(zyh, this.OrganizeId, hljb);
            return Content(data.ToJson());
        }

        public IActionResult GetPrintTempleate(string templateName)
        {
            var data = _blmbRepo.FindEntity(p => p.mbmc == templateName && p.OrganizeId == this.OrganizeId && p.zt == "1");
            string filename = data.mblj;
            string ml = AppDomain.CurrentDomain.BaseDirectory;

            ml = ml.Substring(0, ml.Length - 1);
            string xml = System.IO.File.ReadAllText($"{ml}{filename}");
            BlTemplateVo vo = new BlTemplateVo();
            vo.xmldata = xml;
            return Content(vo.ToJson());
            //return Content(data.ToJson()); ;
        }
        #endregion
    }
}