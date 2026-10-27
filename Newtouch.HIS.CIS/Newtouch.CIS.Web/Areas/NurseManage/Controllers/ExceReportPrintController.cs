using Newtouch.Domain.IRepository;
using Newtouch.CIS.Web.Models;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;


using Newtouch.CIS.Compat;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    [Area("NurseManage")]
    public class ExceReportPrintController : OrgControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IExceReportPrintDmnService _IExceReportPrintDmnService;

        public ExceReportPrintController(ISysConfigRepo sysConfigRepo, IExceReportPrintDmnService iExceReportPrintDmnService)
        {
            this._sysConfigRepo = sysConfigRepo;
            this._IExceReportPrintDmnService = iExceReportPrintDmnService;
        }
        /// <summary>
        /// LấyKhu bệnh ngườiviênThực hiệncây
        /// </summary>
        /// <returns></returns>
        public IActionResult GetPatWardTree(string zyzt, string keyword)
        {
            
            var wardTree = _IExceReportPrintDmnService.GetPatTree(OrganizeId,zyzt,keyword);
            var wardonly = wardTree.GroupBy(p => new { p.bqCode, p.bqmc }).Select(p => new { p.Key.bqCode, p.Key.bqmc });


            var treeList = new List<TreeViewModel>();
            foreach (var item in wardonly)
            {
                var patInfo = wardTree.Where(p => p.bqCode == item.bqCode).Where(p => p.zyh != "").Where(p => p.zyh != null).ToList();
                var NewPatInfo = patInfo.OrderBy(p => p.BedNo);
                foreach (InpWardPatTreeVO itempat in NewPatInfo)
                { 
                    string gender = itempat.sex == "1" ? "Nam" : "Nữ";
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = itempat.zyh;
                    //Số giường + Họ tên(Nhập việnSố ngày)+Số nhập viện + Tuổi +Giới tính+Tính chất bệnh nhân
                    treepat.text = itempat.BedNo + "-" + itempat.hzxm + "(" + itempat.inHosDays + "ngày)" + "-" + itempat.zyh + "-" + itempat.nl + "tuổi-" + gender + "-" + itempat.brxzmc;
                    treepat.value = itempat.zyh;
                    treepat.parentId = item.bqCode;
                    treepat.isexpand = false;
                    treepat.complete = true;
                    treepat.showcheck = true;
                    treepat.checkstate = 0;
                    treepat.hasChildren = false;
                    treepat.Ex1 = "c";
                    treepat.Ex2 = itempat.sex;
                    treepat.Ex3 = itempat.nl;
                    treepat.Ex4 = itempat.hzxm;
                    treepat.Ex5 = itempat.ryrq.ToString();
                    treeList.Add(treepat);
                }

                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = patInfo.Count == 0 ? false : true;
                tree.id = item.bqCode;
                tree.text = item.bqmc;
                tree.value = item.bqCode;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = 0;
                tree.hasChildren = hasChildren;
                tree.Ex1 = "p";
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        } 
        public IActionResult GetExecDetailGridJson(string zyh, DateTime zxsj,string zxdlb,string yzxz)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                return null;
            }
            var data =  _IExceReportPrintDmnService.GetExecDetailGridJson(OrganizeId, zyh, zxsj, zxdlb,yzxz);
            return Content(data.ToJson());
        }

        public IActionResult QueryExecDetailGridJson(Pagination pagination, string zyh, DateTime zxsj, DateTime zxsjend, string zxdlb, string yzxz)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                return null;
            }
            var rowData = _IExceReportPrintDmnService.QueryExecDetailGridJson(pagination, OrganizeId, zyh, zxsj, zxsjend, zxdlb,yzxz);
            var data = new
            {
                rows = rowData,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public override IActionResult Index()
        {
            //Lưu ýxạCách dùngđiều khiển
            ViewBag.ControlzsyfCode = _sysConfigRepo.GetValueByCode("zsyfpz", OrganizeId);
            //khí dungCách dùngđiều khiển
            ViewBag.ControlwhyfCode = _sysConfigRepo.GetValueByCode("whyfpz", OrganizeId);
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            return View();
        }

        public  IActionResult QueryPrintIndex() {
            //Lưu ýxạCách dùngđiều khiển
            ViewBag.ControlzsyfCode = _sysConfigRepo.GetValueByCode("zsyfpz", OrganizeId);
            //khí dungCách dùngđiều khiển
            ViewBag.ControlwhyfCode = _sysConfigRepo.GetValueByCode("whyfpz", OrganizeId);
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            return View();
        }

        public IActionResult PrintedContinue()
        {
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            ViewBag.OrgId = OrganizeId;
            return View();
        }
    }
}