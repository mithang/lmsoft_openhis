using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    public class ModuleButtonController : ControllerBase
    {
        private readonly IModuleApp _moduleApp;
        private readonly IModuleButtonApp _moduleButtonApp;
        private readonly ISysModuleDmnService _sysModuleDmnService;

        public ModuleButtonController(IModuleApp moduleApp, IModuleButtonApp moduleButtonApp
            , ISysModuleDmnService sysModuleDmnService)
        {
            this._moduleApp = moduleApp;
            this._moduleButtonApp = moduleButtonApp;
            this._sysModuleDmnService = sysModuleDmnService;
        }

        //grid json
        public IActionResult GetGridJson(string moduleId)
        {
            var data = _sysModuleDmnService.GetMenuButtonListByTopOrg(Constants.TopOrganizeId, moduleId);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(string keyValue)
        {
            var data = _moduleButtonApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysModuleButtonEntity moduleButtonEntity, string keyValue)
        {
            moduleButtonEntity.zt = moduleButtonEntity.zt == "true" ? "1" : "0";
            _sysModuleDmnService.ModuleButtonSubmitForm(moduleButtonEntity, keyValue, Constants.TopOrganizeId);
            return Success("Thao tác thành công.");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _moduleButtonApp.DeleteForm(keyValue);
            return Success("Xóa thành công.");
        }

        #region 授权机构

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Organizes()
        {
            return View();
        }

        /// <summary>
        /// 授权给指定组织机构
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="orgList"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult UpdateAuthOrganizeList(string keyValue, string orgList)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysModuleDmnService.UpdateAuthOrganizeList(keyValue, orgList, 2);
            }
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// 授权给所有组织机构
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult AuthAllOrganize(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysModuleDmnService.AuthAllOrganize(keyValue, 2);
            }
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// 撤销全部授权（组织机构）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult AuthCancelAllOrganize(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysModuleDmnService.AuthCancelAllOrganize(keyValue, 2);
            }
            return Success("Thao tác thành công.");
        }

        #endregion

    }
}