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

    public class ModuleButtonController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IModuleApp _moduleApp;
        private readonly IModuleButtonApp _moduleButtonApp;
        private readonly ISysModuleDmnService _sysModuleDmnService;
        public ModuleButtonController(IModuleApp moduleApp, IModuleButtonApp moduleButtonApp, ISysModuleDmnService sysModuleDmnService)
        {
            _moduleApp = moduleApp;
            _moduleButtonApp = moduleButtonApp;
            _sysModuleDmnService = sysModuleDmnService;
        }


        //grid json
        public IActionResult GetGridJson(string moduleId)
        {
            var data = _sysModuleDmnService.GetMenuButtonListByTopOrg(SysConfigReader.String("Top_OrganizeId"), moduleId);
            return Content(data.ToJson());
        }

        [HttpGet]

        public IActionResult GetFormJson(string keyValue)
        {
            var data = _moduleButtonApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]

        //[ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysModuleButtonEntity moduleButtonEntity, string keyValue)
        {
            moduleButtonEntity.zt = moduleButtonEntity.zt == "true" ? "1" : "0";
            _sysModuleDmnService.ModuleButtonSubmitForm(moduleButtonEntity, keyValue, SysConfigReader.String("Top_OrganizeId"));
            return Success("Thao tác thành công。");
        }

        [HttpPost]

        //[ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _moduleButtonApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        #region Phân quyền Cơ sở

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Organizes()
        {
            return View();
        }

        /// <summary>
        /// Phân quyềnchochỉđịnhnhómtổ chứcmáycơ quan
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="orgList"></param>
        /// <returns></returns>
        [HttpPost]

        //[ValidateAntiForgeryToken]
        public IActionResult UpdateAuthOrganizeList(string keyValue, string orgList)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysModuleDmnService.UpdateAuthOrganizeList(keyValue, orgList, 2);
            }
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Phân quyềnchonơicónhómtổ chứcmáycơ quan
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]

        //[ValidateAntiForgeryToken]
        public IActionResult AuthAllOrganize(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysModuleDmnService.AuthAllOrganize(keyValue, 2);
            }
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Hủy tất cảPhân quyền(Tổ chức)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]

        //[ValidateAntiForgeryToken]
        public IActionResult AuthCancelAllOrganize(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysModuleDmnService.AuthCancelAllOrganize(keyValue, 2);
            }
            return Success("Thao tác thành công。");
        }

        #endregion
    }
}
