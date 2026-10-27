using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysNationController : ControllerBase
    {
        private readonly ISysNationRepo _sysNationRepo;

        /// <summary>
        /// 
        /// </summary>
        public SysNationController(ISysNationRepo sysNationRepo)
        {
            this._sysNationRepo = sysNationRepo;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetGridJson(string keyword)
        {
            var data = _sysNationRepo.GetList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int keyValue)
        {
            var entity = _sysNationRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysNationEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysNationRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

    }
}