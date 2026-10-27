using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("SystemManage")]

    public class MoneyUpperLimitReminderController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IMoneyUpperLimitReminderDmnService _moneyUpperLimitReminderDmnService;
        private readonly IMoneyUpperLimitReminderRepo _moneyUpperLimitReminderRepo;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        public MoneyUpperLimitReminderController(IMoneyUpperLimitReminderDmnService moneyUpperLimitReminderDmnService, IMoneyUpperLimitReminderRepo moneyUpperLimitReminderRepo, ISysDepartmentRepo sysDepartmentRepo, ISysUserDmnService sysUserDmnService)
        {
            _moneyUpperLimitReminderDmnService = moneyUpperLimitReminderDmnService;
            _moneyUpperLimitReminderRepo = moneyUpperLimitReminderRepo;
            _sysDepartmentRepo = sysDepartmentRepo;
            _sysUserDmnService = sysUserDmnService;
        }

        // GET: SystemManage/MoneyUpperLimitReminder
        public override IActionResult Index()
        {
            return View();
        }

        public override IActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// Tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="reminderType"></param>
        /// <returns></returns>
        public IActionResult GetAllList(Pagination pagination, string keyword, string reminderType)
        {
            var list = new
            {
                rows = _moneyUpperLimitReminderDmnService.GetAllList(pagination, this.OrganizeId, keyword, reminderType),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sxtxId"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(MoneyUpperLimitReminderEntity entity, string sxtxId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;            
            _moneyUpperLimitReminderRepo.SubmitForm(entity, this.UserIdentity.UserCode, sxtxId);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Thêm mớiorSửa
        /// </summary>
        /// <param name="sxtxId"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string sxtxId)
        {
            Pagination pagination = new Pagination();
            pagination.sidx = "CreateTime desc";
            pagination.rows = 1;
            pagination.page = 1;
            var entity = _moneyUpperLimitReminderDmnService.GetAllList(pagination, this.OrganizeId, null, null, sxtxId).FirstOrDefault();
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="sxtxId"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string sxtxId)
        {
            _moneyUpperLimitReminderRepo.DeleteForm(this.OrganizeId, sxtxId);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Lớp nổi khoa
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SelectDepartmentList(string keyword)
        {
            var list = _sysDepartmentRepo.GetList(this.OrganizeId, "1", keyword);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lớp nổi bác sĩ
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SelectTherapistList(string keyword, string ks)
         {
            var list = _sysUserDmnService.GetStaffByDutyCode(this.OrganizeId, "RehabDoctor");
            if (!string.IsNullOrEmpty(keyword))
            {
                list = list.Where(a => a.ks == ks && ( a.StaffName.Contains(keyword.Trim()) || a.StaffGh.Contains(keyword.Trim()))).ToList();
            }
            return Content(list.ToJson());
        }
    }
}
