using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysGlobalConfigController : ControllerBase
    {
        private readonly ISysGlobalConfigRepo _sysGlobalConfigRepo;

        /// <summary>
        /// 
        /// </summary>
        public SysGlobalConfigController(ISysGlobalConfigRepo sysGlobalConfigRepo)
        {
            this._sysGlobalConfigRepo = sysGlobalConfigRepo;
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string keyword)
        {
            var data = _sysGlobalConfigRepo.GetList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửa đổi Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _sysGlobalConfigRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysGlobalConfigEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysGlobalConfigRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

    }
}