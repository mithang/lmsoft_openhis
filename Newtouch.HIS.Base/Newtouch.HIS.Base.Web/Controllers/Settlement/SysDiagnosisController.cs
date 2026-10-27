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
    /// Chẩn đoán hệ thống
    /// </summary>
    public class SysDiagnosisController : ControllerBase
    {
        private readonly ISysDiagnosisRepo _sysDiagnosisRepo;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;

        public SysDiagnosisController(ISysDiagnosisRepo sysDiagnosisRepo
            , ISysOrganizeDmnService sysOrganizeDmnService)
        {
            this._sysDiagnosisRepo = sysDiagnosisRepo;
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
            var list = _sysDiagnosisRepo.GetCommonPagintionList(pagination, keyword);
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
            var data = _sysDiagnosisRepo.FindEntity(keyValue);
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
        public IActionResult SubmitForm(SysDiagnosisEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = "*";
            _sysDiagnosisRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công!");
        }
    }
}