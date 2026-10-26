using Newtouch.HIS.Application;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiChargeRangeController : ControllerBase
    {
        // GET: SystemManage/SysPatiChargeRange
        private readonly ISysPatiChargeRangeApp _sysPatiChargeRangeApp;
        private readonly ISysMedicinApp _sysMedicinApp;

        /// <summary>
        /// View
        /// </summary>
        /// <returns></returns>
        public ActionResult SysPatiChargeRange()
        {
            return View();
        }
        /// <summary>
        /// Lấy danh sách hợp lệ
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string keyword)
        {
            var data = _sysPatiChargeRangeApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// Thêm mới hoặc sửa Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysPatiChargeRangeApp.GetEffectiveList(null, keyValue).FirstOrDefault();
            return Content(entity.ToJson());
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(int keyValue)
        {
            _sysPatiChargeRangeApp.DeleteForm(keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysPatientChargeRangeEntity sysPatiChargeRangeEntity, int? keyValue)
        {
            sysPatiChargeRangeEntity.zt = sysPatiChargeRangeEntity.zt == "true" ? "1" : "0";
            _sysPatiChargeRangeApp.SubmitForm(sysPatiChargeRangeEntity, keyValue );
            return Success("操作成功。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keywrod"></param>
        /// <returns></returns>
        public ActionResult GetYpSelectJsonData(string keywrod)
         {
            var list = _sysMedicinApp.GetYpList(keywrod);
            return Content(list.ToJson());
        }

    }
}