using System;
using NewtouchHIS.Lib.Framework.Attributes;
using Newtouch.CIS.Web.Models;
using Newtouch.CIS.Web.Attributes;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;

using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Common.Web;

using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    [Area("NurseManage")]
    public class InpatientListController : OrgControllerBase
    {

        private readonly ITreatmentRepo _treatmentRepo;
        private readonly IPatientVitalSignsRepo _patientVitalSignsRepo;
        private readonly IInpatientPatientDmnService _inpatientPatientDmnService;
        private readonly IBaseDataDmnService _iBaseDataDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IIDBDmnService _idbDmnService;
        private readonly IVisitDeptSetRepo visitDeptSetRepo;
        private readonly IExceReportPrintDmnService _IExceReportPrintDmnService;
        private readonly IOrderAuditDmnService _OrderAuditDmnService;


        public InpatientListController(ITreatmentRepo treatmentRepo, IPatientVitalSignsRepo patientVitalSignsRepo, IInpatientPatientDmnService inpatientPatientDmnService, IBaseDataDmnService iBaseDataDmnService, ISysConfigRepo sysConfigRepo, IIDBDmnService idbDmnService, IExceReportPrintDmnService iExceReportPrintDmnService, IOrderAuditDmnService orderAuditDmnService)
        {
            this._treatmentRepo = treatmentRepo;
            this._patientVitalSignsRepo = patientVitalSignsRepo;
            this._inpatientPatientDmnService = inpatientPatientDmnService;
            this._iBaseDataDmnService = iBaseDataDmnService;
            this._sysConfigRepo = sysConfigRepo;
            this._idbDmnService = idbDmnService;
            this._IExceReportPrintDmnService = iExceReportPrintDmnService;
            this._OrderAuditDmnService = orderAuditDmnService;

        }
        // GET: NurseManage/InpatientList
        public IActionResult Inpatient()
        {
            var bqAuth = _iBaseDataDmnService.GetWardListByStaffGh(UserIdentity.rygh, OrganizeId).FirstOrDefault();
            if (bqAuth != null)
            {
                ViewBag.bqval = bqAuth.bqCode;
                ViewBag.ysgh = UserIdentity.rygh;
                return View();
            }
            else
            {
                return Content("javascript:void()");
            }
        }
        #region  View
        public IActionResult IndexCenter()
        {
            return View();
        }
        public IActionResult MedicalorderQuery()
        {
            return View();
        }
        public IActionResult OrderAudit()
        {
            return View();
        }
        public IActionResult OrderExecution()
        {
            return View();
        }
        public IActionResult InHosBookkeep()
        {
            return View();
        }
        public IActionResult InHosFeeRefund()
        {
            return View();
        }
        public IActionResult InHosMedReturn()
        {
            return View();
        }
        public IActionResult InHosFee()
        {
            return View();
        }
        public IActionResult InHosRegistration()
        {
            return View();
        }
        public IActionResult InHosRecallOutArea()
        {
            return View();
        }
        public IActionResult InHosQueryIndex()
        {
            return View();
        }
        public IActionResult InHosNursingInput()
        {
            ViewBag.MutipatientNursingInputFlag = _sysConfigRepo.GetIntValueByCode("MutipatientNursingInputFlag", OrganizeId, 0);
            ViewBag.ScdCode = _sysConfigRepo.GetValueByCode("ScdTimePoint", OrganizeId);//Phiếu đo ba chỉ sốThời gianđiểmLẻ chẵnsốCài đặt 1 lẻsố 0 chẵnsố
            return View();
        }
        public IActionResult InHosPrint()
        {
            return View();
        }
        #endregion

        public IActionResult GetPatWardTree(string zyzt, string keyword,string checkzyh=null)
        {
            var wardTree = _OrderAuditDmnService.GetWardTree(this.UserIdentity.StaffId);
            var patInfo = _IExceReportPrintDmnService.GetPatCenterTree(OrganizeId, zyzt, keyword);
            var treeList = new List<TreeViewModel>();
            string[] checkzyhs = new string[200];
            if (!string.IsNullOrWhiteSpace(checkzyh))
            {
                checkzyhs = checkzyh.Split(',');
            }
            foreach (var item in wardTree)
            {
                var NewPatInfo = patInfo.Where(p => p.bqCode == item.bqCode).OrderBy(p=>p.BedNo).ToList();
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
                    treepat.Ex5 = itempat.rqrq;
                    treepat.Ex6 = itempat.cqrq;
                    treeList.Add(treepat);
                    if (((IList)checkzyhs).Contains(itempat.zyh))
                    {
                        treepat.checkstate = 1;
                    }
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
    }
}