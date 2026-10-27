using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.Web.Controllers
{
    /// <summary>
    /// Chứng hậu Đông y
    /// </summary>
    public class SysTCMSyndromeController : ControllerBase
    {
        private readonly ISysTCMSyndromeRepo _sysTCMSyndromeRepo;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;

        public SysTCMSyndromeController(ISysTCMSyndromeRepo sysTCMSyndromeRepo
            , ISysOrganizeDmnService sysOrganizeDmnService)
        {
            this._sysTCMSyndromeRepo = sysTCMSyndromeRepo;
            this._sysOrganizeDmnService = sysOrganizeDmnService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]

        public IActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var list = _sysTCMSyndromeRepo.GetCommonPagintionList(pagination, keyword);
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
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(int? keyValue)
        {
            var data = _sysTCMSyndromeRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysTCMSyndromeEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = "*";
            _sysTCMSyndromeRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công!");
        }
    }
}