using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.HIS.Web.Core.Attributes;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage.Controllers
{
    public class DoctorsAdviceController : OrgControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly FrameworkBase.MultiOrg.Domain.IDomainServices.IBaseDataDmnService _iBaseDataDmnService;
        private readonly IInpatientOrderPackageDmnService _inpatientOrderPackageDmnService;
        private readonly IInpatientOrderPackageRepo _inpatientOrderPackageRepo;
        private readonly IDoctorserviceDmnService _iDoctorserviceDmnService;
        private readonly IInpatientLongTermOrderRepo _inpatientLongTermOrderRepo;
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly ISysUserDmnService _sysUserDmnService;

        [HandlerAuthorizeIgnore]
        public override ActionResult Index()
        {
            //Kiểm tra người đăng nhập có phải là y tá không
            var isDoctor = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "Doctor");
            if (!isDoctor)
            {
                ViewBag.bhbq = _sysConfigRepo.GetBoolValueByCode("DoctorServiceTemplate", OrganizeId, true);//Gói y lệnh nhập viện có bao gồm khu bệnh không
                //Nếu là y tá, vào trang xem y lệnh nhập viện
                return View("ZyyzQuery");
            }
            ViewBag.ysgh = UserIdentity.rygh;//Mã nhân viên bác sĩ
            ViewBag.pcStr = _sysConfigRepo.GetValueByCode("FrequencyStr", OrganizeId)== "" ? "": _sysConfigRepo.GetValueByCode("FrequencyStr", OrganizeId);//Tần suất tạm thời
            ViewBag.bhbq = _sysConfigRepo.GetBoolValueByCode("DoctorServiceTemplate", OrganizeId,true);//Gói y lệnh nhập viện có bao gồm khu bệnh không
            ViewBag.zzlconfig = _sysConfigRepo.GetValueByCode("zllconfig", OrganizeId)=="" ? "每次治疗量": _sysConfigRepo.GetValueByCode("zllconfig", OrganizeId);//Cấu hình văn bản lượng điều trị khi nhập hạng mục nhập viện
            ViewBag.bwhide = _sysConfigRepo.GetBoolValueByCode("bwhide", OrganizeId, false);//Cấu hình hiển thị/ẩn vị trí khi nhập hạng mục nhập viện                                           
            ViewBag.IsOpenJyJcSwitch = _sysConfigRepo.GetBoolValueByCode("OpenZYJyJcSwitch", this.OrganizeId,false);//Có mở xét nghiệm kiểm tra không
            ViewBag.sfxmService = _sysConfigRepo.GetValueByCode("sfxmService", this.OrganizeId)=="" ? "cg" : _sysConfigRepo.GetValueByCode("sfxmService", this.OrganizeId);//Có mở xét nghiệm kiểm tra không
            ViewBag.ISOpenSsyz= _sysConfigRepo.GetBoolValueByCode("ISOpenSsyzSwitch", this.OrganizeId, false);//Có mở y lệnh suất ăn không
            //Tần suất tạm thời
            var frequencyStr = _sysConfigRepo.GetValueByCode("FrequencyStr", OrganizeId) ?? "";
            if (!string.IsNullOrWhiteSpace(frequencyStr))
            {
                var FequencyList = frequencyStr.Split(',');
                var pcInfo = _iDoctorserviceDmnService.getpcInfoByCode(FequencyList[0], OrganizeId);
                ViewBag.pccode = pcInfo.yzpcCode;
                ViewBag.zxcs = pcInfo.zxcs;
                ViewBag.zxzq = pcInfo.zxzq;
                ViewBag.zxzqdw = pcInfo.zxzqdw;
                ViewBag.pcmc = pcInfo.yzpcmc;
            }
            #region 抗生素相关
            ViewBag.IsQyKssKz = _sysConfigRepo.GetBoolValueByCode("openKssQxSwitch", OrganizeId);//Có bật kháng sinh không
            ViewBag.qxjb = "0";//Cấp độ quyền kháng sinh(Mặc định 0 Thuốc không bị hạn chế)
            SysStaffVEntity staff = _sysStaffRepo.GetValidStaffByGh(UserIdentity.rygh, OrganizeId);
            if (staff != null)
            {
                switch (staff.zc)
                {
                    case "ys":
                        ViewBag.qxjb = "0";//Bác sĩ Tương ứng Thuốc không bị hạn chế 0
                        break;
                    case "zzys":
                        ViewBag.qxjb = "1";//Bác sĩ điều trị chính Tương ứng Thuốc bị hạn chế 1
                        break;
                    case "zrys":
                        ViewBag.qxjb = "2";//Bác sĩ trưởng khoa Tương ứng Thuốc đặc biệt 2
                        break;
                    default:
                        ViewBag.qxjb = "0";//Bác sĩ Tương ứng Thuốc không bị hạn chế 0
                        break;
                }
            }
            #endregion 抗生素相关
            //Cấu hình hình ảnh
            ViewBag.PACSCode = _sysConfigRepo.GetValueByCode("PACSCode", OrganizeId);

            #region 滴速
            var frds = _sysConfigRepo.GetValueByCode("frequencyRelDroppingSpeed", OrganizeId);
            frds = string.IsNullOrWhiteSpace(frds) ? "静滴" : frds;
            ViewBag.frequencyRelDroppingSpeed = frds;
            #endregion

            return View();
        }
        public ActionResult AdviceList()
        {
            return View();
        }
        public ActionResult ZyyzQueryAdviceList()
        {
            return View();
        }

        public ActionResult AdviceStop() {
            return View();
        }

        public ActionResult AdviceLeaveHospitalStop() {
            return View();
        }

        public ActionResult ContinuePrint() {
            return View();
        }

        public ActionResult TemplatePresForm() {
            return View();
        }

        public ActionResult TransferWardStop() {
            return View();
        }

        public ActionResult saveAsTemplate(InpatientOrderPackageEntity mbObj, List<InpatientOrderPackageVO> mxList)
        {
            if (mbObj.tcfw==(int)EnumTcfw.Dept)//Khoa
            {
                mbObj.DeptCode=UserIdentity.DepartmentCode;
            }
            if (mbObj.tcfw == (int)EnumTcfw.Ward)//Khu bệnh
            {
                mbObj.WardCode = _iBaseDataDmnService.GetWardListByStaffGh(UserIdentity.rygh, OrganizeId).FirstOrDefault().bqCode;
            }
            _inpatientOrderPackageDmnService.saveAsTemplate(mbObj, mxList, OrganizeId);
            return Success();
        }


        /// <summary>
        /// Cây mẫu
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTreeList(int tcfw,string yzlx)
        {
            var treeList = new List<TreeViewModel>();
            int linqyzlx = int.Parse(yzlx);
            var alldata = _inpatientOrderPackageRepo.IQueryable().Where(p => p.tcfw == tcfw && p.tclx == linqyzlx && p.OrganizeId == OrganizeId && p.zt == "1").ToList();
            switch (tcfw) {
                case (int)EnumTcfw.Person:
                    alldata.Where(p=>p.ysgh==UserIdentity.rygh);
                    break;
                case (int)EnumTcfw.Dept:
                    alldata.Where(p => p.DeptCode == UserIdentity.DepartmentCode);
                    break;
                case (int)EnumTcfw.Ward:
                    alldata.Where(p => p.WardCode == _iBaseDataDmnService.GetWardListByStaffGh(UserIdentity.rygh,OrganizeId).FirstOrDefault().bqCode);
                    break;
                case (int)EnumTcfw.Hosp:
                    alldata.Where(p => p.ysgh=="*"&&p.DeptCode=="*"&&p.WardCode=="*");
                    break;
                default:break;
            }
            foreach (var mb in alldata)
            {
                treeList.Add(new TreeViewModel()
                {
                    id = mb.Id,
                    value = mb.Id,
                    text = mb.tcmc,
                    parentId = null,
                    hasChildren = false,
                    isexpand = false,
                    complete = true
                });
            }
            return Content(treeList.TreeViewJson(null));
        }

        public ActionResult GetMBDetailByMainId(string Id)
        {
           var data= _inpatientOrderPackageDmnService.GetMBDetailByMainId(Id, OrganizeId);
            return Content(data.ToJson());
        }

        public ActionResult GetMBDetailByDetailId(string idList) {
            var data = _inpatientOrderPackageDmnService.GetMBDetailByDetailId(idList, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Tra cứu y lệnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public ActionResult AdviceGridView(Pagination pagination, AdviceListRequestVO req) {
            req.orgId = OrganizeId;
            var data = new
            {
                rows = _iDoctorserviceDmnService.AdviceGridView(pagination,req),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Dừng, hủy y lệnh
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="tzsj"></param>
        /// <returns></returns>
        public ActionResult StopSubmitForm(string yzId, DateTime? tzsj) {
            _iDoctorserviceDmnService.AdviceStop(yzId, tzsj, UserIdentity.rygh,OrganizeId);
            return Success();
        }

        /// <summary>
        /// Xóa y lệnh chưa duyệt
        /// </summary>
        /// <param name="yzId"></param>
        /// <returns></returns>
        public ActionResult DelForm(string yzId) {
            _iDoctorserviceDmnService.AdviceDel(yzId, OrganizeId);
            return Success();
        }

        /// <summary>
        /// HủyDC
        /// </summary>
        /// <param name="yzId"></param>
        /// <returns></returns>
        public ActionResult advicedc(string yzId) {
            _iDoctorserviceDmnService.Advicedc(yzId, OrganizeId);
            return Success();
        }

        /// <summary>
        /// Dừng toàn bộ khi xuất viện
        /// </summary>
        /// <returns></returns>
        public ActionResult AdviceLeaveHospitalStopSubmit(string zyh,DateTime tzsj) {
            _iDoctorserviceDmnService.AdviceLeaveHospitalStopSubmit(zyh, tzsj, OrganizeId, UserIdentity.rygh);
            return Success();
        }

        /// <summary>
        /// Dừng toàn bộ khi chuyển khu
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult AdviceTransferWardStopSubmit(string zyh,string bq,DateTime? kssj) {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("缺少住院号");
            }
            if (string.IsNullOrWhiteSpace(bq)|| bq== "==请选择==")
            {
                throw new FailedException("缺少病区");
            }
            if (kssj==DateTime.MaxValue||kssj==DateTime.MinValue||kssj==null)
            {
                throw new FailedException("缺少开始时间");
            }
            var req = new TransferWardRequestVO
            {
                zyh = zyh,
                bq = bq,
                czr = UserIdentity.rygh,
                kssj = DateTime.Parse(kssj.ToString()),
                orgId = OrganizeId
            };
            _iDoctorserviceDmnService.AdviceTransferWardStopSubmit(req);
            return Success();
        }

        /// <summary>
        /// Kiểm tra bệnh nhân hiện tại có y lệnh dài hạn chưa duyệt không
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult boolwsh(string zyh) {
          int wshzt = (int)EnumYzzt.Ds;
          var wsh= _inpatientLongTermOrderRepo.IQueryable().Where(p=>p.zyh==zyh&&p.OrganizeId==OrganizeId&&p.zt=="1"&&p.yzzt== wshzt);
            if (wsh!=null&&wsh.Count()>0)
            {
                return Error("存在未审核的医嘱");
            }
            return Success();
        }

        /// <summary>
        /// Trả về phạm vi gói
        /// </summary>
        /// <returns></returns>
        public ActionResult GettcfwContainWard()
        {
            SysStaffVEntity sysStaffEntity = _sysStaffRepo.GetValidStaffByGh(this.UserIdentity.rygh, this.OrganizeId);
            int mbqx = Convert.ToInt32((sysStaffEntity.mbqx == null || sysStaffEntity.mbqx == "") ? "1" : sysStaffEntity.mbqx);
            var data = new List<object>();
            var qxz = new
            {
                id = "",
                text = "==请选择=="
            };
            data.Add(qxz);
            bool containbq = _sysConfigRepo.GetBoolValueByCode("DoctorServiceTemplate", OrganizeId).ToBool();
            foreach (EnumTcfw item in Enum.GetValues(typeof(EnumTcfw)))
            {
                if ((!containbq)&&(item==EnumTcfw.Ward || mbqx<(int)item))
                {
                    continue;
                }
                var obj = new
                {
                    id = (int)item,
                    text = ((EnumTcfw)item).GetDescription()
                };
                data.Add(obj);
            }
            return Content(data.ToJson());
        }

        /// <summary>
        /// Chọn chi tiết gói
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTemplatePresDetailByMbId(string mbId) {
            return View();
        }

        /// <summary>
        /// Khi tra cứu y lệnh tạo hộp thả xuống loại y lệnh, hiển thị xét nghiệm kiểm tra đã cấu hình
        /// </summary>
        /// <returns></returns>
        public ActionResult GetyzlxContain() {
            var data = new List<object>();
            var qxz = new
            {
                id = "",
                text = "全部"
            };
            data.Add(qxz);
            bool containbq = _sysConfigRepo.GetBoolValueByCode("OpenZYJyJcSwitch", OrganizeId,false).ToBool();
            foreach (EnumYzlx item in Enum.GetValues(typeof(EnumYzlx)))
            {
                if ((!containbq) && (item == EnumYzlx.jc||item==EnumYzlx.jy))
                {
                    continue;
                }
                var obj = new
                {
                    id = (int)item,
                    text = ((EnumYzlx)item).GetDescription()
                };
                data.Add(obj);
            }
            return Content(data.ToJson());
        }
    }
}