using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices.Rehabilitation;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.Web.Controllers
{
    public class RehabChargeItemController : ControllerBase
    {
        private readonly IRehabChargeItemRepo _rehabChargeItemRepo;
        private readonly IRehabilitationDmnService _rehabilitationDmnService;
        private readonly IRehabChargeClassificationRepo _rehabChargeClassificationRepo;
        public RehabChargeItemController(IRehabChargeClassificationRepo rehabChargeClassificationRepo, IRehabChargeItemRepo rehabChargeItemRepo, IRehabilitationDmnService rehabilitationDmnService)
        {
            this._rehabChargeClassificationRepo = rehabChargeClassificationRepo;
            this._rehabChargeItemRepo = rehabChargeItemRepo;
            this._rehabilitationDmnService = rehabilitationDmnService;
        }
        /// <summary>
        /// Trang chính
        /// </summary>
        /// <returns></returns>
        public override IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Thêm mới/Sửa
        /// </summary>
        /// <returns></returns>
        public override IActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// Thả xuống phân loại thu phí
        /// </summary>
        /// <returns></returns>
        public IActionResult GetsfflBindSelect()
        {
            var data = _rehabChargeClassificationRepo.GetRehabChargeClassificationList(null);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Code;
                treeModel.text = item.Name;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        public IActionResult SubmitForm(RehabChargeItemEntity entity, string sfxmId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _rehabChargeItemRepo.SubmitForm(entity, sfxmId);
            return Success("Thao tác thành công.");
        }


        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        public IActionResult GetRehabChargeItemList(string keyword)
        {
            var data = _rehabilitationDmnService.GetRehabChargeItemList(keyword, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        public IActionResult GetFormJson(string sfxmId)
        {
            var entity = _rehabilitationDmnService.GetRehabChargeItemEntity(sfxmId, OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Xóa
        /// </summary>
        public IActionResult DeleteForm(string sfxmId)
        {
            _rehabChargeItemRepo.DeleteForm(sfxmId);
            return Success("Thao tác thành công.");
        }


    }
}