using Newtouch.Domain.IDomainServices;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.CIS.Repository;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtouch.CIS.Compat;
using Newtouch.Application.Interface;
using System.Collections.Generic;
using System;
using Newtouch.Domain.ViewModels;


namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 预约管理
    /// </summary>
    [Area("SystemManage")]
    public class BespeakRegisterManageController : OrgControllerBase
    {

        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysBespeakRegisterDmnService _sysBespeakRegisterDmnService;
        private readonly ISysBespeakRegisterRepo _sysBespeakRegisterRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IBespeakRegisterApp _bespeakRegisterApp;
        private readonly IMzyyghDmnService _mzyyghDmnService;
        private readonly IMzyyghRepo _mzyyghRepo;

        public BespeakRegisterManageController(ISysConfigRepo sysConfigRepo, ISysBespeakRegisterDmnService sysBespeakRegisterDmnService, ISysBespeakRegisterRepo sysBespeakRegisterRepo, ISysUserDmnService sysUserDmnService, IBespeakRegisterApp bespeakRegisterApp, IMzyyghDmnService mzyyghDmnService, IMzyyghRepo mzyyghRepo)
        {
            this._sysConfigRepo = sysConfigRepo;
            this._sysBespeakRegisterDmnService = sysBespeakRegisterDmnService;
            this._sysBespeakRegisterRepo = sysBespeakRegisterRepo;
            this._sysUserDmnService = sysUserDmnService;
            this._bespeakRegisterApp = bespeakRegisterApp;
            this._mzyyghDmnService = mzyyghDmnService;
            this._mzyyghRepo = mzyyghRepo;
        }

        /// <summary>
        /// 预约挂号管理
        /// </summary>
        /// <returns></returns>
        public IActionResult BespeakRegister()
        {
            ViewBag.OrganizeId = OrganizeId;
            ViewBag.defaultBespeakRegInfo = _bespeakRegisterApp.GetRegisterDateByWeek(DateTime.Now);
            return View();
        }

        /// <summary>
        /// 获取主列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _sysBespeakRegisterDmnService.SelectSysBespeakregister(pagination, keyword, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取挂号Xếp lịch
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="blh"></param>
        /// <returns></returns>
        public IActionResult GetArrangedRegistersGridJson(Pagination pagination, string blh)
        {
            if (string.IsNullOrWhiteSpace(blh)) return Content("");
            var data = new
            {
                rows = _mzyyghDmnService.SelectMzyyghDetail(pagination, blh, OrganizeId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取挂号Xếp lịch日期
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public IActionResult GetArrangedRegDateGridJson(string deptCode)
        {
            var tmpDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            var regData = _sysBespeakRegisterRepo.IQueryable(p => p.departmentCode == deptCode && p.OrganizeId == OrganizeId && p.regDate >= tmpDate);
            if (regData == null || !regData.Any()) return Content(null);
            {
                var result = new List<object>();
                regData.Select(p => p.regDate).Distinct().OrderBy(q => q).ToList().ForEach(p =>
                {
                    result.Add(new { regDate = p.ToString("yyyy-MM-dd") });
                });
                return Content(result.ToJson());
            }
        }

        /// <summary>
        /// 获取Khung giờ
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        public IActionResult GetRegTimes(string deptCode, string regDate, string gh)
        {
            if (string.IsNullOrWhiteSpace(deptCode)) return Content(null);
            if (string.IsNullOrWhiteSpace(regDate)) return Content(null);
            var tmpRegDate = Convert.ToDateTime(regDate);
            var regData = _sysBespeakRegisterRepo.GetRegTime(deptCode, tmpRegDate, gh ?? "", OrganizeId);
            return Content(regData.ToJson());
        }

        /// <summary>
        /// 修改信息时，把信息带到新页面
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        public IActionResult GetDoctorsByDept(string deptCode, DateTime? regDate)
        {
            var result = regDate == null ? _sysBespeakRegisterDmnService.SelectSysBespeakregister(deptCode, OrganizeId) : _sysBespeakRegisterDmnService.SelectSysBespeakregister(deptCode, (DateTime)regDate, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(List<SysBespeakRegisterEntity> regData)
        {
            var result = _bespeakRegisterApp.SubmitForm(regData);
            return string.IsNullOrWhiteSpace(result) ? Success("Thao tác thành công.") : Error(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            var entity = _sysBespeakRegisterRepo.FindEntity(p => p.Id == keyValue);
            if (entity != null)
            {
                return _sysBespeakRegisterRepo.Delete(entity) > 0 ? Success("Xóa thành công.") : Error("Xóathất bại");
            }
            return Error("Vui lòng chọn thông tin cần xóa.");
        }

        /// <summary>
        /// 获取专家
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public IActionResult GetStaff(string deptCode)
        {
            if (string.IsNullOrWhiteSpace(deptCode)) return Content("");
            var data = _sysUserDmnService.GetStaffListByOrg(OrganizeId);
            if (data == null || data.Count <= 0) return Content("");
            var result = data.Where(p => p.DepartmentCode == deptCode);
            return Content(result.ToList().ToJson());
        }

        /// <summary>
        /// 获取多个Guid
        /// </summary>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public IActionResult GetGuid(int? rowCount)
        {
            var result = new List<string>();
            if (rowCount == null) return Content("");
            for (var i = 0; i <= rowCount; i++)
            {
                result.Add(Guid.NewGuid().ToString());
            }
            return Content(result.ToJson());
        }

        /// <summary>
        /// 获取追加行HTML
        /// </summary>
        /// <param name="lastRegDate"></param>
        /// <param name="regTime"></param>
        /// <returns></returns>
        public string GetNewRowHtml(string lastRegDate, string regTime)
        {
            if (string.IsNullOrWhiteSpace(lastRegDate)) return "";
            var lRegDate = Convert.ToDateTime(lastRegDate);
            var regTimeData = regTime.ToObject<List<SysBespeakRegisterTimeVO>>();
            if (regTimeData == null || regTimeData.Count <= 0) return "";
            var sRegDate = lRegDate.AddDays(1);
            var lWeekDate = _bespeakRegisterApp.GetRegisterDateByWeek(sRegDate);
            var result = _bespeakRegisterApp.BuildNewRow(lWeekDate, regTimeData);
            return result;
        }

        /// <summary>
        /// 获取科室预约挂号信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public string GetRegDataHtmlByKs(string deptCode)
        {
            _bespeakRegisterApp.DeleteExpireRegData();
            var result = _bespeakRegisterApp.GetRegDataHtmlByKs(deptCode);
            return string.IsNullOrWhiteSpace(result) ? GetDefaultHtmlByKs(deptCode) : result;
        }

        /// <summary>
        /// 获取科室默认预约挂号
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public string GetDefaultHtmlByKs(string deptCode)
        {
            return _bespeakRegisterApp.GetDefaultHtmlByKs(deptCode);
        }

        /// <summary>
        /// 根据科室和工号组装预约挂号信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public string GetRegDataHtmlByKsAndGh(string deptCode, string gh)
        {
            _bespeakRegisterApp.DeleteExpireRegData();
            var result = _bespeakRegisterApp.GetRegDataHtmlByKsAndGh(deptCode, gh);
            return string.IsNullOrWhiteSpace(result) ? GetDefaultHtmlByKs(deptCode) : result;
        }

        /// <summary>
        /// 获取可预约总人数和已预约数
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public IActionResult GetBespeakMaxCount(string deptCode, DateTime regDate, string regTime, string gh)
        {
            return string.IsNullOrWhiteSpace(deptCode) ? Content(null) : Content(regDate <= Convert.ToDateTime("1970-01-01") ? null : _bespeakRegisterApp.GetBespeakMaxCount(deptCode, regDate, regTime, gh).ToJson());
        }

        /// <summary>
        /// 保存预约
        /// </summary>
        /// <param name="yyId"></param>
        /// <param name="brId"></param>
        /// <param name="blh"></param>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <returns></returns>
        public IActionResult SaveYy(string brId, string blh, int? zjlx, string zjh)
        {
            var result = _bespeakRegisterApp.SaveYY("", brId, blh, zjlx, zjh);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// 取消预约挂号
        /// </summary>
        /// <param name="yyId"></param>
        /// <returns></returns>
        public IActionResult CancelYy(string yyId)
        {
            return _mzyyghRepo.DeleteById(yyId) > 0 ? Success() : Error("Xóathất bại");
        }

        /// <summary>
        /// 获取科室或医生预约Xếp lịch
        /// </summary>
        /// <param name="year">预约年</param>
        /// <param name="month">预约月</param>
        /// <param name="deptCode">挂号科室</param>
        /// <param name="ysgh">专家工号</param>
        /// <returns></returns>
        public IActionResult GetAllBespeakRegisterByMonth(int year, int month, string deptCode, string ysgh)
        {
            if (month <= 0) throw new Exception("Tháng truyền vào không hợp lệ, vui lòng truyền số tự nhiên");
            var mzlx = string.IsNullOrWhiteSpace(ysgh) ? "1" : "3";//门诊住院标志 1：普通门诊  2：急诊   3：专家门诊
            var beginTime = Convert.ToDateTime(year + "-" + month + "-01");
            var endTime = beginTime.AddMonths(1).AddDays(-1);
            var yyghDetail = _mzyyghDmnService.SelectMzyyghDetail(mzlx, deptCode, ysgh, beginTime, endTime, OrganizeId);
            return Content(yyghDetail.ToJson());
        }

        /// <summary>
        /// 生成预约Xếp lịch日历
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="deptCode"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        public string BuildClendar(int year, int month, string deptCode, string ysgh)
        {
            return _bespeakRegisterApp.BuildCalendar(year, month, deptCode, ysgh);
        }

    }
}