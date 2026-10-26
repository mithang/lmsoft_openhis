using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class SysMedicalOrderFrequencyController : ControllerBase
    {
        private readonly ISysMedicalOrderFrequencyRepo _sysMedicalOrderFrequencyRepo;

        public SysMedicalOrderFrequencyController(ISysMedicalOrderFrequencyRepo sysMedicalOrderFrequencyRepo)
        {
            this._sysMedicalOrderFrequencyRepo = sysMedicalOrderFrequencyRepo;
        }
        // GET: SysMedicalOrderFrequency
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
        /// <returns></returns>
        public ActionResult SubmitForm(SysMedicalOrderFrequencyEntity entity, int? yzpcId)
        {
            entity.zbz = entity.zbz ?? "";  //Vấn đề loại dữ liệu
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysMedicalOrderFrequencyRepo.SubmitForm(entity, yzpcId);
            return Success("操作成功。");
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        public ActionResult GetGridJson(string keyword,string orgId)
        {
            var data = _sysMedicalOrderFrequencyRepo.GetOrderFrequencyList(orgId,keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        public ActionResult GetFormJson(int? yzpcId,string orgId)
        {
            var entity = _sysMedicalOrderFrequencyRepo.GetOrderFrequencyEntity(yzpcId,orgId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Xóa
        /// </summary>
        public ActionResult DeleteForm(int yzpcId,string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            _sysMedicalOrderFrequencyRepo.DeleteForm(orgId,yzpcId);
            return Success("操作成功。");
        }


    }
}