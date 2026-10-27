using Newtouch.MR.ManageSystem.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Init;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Infrastructure;
using Newtouch.MRMS.Web.Models;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.MRMS.Web.Areas.RecordManage.Controllers
{
    [Area("RecordManage")]
    public class MrFeeController : OrgControllerBase
    {
        public MrFeeController(IMrFeeDmnService MrFeeDmnService, IMrFeeRepo MrFeeRepo)
        {
            this._MrFeeDmnService = MrFeeDmnService;
            this._MrFeeRepo = MrFeeRepo;
        }


#pragma warning disable CS0649 // Field 'MrFeeController._MrFeeDmnService' is never assigned to, and will always have its default value null
        private readonly IMrFeeDmnService _MrFeeDmnService;
#pragma warning restore CS0649 // Field 'MrFeeController._MrFeeDmnService' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'MrFeeController._MrFeeRepo' is never assigned to, and will always have its default value null
        private readonly IMrFeeRepo _MrFeeRepo;
#pragma warning restore CS0649 // Field 'MrFeeController._MrFeeRepo' is never assigned to, and will always have its default value null

        // GET: RecordManage/MrFee
#pragma warning disable CS0114 // 'MrFeeController.Index()' hides inherited member 'BaseController.Index()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        public override IActionResult Index()
#pragma warning restore CS0114 // 'MrFeeController.Index()' hides inherited member 'BaseController.Index()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        {
            return View();
        }

        public IActionResult GetPagintionList(Pagination pagination, string keyword)
        {
            var oplist = _MrFeeDmnService.GetPaginationList(pagination, OrganizeId,keyword);

            var data = new
            {
                rows = oplist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public IActionResult GetFormJson(string keyValue,int index)
        {
            var entity = _MrFeeDmnService.GetFormJson(keyValue,index);
            return Content(entity.ToJson());
        }

        //获取Nhóm thu phí cấp 1
        public IActionResult GetFeeOne(string orgId) {
            var result = _MrFeeDmnService.GetFeeOne(OrganizeId);
            return Content(result.ToJson());
        }
        //获取Nhóm thu phí cấp 2
        public IActionResult GetFeeTwo(string orgId, string parentCode) {
            var result = _MrFeeDmnService.GetFeeTwo(OrganizeId, parentCode);
            return Content(result.ToJson());
        }

        public IActionResult submitForm(bafeeVO entity, string keyValue)
        {
			//if (string.IsNullOrWhiteSpace(entity.OrganizeId))
			//{
			//    entity.OrganizeId = OrganizeId;
			//}

			//Theo主键获取Sửa前的数据
			var oldEntity=_MrFeeDmnService.GetFormJson(keyValue, entity.Lev);

			_MrFeeRepo.SubmitForm(entity, keyValue,OrganizeId,oldEntity);
            return Success("Thao tác thành công。");
        }

        public IActionResult DeleteData(string keyValue)
        {
            _MrFeeRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công。");
        }
    }
}