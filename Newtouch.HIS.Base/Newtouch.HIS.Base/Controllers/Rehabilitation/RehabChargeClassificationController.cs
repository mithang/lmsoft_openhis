using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.Controllers
{
    public class RehabChargeClassificationController : ControllerBase
    {
        private readonly IRehabChargeClassificationRepo _rehabChargeClassificationRepo;
        public RehabChargeClassificationController(IRehabChargeClassificationRepo rehabChargeClassificationRepo)
        {
            this._rehabChargeClassificationRepo = rehabChargeClassificationRepo;
        }
        public override ActionResult Index()
        {
            return View();
        }

        public override ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(RehabChargeClassificationEntity entity, string sfflId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _rehabChargeClassificationRepo.SubmitForm(entity, sfflId);
            return Success("操作成功。");
        }


        /// <summary>
        /// Lấy danh sách có hiệu lực
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult GetRehabChargeClassificationList(string keyword)
        {
            var data = _rehabChargeClassificationRepo.GetRehabChargeClassificationList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửa đổi Form
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string sfflId)
        {
            var entity = _rehabChargeClassificationRepo.GetRehabChargeClassificationEntity(sfflId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string sfflId)
        {
            _rehabChargeClassificationRepo.DeleteForm(sfflId);
            return Success("操作成功。");
        }


    }
}