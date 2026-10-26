using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using Newtouch.Tools;

namespace Newtouch.MR.ManageSystem.Web.Areas.ReportManage.Controllers
{
    public class PatientHistoryController : OrgControllerBase
    {
	    private readonly IMrbasyRepo _MrbasyRepo;
	    private readonly IPatientHistoryDmnService _PatientHistoryDmnService;

		// GET: ReportManage/PatientHistory
		public ActionResult Index()
        {
            return View();
        }

        //LấyThông tin nhập viện
        public ActionResult GetPagintionHospitalList(Pagination pagination,  string jkkh, string keyword)
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

		//TheoIDTìm kiếmThông tin bệnh án
        public ActionResult GetFormJson(string id)
        {
	        var entity = _MrbasyRepo.FindEntity(id);
	        return Content(entity.ToJson());
        }

		//Lấy theo idThông tin chẩn đoán
		public ActionResult GetPagintionZdList(Pagination pagination, string id)
		{
			var pat = new List<zys_zdVO>();
			if (id != "")
			{
				//LấyChẩn đoán ngoại trú
				var mzzdList = _PatientHistoryDmnService.GetmzzdList(OrganizeId, id);
				//LấyChẩn đoán xuất việnvàChẩn đoán khác
				var zdList = _PatientHistoryDmnService.GetzdList(OrganizeId, id);
				//Ghép nốiChẩn đoánDữ liệu
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
		public ActionResult GetPagintionSSList(string id)
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