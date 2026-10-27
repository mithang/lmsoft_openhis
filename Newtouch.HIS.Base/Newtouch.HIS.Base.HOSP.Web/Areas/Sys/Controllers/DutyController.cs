using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Areas.Sys.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("Sys")]
    public class DutyController : ControllerBase
    {
        private readonly ISysDutyRepo _sysDutyRepo;

        public DutyController(ISysDutyRepo sysDutyRepo)
        {
            this._sysDutyRepo = sysDutyRepo;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(string keyword)
        {
            var data = _sysDutyRepo.GetList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _sysDutyRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysDutyEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysDutyRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

    }
}