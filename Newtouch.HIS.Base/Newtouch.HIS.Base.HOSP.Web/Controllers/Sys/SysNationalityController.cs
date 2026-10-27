using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysNationalityController : ControllerBase
    {
        private readonly ISysNationalityRepo _sysNationalityRepo;

        /// <summary>
        /// 
        /// </summary>
        public SysNationalityController(ISysNationalityRepo sysNationalityRepo)
        {
            this._sysNationalityRepo = sysNationalityRepo;
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetGridJson(string keyword)
        {
            var data = _sysNationalityRepo.GetList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int keyValue)
        {
            var entity = _sysNationalityRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysNationalityEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysNationalityRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

    }
}