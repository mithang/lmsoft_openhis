using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices.Rehabilitation;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    public class RehabChargeItemComparisonController : ControllerBase
    {
        private readonly IRehabChargeItemComparisonRepo _rehabChargeItemComparisonRepo;
        private readonly IRehabilitationDmnService _rehabilitationDmnService;
        public RehabChargeItemComparisonController(IRehabilitationDmnService rehabilitationDmnService, IRehabChargeItemComparisonRepo rehabChargeItemComparisonRepo)
        {
            this._rehabilitationDmnService = rehabilitationDmnService;
            this._rehabChargeItemComparisonRepo = rehabChargeItemComparisonRepo;
        }

        public override IActionResult Index()
        {
            return View();
        }

        public override IActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(RehabChargeItemComparisonEntity entity, string sfxmdzId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _rehabChargeItemComparisonRepo.SubmitForm(entity, sfxmdzId);
            return Success("Thao tác thành công.");
        }


        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetRehabChargeItemComparisonList(string keyword,string OrganizeId)
        {
            var data = _rehabilitationDmnService.GetRehabChargeItemComparisonList(keyword,OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(string sfxmdzId, string OrganizeId)
        {
            var entity = _rehabilitationDmnService.GetRehabChargeItemComparisonEntity(sfxmdzId,OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult DeleteForm(string sfxmdzId, string OrganizeId)
        {
            _rehabChargeItemComparisonRepo.DeleteForm(sfxmdzId,OrganizeId);
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// Thả xuống hạng mục phục hồi chức năng
        /// </summary>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetRehabBindSelect(string keyword, string OrganizeId)
        {
            if (string.IsNullOrEmpty(OrganizeId))
            {
                return null;
            }
            var list = _rehabilitationDmnService.GetRehabChargeItemList(keyword, OrganizeId);
            return Json(list);
        }

        /// <summary>
        /// Hạng mục thu phí Chọn Search
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns
        [HandlerAjaxOnly]
        public JsonResult GetHisBindSelect(string keyword, string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            var list = _rehabilitationDmnService.GetHisBindSelect(keyword, orgId);
            return Json(list);
        }

    }
}