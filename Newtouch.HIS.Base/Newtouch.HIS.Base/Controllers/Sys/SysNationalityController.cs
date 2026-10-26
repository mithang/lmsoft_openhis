using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.Controllers
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
        public ActionResult GetGridJson(string keyword)
        {
            var data = _sysNationalityRepo.GetList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửa đổi Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysNationalityRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysNationalityEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysNationalityRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

    }
}