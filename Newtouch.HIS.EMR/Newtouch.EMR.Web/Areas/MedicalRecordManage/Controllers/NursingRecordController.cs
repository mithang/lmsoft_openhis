using Newtouch.EMR.Domain.Entity;

using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Framework.Web.Controllers;
using System.Collections.Generic;

using Newtouch.EMR.Domain.IRepository;
using System.Linq;
using System;

using Newtouch.EMR.Domain.IDomainServices;

using Newtouch.Domain.ValueObjects;
namespace Newtouch.EMR.Web.Areas.MedicalRecordManage
{
    /// <summary>
    /// 创 建：mhz
    /// 日 期：2023-04-24 15:46
    /// 描 述：护理记录录入
    /// </summary>
    [Area("MedicalRecordManage")]
    public class NursingRecordController : OrgControllerBase
    {
        private readonly IMedicalRecordDmnService _medicalRecordDmnService;
        private readonly INursingRecordDmnService _nursingRecordDmnService;
        private readonly INursingRecordWZDmnService _nursingRecordWZDmnService;
        private readonly INursingRecordSSDmnService _nursingRecordSSDmnService;

        public NursingRecordController(IMedicalRecordDmnService medicalRecordDmnService, INursingRecordDmnService nursingRecordDmnService, INursingRecordWZDmnService nursingRecordWZDmnService, INursingRecordSSDmnService nursingRecordSSDmnService)
        {
            this._medicalRecordDmnService = medicalRecordDmnService;
            this._nursingRecordDmnService = nursingRecordDmnService;
            this._nursingRecordWZDmnService = nursingRecordWZDmnService;
            this._nursingRecordSSDmnService = nursingRecordSSDmnService;
        }
        /// <summary>
        /// 护理查询
        /// </summary>
        /// <returns></returns>
        public override IActionResult Index()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = EmrConfig.GetAppConfigValue("ReportServer.HOST");
            return View();
        }
        public override IActionResult Form()
        {
            ViewBag.hsqm = UserIdentity.UserName;
            return View();
        }
        public  IActionResult WZForm()
        {
            ViewBag.hsqm = UserIdentity.UserName;
            return View();
        }
        public IActionResult SSForm()
        {
            ViewBag.hsqm = UserIdentity.UserName;
            return View();
        }
        public IActionResult OpenOutForm()
        {
            ViewBag.hsqm = UserIdentity.UserName;
            return View();
        }
        public IActionResult OpenPutForm()
        {
            ViewBag.hsqm = UserIdentity.UserName;
            return View();
        }
        public IActionResult GetPatWard(string zyh)
        {
            var wardTree = _nursingRecordDmnService.GetPatList(OrganizeId, zyh);
            return Content(wardTree.ToJson());
        }
        public IActionResult SubmitSrl(string srlstr,string zyh,string rq,string sj,string bllx)
        {
            var orgId = OrganizeId;
            _nursingRecordDmnService.DeleteSrl(orgId, zyh, rq, sj, bllx, UserIdentity.UserCode);
            for (int i = 0; i < srlstr.Split(',').Length; i++)
            {
                _nursingRecordDmnService.SubmitSrl(srlstr.Split(',')[i].Split('|')[0], srlstr.Split(',')[i].Split('|')[1], srlstr.Split(',')[i].Split('|')[2], srlstr.Split(',')[i].Split('|')[3], orgId, zyh, rq, sj, bllx, UserIdentity.UserCode);
            }
            return Success("Thao tác thành công。");
        }
        public IActionResult SubmitScl(string srlstr, string zyh, string rq, string sj, string bllx)
        {
            var orgId = OrganizeId;
            _nursingRecordDmnService.DeleteScl(orgId, zyh, rq, sj, bllx, UserIdentity.UserCode);
            for (int i = 0; i < srlstr.Split(',').Length; i++)
            {
                _nursingRecordDmnService.SubmitScl(srlstr.Split(',')[i].Split('|')[0], srlstr.Split(',')[i].Split('|')[1], srlstr.Split(',')[i].Split('|')[2], srlstr.Split(',')[i].Split('|')[3], orgId, zyh, rq, sj, bllx, UserIdentity.UserCode);
            }
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// 获取病区人员执行树
        /// </summary>
        /// <returns></returns>
        public IActionResult GetPatWardTree(string zyzt, string keyword)
        {

            var wardTree = _nursingRecordDmnService.GetPatTree(OrganizeId, zyzt, keyword);
            var wardonly = wardTree.GroupBy(p => new { p.bqCode, p.bqmc }).Select(p => new { p.Key.bqCode, p.Key.bqmc });

            var treeList = new List<TreeViewModel>();
            foreach (var item in wardonly)
            {
                var patInfo = wardTree.Where(p => p.bqCode == item.bqCode).Where(p => p.zyh != "").Where(p => p.zyh != null).ToList();

                foreach (InpWardPatTreeVO itempat in patInfo)
                {
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = itempat.zyh;
                    treepat.text = itempat.zyh + "-" + itempat.hzxm;
                    treepat.value = itempat.zyh;
                    treepat.parentId = item.bqCode;
                    treepat.isexpand = false;
                    treepat.complete = true;
                    treepat.showcheck = true;
                    treepat.checkstate = 0;
                    treepat.hasChildren = false;
                    treepat.Ex1 = "c";
                    treepat.Ex2 = itempat.rqrq;
                    treepat.Ex3 = itempat.cqrq;
                    treepat.Ex4 = itempat.hzxm;
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
        /// <summary>
        /// 提交保存护理录入
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult SubmitForm(bl_hljl_ybEntity entity, string keyValue)
        {
            entity.OrganizeId = this.OrganizeId;
            entity.zt = "1";
            _nursingRecordDmnService.SubmitForm(entity, keyValue);
            return Success("Lưu thành công。");
        }
        /// <summary>
        /// 提交保存护理录入
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult SubmitFormWZ(bl_hljl_wzEntity entity, string keyValue)
        {
            entity.OrganizeId = this.OrganizeId;
            entity.zt = "1";
            _nursingRecordWZDmnService.SubmitForm(entity, keyValue);
            return Success("Lưu thành công。");
        }
        /// <summary>
        /// 提交保存护理录入
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult SubmitFormSS(bl_hljl_ssEntity entity, string keyValue)
        {
            entity.OrganizeId = this.OrganizeId;
            entity.zt = "1";
            _nursingRecordSSDmnService.SubmitForm(entity, keyValue);
            return Success("Lưu thành công。");
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(Pagination pagination, DateTime? kssj, DateTime? jssj, string zyh, string wardCode, string isShowDelete,string hllx)
        {
            if (hllx == "1")
            {
                var content = _nursingRecordWZDmnService.GetPaginationListWZ(pagination, this.OrganizeId, kssj, jssj, zyh, wardCode, isShowDelete);
                return Content(content.ToJson());
            }
            else if (hllx == "2")
            {
                var content = _nursingRecordSSDmnService.GetPaginationListSS(pagination, this.OrganizeId, kssj, jssj, zyh, wardCode, isShowDelete);
                return Content(content.ToJson());

            }
            else {
                var rows = _nursingRecordDmnService.GetPaginationList(pagination, this.OrganizeId, kssj, jssj, zyh, wardCode, isShowDelete);
                return Content(rows.ToJson());
            }
        }
        public IActionResult GetFormJson(string keyValue)
        {
            var data = _nursingRecordDmnService.FindEntity(keyValue);
            return Content(data.ToJson());
        }
        public IActionResult GetFormJsonWZ(string keyValue)
        {
            var data = _nursingRecordWZDmnService.FindEntity(keyValue);
            return Content(data.ToJson());
        }
        public IActionResult GetFormJsonSS(string keyValue)
        {
            var data = _nursingRecordSSDmnService.FindEntity(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _nursingRecordDmnService.DeleteForm(keyValue);
            return Success("Hủy hiệu lực thành công。");
        }
        public IActionResult GetSrlScl(string zyh, string rq, string sj, string bllx)
        {
            var wardTree = _nursingRecordDmnService.GetSrlScl(OrganizeId, zyh, rq, sj, bllx);
            return Content(wardTree.ToJson());
        }
        public IActionResult GetScl(string zyh, string rq, string sj, string bllx)
        {
            var wardTree = _nursingRecordDmnService.GetScl(OrganizeId, zyh, rq, sj, bllx);
            return Content(wardTree.ToJson());
        }
        public IActionResult Getzyhrq(string zyh)
        {
            var patInfo = _nursingRecordDmnService.GetInfoByZyh(zyh, this.OrganizeId);
            string ryrq = "";
            if (patInfo != null && patInfo.ryrq != null)
            {
                ryrq = patInfo.ryrq.ToString();
            }
            return Content(ryrq);
        }
        public IActionResult Getzyhcqrq(string zyh)
        {
            var patInfo = _nursingRecordDmnService.GetInfoByZyh(zyh, this.OrganizeId);
            string cqrq = "";
            if (patInfo != null && patInfo.cqrq != null && patInfo.cqrq.ToString() != "")
            {
                cqrq = patInfo.cqrq.ToString();
            }
            return Content(cqrq);
        }
    }
}