using System;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IDomainServices.Outpatient;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
	[Area("SystemManage")]
	public class SysBaseDataController : OrgControllerBase
	{
		private readonly IBaseDataDmnService _baseDataDmnService;
		private readonly ISysDepartmentRepo _sysDepartmentRepo;
		private readonly ISysTCMSyndromeRepo _sysTCMSyndromeRepo;
		private readonly ISysDiagnosisRepo _sysDiagnosisRepo;
        private readonly IOutpatientConsultDmnService _outpatientConsultDmnService;

        public SysBaseDataController(IBaseDataDmnService baseDataDmnService, ISysDepartmentRepo sysDepartmentRepo, ISysTCMSyndromeRepo sysTCMSyndromeRepo, ISysDiagnosisRepo sysDiagnosisRepo, IOutpatientConsultDmnService outpatientConsultDmnService)
        {
            this._baseDataDmnService = baseDataDmnService;
            this._sysDepartmentRepo = sysDepartmentRepo;
            this._sysTCMSyndromeRepo = sysTCMSyndromeRepo;
            this._sysDiagnosisRepo = sysDiagnosisRepo;
            this._outpatientConsultDmnService = outpatientConsultDmnService;
        }

        /// <summary>
        /// Cách dùng thuốc Tìm kiếm
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public JsonResult GetMedicineUsageList(string keyword)
		{
			var list = _baseDataDmnService.GetMedicineUsageList();
			if (!string.IsNullOrWhiteSpace(keyword))
			{
				list = list.Where(a => a.yfCode.ToLower().Contains(keyword.ToLower()) || a.yfmc.ToLower().Contains(keyword.ToLower())).ToList();
			}

			list = list.Where(a => a.yplx == ((int)EnumYpyf.WM).ToString()).ToList();
			return Json(list);
		}

		public JsonResult GetZYMedicineUsageList(string keyword)
		{
			var list = _baseDataDmnService.GetMedicineUsageList();
			if (!string.IsNullOrWhiteSpace(keyword))
			{
				list = list.Where(a => a.yfCode.ToLower().Contains(keyword.ToLower()) || a.yfmc.ToLower().Contains(keyword.ToLower())).ToList();
			}

			list = list.Where(a => a.yplx == ((int)EnumYpyf.TCM).ToString()).ToList();
			return Json(list);
		}
        public JsonResult GetTCMDjfsUsageList(string keyword)
        {
            var list = _baseDataDmnService.GetDjfsUsageList(this.OrganizeId);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                list = list.Where(a => a.Code.ToLower().Contains(keyword.ToLower()) || a.Name.ToLower().Contains(keyword.ToLower())).ToList();
            }
            return Json(list);
        }
        /// <summary>
        /// Khoa dướikéo 
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectDepartmentList(string keyword, string zlks)
		{
			var data = _sysDepartmentRepo.GetList(this.OrganizeId, "1", keyword);
            if (zlks != null && zlks != "")
            {
                data = data.Where(a => a.zlks == zlks).ToList();
            }
            return Content(data.ToJson());
        }

		/// <summary>
		/// Chẩn đoán Tìm kiếm
		/// </summary>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public JsonResult GetDiagnosisList(string keyword, string zdlx, string ybnhlx)
		{
			if (string.IsNullOrEmpty(ybnhlx))
			{
				ybnhlx = null;
			}
			var list = _sysDiagnosisRepo.GetList(this.OrganizeId, keyword, zdlx, ybnhlx);
			return Json(list);
		}

		/// <summary>
		/// hội chứng Tìm kiếm
		/// </summary>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public IActionResult GetTCMSymptomsList(string keyword)
		{
			var list = _sysTCMSyndromeRepo.GetList(this.OrganizeId, keyword);
			return Json(list);
		}

		/// <summary>
		/// Lấysắc hộphí
		/// </summary>
		/// <returns></returns>
		public JsonResult GetDjFee()
		{
			var fee = _baseDataDmnService.GetDjFee(this.OrganizeId);
			return Json(fee);
		}

		/// <summary>
		/// dài hạn/tạm thờiCờ
		/// </summary>
		/// <returns></returns>
		public IActionResult GetYzlb()
		{
			var result = new object[] {
					new { yzlb = "长",Name="Dài hạn" },
					new { yzlb = "临",Name="Tạm thời" }
				};
			return Content(result.ToJson());
		}

		/// <summary>
		/// LấyBác sĩmởthuốcQuyền
		/// </summary>
		/// <returns></returns>
		public int GetPermissions(string tsypzl,string dlcode,string kssqxjb)
		{
			var orgid = this.OrganizeId;
			var gh = UserIdentity.rygh;
			return _baseDataDmnService.GetPermissions(orgid, gh, tsypzl, dlcode, kssqxjb);
		}

        /// <summary>
        /// Hệ thốngPhòng khám
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SelectConsultList(string ksCode,string keyword)
        {
            var data = _outpatientConsultDmnService.SelectConsultList(ksCode, keyword,this.OrganizeId );
            return Content(data.ToJson());
        }
    }
}