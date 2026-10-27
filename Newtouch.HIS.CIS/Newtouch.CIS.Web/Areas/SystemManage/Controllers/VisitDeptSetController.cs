using System;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Domain.IDomainServices;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.Application.Interface;

using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ViewModels;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 出诊管理
    /// </summary>
    [Area("SystemManage")]
    public class VisitDeptSetController : OrgControllerBase
    {
        private readonly IVisitDeptSetDmnService _visitDeptSetDmnService;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IVisitDeptSetRepo _visitDeptSetRepo;
        private readonly IVisitDeptSetApp _visitDeptSetApp;

        public VisitDeptSetController(IVisitDeptSetDmnService visitDeptSetDmnService, ISysUserDmnService sysUserDmnService, IVisitDeptSetRepo visitDeptSetRepo, IVisitDeptSetApp visitDeptSetApp)
        {
            this._visitDeptSetDmnService = visitDeptSetDmnService;
            this._sysUserDmnService = sysUserDmnService;
            this._visitDeptSetRepo = visitDeptSetRepo;
            this._visitDeptSetApp = visitDeptSetApp;
        }

        /// <summary>
        /// 获取Bác sĩ khám信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(Pagination pagination, string keyword)
        {
            keyword = string.IsNullOrWhiteSpace(keyword) ? "Doctor" : keyword;
            var rows = _sysUserDmnService.GetStaffByDutyCode(OrganizeId, keyword);
            var total = Math.Ceiling(Convert.ToDecimal(rows.Count) / (pagination.rows ?? 20));
            if (rows.Count > 0)
                rows = rows.Skip(((pagination.page ?? 1) - 1) * (pagination.rows ?? 20)).Take(pagination.rows ?? 20).ToList();
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
        /// 获取出诊明细
        /// </summary>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        public IActionResult GetVisitDeptDetail(string ysgh)
        {
            var result = _visitDeptSetDmnService.SelectVisitDeptSetDetail(ysgh, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult DeleteItem(string keyword)
        {
            return _visitDeptSetRepo.DeleteItem(keyword) > 0 ? Success() : Error("");
        }

        /// <summary>
        /// 根据ID获取Khoa khám信息
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string ysgh, string keyValue)
        {
            var result = _visitDeptSetDmnService.SelectDoctorInfo(ysgh, OrganizeId, keyValue);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(VisitDeptDetail vo)
        {
            vo.zt = vo.zt == "true" ? "1" : "0";
            var result = _visitDeptSetApp.SubmitForm(vo, UserIdentity.UserCode, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }
    }
}