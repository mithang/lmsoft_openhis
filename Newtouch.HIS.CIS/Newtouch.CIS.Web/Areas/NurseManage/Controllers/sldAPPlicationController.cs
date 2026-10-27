using System;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.Common.Web;

using Newtouch.CIS.Compat;
using Newtouch.CIS.Compat;
using Newtouch.Domain.DTO;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset.Zyypyz;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    [Area("NurseManage")]
    public class sldAPPlicationController : OrgControllerBase
    {

        private readonly IWZsldDmnService _wZsldDmnService;

        public sldAPPlicationController(IWZsldDmnService wZsldDmnService)
        {
            this._wZsldDmnService = wZsldDmnService;
        }
        //// GET: NurseManage/sldAPPlication
        //public IActionResult WzSldIndex()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// đăng kýlĩnhphiếusố
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public IActionResult GetNewCkzksdh()
        //{
        //    var result = "SLD"+string.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
        //    return Content(result);
        //}

        ///// <summary>
        ///// dướikéovật tưthông tin
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="slksbm"></param>
        ///// <returns></returns>

        //[HttpGet]
        //public IActionResult DepartmentStockListQuery(string key, string slksbm)
        //{
        //    var param = new DepartmentStockListQueryParamDTO
        //    {
        //        key = key,
        //        organizeId = OrganizeId,
        //        warehouseId = slksbm,
        //        zt = "1"
        //    };
        //    var result = _wZsldDmnService.DepartmentStockListQuery(param);
        //    return Content(result.ToJson());
        //}


        ///// <summary>
        ///// dướikéovật tưSố lôLôthông tin
        ///// </summary>
        ///// <param name="proId">vật tưID</param>
        ///// <param name="gysId">Nhà cung cấpID</param>
        ///// <param name="deliveryNo">phốigửiphiếusố</param>
        ///// <param name="keyword"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public IActionResult ProductBatchQuery(string proId,  string slksbm, string keyword = "")
        //{
        //    return Content(_wZsldDmnService.ProductBatchQuery(proId, slksbm, OrganizeId, keyword: keyword).ToJson());
        //}

        ///// <summary>
        ///// LấyKho
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult GetDeptByKf(string keyword)
        //{
        //    var list = _wZsldDmnService.GetList(OrganizeId, keyword ?? "");
        //    return Content(list.ToJson());
        //}

        ///// <summary>
        ///// LấyKhu bệnh
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult GetDeptBy(string keyword)
        //{
        //    var list = _wZsldDmnService.GetDeptList(OrganizeId, keyword ?? "");
        //    return Content(list.ToJson());
        //}
    }
}