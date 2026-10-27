using System;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    /// <summary>
    /// 系统药品
    /// </summary>
    public class SysMedicineController : ControllerBase
    {
        // GET: SysMedicine

        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysMedicineRepo _SysMedicineRepository;
        private readonly ISysMedicineDmnService _sysMedicineDmnService;
        private readonly ISysMedicalOrderFrequencyRepo _sysMedicalOrderFrequencyRepo;
        private readonly ISysMedicineAntibioticInfoRepo _sysMedicineAntibioticInfoRepo;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;
        private readonly ISysPharmacyDepartmentApp _sysPharmacyDepartmentApp;
        private readonly ISysMedicineUsageRepo _sysMedicineUsageRepo;

        public SysMedicineController(ISysOrganizeDmnService sysOrganizeDmnService
            , ISysMedicineRepo SysMedicineRepository, ISysMedicineDmnService sysMedicineDmnService
            , ISysMedicalOrderFrequencyRepo SysMedicalOrderFrequencyRepo
            , ISysMedicineAntibioticInfoRepo MedicineAntibioticInfoRepo, ISysPharmacyDepartmentRepo sysPharmacyDepartmentRepo, ISysPharmacyDepartmentApp sysPharmacyDepartmentApp)
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._SysMedicineRepository = SysMedicineRepository;
            this._sysMedicineDmnService = sysMedicineDmnService;
            this._sysMedicalOrderFrequencyRepo = SysMedicalOrderFrequencyRepo;
            this._sysMedicineAntibioticInfoRepo = MedicineAntibioticInfoRepo;
            _sysPharmacyDepartmentRepo = sysPharmacyDepartmentRepo;
            _sysPharmacyDepartmentApp = sysPharmacyDepartmentApp;
        }

        public IActionResult SysMedicineAdd()
        {
            return View();
        }

        //grid json 数据源
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetGridJson(Pagination pagination, string keyword, string organizeId,string zt,string ypflCode)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _sysMedicineDmnService.GetPaginationList(organizeId, pagination, zt, ypflCode, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        #region 绑定下拉控件的值

        /// <summary>
        /// 根据组织机构获取药品信息列表
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetFloatingYPXXJson(string organizeId, string keyword)
        {
            var list = _SysMedicineRepository.GetYbMedicineList(organizeId, keyword);
            return Json(list);
        }
        public JsonResult GetYbName(string organizeId, string keyword,string lx)
        {
            var list = _SysMedicineRepository.GetYbName(organizeId,lx, keyword);
            return Json(list);
        }

        public JsonResult GetFloatingYPXNHXXJson(string organizeId, string keyword)
        {
            var list = _SysMedicineRepository.GetYbXNHMedicineList(keyword);
            return Json(list);
        }

        #endregion

        /// <summary>
        /// 提交数据信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="p"></param>
        /// <param name="ypCode"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult submitForm(SysMedicineVO model, List<string> p, string ypCode, int? keyValue)
        {
            model.zt = model.zt == "true" ? "1" : "0";
            model.ycmc = model.ycmc ?? "";
            //model.ybbz = model.ybbz == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(model.OrganizeId))
            {
                throw new FailedException("Vui lòng chọn tổ chức");
            }

            if (!_sysOrganizeDmnService.IsMedicalOrganize(model.OrganizeId) && !model.OrganizeId.Equals("*"))
            {
                throw new FailedException("Vui lòng chọn cơ sở y tế(Bệnh viện hoặc phòng khám)");
            }
            _sysMedicineDmnService.SubmitMedicine(model, keyValue);
            var t = _sysPharmacyDepartmentApp.SubmitEmpowermentPharmacyDepartment(keyValue, ypCode, model.OrganizeId, UserIdentity.UserCode, p);

            var msg = string.IsNullOrWhiteSpace(t) ? "Thao tác thành công." : ("Lưu thông tin thuốc thành công,nhưng ủy quyền nhà thuốc/kho thuốc thất bại," + t);
            return Success(msg);
        }

        /// <summary>
        /// 修改信息时，把信息带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(int keyValue)
        {
            var data = _sysMedicineDmnService.GetFormJson(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 药品信息同步医保
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult YibaoUploadApi(int ypId, string flag)
        {
            if (ypId <= 0)
            {
                return Success("Vui lòng chọn thuốc cần đồng bộ!");
            }
            var ypEntity = _sysMedicineDmnService.GetFormJson(ypId);
            if (!string.IsNullOrWhiteSpace(ypEntity.ybdm))
            {
                var Result = newtouchyibao.YibaoUniteInterface.Mcatalogrl("Z092", "0", ypEntity.ybdm, ypEntity.ypCode, ypEntity.ypmc, ypEntity.jxmc, ypEntity.ycmc, ypEntity.ypgg, ypEntity.bzdw, ypEntity.lsj, flag);
                if (Result.Code == 0 && Result.Data.Count > 0 && Result.Data[0].TPCODE == 0)
                {
                    string error = "";
                    if (!_sysMedicineDmnService.YibaoUpload(ypId, out error))
                    {
                        return Error(error);
                    }
                    return Success(string.Format("[{0}]Đồng bộ BHYT thành công!", ypEntity.ypmc));
                }
                else
                {
                    return Error(string.Format("[{0}]Đồng bộ BHYT thất bại:{1}", ypEntity.ypmc, Result.ErrorMsg));
                }
            }
            else
            {
                return Success("Thiếu mã BHYT!");
            }
        }

        public IActionResult GetPcSelectJson()
        {
            var data = new List<object>();
            List<SysMedicalOrderFrequencyEntity> entityList = _sysMedicalOrderFrequencyRepo.GetOrderFrequencyList(this.OrganizeId);
            foreach (var item in entityList)
            {
                var obj = new
                {
                    id = item.yzpcCode,
                    text = item.yzpcmc
                };
                data.Add(obj);
            }
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取抗生素信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult GetKssById(string Id)
        {
            SysMedicineAntibioticInfoEntity entity = _sysMedicineAntibioticInfoRepo.GetKssInfo(Id, OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存抗生素信息
        /// </summary>
        /// <param name="AntibioticInfo"></param>
        /// <returns></returns>
        public IActionResult SubmitKssForm(SysMedicineAntibioticInfoEntity AntibioticInfo)
        {
            AntibioticInfo.OrganizeId = OrganizeId;
            AntibioticInfo.zt = "1";
            AntibioticInfo.LastModifierCode = UserIdentity.UserCode;
            AntibioticInfo.LastModifyTime = DateTime.Now;
            var getId = _sysMedicineAntibioticInfoRepo.SubmitForm(AntibioticInfo);
            return string.IsNullOrWhiteSpace(getId) ? Error("Lưu kháng sinh thất bại") : Success("", getId);
        }

        #region 自动授权药房药库


        /// <summary>
        /// 获取所有药房药库
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllYfbm(string organizeId)
        {
            var result = _sysPharmacyDepartmentRepo.GetEffectiveList(organizeId ?? OrganizeId, null);
            return Success("", result.ToJson());
        }

        /// <summary>
        /// 获取该药品已授权的药房药库
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        public IActionResult GetEmpowermentPharmacyDepartment(string ypId,string organizeId)
        {
            var result = _sysPharmacyDepartmentApp.EmpowermentPharmacyDepartmentQuery(ypId, organizeId ?? OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 提交授权药房药库
        /// </summary>
        /// <param name="p"></param>
        /// <param name="ypCode">药品代码</param>
        /// <param name="keyValue">药品ID</param>
        /// <returns></returns>
        public IActionResult SubmitEmpowermentPharmacyDepartment(List<string> p, string ypCode, int? keyValue)
        {
            var result = _sysPharmacyDepartmentApp.SubmitEmpowermentPharmacyDepartment(keyValue, ypCode, OrganizeId, UserIdentity.UserCode, p);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

		public IActionResult GetItemDetailsByCode(string name) {
			string result = _sysMedicineDmnService.GetTradePricePlus(name);
			return Content(result); ;
		}

        #endregion


        //药品用法下拉浮层
        public IActionResult GetUsageFloat(string keyword = null)
        {
            var list = _sysMedicineDmnService.GetUsageFloat(keyword);
            return Content(list.ToJson());
        }
    }
}