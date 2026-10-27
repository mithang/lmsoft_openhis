using System;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Init;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Infrastructure;
using Newtouch.MRMS.Web.Models;
using System.Collections.Generic;
using System.Linq;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;

namespace Newtouch.MRMS.Web.Areas.ReportManage.Controllers
{
    [Area("ReportManage")]
    public class PatientHistoryController : OrgControllerBase
    {
        public PatientHistoryController(IMrbasyRepo MrbasyRepo, IPatientHistoryDmnService PatientHistoryDmnService)
        {
            this._MrbasyRepo = MrbasyRepo;
            this._PatientHistoryDmnService = PatientHistoryDmnService;
        }

	    private readonly IMrbasyRepo _MrbasyRepo;
	    private readonly IPatientHistoryDmnService _PatientHistoryDmnService;

		// GET: ReportManage/PatientHistory
		public override IActionResult Index()
        {
            return View();
        }

        //LấyThông tin nội trú
        public IActionResult GetPagintionHospitalList(Pagination pagination,  string jkkh, string keyword)
        {

	        var pat = _MrbasyRepo.GetPagintionHospitalList(pagination, OrganizeId, jkkh, keyword);

	        var data = new
	        {
		        rows = pat,
		        total = pagination.total,
		        page = pagination.page,
		        records = pagination.records
	        };
	        return Content(data.ToJson());
        }

		//Truy vấn thông tin bệnh án theo ID
        public IActionResult GetFormJson(string id)
        {
	        var entity = _MrbasyRepo.FindEntity(id);
	        return Content(entity.ToJson());
        }

		//Lấy thông tin chẩn đoán theo id
		public IActionResult GetPagintionZdList(Pagination pagination, string id)
		{
			var pat = new List<zys_zdVO>();
			if (id != "")
			{
				//LấyChẩn đoán ngoại trú
				var mzzdList = _PatientHistoryDmnService.GetmzzdList(OrganizeId, id);
				//Lấy chẩn đoán xuất viện và chẩn đoán khác
				var zdList = _PatientHistoryDmnService.GetzdList(OrganizeId, id);
				//NốiChẩn đoánDữ liệu
				foreach (var mzzdObj in mzzdList)
				{
					pat.Add(mzzdObj);
				}

				foreach (var zdObj in zdList)
				{
					pat.Add(zdObj);
				}
			}

			var data = new
			{
				rows = pat,
				total = 1,
				page = 1,
				records = 1
			};
			return Content(data.ToJson());
		}


		//LấyThông tin phẫu thuật
		public IActionResult GetPagintionSSList(string id)
		{
			IList<zys_ssVO> pat = new List<zys_ssVO>();
			if (id != "")
			{
				pat = _PatientHistoryDmnService.GetssList( OrganizeId, id);
			}

			var data = new
			{
				rows = pat,
				total = 1,
				page = 1,
				records = 1
			};
			return Content(data.ToJson());
		}

	}
}