using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Model;
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
    public class OpNotchGradeController : OrgControllerBase
    {
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IORNotchGradeRepo _ORNotchGradeRepo;
        public OpNotchGradeController(ICommonDmnService commonDmnService, IORNotchGradeRepo oRNotchGradeRepo)
        {
            _CommonDmnService = commonDmnService;
            _ORNotchGradeRepo = oRNotchGradeRepo;
        }
        // GET: Operation/OpNotchGrade
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetGridList(Pagination pagination, string keyword)
        {
            var oplist = _ORNotchGradeRepo.GetPagintionList(pagination, keyword,OrganizeId);

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
            var pat = _ORNotchGradeRepo.GetPagintionList(pagination, keyword,OrganizeId);

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
            var entity = _ORNotchGradeRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }
        public IActionResult submitForm(ORNotchGradeEntity entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            _ORNotchGradeRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }
        public IActionResult DeleteData(string keyValue)
        {
            _ORNotchGradeRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }
    }
}