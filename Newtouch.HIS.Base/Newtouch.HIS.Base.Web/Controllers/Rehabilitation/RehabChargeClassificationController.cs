using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.Web.Controllers
{
    public class RehabChargeClassificationController : ControllerBase
    {
        private readonly IRehabChargeClassificationRepo _rehabChargeClassificationRepo;
        public RehabChargeClassificationController(IRehabChargeClassificationRepo rehabChargeClassificationRepo)
        {
            this._rehabChargeClassificationRepo = rehabChargeClassificationRepo;
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
        public IActionResult SubmitForm(RehabChargeClassificationEntity entity, string sfflId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _rehabChargeClassificationRepo.SubmitForm(entity, sfflId);
            return Success("Thao tác thành công.");
        }


        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IActionResult GetRehabChargeClassificationList(string keyword)
        {
            var data = _rehabChargeClassificationRepo.GetRehabChargeClassificationList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string sfflId)
        {
            var entity = _rehabChargeClassificationRepo.GetRehabChargeClassificationEntity(sfflId, OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string sfflId)
        {
            _rehabChargeClassificationRepo.DeleteForm(sfflId);
            return Success("Thao tác thành công.");
        }


    }
}