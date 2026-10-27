using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.OR.ManageSystem.Domain.DTO;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace Newtouch.OR.ManageSystem.Web.Areas.Operation.Controllers
{
    [Area("Operation")]
    public class OpOperationController : OrgControllerBase
    {
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IOROperationRepo _OROperationRepo;
        public OpOperationController(ICommonDmnService commonDmnService, IOROperationRepo oROperationRepo)
        {
            _CommonDmnService = commonDmnService;
            _OROperationRepo = oROperationRepo;
        }

        // GET: Operation/OpOperation
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetGridList(Pagination pagination, string keyword)
        {
            var oplist = _OROperationRepo.GetPagintionList(pagination,keyword,OrganizeId);

            var data = new
            {
                rows = oplist,
                page = 1,
                records = 1,
                total = 1
            };
            return Content(data.ToJson());
        }
        public IActionResult GetPagintionList(Pagination pagination, string keyword)
        {
            var pat = _OROperationRepo.GetPagintionList(pagination, keyword,OrganizeId);

            var data = new
            {
                rows = pat,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _OROperationRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }
        public IActionResult submitForm(OROperationEntity entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            _OROperationRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }
        public IActionResult DeleteData(string keyValue)
        {
            _OROperationRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }
    }
}