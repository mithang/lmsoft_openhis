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
    public class OPAnesthesiaController : OrgControllerBase
    {
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IORAnesthesiaRepo _ORAnesthesiaRepo;
        public OPAnesthesiaController(ICommonDmnService commonDmnService, IORAnesthesiaRepo oRAnesthesiaRepo)
        {
            _CommonDmnService = commonDmnService;
            _ORAnesthesiaRepo = oRAnesthesiaRepo;
        }
        // GET: Operation/OPAnesthesia

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetGridList(Pagination pagination, string keyword)
        {
            var oplist = _ORAnesthesiaRepo.GetPagintionList(pagination, keyword,OrganizeId);

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
            var pat = _ORAnesthesiaRepo.GetPagintionList(pagination, keyword,OrganizeId);

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
            var entity = _ORAnesthesiaRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }
        public IActionResult submitForm(ORAnesthesiaEntity entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            _ORAnesthesiaRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }
        public IActionResult DeleteData(string keyValue)
        {
            _ORAnesthesiaRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }
    }
}