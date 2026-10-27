using Newtouch.MR.ManageSystem.Domain.IDomainServices;
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
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Infrastructure;
using Newtouch.MRMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.MRMS.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class CommonController : OrgControllerBase
    {
        public CommonController(ICommonDmnService CommonDmnService, ISysDiagnosisRepo sysDiagnosisRepo, ISysDepartmentRepo SysDepartmentRepo)
        {
            this._CommonDmnService = CommonDmnService;
            this._sysDiagnosisRepo = sysDiagnosisRepo;
            this._SysDepartmentRepo = SysDepartmentRepo;
        }

#pragma warning disable CS0649 // Field 'CommonController._CommonDmnService' is never assigned to, and will always have its default value null
        private readonly ICommonDmnService _CommonDmnService;
#pragma warning restore CS0649 // Field 'CommonController._CommonDmnService' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'CommonController._sysDiagnosisRepo' is never assigned to, and will always have its default value null
        private readonly ISysDiagnosisRepo _sysDiagnosisRepo;
#pragma warning restore CS0649 // Field 'CommonController._sysDiagnosisRepo' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'CommonController._SysDepartmentRepo' is never assigned to, and will always have its default value null
        private readonly ISysDepartmentRepo _SysDepartmentRepo;
#pragma warning restore CS0649 // Field 'CommonController._SysDepartmentRepo' is never assigned to, and will always have its default value null

        /// <summary>
        /// Lấy danh sách chẩn đoán hệ thống
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDiagList()
        {
            var ety = _CommonDmnService.ZdList(OrganizeId, "");
            return Content(ety.ToJson());
        }

        /// <summary>
        /// Chẩn đoán Tìm kiếm
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetDiagnosisList(string keyword, string zdlx, string ybnhlx)
        {
            if (string.IsNullOrEmpty(ybnhlx))
            {
                ybnhlx = null;
            }
            var list = _sysDiagnosisRepo.GetList(this.OrganizeId, keyword, zdlx, ybnhlx);
            return Json(list);
        }

        /// <summary>
        /// Phẫu thuật Tìm kiếm
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>	
        public IActionResult GetOperationList(string keyword, bool type)
        {
            var list = _CommonDmnService.OpList(this.OrganizeId, keyword, type);
            return Json(list);
        }
        /// <summary>
        /// Phương pháp gây mê
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAnesList(string keyword)
        {
            var ety = _CommonDmnService.AnesList(OrganizeId, keyword);
            return Content(ety.ToJson());
        }

        public IActionResult GetNotchGradeList(string keyword)
        {
            var ety = _CommonDmnService.NotchGradeList(OrganizeId, keyword);
            return Content(ety.ToJson());
        }

        public IActionResult GetCommonList(string keyword,string type)
        {
            var ety = _CommonDmnService.DicCommonList(OrganizeId, keyword,type);
            return Content(ety.ToJson());
        }

        public IActionResult GetDepartment(string keyword)
        {
            var ety = _SysDepartmentRepo.GetList(this.OrganizeId, "1",keyword);
            return Content(ety.ToJson());
        }

        public IActionResult GetXtbrxz(string keyword)
        {
            var ety = _CommonDmnService.BrxzList(this.OrganizeId, keyword);
            return Content(ety.ToJson());
        }
        /// <summary>
        /// Quốc tịch
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetNationality(string keyword)
        {
            var ety = _CommonDmnService.DicNationalityList(this.OrganizeId, keyword);
            return Content(ety.ToJson());
        }
        /// <summary>
        /// Dân tộc
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetNationas(string keyword)
        {
            var ety = _CommonDmnService.DicNationsList(this.OrganizeId, keyword);
            return Content(ety.ToJson());
        }

        /// <summary>
        /// Danh sách nhân viên theo vị trí
        /// </summary>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetStaffListByDutyCode(string dutyCode, string keyword)
        {
            var list = _CommonDmnService.GetStaffByDutyCode(this.OrganizeId, dutyCode, keyword);
            return Content(list.ToJson());
        }

        public IActionResult GetHisSfdl(string keyword)
        {
            var list = _CommonDmnService.GetHisSfdl(OrganizeId,keyword);
            return Content(list.ToJson());
        }
    }
}