using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices.Rehabilitation;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.Controllers
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
        public override ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Thêm mới/Sửa đổi
        /// </summary>
        /// <returns></returns>
        public override ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// Danh sách thả xuống phân loại thu phí
        /// </summary>
        /// <returns></returns>
        public ActionResult GetsfflBindSelect()
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
        public ActionResult SubmitForm(RehabChargeItemEntity entity, string sfxmId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _rehabChargeItemRepo.SubmitForm(entity, sfxmId);
            return Success("操作成功。");
        }


        /// <summary>
        /// Lấy danh sách có hiệu lực
        /// </summary>
        public ActionResult GetRehabChargeItemList(string keyword)
        {
            var data = _rehabilitationDmnService.GetRehabChargeItemList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửa đổi Form
        /// </summary>
        public ActionResult GetFormJson(string sfxmId)
        {
            var entity = _rehabilitationDmnService.GetRehabChargeItemEntity(sfxmId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Xóa
        /// </summary>
        public ActionResult DeleteForm(string sfxmId)
        {
            _rehabChargeItemRepo.DeleteForm(sfxmId);
            return Success("操作成功。");
        }


    }
}