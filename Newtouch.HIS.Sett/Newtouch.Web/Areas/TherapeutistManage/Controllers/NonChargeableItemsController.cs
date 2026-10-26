using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage;
using Newtouch.Tools;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Web.Areas.TherapeutistManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class NonChargeableItemsController : ControllerBase
    {
        private readonly ITherapeutistCompleteDmnService _therapeutistCompleteDmnService;

        #region 页面初始化
        public ActionResult UpgradeWorkingTime()
        {
            return View();
        }

        public ActionResult UpdateView()
        {
            return View();
        }

        #endregion

        #region 时长调整

        /// <summary>
        /// Tra cứu được tổng thời lượng
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ysgh"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public ActionResult GetStaffHourList(Pagination pagination, string ysgh, string year, string month)
        {
            var data = _therapeutistCompleteDmnService.GetStaffEachMonthHour(pagination, OrganizeId, ysgh, year, month);
            var list = new
            {
                rows = data,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lấy thời lượng chi tiết từng tháng của mỗi kỹ thuật viên trị liệu
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(AdjustStaffHourVO vo)
        {
            var data = _therapeutistCompleteDmnService.GetStaffWorkedHour(OrganizeId, vo.ysgh, vo.syear.ToString(), vo.smonth.ToString());
            return Content(data.ToJson());
        }

        /// <summary>
        /// Điều chỉnh thời lượng，Cập nhật vào cơ sở dữ liệu
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ActionResult submitUpdateTime(Dictionary<string, List<submitTimeVO>> vo)
        {
            _therapeutistCompleteDmnService.UpdateTime(vo);
            return Success("操作成功。");
        }

        #endregion
    }
}