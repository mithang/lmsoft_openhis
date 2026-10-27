using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.Settlement;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    public class SysWardBedController : ControllerBase
    {
        private readonly ISysWardBedRepo _sysWardBedRepo;
        private readonly ISysWardBedDmnService _sysWardBedDmnService;
        private readonly ISysWardRepo _sysWardRepo;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;

        public SysWardBedController(ISysWardRepo sysWardRepo,ISysWardBedRepo sysWardBedRepo, ISysWardBedDmnService sysWardBedDmnService
            , ISysOrganizeDmnService sysOrganizeDmnService)
        {
            this._sysWardRepo = sysWardRepo;
            this._sysWardBedRepo = sysWardBedRepo;
            this._sysWardBedDmnService = sysWardBedDmnService;
            this._sysOrganizeDmnService = sysOrganizeDmnService;
        }

        // GET: SysBed
        public override IActionResult Index()
        {
            return View();
        }

        public override IActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// Thả xuống khu bệnh
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IActionResult GetWardBindSelect(string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var data = _sysWardRepo.SelectWardList(orgId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.bqCode;
                treeModel.text = item.bqmc;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <returns></returns>
        public IActionResult SubmitForm(SysWardBedEntity entity, int? cwId)
        {
            entity.jcbz = entity.jcbz == "true" ? "1" : "0";
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysWardBedRepo.SubmitForm(entity, cwId);
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        public IActionResult GetGridJson(Pagination pagination, string keyword, string organizeId)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            if (!_sysOrganizeDmnService.IsMedicalOrganize(organizeId))
            {
                return Content(new
                {
                    rows = new List<SysWardBedVO>(),
                    total = 0,
                    page = pagination.page,
                    records = 0,
                }.ToJson());
            }
            var list = _sysWardBedDmnService.GetPagintionList(organizeId, pagination, keyword);
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
        /// Thêm mới hoặc sửaForm
        /// </summary>
        public IActionResult GetFormJson(int? cwId, string orgId)
        {
            var entity = _sysWardBedDmnService.GetWardBedList(cwId, orgId).FirstOrDefault();
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Xóa
        /// </summary>
        public IActionResult DeleteForm(int cwId, string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            _sysWardBedRepo.DeleteForm(cwId, orgId);
            return Success("Thao tác thành công.");
        }
    }
}