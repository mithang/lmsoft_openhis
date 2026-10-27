using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.CIS.Web.Helpers;
using Newtouch.CIS.Web.Attributes;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using Newtouch.Application.Interface;

using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.CIS.Compat;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 用法联动维护
    /// </summary>
    [Area("SystemManage")]
    public class UsageLinkageController : OrgControllerBase
    {
        private readonly IUsageLinkageApp _usageLinkageApp;
        private readonly ISysUsageLinkageRepo _sysUsageLinkageRepo;
        private readonly ISysUsageLinkageDmnService _sysUsageLinkageDmnService;

        public UsageLinkageController(IUsageLinkageApp usageLinkageApp, ISysUsageLinkageRepo sysUsageLinkageRepo, ISysUsageLinkageDmnService sysUsageLinkageDmnService)
        {
            this._usageLinkageApp = usageLinkageApp;
            this._sysUsageLinkageRepo = sysUsageLinkageRepo;
            this._sysUsageLinkageDmnService = sysUsageLinkageDmnService;
        }

        /// <summary>
        /// 获取主列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="yfCode"></param>
        /// <param name="xmCode"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeGridJson(Pagination pagination, string keyword, string yfCode, string xmCode)
        {
            var data = new
            {
                rows = _sysUsageLinkageDmnService.SelectSysUsageLinkage(pagination, keyword, yfCode, xmCode, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 修改信息时，把信息带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(string keyValue)
        {
            var result = _sysUsageLinkageDmnService.SelectSysUsageLinkage(keyValue);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(string keyValue)
        {
            var sysUsageLinkageEntity = new SysUsageLinkageEntity
            {
                Id = keyValue,
                dlCode = Request.Param("sfdlCode"),
                OrganizeId = OrganizeId,
                sfxmCode = Request.Param("sfxmCode"),
                yfCode = Request.Param("yfCode"),
                zt = Request.Param("zt")
            };
            var result = _usageLinkageApp.SubmitForm(sysUsageLinkageEntity);
            return string.IsNullOrWhiteSpace(result) ? Success("Thao tác thành công.") : Error(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            var entity = _sysUsageLinkageRepo.FindEntity(keyValue);
            if (entity == null) return Success("Xóa thành công。");
            return _sysUsageLinkageRepo.Delete(entity) > 0 ? Success("Xóa thành công.") : Error("Xóathất bại.");
        }
    }
}