using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// Thuốc hệ thống
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

        public ActionResult SysMedicineAdd()
        {
            return View();
        }

        //grid json Nguồn dữ liệu
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string organizeId,string zt,string ypflCode)
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
        /// Lấy danh sách thông tin thuốc theo tổ chức
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetFloatingYPXXJson(string organizeId, string keyword)
        {
            var list = _SysMedicineRepository.GetYbMedicineList(organizeId, keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetYbName(string organizeId, string keyword,string lx)
        {
            var list = _SysMedicineRepository.GetYbName(organizeId,lx, keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFloatingYPXNHXXJson(string organizeId, string keyword)
        {
            var list = _SysMedicineRepository.GetYbXNHMedicineList(keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        /// <summary>
        /// Gửi thông tin dữ liệu
        /// </summary>
        /// <param name="model"></param>
        /// <param name="p"></param>
        /// <param name="ypCode"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult submitForm(SysMedicineVO model, List<string> p, string ypCode, int? keyValue)
        {
            model.zt = model.zt == "true" ? "1" : "0";
            model.ycmc = model.ycmc ?? "";
            //model.ybbz = model.ybbz == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(model.OrganizeId))
            {
                throw new FailedException("请选择组织机构");
            }

            if (!_sysOrganizeDmnService.IsMedicalOrganize(model.OrganizeId) && !model.OrganizeId.Equals("*"))
            {
                throw new FailedException("请选择医疗机构（医院或诊所）");
            }
            _sysMedicineDmnService.SubmitMedicine(model, keyValue);
            var t = _sysPharmacyDepartmentApp.SubmitEmpowermentPharmacyDepartment(keyValue, ypCode, model.OrganizeId, UserIdentity.UserCode, p);

            var msg = string.IsNullOrWhiteSpace(t) ? "操作成功。" : ("保存药品信息成功，但授权药房药库失败，" + t);
            return Success(msg);
        }

        /// <summary>
        /// Khi sửa thông tin，Đưa thông tin sang trang mới
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(int keyValue)
        {
            var data = _sysMedicineDmnService.GetFormJson(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Đồng bộ thông tin thuốc với BHYT
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult YibaoUploadApi(int ypId, string flag)
        {
            if (ypId <= 0)
            {
                return Success("请选择同步的药品！");
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
                    return Success(string.Format("[{0}]医保同步成功！", ypEntity.ypmc));
                }
                else
                {
                    return Error(string.Format("[{0}]医保同步失败：{1}", ypEntity.ypmc, Result.ErrorMsg));
                }
            }
            else
            {
                return Success("缺少医保代码！");
            }
        }

        public ActionResult GetPcSelectJson()
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
        /// Lấy thông tin kháng sinh
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetKssById(string Id)
        {
            SysMedicineAntibioticInfoEntity entity = _sysMedicineAntibioticInfoRepo.GetKssInfo(Id, OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu thông tin kháng sinh
        /// </summary>
        /// <param name="AntibioticInfo"></param>
        /// <returns></returns>
        public ActionResult SubmitKssForm(SysMedicineAntibioticInfoEntity AntibioticInfo)
        {
            AntibioticInfo.OrganizeId = OrganizeId;
            AntibioticInfo.zt = "1";
            AntibioticInfo.LastModifierCode = UserIdentity.UserCode;
            AntibioticInfo.LastModifyTime = DateTime.Now;
            var getId = _sysMedicineAntibioticInfoRepo.SubmitForm(AntibioticInfo);
            return string.IsNullOrWhiteSpace(getId) ? Error("保存抗生素失败") : Success("", getId);
        }

        #region 自动授权药房药库


        /// <summary>
        /// Lấy tất cả nhà thuốc/kho thuốc
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllYfbm(string organizeId)
        {
            var result = _sysPharmacyDepartmentRepo.GetEffectiveList(organizeId ?? OrganizeId, null);
            return Success("", result.ToJson());
        }

        /// <summary>
        /// Lấy nhà thuốc/kho thuốc đã được ủy quyền của thuốc này
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        public ActionResult GetEmpowermentPharmacyDepartment(string ypId,string organizeId)
        {
            var result = _sysPharmacyDepartmentApp.EmpowermentPharmacyDepartmentQuery(ypId, organizeId ?? OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Gửi nhà thuốc/kho thuốc được ủy quyền
        /// </summary>
        /// <param name="p"></param>
        /// <param name="ypCode">Mã thuốc</param>
        /// <param name="keyValue">ThuốcID</param>
        /// <returns></returns>
        public ActionResult SubmitEmpowermentPharmacyDepartment(List<string> p, string ypCode, int? keyValue)
        {
            var result = _sysPharmacyDepartmentApp.SubmitEmpowermentPharmacyDepartment(keyValue, ypCode, OrganizeId, UserIdentity.UserCode, p);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

		public ActionResult GetItemDetailsByCode(string name) {
			string result = _sysMedicineDmnService.GetTradePricePlus(name);
			return Content(result); ;
		}

        #endregion


        //Lớp nổi thả xuống cách dùng thuốc
        public ActionResult GetUsageFloat(string keyword = null)
        {
            var list = _sysMedicineDmnService.GetUsageFloat(keyword);
            return Content(list.ToJson());
        }
    }
}