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

            // FIX-2 C2-B3: truoc day hardcode page/records/total = 1 nen khi
            // keyword loc ra rows rong thi pager van bao 1. Dung pagination that
            // do QueryWithPage dien (giong GetPagintionList ben canh).
            var data = new
            {
                rows = oplist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
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
            // G24 (theo mau G23 OPAnesthesiaController): khoa rong/khong ton
            // tai/khac co so -> loi ro rang, khong xoa nham dong cua co so
            // khac; repo chi nhan Id nen rang buoc co so phai kiem tra o day.
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return Error("Không tìm thấy nhân viên phẫu thuật");
            }
            var dbEntity = _ORStaffRepo.FindEntity(p => p.Id == keyValue && p.OrganizeId == OrganizeId && p.zt == "1");
            if (dbEntity == null)
            {
                return Error("Không tìm thấy nhân viên phẫu thuật");
            }
            _ORStaffRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }
    }
}