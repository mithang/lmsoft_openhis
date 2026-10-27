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
using Newtouch.HIS.Domain.Entity.Settlement;
using Newtouch.HIS.Domain.IRepository.Settlement;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    /// <summary>
    /// 系统诊断
    /// </summary>
    public class SysDiagnosisController : ControllerBase
    {
        private readonly ISysDiagnosisRepo _sysDiagnosisRepo;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
		private readonly ISysSurgeryRepo _syssurgeryrepo;

		public SysDiagnosisController(ISysDiagnosisRepo sysDiagnosisRepo
            , ISysOrganizeDmnService sysOrganizeDmnService
			, ISysSurgeryRepo syssurgeryrepo
			)
        {
            this._sysDiagnosisRepo = sysDiagnosisRepo;
            this._sysOrganizeDmnService = sysOrganizeDmnService;
			this._syssurgeryrepo = syssurgeryrepo;
		}


		public IActionResult Surgery()
		{
			return View();
		}

		public IActionResult SurgeryFrom()
		{
			return View();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		[HttpGet]
        [HandlerAjaxOnly]

        public IActionResult GetGridJson(Pagination pagination, string keyword, string organizeId)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            if (!_sysOrganizeDmnService.IsMedicalOrganize(organizeId))
            {
                return Content(new
                {
                    rows = new List<SysDiagnosisEntity>(),
                    total = 0,
                    page = pagination.page,
                    records = 0,
                }.ToJson());
            }
            var list = _sysDiagnosisRepo.GetPagintionList(organizeId, pagination, keyword);
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
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		[HttpGet]
		[HandlerAjaxOnly]

		public IActionResult SurgeryGetGridJson(Pagination pagination, string keyword)
		{
			pagination.sidx = "id desc";
			pagination.sord = "asc";
			var list = _syssurgeryrepo.SurgeryGetGridJson(pagination, keyword);
			var data = new
			{
				rows = list,
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}


		public IActionResult SurgeryUpGetGridJson(int keyValue)
		{
			var list = _syssurgeryrepo.FindEntity(keyValue);
			return Content(list.ToJson());
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
            entity.mxb = entity.mxb == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("Vui lòng chọn tổ chức");
            }
            else if (!_sysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId)&& entity.OrganizeId!="*")
            {
                throw new FailedException("Vui lòng chọn cơ sở y tế(Bệnh viện hoặc phòng khám)");
            }
            _sysDiagnosisRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công!");
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
		public IActionResult SurSubmitForm(SurgeryEntity entity, int? keyValue)
		{
			_syssurgeryrepo.SubmitForm(entity, keyValue);
			return Success("Thao tác thành công!");
		}
	}
}