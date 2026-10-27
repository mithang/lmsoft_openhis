using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using Newtouch.Domain.IDomainServices;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage.Controllers
{
    [Area("DoctorManage")]
    public class OrginalController : OrgControllerBase
    {
        private readonly IDoctorserviceDmnService _doctorserviceDmnService;

        public OrginalController(IDoctorserviceDmnService doctorserviceDmnService)
        {
            this._doctorserviceDmnService = doctorserviceDmnService;
        }

        public IActionResult CurrentdayView()
        {
            return View();
        }
        public IActionResult ConsumablesIndex()
        {
            return View();
        }
        public IActionResult gridPatientList(Pagination pagination, string zyh)
        {
            var data = new
            {
                rows = _doctorserviceDmnService.GetTodayValidService(pagination,OrganizeId, zyh),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
    }
}