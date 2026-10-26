using Newtouch.HIS.Application;
using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class SysMRChargeClassController : ControllerBase
    {
        // GET: SystemManage/SysMediRecChargeCateg
        private ISysMRChargeClassApp _sysMRChargeClassApp;

        public SysMRChargeClassController(ISysMRChargeClassApp sysMRChargeClassApp)
        {
            this._sysMRChargeClassApp = sysMRChargeClassApp;
        }
        /// <summary>
        /// View
        /// </summary>
        /// <returns></returns>
        public ActionResult SysMRChargeClass()
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
            var data = _sysMRChargeClassApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// Thêm mới hoặc sửa Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {

            var entity = _sysMRChargeClassApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(SysMedicalRecordChargeCategoryEntity sysMRChargeClassEntity)
        {
            _sysMRChargeClassApp.DeleteForm(sysMRChargeClassEntity);
            return Success("操作成功。");
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="sysMRChargeClassEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysMedicalRecordChargeCategoryEntity sysMRChargeClassEntity, int? keyValue)
        {
            sysMRChargeClassEntity.zt = sysMRChargeClassEntity.zt == "true" ? "1" : "0";
            _sysMRChargeClassApp.SubmitForm(sysMRChargeClassEntity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// Lấy pinyin tiếng Trung
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public ActionResult GetSpell(string strText)
        {
            var py = StringUtil.GetChineseSpell(strText);
            return Content(py);
        }
    }
}