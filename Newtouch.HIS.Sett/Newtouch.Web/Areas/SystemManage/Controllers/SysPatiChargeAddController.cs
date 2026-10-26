using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.HIS.Domain.IDomainServices;
using System.Linq;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiChargeAddController : ControllerBase
    {
        private readonly ISysPatiChargeAddApp _sysPatiChargeAddApp;
        private readonly ISysChargeAddCategApp _xt_SysChargeAddCategApp;
        private readonly ISysPatiChargeAddDmnService _sysPatiChargeAddDmnService;

        /// <summary>
        /// Chế độ xem
        /// </summary>
        /// <returns></returns>
        public ActionResult SysPatiChargeAdd()
        {
            return View();
        }
        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string keyword)
        {
            var data = _sysPatiChargeAddApp.GetSysPatiChargeAddVoList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 附加显示大类
        /// </summary>
        /// <returns></returns>
        public ActionResult GetfjsfdlList(string keyValue)
        {
            var data = _sysPatiChargeAddApp.GetfjsfdlList(keyValue);
            return Json(data);
        }
        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysPatiChargeAddApp.GetSysPatiChargeAddVoList(null, keyValue).FirstOrDefault();
            return Content(entity.ToJson());
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(SysPatientChargeAdditionalEntity SysPatiChargeAddEntity)
        {
            _sysPatiChargeAddApp.DeleteForm(SysPatiChargeAddEntity);
            return Success("操作成功。");
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysPatientChargeAdditionalEntity SysPatiChargeAddEntity, int? keyValue)
        {
            SysPatiChargeAddEntity.zt = SysPatiChargeAddEntity.zt == "true" ? "1" : "0";
            _sysPatiChargeAddApp.SubmitForm(SysPatiChargeAddEntity, keyValue);
            return Success("操作成功。");
        }

    }
}
