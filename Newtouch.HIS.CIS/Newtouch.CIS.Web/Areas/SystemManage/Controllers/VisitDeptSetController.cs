using System;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Application.Interface;
using Newtouch.Core.Common;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ViewModels;
using Newtouch.Tools;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// Quản lý khám bệnh
    /// </summary>
    public class VisitDeptSetController : OrgControllerBase
    {
        private readonly IVisitDeptSetDmnService _visitDeptSetDmnService;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IVisitDeptSetRepo _visitDeptSetRepo;
        private readonly IVisitDeptSetApp _visitDeptSetApp;

        /// <summary>
        /// Lấy thông tin bác sĩ khám bệnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            keyword = string.IsNullOrWhiteSpace(keyword) ? "Doctor" : keyword;
            var rows = _sysUserDmnService.GetStaffByDutyCode(OrganizeId, keyword);
            var total = Math.Ceiling(Convert.ToDecimal(rows.Count) / pagination.rows);
            if (rows.Count > 0)
                rows = rows.Skip((pagination.page - 1) * pagination.rows).Take(pagination.rows).ToList();
            var list = new
            {
                rows = rows,
                total = total,
                page = pagination.page,
                records = rows.Count//pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lấy chi tiết khám bệnh
        /// </summary>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        public ActionResult GetVisitDeptDetail(string ysgh)
        {
            var result = _visitDeptSetDmnService.SelectVisitDeptSetDetail(ysgh, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Xóa hạng mục
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult DeleteItem(string keyword)
        {
            return _visitDeptSetRepo.DeleteItem(keyword) > 0 ? Success() : Error("");
        }

        /// <summary>
        /// TheoIDLấy thông tin khoa khám bệnh
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string ysgh, string keyValue)
        {
            var result = _visitDeptSetDmnService.SelectDoctorInfo(ysgh, OrganizeId, keyValue);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Gửi biểu mẫu
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(VisitDeptDetail vo)
        {
            vo.zt = vo.zt == "true" ? "1" : "0";
            var result = _visitDeptSetApp.SubmitForm(vo, UserIdentity.UserCode, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }
    }
}