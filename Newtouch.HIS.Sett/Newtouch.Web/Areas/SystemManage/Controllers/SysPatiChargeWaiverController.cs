using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiChargeWaiverController : ControllerBase
    {
        private readonly ISysPatiChargeWaiverApp _sysPatiChargeWaiverApp;

        /// <summary>
        /// Chế độ xem
        /// </summary>
        /// <returns></returns>
        public ActionResult SysPatiChargeWaiver()
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
            var data = _sysPatiChargeWaiverApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysPatiChargeWaiverApp.GetEffectiveList(null, keyValue).FirstOrDefault();
            return Content(entity.ToJson());
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(int keyValue)
        {
            _sysPatiChargeWaiverApp.DeleteForm(keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysPatientChargeWaiverEntity sysPatiChargeWaiverEntity, int? keyValue)
        {
            sysPatiChargeWaiverEntity.zt = sysPatiChargeWaiverEntity.zt == "true" ? "1" : "0";
            _sysPatiChargeWaiverApp.SubmitForm(sysPatiChargeWaiverEntity, keyValue);
            return Success("操作成功。");
        }

    }
}
