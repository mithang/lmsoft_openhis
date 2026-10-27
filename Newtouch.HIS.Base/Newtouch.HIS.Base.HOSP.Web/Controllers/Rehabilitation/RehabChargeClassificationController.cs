using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    public class RehabChargeClassificationController : ControllerBase
    {
        private readonly IRehabChargeClassificationRepo _rehabChargeClassificationRepo;
        public RehabChargeClassificationController(IRehabChargeClassificationRepo rehabChargeClassificationRepo)
        {
            this._rehabChargeClassificationRepo = rehabChargeClassificationRepo;
        }

        public override IActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IActionResult GetRehabChargeClassificationList(string keyword, string OrganizeId)
        {
            var data = _rehabChargeClassificationRepo.GetRehabChargeClassificationList(OrganizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(RehabChargeClassificationEntity entity, string sfflId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _rehabChargeClassificationRepo.SubmitForm(entity, sfflId);
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string sfflId, string OrganizeId)
        {
            var entity = _rehabChargeClassificationRepo.GetRehabChargeClassificationEntity(sfflId,OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string sfflId, string OrganizeId)
        {
            _rehabChargeClassificationRepo.DeleteForm(sfflId,OrganizeId);
            return Success("Thao tác thành công.");
        }



    }
}