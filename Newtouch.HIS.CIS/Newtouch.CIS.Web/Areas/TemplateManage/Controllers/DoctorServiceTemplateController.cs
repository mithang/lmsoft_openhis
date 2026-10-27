using Newtouch.Domain.IRepository;
using Newtouch.CIS.Web.Models;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.CIS.Compat;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.TemplateManage.Controllers
{
    [Area("TemplateManage")]
    public class DoctorServiceTemplateController : OrgControllerBase
    {
        private readonly IInpatientOrderPackageRepo _inpatientOrderPackageRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IInpatientOrderPackageDmnService _inpatientOrderPackageDmnService;
        private readonly IBaseDataDmnService _iBaseDataDmnService;

        public DoctorServiceTemplateController(IInpatientOrderPackageRepo inpatientOrderPackageRepo, ISysConfigRepo sysConfigRepo, IInpatientOrderPackageDmnService inpatientOrderPackageDmnService, IBaseDataDmnService iBaseDataDmnService)
        {
            this._inpatientOrderPackageRepo = inpatientOrderPackageRepo;
            this._sysConfigRepo = sysConfigRepo;
            this._inpatientOrderPackageDmnService = inpatientOrderPackageDmnService;
            this._iBaseDataDmnService = iBaseDataDmnService;
        }
        public override IActionResult Index() {
            ViewBag.bhbq = _sysConfigRepo.GetBoolValueByCode("DoctorServiceTemplate", OrganizeId, true);//Nhập việnY lệnhgóiCóKhônggóichứaKhu bệnh
            return View();
        }
        public IActionResult ItemForm()
        {
            ViewBag.bwhide = _sysConfigRepo.GetValueByCode("bwhide", OrganizeId) ?? "false";//Nhập việnMụcnhậpVị tríHiển thịẨncấu hình    
            return View();
        }
        public IActionResult MedicineForm()
        {
            ViewBag.qzfs = _sysConfigRepo.GetValueByCode("QZFS", OrganizeId) ?? "day";
            return View();
        }
        public IActionResult TakeMedicineForm()
        {
            ViewBag.qzfs = _sysConfigRepo.GetValueByCode("QZFS", OrganizeId) ?? "day";
            return View();
        }
        public IActionResult WordForm()
        {
            return View();
        }
        public IActionResult RehabForm()
        {
           ViewBag.bwhide = _sysConfigRepo.GetValueByCode("bwhide", OrganizeId) ?? "false";//Nhập việnMụcnhậpVị tríHiển thịẨncấu hình    
            return View();
        }

        public IActionResult FoodForm()
        {
            return View();
        }

        public IActionResult TCMmedicineForm() {
            return View();
        }

        /// <summary>
        /// Đơn thuốcmẫucây
        /// </summary>
        /// <param name="mblx"></param>
        /// <param name="cflx"></param>
        /// <returns></returns>
        public IActionResult GetTreeList(int tclx, int tcfw, int? expandyzlx)
        {
            var treeList = new List<TreeViewModel>();

            if (tclx == 0)
            {
                treeList.AddRange(GetStaticTreeList(tcfw, (int)EnumYzlx.Yp, expandyzlx));
                treeList.AddRange(GetStaticTreeList(tcfw, (int)EnumYzlx.Wz, expandyzlx));
                treeList.AddRange(GetStaticTreeList(tcfw, (int)EnumYzlx.sfxm, expandyzlx));
                treeList.AddRange(GetStaticTreeList(tcfw, (int)EnumYzlx.Cydy, expandyzlx));
                treeList.AddRange(GetStaticTreeList(tcfw, (int)EnumYzlx.ssyz, expandyzlx));
                treeList.AddRange(GetStaticTreeList(tcfw, (int)EnumYzlx.zcy, expandyzlx));
                treeList.AddRange(GetStaticTreeList(tcfw, (int)EnumYzlx.rehab, expandyzlx));
            }
            else
            {
                treeList.AddRange(GetStaticTreeList(tcfw, tclx));
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mblx"></param>
        /// <param name="cflx"></param>
        /// <returns></returns>
        private List<TreeViewModel> GetStaticTreeList(int tcfw, int tclx, int? expandyzlx = null)
        {
            var yzlxmc = ((EnumYzlx)tclx).GetDescription();

            var treeList = new List<TreeViewModel>();

            //gói
            treeList.Add(new TreeViewModel()
            {
                id = tclx.ToString(),
                value = tclx.ToString(),
                text = yzlxmc,
                parentId = null,
                hasChildren = true,
                isexpand = (expandyzlx ?? 0) != 0 ? (tclx == expandyzlx ? true : false) : true,
                complete = true,
            });

            //góichi tiết
            var data = _inpatientOrderPackageRepo.IQueryable().Where(a => a.OrganizeId == this.OrganizeId && a.tclx == tclx && a.tcfw == tcfw && a.zt == "1").Select(a => new { a.tcmc, a.tclx, a.Id, a.CreateTime, a.LastModifyTime }).OrderByDescending(a => a.CreateTime).OrderByDescending(a => a.LastModifyTime).ToList();
            foreach (var item in data)
            {
                treeList.Add(new TreeViewModel()
                {
                    id = "",   //mẫuId
                    value = item.tclx.ToString(),
                    text = item.tcmc,
                    parentId = item.tclx.ToString(),
                    hasChildren = false,
                    isexpand = false,
                    complete = true,
                    Ex1 = item.Id
                });
            }
            return treeList;
        }

        public IActionResult Submit(InpatientOrderPackageEntity mbObj, List<InpatientOrderPackageVO> mbDetailList) {
            if (mbObj == null|| string.IsNullOrWhiteSpace(mbObj.tcfw.ToString()))
            {
                throw new FailedException("Thiếu dữ liệu gói");
            }
            switch (mbObj.tcfw) {
                case (int)EnumCfMbLx.personal:
                    mbObj.ysgh = UserIdentity.rygh;
                    break; //Cá nhânkhi，Bác sĩGán giá trị
                case (int)EnumCfMbLx.department:
                    mbObj.DeptCode = UserIdentity.DepartmentCode;
                    break; //Khoakhi
                //case (int)EnumTcfw.Ward:
                //    var bq = _iBaseDataDmnService.GetWardListByStaffGh(UserIdentity.rygh, OrganizeId).FirstOrDefault().bqCode;
                //    mbObj.WardCode = bq;
                //    break; //Khu bệnhkhi
                case (int)EnumCfMbLx.hospital:
                    mbObj.ysgh = "*";
                    mbObj.DeptCode = "*";
                    mbObj.ysgh = "*";
                    break;
                    //Toàn việnkhi
            }
          var mbId=  _inpatientOrderPackageDmnService.saveAsTemplate(mbObj, mbDetailList, OrganizeId);
            return Success(null, mbId);
        }

        public IActionResult Delete(string mbId) {
            try
            {
                _inpatientOrderPackageDmnService.DeleteTemplate(mbId, OrganizeId);
                
                return Success();
            }
            catch (System.Exception e)
            {

                return Error("Xóathất bại");
            }
        }
    }
}