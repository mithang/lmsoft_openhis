using Newtouch.MR.ManageSystem.Domain.IDomainServices;
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
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.MRMS.Web.Areas.RecordManage.Controllers
{
    [Area("RecordManage")]
    public class MrFeeRelController : OrgControllerBase
    {
        public MrFeeRelController(IMrFeeRelRepo MrFeeRelRepo, IMrFeeRelDmnService MrFeeRelDmnService)
        {
            this._MrFeeRelRepo = MrFeeRelRepo;
            this._MrFeeRelDmnService = MrFeeRelDmnService;
        }


        private readonly IMrFeeRelRepo _MrFeeRelRepo;
        private readonly IMrFeeRelDmnService _MrFeeRelDmnService;

        // GET: RecordManage/MrFeeRel
        public override IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPagintionList(Pagination pagination, string keyword,string code)
        {
            var pat = _MrFeeRelDmnService.GetPagintionList(pagination, keyword, OrganizeId,code);

            var data = new
            {
                rows = pat,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        //获取二级和三级Nhóm thu phí
        public IActionResult GetFeeSel(string orgId)
        {
            var result = _MrFeeRelDmnService.GetFeeSel(OrganizeId);
            return Content(result.ToJson());
        }

        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _MrFeeRelDmnService.GetFormJson(keyValue, OrganizeId);
            return Content(entity.ToJson());
        }

        public int SubmitForm(bafeeRelVO entity)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            entity.CreateTime = Convert.ToDateTime(DateTime.Now);
            entity.CreatorCode = UserIdentity.UserName;
            _MrFeeRelDmnService.SubmitForm(entity);
            return 1;
        }

        public IActionResult Save(IList<bafeeRelVO> list) {
            foreach (var obj in list) {
                SubmitForm(obj);
            }
            return Success("Thao tác thành công。");
        }

    }
}