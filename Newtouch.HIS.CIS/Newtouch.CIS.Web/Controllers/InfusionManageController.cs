using System;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.CIS.Compat;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.CIS.Repository;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Application.Interface;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ViewModels;
using Newtouch.Domain.ViewModels.Outpatient;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// truyền dịchQuản lý chuyểnđộngđầu
    /// </summary>
    public class InfusionManageController : ControllerBase
    {
        private readonly IMzsyypxxRepo _mzsyypxxRepo;
        private readonly IInfusionApp _infusionApp;

        public InfusionManageController(IMzsyypxxRepo mzsyypxxRepo, IInfusionApp infusionApp)
        {
            _mzsyypxxRepo = mzsyypxxRepo;
            _infusionApp = infusionApp;
        }

        /// <summary>
        /// truyền dịchthông tinTra cứu
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Query(string tags, string organizeId)
        {
            // FIX-2 B-CIS-05: view luon bind ViewBag.drugs / ViewBag.patInfo
            // (dynamic). Truoc day khi tags rong thi controller tra View() ma
            // khong dat 2 gia tri nay -> "Cannot perform runtime binding on a
            // null reference"; tags sai dinh dang thi Convert.ToInt64 nem
            // FormatException. Bay gio: mac dinh luon duoc dat, tags sai bi loai
            // bo bang TryParse, khong co thuoc thi view trong + thong bao.
            ViewBag.drugs = new List<MzsyypxxVO>();
            ViewBag.patInfo = new InfusionExecVO();
            if (string.IsNullOrWhiteSpace(tags))
            {
                ViewBag.InfusionNotice = "Chưa nhập mã phiếu truyền dịch.";
                return View();
            }
            var ids = tags.Split(',');
            if (ids.Length == 0)
            {
                ViewBag.InfusionNotice = "Chưa nhập mã phiếu truyền dịch.";
                return View();
            }
            var tmpids = new List<long>();
            foreach (var item in ids)
            {
                if (long.TryParse(item, out var v) && v != 0) tmpids.Add(v);
            }
            if (tmpids.Count == 0)
            {
                ViewBag.InfusionNotice = "Mã phiếu truyền dịch không hợp lệ.";
                return View();
            }
            var drugs = _infusionApp.MzsyxxQuery(tmpids, string.IsNullOrWhiteSpace(organizeId) ? OrganizeId : organizeId);
            ViewBag.drugs = drugs ?? new List<MzsyypxxVO>();
            if (drugs != null && drugs.Count > 0)
            {
                ViewBag.patInfo.kh = drugs[0].kh;
                ViewBag.patInfo.patientName = drugs[0].xm;
                ViewBag.patInfo.seatNum = drugs[0].seatNum;
                ViewBag.patInfo.remark = drugs[0].remark;
                ViewBag.patInfo.dispenser = drugs[0].dispenser;
                ViewBag.patInfo.dispenserName = drugs[0].dispenserName;
                ViewBag.patInfo.executor = drugs[0].executor;
                ViewBag.patInfo.executorName = drugs[0].executorName;
                ViewBag.patInfo.sykssj = drugs[0].sykssj;
                ViewBag.patInfo.syjssj = drugs[0].syjssj;
            }
            else
            {
                ViewBag.InfusionNotice = "Không tìm thấy phiếu truyền dịch.";
            }
            return View();
        }
    }
}