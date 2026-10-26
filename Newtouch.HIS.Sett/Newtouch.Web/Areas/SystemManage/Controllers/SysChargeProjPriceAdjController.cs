using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysChargeProjPriceAdjController : ControllerBase
    {
        // GET: SystemManage/SysChargeProjPriceAdj
        private readonly ISysChargeProjPriceAdjApp _sysChargeProjPriceAdjApp;

        /// <summary>
        /// View
        /// </summary>
        /// <returns></returns>
        public ActionResult SysChargeProjPriceAdj()
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
            var data = _sysChargeProjPriceAdjApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// Thêm mới Điều chỉnh giá khoản mục không cho phép sửa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int? keyValue)
        {
            var entity = _sysChargeProjPriceAdjApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity)
        {
            _sysChargeProjPriceAdjApp.DeleteForm(sysChargeProjPriceAdjEntity);
            return Success("操作成功。");
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity, int? keyValue)
        {
            _sysChargeProjPriceAdjApp.SubmitForm(sysChargeProjPriceAdjEntity, keyValue);
            return Success("操作成功。");
        }
    }
}