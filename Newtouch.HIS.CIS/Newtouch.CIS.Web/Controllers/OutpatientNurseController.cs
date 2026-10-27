using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.CIS.Web.Models;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using NewtouchHIS.Lib.Framework.Operator;

using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Controllers
{
    public class OutpatientNurseController : OrgControllerBase
    {
        private readonly IOutpatientNurseDmnServise _OutpatientNurseDmnService;
        private readonly IOrderAuditDmnService _OrderAuditDmnService;

        public OutpatientNurseController(IOutpatientNurseDmnServise outpatientNurseDmnService, IOrderAuditDmnService orderAuditDmnService)
        {
            this._OutpatientNurseDmnService = outpatientNurseDmnService;
            this._OrderAuditDmnService = orderAuditDmnService;
        }

        #region View Chế độ xem
        // GET: OutpatientNurse
        /// <summary>
        /// Test daThực hiệntrangmặt
        /// </summary>
        /// <returns></returns>
        public IActionResult PerformIndex()
        {
            return View();
        }
        /// <summary>
        /// Test daTra cứu
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult Skintestquery()
        {
            return View();
        }

        /// <summary>
        /// Đơn thuốc ngoại trúTra cứu
        /// </summary>
        /// <returns></returns>
        public IActionResult prescriptionquery()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            return View();
        }
        /// <summary>
        /// Ngoại trúgiữxemTra cứu
        /// </summary>
        /// <returns></returns>
        public IActionResult observationQuery()
        {
            return View();
        }
        /// <summary>
        /// Ngoại trúvạchgiá
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientPricing()
        {
            return View();
        }
        #endregion
        
        #region Test da
        //Test datrangmặtcâykiểm soátmục
        public IActionResult SkintestTree(string keyword, DateTime? kssj, DateTime? jssj, string type)
        {
            var wardTree = _OutpatientNurseDmnService.OutpatientNurseTreeVO(this.OrganizeId, keyword, kssj, jssj, type);
            var wardonly = wardTree.GroupBy(p => new { p.ghksmc }).Select(p => new { p.Key.ghksmc });


            var treeList = new List<TreeViewModel>();
            foreach (var item in wardonly)
            {
                var patInfo = wardTree.Where(p => p.ghksmc == item.ghksmc).Where(p => p.mzh != "").Where(p => p.mzh != null).ToList();

                foreach (OutpatientNurseTreeVO itempat in patInfo)
                {
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = itempat.jzId;
                    treepat.text = itempat.mzh + "-" + itempat.xm;
                    treepat.value = itempat.jzId;
                    treepat.parentId = item.ghksmc;
                    treepat.isexpand = false;
                    treepat.complete = true;
                    treepat.showcheck = true;
                    treepat.checkstate = 0;
                    treepat.hasChildren = false;
                    treepat.Ex1 = "c";
                    treeList.Add(treepat);
                }

                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = patInfo.Count == 0 ? false : true;
                tree.id = item.ghksmc;
                tree.text = item.ghksmc;
                tree.value = item.ghksmc;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = 0;
                tree.hasChildren = hasChildren;
                tree.Ex1 = "p";
                treeList.Add(tree);
            }
            var a = treeList.TreeViewJson(null);
            return Content(treeList.TreeViewJson(null));
        }

        public IActionResult Inputinformation(Pagination pagination, string patList, string organizeId, string selectkey)
        {

            IList<OutpatientNursequeryVO> list = new List<OutpatientNursequeryVO>();

            list = _OutpatientNurseDmnService.OutpatientNursequery(pagination, patList, OrganizeId);

            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }


        public IActionResult EnteragainMuti(string cfmxid, string lrjg)
        {
            OperatorModel user = this.UserIdentity;
            string msg = _OutpatientNurseDmnService.Enteragain(user, cfmxid, lrjg);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                return Error(msg);
            }
            else
            {
                return Success("Lưu thành công");
            }

        }
        
        public IActionResult skintesfrom(Pagination pagination, DateTime? kssj, DateTime? jssj, string keyword)
        {
            IList<OutpatientNursequeryVO> list = new List<OutpatientNursequeryVO>();

            list = _OutpatientNurseDmnService.skintesfrom(pagination, keyword, kssj, jssj, OrganizeId);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());

        }

        public IActionResult skintescancel(string gmxxid)
        {
            OperatorModel user = this.UserIdentity;
            string msg = _OutpatientNurseDmnService.skintescancel(gmxxid, user);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                return Error(msg);
            }
            else
            {
                return Success("HủyThực hiệnthành công");
            }
        }
        #endregion

        #region Ngoại trúBệnh ánIn
        /// <summary>
        /// LấyĐơn thuốc ngườiviênTra cứucây
        /// </summary>
        /// <returns></returns>
        public IActionResult GetPatcfTree(string keyword)
        {

            var wardTree = _OutpatientNurseDmnService.GetPatTree(OrganizeId, keyword);
            var wardonly = wardTree.GroupBy(p => new { p.ghksmc }).Select(p => new { p.Key.ghksmc });


            var treeList = new List<TreeViewModel>();
            foreach (var item in wardonly)
            {
                var patInfo = wardTree.Where(p => p.ghksmc == item.ghksmc).Where(p => p.mzh != "").Where(p => p.mzh != null).ToList();

                foreach (OutpatientNurseTreeVO itempat in patInfo)
                {
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = itempat.jzId;
                    treepat.text = itempat.mzh + "-" + itempat.xm;
                    treepat.value = itempat.mzh;
                    treepat.parentId = item.ghksmc;
                    treepat.isexpand = false;
                    treepat.complete = true;
                    treepat.showcheck = true;
                    treepat.checkstate = 0;
                    treepat.hasChildren = false;
                    treepat.Ex1 = "c";
                    treepat.Ex2 = itempat.sex;
                    treepat.Ex3 = itempat.nlshow;
                    treepat.Ex4 = itempat.xm;
                    treeList.Add(treepat);
                }

                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = patInfo.Count == 0 ? false : true;
                tree.id = item.ghksmc;
                tree.text = item.ghksmc;
                tree.value = item.ghksmc;
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

        public IActionResult GetBarCodeBycfh(string cfid)
        {
            var cfh = _OutpatientNurseDmnService.getcfh(cfid, OrganizeId);
            return Content(CommmHelper.GenerateBarCode(cfh, 25, 25));
        }
        public IActionResult prescriptionfrom(Pagination pagination, string jzid, DateTime? klrq, string cfdlb)
        {
            IList<OutpatientNursequeryVO> list = new List<OutpatientNursequeryVO>();

            list = _OutpatientNurseDmnService.prescriptionfrom(pagination, jzid, klrq, OrganizeId, cfdlb);
            if (list.Count > 0)
            {
                var Total = list.Sum(t => t.je);
                OutpatientNursequeryVO zje = new OutpatientNursequeryVO();
                zje.cfh = "Tổng cộng"; zje.je = Total; zje.kssj = list[0].kssj;
                list.Add(zje);
            }
            foreach (var item in list)
            {
                if (item.cflx == 5 || item.cflx == 4)
                {
                    item.barcode = CommmHelper.GenerateBarCode(item.cfh, 25, 25);
                }
            }
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        #endregion

        #region Theo dõi
        public IActionResult observationfrom(Pagination pagination, DateTime? kssj, DateTime? jssj, string keyword)
        {
            IList<ObservationFromVO> list = new List<ObservationFromVO>();

            list = _OutpatientNurseDmnService.observationquery(pagination, keyword, kssj, jssj, OrganizeId);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());

        }
        #endregion

        #region Ngoại trúvạchgiá


        #endregion
    }
}