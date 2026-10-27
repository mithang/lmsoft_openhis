using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using Newtouch.Domain.IDomainServices.Outpatient;
using Newtouch.Domain.ValueObjects.Outpatient;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage.Controllers
{
    [Area("DoctorManage")]
    public class ElectronicPrescriptionController : OrgControllerBase
    {
        private readonly IElectronicPrescriptionDmnService _electronicPrescriptionDmnService;

        public ElectronicPrescriptionController(IElectronicPrescriptionDmnService electronicPrescriptionDmnService)
        {
            this._electronicPrescriptionDmnService = electronicPrescriptionDmnService;
        }
        // GET: DoctorManage/ElectronicPrescription
        public override IActionResult Index()
        {
            return View();
        }

        public IActionResult examForm()
        {
            return View();
        }

        public IActionResult medicineForm()
        {
            return View();
        }


        public IActionResult cfForm()
        {
            return View();
        }

        public IActionResult backForm()
        {
            return View();
        }

        public IActionResult GetGridJson(Pagination pagination, DateTime kssj, DateTime jssj, string xm)
        {
            var tt = _electronicPrescriptionDmnService.GetGridJson(pagination, OrganizeId, kssj, jssj, xm);
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); ;
        }

        

    }
}