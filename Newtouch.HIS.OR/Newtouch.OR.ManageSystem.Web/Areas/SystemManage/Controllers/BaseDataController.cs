using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Areas.SystemManage.Controllers
{
    public class SysBaseDataController : OrgControllerBase
    {
        private readonly ISysDiagnosisRepo _sysDiagnosisRepo;
        private readonly IOROperationRepo _OROperationRepo;
		private readonly IORStaffRepo _ORStaffRepo;
		// GET: SystemManage/BaseData
		//public ActionResult Index()
		//{
		//    return View();
		//}


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
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Phẫu thuật Tìm kiếm
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public JsonResult GetOperationList(string keyword, bool type)
        {
			var list = _OROperationRepo.OpList(this.OrganizeId, keyword, type);
			return Json(list, JsonRequestBehavior.AllowGet);
        }

		/// <summary>
		/// Khung nổi Danh sách bác sĩ Tìm kiếm
		/// </summary>
		/// <param name="organizeId"></param>
		/// <returns></returns>
		public JsonResult GetFloatStaffList(string organizeId, string keyword)
		{
			if (string.IsNullOrEmpty(organizeId))
			{
				organizeId = OrganizeId;
			}
			var list = _ORStaffRepo.GetFloatStaffList(organizeId, keyword);
			return Json(list, JsonRequestBehavior.AllowGet);
		}

	}
}