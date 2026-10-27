using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.OR.ManageSystem.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Areas.Operation.Controllers
{
    [Area("Operation")]
    public class CommonController : OrgControllerBase
    {
        private readonly ICommonDmnService _CommonDmnService;
        public CommonController(ICommonDmnService commonDmnService)
        {
            _CommonDmnService = commonDmnService;
        }
        // GET: Operation/Common
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Getbqlist(string bqcode)
        {
            var bq = _CommonDmnService.GetBqlist(bqcode, OrganizeId, null);
            return Content(bq.ToJson());
        }

        public IActionResult GetOplist(string ssdm)
        {
            var op = _CommonDmnService.GetOplist(ssdm, OrganizeId);
            return Content(op.ToJson());
        }
        public IActionResult GetOperationAjax(string ssdm, string keyword)
        {
            string[] ssdmData = !string.IsNullOrWhiteSpace(ssdm) ? ssdm.Split(',') : null;
            return GetOperations(ssdmData, keyword);
        }
        public IActionResult GetOperations(string[] ssdmData, string keyword)
        {
            
             // Redis OperationDic cache dropped in the Core port (see ClientsDataController):
            // the operation dictionary is read from the database on every call.
            var dicCache = _CommonDmnService.GetOperations(ssdmData, OrganizeId);
            var result = new List<OperationDicVO>();
            if (ssdmData == null || ssdmData.Length == 0 || string.IsNullOrWhiteSpace(string.Join(",", ssdmData)))
            {
                result = dicCache.Take(100).ToList();
            }
            else if (ssdmData.Length == 1)
            {
                result = dicCache.Where(p => p.ssdm == ssdmData[0] || p.ssmc == ssdmData[0] || p.ssdm.Contains(ssdmData[0]) || p.ssmc.Contains(ssdmData[0])).ToList();
            }
            else
            {
                result = dicCache.Where(p => ssdmData.Contains(p.ssdm)).ToList();
            }
            return Content(result.ToJson());
        }

        //public IActionResult GetStafflist(string rygh)
        //{
        //    var ys = _CommonDmnService.GetStafflist(rygh, OrganizeId);
        //    return Content(ys.ToJson());
        //}
        public IActionResult GetStafflist(string Code)
        {
            var ys = _CommonDmnService.GetStafflist(Code, OrganizeId);
            return Content(ys.ToJson());
        }
        public IActionResult GetAneslist(string code)
        {
            var result = _CommonDmnService.GetAneslistlist(code, OrganizeId);
            return Content(result.ToJson());
        }
        public IActionResult GetRoomlist(string Code)
        {
            var result = _CommonDmnService.GetRoomlist(Code, OrganizeId);
            return Content(result.ToJson());
        }
        public IActionResult GetNotchGradelist(string Code)
        {
            var result = _CommonDmnService.GetNotchGradelist(Code, OrganizeId);
            return Content(result.ToJson());
        }
    }
}