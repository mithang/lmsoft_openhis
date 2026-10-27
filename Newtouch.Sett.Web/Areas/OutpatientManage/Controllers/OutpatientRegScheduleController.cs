using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;











namespace Newtouch.Sett.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("OutpatientManage")]

    public class OutpatientRegScheduleController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ISysRegistSpecialDiseaseRepo _sysRegistSpecialDiseaseRepo;
        private readonly IOutpatientRegistScheduleRepo _outpatientRegistScheduleRepo;
        private readonly IOutPatientDmnService _outPatientDmnService;

        public OutpatientRegScheduleController(ISysDepartmentRepo sysDepartmentRepo, ISysUserDmnService sysUserDmnService, ISysRegistSpecialDiseaseRepo sysRegistSpecialDiseaseRepo, IOutpatientRegistScheduleRepo outpatientRegistScheduleRepo, IOutPatientDmnService outPatientDmnService)
        {
            _sysDepartmentRepo = sysDepartmentRepo;
            _sysUserDmnService = sysUserDmnService;
            _sysRegistSpecialDiseaseRepo = sysRegistSpecialDiseaseRepo;
            _outpatientRegistScheduleRepo = outpatientRegistScheduleRepo;
            _outPatientDmnService = outPatientDmnService;
        }

        public override IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[HandlerAuthorize]
        public new IActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// lấykhoa
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSysDepartmentList()
        {
            var data = _sysDepartmentRepo.GetList(OperatorProvider.GetCurrent().OrganizeId, "1");
            return Json(data);
        }

        /// <summary>
        /// lấybác sĩ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dutyCode"></param>
        /// <returns></returns>
        public IActionResult GetStaffByDutyCode()
        {
            var data = _sysUserDmnService.GetStaffByDutyCode(OperatorProvider.GetCurrent().OrganizeId, "Doctor");
            return Json(data);
        }

        /// <summary>
        /// lấyđăng ký khámbệnh chuyên khoa
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectSysChargeItemByghzbList()
        {
            var data = _sysRegistSpecialDiseaseRepo.SelectSysChargeItemByghzbList(OperatorProvider.GetCurrent().OrganizeId);
            return Json(data);
        }

        /// <summary>
        /// lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ghpbId"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(OutpatientRegistScheduleEntity entity, int? ghpbId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _outpatientRegistScheduleRepo.SubmitForm(entity, ghpbId);
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// lấycódanh sách hiệu lực
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IActionResult GetOutpatientRegistScheduleList(string keyword, string organizeId)
        {
            var data = _outPatientDmnService.GetOutpatientRegistScheduleList(organizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// thêm mớihoặcsửaForm
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int ghpbId)
        {
            var entity = _outPatientDmnService.GetOutpatientRegistScheduleList(null, null, ghpbId).FirstOrDefault();
            return Content(entity.ToJson());
        }

        /// <summary>
        /// xóa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IActionResult DeleteForm(int ghpbId, string OrganizeId)
        {
            _outpatientRegistScheduleRepo.DeleteForm(ghpbId, OrganizeId);
            return Success("Thao tác thành công.");
        }


    }
}
