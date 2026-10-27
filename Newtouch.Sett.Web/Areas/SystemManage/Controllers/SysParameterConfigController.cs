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

    /// <summary>
    /// 系统参数设置
    /// </summary>
    [Area("SystemManage")]

    public class SysParameterConfigController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysParameterConfigApp _sysParameterConfigApp;
        private readonly ISysChargeTemplateRepo _sysChargeTemplateRepo;
        private readonly ISysChargeItemDmnService _sysChargeItemDmnService;
        private readonly ISysDepartmentRepo _SysDepartmentRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysFeeDmnService _sysFeeDmnService;
        public SysParameterConfigController(ISysParameterConfigApp sysParameterConfigApp, ISysChargeTemplateRepo sysChargeTemplateRepo, ISysChargeItemDmnService sysChargeItemDmnService, ISysDepartmentRepo sysDepartmentRepo, ISysConfigRepo sysConfigRepo, ISysFeeDmnService sysFeeDmnService)
        {
            _sysParameterConfigApp = sysParameterConfigApp;
            _sysChargeTemplateRepo = sysChargeTemplateRepo;
            _sysChargeItemDmnService = sysChargeItemDmnService;
            _SysDepartmentRepo = sysDepartmentRepo;
            _sysConfigRepo = sysConfigRepo;
            _sysFeeDmnService = sysFeeDmnService;
        }


        #region mẫu thu phí

        /// <summary>
        /// 收费模板 Index
        /// </summary>
        /// <returns></returns>
        public IActionResult ChargeTemplateIndex()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sfmbbh"></param>
        /// <returns></returns>
        public IActionResult ChargeTemplate_EditForm()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult ChargeTemplate_GetGridJson(Pagination pagination, string keyword, string yxbz)
        {
            var list = _sysChargeItemDmnService.Search(pagination, keyword, this.OrganizeId,yxbz);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sfmbbh"></param>
        /// <returns></returns>
        public IActionResult ChargeTemplate_EditForm_Data(string keyValue)
        {
            SysChargeTemplateInfoVM vm = null;
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                vm = _sysParameterConfigApp.GetSysChargeTemplateInfo(keyValue);
            }

            vm = vm ?? new SysChargeTemplateInfoVM();
            return Content(vm.ToJson());
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]

        //[ValidateAntiForgeryToken]
        public IActionResult ChargeTemplate_SubmitForm(SysChargeTemplateEntity entity, string xmListStr)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _sysParameterConfigApp.ChargeTemplate_SubmitForm(entity, xmListStr);
            return Success("Thao tác thành công。");
        }

        public IActionResult GetUsages() {

            var data = _sysChargeItemDmnService.GetYF();
            return Content(data.ToJson());

        }
        public IActionResult deleteid(string keyValue)
        {
            _sysFeeDmnService.DeleteById(keyValue, this.OrganizeId);
            return Success("Xóa thành công");
        }
        #endregion

        #region


        #endregion

    }
}
