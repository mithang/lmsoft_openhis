using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    public class SysMedicineController : ControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysMedicineDmnService _sysMedicineDmnService;
        private readonly ISysMedicineBaseDmnService _sysMedicineBaseDmnService;
        private readonly ISysPharmacyDepartmentBaseDmnService _sysPharmacyDepartmentBaseDmnService;
        private readonly ISysPharmacyDepartmentDmnService _sysPharmacyDepartmentDmnService;
        //private readonly ISysPharmacyDepartmentApp _sysPharmacyDepartmentApp;
        private readonly ISysPharmacyDepartmentMedicineRepo _sysPharmacyDepartmentMedicineRepo;


        public SysMedicineController(
            ISysOrganizeDmnService sysOrganizeDmnService
            , ISysMedicineDmnService sysMedicineDmnService
            ,ISysMedicineBaseDmnService sysMedicineBaseDmnService
            , ISysPharmacyDepartmentBaseDmnService sysPharmacyDepartmentBaseDmnService
            ,ISysPharmacyDepartmentDmnService sysPharmacyDepartmentDmnService
            //, ISysPharmacyDepartmentApp sysPharmacyDepartmentApp
            , ISysPharmacyDepartmentMedicineRepo sysPharmacyDepartmentMedicineRepo
            )
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysMedicineDmnService = sysMedicineDmnService;
            this._sysMedicineBaseDmnService = sysMedicineBaseDmnService;
            this._sysPharmacyDepartmentBaseDmnService = sysPharmacyDepartmentBaseDmnService;
            this._sysPharmacyDepartmentDmnService = sysPharmacyDepartmentDmnService;
            //this._sysPharmacyDepartmentApp = sysPharmacyDepartmentApp;
            this._sysPharmacyDepartmentMedicineRepo = sysPharmacyDepartmentMedicineRepo;
        }

        public ActionResult SysMedicineAdd()
        {
            return View();
        }

        //grid json nguồn dữ liệu
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string organizeId, string zt, string ypflCode)
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
        /// theo tổ chức lấy thông tin thuốc danh sách
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetFloatingYPXXJson(string organizeId, string keyword)
        {
            var list = _sysMedicineDmnService.GetYbMedicineList(organizeId, keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetYbName(string organizeId, string keyword, string lx)
        {
            var list = _sysMedicineDmnService.GetYbName(organizeId, lx, keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFloatingYPXNHXXJson(string organizeId, string keyword)
        {
            var list = _sysMedicineDmnService.GetYbXNHMedicineList(keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        /// <summary>
        /// gửi dữ liệu thông tin
        /// </summary>
        /// <param name="model"></param>
        /// <param name="p"></param>
        /// <param name="ypCode"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        //[HttpPost]
        //[HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        //public ActionResult submitForm(SysMedicineVO model, List<string> p, string ypCode, int? keyValue)
        //{
        //    model.zt = model.zt == "true" ? "1" : "0";
        //    model.ycmc = model.ycmc ?? "";
        //    //model.ybbz = model.ybbz == "true" ? "1" : "0";
        //    if (string.IsNullOrWhiteSpace(model.OrganizeId))
        //    {
        //        throw new FailedException("vui lòng chọn tổ chức");
        //    }

        //    if (!_sysOrganizeDmnService.IsMedicalOrganize(model.OrganizeId))
        //    {
        //        throw new FailedException("vui lòng chọn cơ sở y tế (bệnh viện hoặc phòng khám)");
        //    }
        //    _sysMedicineDmnService.SubmitMedicine(model, keyValue);
        //    var t = _sysPharmacyDepartmentApp.SubmitEmpowermentPharmacyDepartment(keyValue, ypCode, OrganizeId, UserIdentity.UserCode, p);

        //    var msg = string.IsNullOrWhiteSpace(t)? "thao tác thành công.": ("lưu thông tin thuốc thành công, nhưng ủy quyền nhà thuốc kho thuốc thất bại, " + t);
        //    return Success(msg);
        //}

        /// <summary>
        /// sửa thông tin giờ, đưa thông tin sang trang mới
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
        /// thông tin thuốc đồng bộ BHYT
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        //[HttpPost]
        //[HandlerAjaxOnly]
        //public ActionResult YibaoUploadApi(int ypId, string flag)
        //{
        //    if (ypId <= 0)
        //    {
        //        return Success("vui lòng chọn đồng bộ của thuốc!");
        //    }
        //    var ypEntity = _sysMedicineDmnService.GetFormJson(ypId);
        //    if (!string.IsNullOrWhiteSpace(ypEntity.ybdm))
        //    {
        //        var Result = newtouchyibao.YibaoUniteInterface.Mcatalogrl("Z092", "0", ypEntity.ybdm, ypEntity.ypCode, ypEntity.ypmc, ypEntity.jxmc, ypEntity.ycmc, ypEntity.ypgg, ypEntity.bzdw, ypEntity.lsj, flag);
        //        if (Result.Code == 0 && Result.Data.Count > 0 && Result.Data[0].TPCODE == 0)
        //        {
        //            string error = "";
        //            if (!_sysMedicineDmnService.YibaoUpload(ypId, out error))
        //            {
        //                return Error(error);
        //            }
        //            return Success(string.Format("[{0}]BHYT đồng bộ thành công!", ypEntity.ypmc));
        //        }
        //        else
        //        {
        //            return Error(string.Format("[{0}]BHYT đồng bộ thất bại: {1}", ypEntity.ypmc, Result.ErrorMsg));
        //        }
        //    }
        //    else
        //    {
        //        return Success("缺少医保代码!");
        //    }
        //}

        public ActionResult GetPcSelectJson()
        {
            var data = new List<object>();
            List<SysMedicalOrderFrequencyVO> entityList = _sysMedicineBaseDmnService.GetOrderFrequencyList(this.OrganizeId);
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
        /// lấy thông tin kháng sinh
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetKssById(string Id)
        {
            SysMedicineAntibioticInfoVO entity = _sysMedicineBaseDmnService.GetKssInfo(Id, OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// lưu kháng sinh thông tin
        /// </summary>
        /// <param name="AntibioticInfo"></param>
        /// <returns></returns>
        //public ActionResult SubmitKssForm(SysMedicineAntibioticInfoEntity AntibioticInfo)
        //{
        //    AntibioticInfo.OrganizeId = OrganizeId;
        //    AntibioticInfo.zt = "1";
        //    AntibioticInfo.LastModifierCode = UserIdentity.UserCode;
        //    AntibioticInfo.LastModifyTime = DateTime.Now;
        //    var getId = _sysMedicineAntibioticInfoRepo.SubmitForm(AntibioticInfo);
        //    return string.IsNullOrWhiteSpace(getId)? Error("lưu kháng sinh thất bại"): Success("", getId);
        //}

        #region 自动授权药房药库


        /// <summary>
        /// lấy tất cả nhà thuốc kho thuốc
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllYfbm()
        {
            var result = _sysPharmacyDepartmentBaseDmnService.GetEffectiveList(OrganizeId, null);
            return Success("", result.ToJson());
        }

        /// <summary>
        /// lấy nên thuốc đã ủy quyền của nhà thuốc kho thuốc
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        public ActionResult GetEmpowermentPharmacyDepartment(string ypId)
        {
            var result = _sysPharmacyDepartmentDmnService.SelectEmpowermentYfbmByYp(ypId, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// gửi ủy quyền nhà thuốc kho thuốc
        /// </summary>
        /// <param name="p"></param>
        /// <param name="ypCode">mã thuốc</param>
        /// <param name="keyValue">thuốc ID</param>
        /// <returns></returns>
        //public ActionResult SubmitEmpowermentPharmacyDepartment(List<string> p, string ypCode, int? keyValue)
        //{
        //    var result = _sysPharmacyDepartmentApp.SubmitEmpowermentPharmacyDepartment(keyValue, ypCode, OrganizeId, UserIdentity.UserCode, p);
        //    return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        //}

        public ActionResult GetItemDetailsByCode(string name)
        {
            string result = _sysMedicineDmnService.GetTradePricePlus(name);
            return Content(result); ;
        }

        #endregion


        //cách dùng thuốc thả xuống lớp nổi
        public ActionResult GetUsageFloat(string keyword = null)
        {
            var list = _sysMedicineDmnService.GetUsageFloat(keyword);
            return Content(list.ToJson());
        }


        #region base基础数据

        /// <summary>
        /// theo từ khóa tra cứu phân loại thuốc
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetListSelectJson(string keyword)
        {
            var list = _sysMedicineBaseDmnService.GetValidList(keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //hộp thả xuống dữ liệu gắn
        public ActionResult GetSelectJson(string code)
        {
            var data = _sysMedicineBaseDmnService.GetValidListByItemCode(code, "");
            var list = new List<object>();
            foreach (SysItemsDetailVO item in data)
            {
                list.Add(new { id = item.Code, text = item.Name });
            }
            return Content(list.ToJson());
        }

        //cây danh mục thu phí lớn (bệnh viện) thả xuống nguồn dữ liệu
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string organizeId, string treeidFieldName = "Code")
        {
            var data = _sysMedicineBaseDmnService.GetsfdlValidList(organizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                if (treeidFieldName == "Code")
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.dlCode;
                    treeModel.text = item.dlmc;
                    treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0)
                        ? data.Where(p => p.dlId == item.ParentId).Select(p => p.dlCode).FirstOrDefault()
                        : null;
                    treeList.Add(treeModel);
                }
                else
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.dlId.ToString();
                    treeModel.text = item.dlmc;
                    treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0) ? item.ParentId.ToString() : null;
                    treeList.Add(treeModel);
                }
            }
            return Content(treeList.TreeSelectJson(null));
        }

        public JsonResult GetypjxListSelectJson(string keyword)
        {
            var list = _sysMedicineBaseDmnService.GetypjxValidList(keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// lấy thuốc đơn vị danh sách
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public ActionResult GetypdwListSelectJson(string keyword)
        {
            var data = _sysMedicineBaseDmnService.GetypdwValidList(keyword);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GettsypbzSelectJson(string code, string keyword, string OrganizeId)
        {
            List<object> list = new List<object>();
            if (code == "tsypbz")
            {
                foreach (var item in System.Enum.GetValues(typeof(EnumYpsx)))
                {
                    object name = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
                    list.Add(new { id = (int)item, text = (name as DescriptionAttribute).Description });
                }
                return Content(list.ToJson());
            }
            var data = _sysMedicineBaseDmnService.GetValidListByItemCode(code, keyword, OrganizeId);
            foreach (SysItemsDetailVO item in data)
            {
                list.Add(new { id = item.Code, text = item.Name });
            }
            return Content(list.ToJson());
        }

        #endregion
        
    }
}