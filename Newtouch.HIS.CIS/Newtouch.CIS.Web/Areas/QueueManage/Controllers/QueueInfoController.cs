using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Attributes;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using Newtouch.Common.Web;

using Newtouch.Domain.IDomainServices.Queue;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Queue;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.QueueManage.Controllers
{
    [Area("QueueManage")]
    public class QueueInfoController : OrgControllerBase
    {
        private readonly IQueueDmnService _queueDmnService;

        public QueueInfoController(IQueueDmnService queueDmnService)
        {
            this._queueDmnService = queueDmnService;
        }
        // GET: QueueManage/QueueInfo
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult GetQueueInfo(Pagination pagination, string qdsj, string ks, string ys, string ywbz, string ywlx, int? calledstu, string keyword)
        {
            pagination = pagination ?? new Pagination();
            // FIX-2 B-CIS-06: qdsj rong/sai/ngoai khoang datetime cua SQL Server
            // (1753-01-01..9999-12-31) thi Convert.ToDateTime / truy van nem
            // SqlDateTime overflow. Tra loi validation thay vi exception SQL.
            if (!TryQueueRange(qdsj, out var kssj, out var jssj))
            {
                return Error("Vui lòng chọn ngày xếp hàng hợp lệ (từ 01/01/1753 đến 30/12/9999).");
            }
            var data = new
            {
                rows = _queueDmnService.GetQueue(pagination,OrganizeId, kssj, jssj, ks, ys, ywbz, ywlx, calledstu, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>Kiem chung ngay hang doi: parse duoc va nam trong khoang
        /// datetime SQL Server, tru hao 1 ngay cho jssj = kssj + 1 ngay.</summary>
        private static bool TryQueueRange(string qdsj, out DateTime kssj, out DateTime jssj)
        {
            kssj = default;
            jssj = default;
            if (string.IsNullOrWhiteSpace(qdsj)) return false;
            if (!DateTime.TryParse(qdsj, out var d)) return false;
            if (d < new DateTime(1753, 1, 1) || d > new DateTime(9999, 12, 30)) return false;
            kssj = d;
            jssj = d.AddDays(1).AddMilliseconds(-1);
            return true;
        }
        /// <summary>
        /// xếpđộiTrạng tháiSửa
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="calledstu"></param>
        /// <returns></returns>
        public IActionResult CallNumber(string mzh, string calledstu)
        {
            var reqObj = new
            {
                mzh = mzh,
                calledstu = calledstu,
                yhcode = this.UserIdentity.rygh,
                orgId = this.OrganizeId
            };
            var apiResp = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<SignInStateRequest>>(
                 "api/SignInAppointment/SignInState", reqObj);
            if (apiResp.msg != null) return Success("Gọi giao diện thất bại 【" + apiResp.msg + apiResp.sub_msg + "】");
            return Success();
        }

        public IActionResult getNextCall(string qdsj, string ks, string ys, string ywbz, string ywlx, int? calledstu, string keyword, string mzh) {
            // FIX-2 B-CIS-06: cung guard ngay nhu GetQueueInfo (tranh overflow).
            if (!TryQueueRange(qdsj, out var kssj, out var jssj))
            {
                return Error("Vui lòng chọn ngày xếp hàng hợp lệ (từ 01/01/1753 đến 30/12/9999).");
            }
            //var callData = _queueDmnService.GetQueueByMzh(mzh, OrganizeId);
            //var alldata = _inpatientOrderPackageRepo.IQueryable().Where(p => p.tcfw == tcfw && p.tclx == linqyzlx && p.OrganizeId == OrganizeId && p.zt == "1").OrderBy(a => a.CreateTime).ThenBy(a => a.tcmc).ToList();

            var allCallData = _queueDmnService.GetQueue(OrganizeId, kssj, jssj, ks, ys, ywbz, ywlx, calledstu, keyword).Where(a => a.calledstu == 2 && a.zt == "1").OrderByDescending(a => a.LastModifyTime);
            var callData = new QueueInfo();
            if (allCallData.Count() != 0)
            {
                callData = allCallData.FirstOrDefault();
            }
            var queueData = _queueDmnService.GetQueue(OrganizeId, kssj, jssj, ks, ys, ywbz, ywlx, calledstu, keyword).Where(a => a.calledstu == 1 && a.zt == "1").OrderBy(a => a.Period).ThenBy(a => a.queno);
            
            var nextData = new QueueInfo();
            if (queueData.Count() != 0) { 
                nextData = queueData.FirstOrDefault();
            }

            QueueVO vo = new QueueVO();
            //vo.sp_calling_no=callData.

                vo.sp_calling_no = callData.queno;
                vo.sp_calling_xm = callData.xm;
                vo.sp_calling_dept = callData.czksmc;
                vo.sp_calling_doc = callData.czysxm;
            vo.sp_calling_ywlx = callData.ywlx;
            vo.sp_nextcall_no = nextData.queno;
            vo.sp_nextcall_xm = nextData.xm;
            vo.sp_nextcall_dept = nextData.czksmc;
            vo.sp_nextcall_doc = nextData.czysxm;
            vo.sp_nextcall_ywlx = nextData.ywlx;
            vo.sp_waiting_count = queueData.Count();
            return Content(vo.ToJson());
        }
    }
}