using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.TemplateManage.Controllers
{
    public class DoctorServiceTemplateController : OrgControllerBase
    {
        private readonly IInpatientOrderPackageRepo _inpatientOrderPackageRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IInpatientOrderPackageDmnService _inpatientOrderPackageDmnService;
        private readonly FrameworkBase.MultiOrg.Domain.IDomainServices.IBaseDataDmnService _iBaseDataDmnService;
        public override ActionResult Index() {
            ViewBag.bhbq = _sysConfigRepo.GetBoolValueByCode("DoctorServiceTemplate", OrganizeId, true);//Gói y lệnh nhập viện có bao gồm khu bệnh không
            return View();
        }
        public ActionResult ItemForm()
        {
            ViewBag.bwhide = _sysConfigRepo.GetValueByCode("bwhide", OrganizeId) ?? "false";//Cấu hình hiển thị/ẩn vị trí khi nhập hạng mục nhập viện    
            return View();
        }
        public ActionResult MedicineForm()
        {
            ViewBag.qzfs = _sysConfigRepo.GetValueByCode("QZFS", OrganizeId) ?? "day";
            return View();
        }
        public ActionResult TakeMedicineForm()
        {
            ViewBag.qzfs = _sysConfigRepo.GetValueByCode("QZFS", OrganizeId) ?? "day";
            return View();
        }
        public ActionResult WordForm()
        {
            return View();
        }
        public ActionResult RehabForm()
        {
           ViewBag.bwhide = _sysConfigRepo.GetValueByCode("bwhide", OrganizeId) ?? "false";//Cấu hình hiển thị/ẩn vị trí khi nhập hạng mục nhập viện    
            return View();
        }

        public ActionResult FoodForm()
        {
            return View();
        }

        public ActionResult TCMmedicineForm() {
            return View();
        }

        /// <summary>
        /// Cây mẫu đơn thuốc
        /// </summary>
        /// <param name="mblx"></param>
        /// <param name="cflx"></param>
        /// <returns></returns>
        public ActionResult GetTreeList(int tclx, int tcfw, int? expandyzlx)
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

            //Gói
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

            //Chi tiết gói
            var data = _inpatientOrderPackageRepo.IQueryable().Where(a => a.OrganizeId == this.OrganizeId && a.tclx == tclx && a.tcfw == tcfw && a.zt == "1").Select(a => new { a.tcmc, a.tclx, a.Id, a.CreateTime, a.LastModifyTime }).OrderByDescending(a => a.CreateTime).OrderByDescending(a => a.LastModifyTime).ToList();
            foreach (var item in data)
            {
                treeList.Add(new TreeViewModel()
                {
                    id = "",   //MẫuId
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

        public ActionResult Submit(InpatientOrderPackageEntity mbObj, List<InpatientOrderPackageVO> mbDetailList) {
            if (mbObj == null|| string.IsNullOrWhiteSpace(mbObj.tcfw.ToString()))
            {
                throw new FailedException("缺少套餐数据");
            }
            switch (mbObj.tcfw) {
                case (int)EnumCfMbLx.personal:
                    mbObj.ysgh = UserIdentity.rygh;
                    break; //Khi là cá nhân, gán bác sĩ
                case (int)EnumCfMbLx.department:
                    mbObj.DeptCode = UserIdentity.DepartmentCode;
                    break; //Khi là khoa
                //case (int)EnumTcfw.Ward:
                //    var bq = _iBaseDataDmnService.GetWardListByStaffGh(UserIdentity.rygh, OrganizeId).FirstOrDefault().bqCode;
                //    mbObj.WardCode = bq;
                //    break; //Khi là khu bệnh
                case (int)EnumCfMbLx.hospital:
                    mbObj.ysgh = "*";
                    mbObj.DeptCode = "*";
                    mbObj.ysgh = "*";
                    break;
                    //Khi là toàn viện
            }
          var mbId=  _inpatientOrderPackageDmnService.saveAsTemplate(mbObj, mbDetailList, OrganizeId);
            return Success(null, mbId);
        }

        public ActionResult Delete(string mbId) {
            try
            {
                _inpatientOrderPackageDmnService.DeleteTemplate(mbId, OrganizeId);
                
                return Success();
            }
            catch (System.Exception e)
            {

                return Error("删除失败");
            }
        }
    }
}