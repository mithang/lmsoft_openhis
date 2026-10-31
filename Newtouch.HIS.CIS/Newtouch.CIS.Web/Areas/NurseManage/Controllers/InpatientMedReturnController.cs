using Newtouch.Domain.IRepository;
using Newtouch.CIS.Web.Models;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using NewtouchHIS.Framework.Web.Controllers;

using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.CIS.Compat;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository.Inpatient;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    [Area("NurseManage")]
    public class InpatientMedReturnController : OrgControllerBase
    {
        // GET: NurseManage/InpatientMedReturn
        private readonly IInpatientMedReturnDmnService _InpatientMedReturnDmnService;

        private readonly IDrug_withdrawalzy_tyjlRepo _IDrug_withdrawalzy_tyjlRepo;
        private readonly ISysConfigRepo _sysConfigRepo;

        public InpatientMedReturnController(IInpatientMedReturnDmnService inpatientMedReturnDmnService, IDrug_withdrawalzy_tyjlRepo iDrug_withdrawalzy_tyjlRepo, ISysConfigRepo sysConfigRepo)
        {
            this._InpatientMedReturnDmnService = inpatientMedReturnDmnService;
            this._IDrug_withdrawalzy_tyjlRepo = iDrug_withdrawalzy_tyjlRepo;
            this._sysConfigRepo = sysConfigRepo;
        }
        public IActionResult GetGridJson(string patInfo, string keyword, string kssj, string jssj)
        {
            IList<InpatientMedicineGrantDto> patList = null;
            if (patInfo != null && patInfo != "")
            {
                string ReturnZcyMed = _sysConfigRepo.GetValueByCode("EnableReturnZcyMed", this.OrganizeId);
                patList = _InpatientMedReturnDmnService.GetPatMedReturnList(patInfo, keyword, kssj, jssj, this.OrganizeId, ReturnZcyMed);
            }
            return Content(patList.ToJson());
        }
        /// <summary>
        /// LấyKhu bệnh ngườiviênphát thuốccây
        /// </summary>
        /// <returns></returns>
        public IActionResult GetPatWardTree(string keyword)
        {
            string ReturnZcyMed = _sysConfigRepo.GetValueByCode("EnableReturnZcyMed", this.OrganizeId);
            string staffId = this.UserIdentity.StaffId;
            var wardTree = _InpatientMedReturnDmnService.GetPatTree(keyword, staffId, this.OrganizeId, ReturnZcyMed);
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
                    //Số giường + Họ tên(Nhập việnSố ngày)+Số nhập viện + Tuổi +Giới tính
                    treepat.text = itempat.BedNo + "-"  + itempat.hzxm + "(" + itempat.inHosDays + "ngày)" + "-" + itempat.zyh+ "-" + itempat.nl + "tuổi-" + gender;
                    treepat.value = itempat.zyh;
                    treepat.parentId = item.bqCode;
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
            var a = treeList.TreeViewJson(null);
            return Content(treeList.TreeViewJson(null));
        }
        
        public IActionResult MedReturnSubmit(string medIds)
        {
            OperatorModel user = this.UserIdentity;
            if (!String.IsNullOrWhiteSpace(medIds))
            {
                string tyxh = _InpatientMedReturnDmnService.MedReturnSubmit(user, medIds);
                //return Content(new { tyxh = tyxh }.ToJson());
                return Success("Trả thuốcthành công", tyxh);
            }
            else
            {
                return Error("Vui lòng chọnThuốc");
            }

        }




        //--------------------------------------lấydướiCó:Y lệnhtrảthuốcTra cứu--------------------------------------

        public IActionResult Drug_withdrawal() {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }

        public IActionResult Griddata(string patInfo, string keyword, string kssj, string jssj) {
            var kongzhi = "";
            if (patInfo != null && patInfo != "")
            {
                var data = _IDrug_withdrawalzy_tyjlRepo.Griddata(patInfo, keyword, kssj, jssj, this.OrganizeId);
                

                return Content(data.ToJson());
            }
            else {
                return Content(kongzhi);
            }
        }

        
        public IActionResult Treecx(string keyword) {
            

            string staffId = this.UserIdentity.StaffId;
            var wardTree = _IDrug_withdrawalzy_tyjlRepo.treecx(keyword, this.UserIdentity.StaffId, this.OrganizeId);
            var wardonly = wardTree.GroupBy(p => new { p.bqCode, p.bqmc }).Select(p => new { p.Key.bqCode, p.Key.bqmc });
             
            var treeList = new List<TreeViewModel>();
            foreach (var item in wardonly)
            {
                var patInfo = wardTree.Where(p => p.bqCode == item.bqCode).Where(p => p.zyh != "").Where(p => p.zyh != null).ToList();
                var NewPatInfo = patInfo.OrderBy(p => p.BedNo);
                foreach (GrugTreezsVO itempat in NewPatInfo)
                {
                    string gender = itempat.sex == "1" ? "Nam" : "Nữ";
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = itempat.zyh;
                    treepat.text = itempat.BedNo + "-" + itempat.hzxm + "(" + itempat.inHosDays + "ngày)" + "-" +itempat.zyh + "-" + itempat.nl + "tuổi-" + gender;
                    treepat.value = itempat.zyh;
                    treepat.parentId = item.bqCode;
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
            var a = treeList.TreeViewJson(null);
            return Content(treeList.TreeViewJson(null));
            
            
        }

        
    }
}