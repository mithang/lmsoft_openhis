using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers.PharmacyDrugStorage
{
    public class SysMedicineElectronicPrescriptionController : Controller
    {
        // GET: SysMedicineElectronicPrescription
        private readonly ISysMedicineElectronicPrescriptionDmnService _sysMedicineElectronicPrescriptionDmnService;

        public SysMedicineElectronicPrescriptionController(ISysMedicineElectronicPrescriptionDmnService SysMedicineElectronicPrescriptionDmnService)
        {
            this._sysMedicineElectronicPrescriptionDmnService = SysMedicineElectronicPrescriptionDmnService;
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 电子处方目录
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="genname"></param>
        /// <param name="medListCodg"></param>
        /// <param name="listType"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetGridJson(Pagination pagination, string genname, string medListCodg,string listType,string zt)
        {
            var data = new
            {
                rows = _sysMedicineElectronicPrescriptionDmnService.GetPaginationList(pagination, genname,  medListCodg,listType,zt),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
    }
}