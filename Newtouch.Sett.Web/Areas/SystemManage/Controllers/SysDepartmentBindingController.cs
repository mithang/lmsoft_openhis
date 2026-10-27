using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{

    [Area("SystemManage")]

    public class SysDepartmentBindingController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysDepartmentBindingRepo _sysDepartmentBindingRepo;

        private readonly ISysDepartmentBindingDmnService _sysDepartmentBindingDmnService;
        public SysDepartmentBindingController(ISysDepartmentBindingRepo sysDepartmentBindingRepo, ISysDepartmentBindingDmnService sysDepartmentBindingDmnService)
        {
            _sysDepartmentBindingRepo = sysDepartmentBindingRepo;
            _sysDepartmentBindingDmnService = sysDepartmentBindingDmnService;
        }


        // GET: SystemManage/SysDepartmentBinding
        public IActionResult SysDepartmentBindingIndex()
        {
            return View();
        }

        //[HandlerAuthorize]
        public override IActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetSysDepartmentBindingList(string keyword)
        {
            var data = _sysDepartmentBindingRepo.GetSysDepartmentBindingList(keyword, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string bddm)
        {
            var entity = _sysDepartmentBindingRepo.GetSysDepartmentBindingEntity(bddm, this.OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysDepartmentBindingEntity sysDepartmentBindgEntity, string bddm)
        {
            sysDepartmentBindgEntity.zt = sysDepartmentBindgEntity.zt == "true" ? "1" : "0";
            sysDepartmentBindgEntity.OrganizeId = this.OrganizeId;
            var Screen = _sysDepartmentBindingRepo.FindEntity(o => o.ks == sysDepartmentBindgEntity.ks && o.OrganizeId==OrganizeId && o.zt=="1");
            if (Screen!=null && !string.IsNullOrEmpty(Screen.bddm))
            {
                return Error("Vui lòng một một đối với phải，Không thể gắn nhiều！");
            }
           
            var saffentity = _sysDepartmentBindingDmnService.GetSaffEntity(sysDepartmentBindgEntity.gh, this.OrganizeId);
            if (string.IsNullOrEmpty(saffentity.zjh))
            {
                return Error("Vui lòng bảo trì thông tin CMND/CCCD nhân viên!");
            }
            sysDepartmentBindgEntity.xb = saffentity.xb; ;
            sysDepartmentBindgEntity.zjlx = saffentity.zjlx;
            sysDepartmentBindgEntity.zjh = saffentity.zjh;
           
            _sysDepartmentBindingRepo.SubmitForm(sysDepartmentBindgEntity, bddm);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string bddm)
        {
            _sysDepartmentBindingRepo.DeleteForm(bddm, this.OrganizeId);
            return Success("Thao tác thành công。");
        }
       
    }
}
