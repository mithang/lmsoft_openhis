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
    public class OpStaffController : OrgControllerBase
    {
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IORStaffRepo _ORStaffRepo;
        public OpStaffController(ICommonDmnService commonDmnService, IORStaffRepo oRStaffRepo)
        {
            _CommonDmnService = commonDmnService;
            _ORStaffRepo = oRStaffRepo;
        }
        // GET: Operation/OPStaff
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetGridList(Pagination pagination, string keyword)
        {
            var oplist = _ORStaffRepo.GetPagintionList(pagination, keyword,OrganizeId);

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
            var pat = _ORStaffRepo.GetPagintionList(pagination, keyword,OrganizeId);

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
            var entity = _ORStaffRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }
        public IActionResult submitForm(ORStaffEntity entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            _ORStaffRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }
        public IActionResult DeleteData(string keyValue)
        {
            _ORStaffRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }
    }
}