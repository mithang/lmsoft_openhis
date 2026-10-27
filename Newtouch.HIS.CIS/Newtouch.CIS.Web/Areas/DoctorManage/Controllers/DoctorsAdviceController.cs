
using Newtouch.Domain.IDomainServices;
using Newtouch.CIS.Web.Models;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;


using Newtouch.CIS.Compat;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using NewtouchHIS.Lib.Framework.Attributes;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;

using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage.Controllers
{
    [Area("DoctorManage")]
    public class DoctorsAdviceController : OrgControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IBaseDataDmnService _iBaseDataDmnService;
        private readonly IInpatientOrderPackageDmnService _inpatientOrderPackageDmnService;
        private readonly IInpatientOrderPackageRepo _inpatientOrderPackageRepo;
        private readonly IDoctorserviceDmnService _iDoctorserviceDmnService;
        private readonly IInpatientLongTermOrderRepo _inpatientLongTermOrderRepo;
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly ISysUserDmnService _sysUserDmnService;

        public DoctorsAdviceController(ISysConfigRepo sysConfigRepo, IBaseDataDmnService iBaseDataDmnService, IInpatientOrderPackageDmnService inpatientOrderPackageDmnService, IInpatientOrderPackageRepo inpatientOrderPackageRepo, IDoctorserviceDmnService iDoctorserviceDmnService, IInpatientLongTermOrderRepo inpatientLongTermOrderRepo, ISysStaffRepo sysStaffRepo, ISysUserDmnService sysUserDmnService)
        {
            this._sysConfigRepo = sysConfigRepo;
            this._iBaseDataDmnService = iBaseDataDmnService;
            this._inpatientOrderPackageDmnService = inpatientOrderPackageDmnService;
            this._inpatientOrderPackageRepo = inpatientOrderPackageRepo;
            this._iDoctorserviceDmnService = iDoctorserviceDmnService;
            this._inpatientLongTermOrderRepo = inpatientLongTermOrderRepo;
            this._sysStaffRepo = sysStaffRepo;
            this._sysUserDmnService = sysUserDmnService;
        }

        [HandlerAuthorizeIgnore]
        public override IActionResult Index()
        {
            //kiểm trađănglụcngườithânphầnCóKhôngCóĐiều dưỡng
            var isDoctor = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "Doctor");
            if (!isDoctor)
            {
                ViewBag.bhbq = _sysConfigRepo.GetBoolValueByCode("DoctorServiceTemplate", OrganizeId, true);//Nhập việnY lệnhgóiCóKhônggóichứaKhu bệnh
                //VDnếuCóĐiều dưỡng，vàovàoNhập việnY lệnhXemtrangmặt
                return View("ZyyzQuery");
            }
            ViewBag.ysgh = UserIdentity.rygh;//Bác sĩMã nhân viên
            ViewBag.pcStr = _sysConfigRepo.GetValueByCode("FrequencyStr", OrganizeId) == "" ? "" : _sysConfigRepo.GetValueByCode("FrequencyStr", OrganizeId);//Tạm thờiTần suất
            ViewBag.bhbq = _sysConfigRepo.GetBoolValueByCode("DoctorServiceTemplate", OrganizeId, true);//Nhập việnY lệnhgóiCóKhônggóichứaKhu bệnh
            ViewBag.zzlconfig = _sysConfigRepo.GetValueByCode("zllconfig", OrganizeId) == "" ? "Liều mỗi lần điều trị" : _sysConfigRepo.GetValueByCode("zllconfig", OrganizeId);//Nhập việnMụcnhậpĐiều trịlượngvăn bảncấu hình
            ViewBag.bwhide = _sysConfigRepo.GetBoolValueByCode("bwhide", OrganizeId, false);//Nhập việnMụcnhậpVị tríHiển thịẨncấu hình                                           
            ViewBag.IsOpenJyJcSwitch = _sysConfigRepo.GetBoolValueByCode("OpenZYJyJcSwitch", this.OrganizeId, false);//Có mở khôngXét nghiệmKiểm tra
            ViewBag.IsOpenRehabSwitch = _sysConfigRepo.GetBoolValueByCode("OpenZYRehabSwitch", this.OrganizeId, false);//Có mở khôngPhục hồi chức năngY lệnh
            
            ViewBag.ISOpenWzhc = _sysConfigRepo.GetBoolValueByCode("openWzhccf", this.OrganizeId);//Công tắc：Có mở khôngydùngVật tư tiêu hao
            ViewBag.sfxmService = _sysConfigRepo.GetValueByCode("sfxmService", this.OrganizeId) == "" ? "cg" : _sysConfigRepo.GetValueByCode("sfxmService", this.OrganizeId);//CóKhôngmởHạng mục thông thường
            ViewBag.ISOpenSsyz = _sysConfigRepo.GetBoolValueByCode("ISOpenSsyzSwitch", this.OrganizeId, false);//Có mở khôngăn uốngY lệnh																					  
            ViewBag.ISWithRehabSuggestion = _sysConfigRepo.GetBoolValueByCode("IS_WithRehabSuggestion", this.OrganizeId, false);//Phục hồi chức năngĐơn thuốcCóKhôngTrích dẫnĐiều trịxâynghị
                                                                                                                                
            var frequencyStr = _sysConfigRepo.GetValueByCode("FrequencyStr", OrganizeId) ?? "";//Tạm thờiTần suấtchuỗi phútcắtcủaTần suấtMãchữkýchuỗiVDVD：00,01
            ViewBag.isqfswith = _sysConfigRepo.GetValueByCode("accountqfexecute_switch", OrganizeId);//nợ phíY lệnhmởlập、Thực hiệnCông tắc
            ViewBag.isopenPriorReview = _sysConfigRepo.GetValueByCode("OpenPriorReview", OrganizeId);//CóKhôngbậtviệctrướcPhê duyệtgiao diện
            ViewBag.openYjzxSwitch = _sysConfigRepo.GetValueByCode("openYjzxSwitch", OrganizeId); //Công tắc：kỹ thuật y họcQuản lýCóKhôngKích hoạt 
            if (!string.IsNullOrWhiteSpace(frequencyStr))
            {
                var FequencyList = frequencyStr.Split(',');
                var pcInfo = _iDoctorserviceDmnService.getpcInfoByCode(FequencyList[0], OrganizeId);
                if (pcInfo != null)
                {
                    ViewBag.pccode = pcInfo.yzpcCode;
                    ViewBag.zxcs = pcInfo.zxcs;
                    ViewBag.zxzq = pcInfo.zxzq;
                    ViewBag.zxzqdw = pcInfo.zxzqdw;
                    ViewBag.pcmc = pcInfo.yzpcmc;
                }
                else {
                    throw new Exception($"Không tìm thấy thông tin tần suất y lệnh hiệu lực (số hiệu tần suất y lệnh: {FequencyList[0]}), vui lòng đợi giây lát thử lại hoặc liên hệ quản trị viên.");
                }
                
            }
            #region Kháng sinhliên quan
            ViewBag.IsQyKssKz = _sysConfigRepo.GetBoolValueByCode("openKssQxSwitch", OrganizeId);//CóKhôngKích hoạtKháng sinh
            ViewBag.qxjb = "0";//Kháng sinhCấp quyền(Mặc định 0 khôngthuốc hạn chế)
            SysStaffVEntity staff = _sysStaffRepo.GetValidStaffByGh(UserIdentity.rygh, OrganizeId);
            if (staff != null)
            {
                switch (staff.zc)
                {
                    case "ys":
                        ViewBag.qxjb = "0";//Bác sĩ tương ứng khôngthuốc hạn chế 0
                        break;
                    case "zzys":
                        ViewBag.qxjb = "1";//Bác sĩ điều trị tương ứng thuốc hạn chế 1
                        break;
                    case "zrys":
                        ViewBag.qxjb = "2";//chínhnhiệmBác sĩ tương ứng đặcđặcdùngthuốc 2
                        break;
                    default:
                        ViewBag.qxjb = "0";//Bác sĩ tương ứng khôngthuốc hạn chế 0
                        break;
                }
            }
            #endregion Kháng sinhliên quan
            //Hình ảnhcấu hình
            ViewBag.PACSCode = _sysConfigRepo.GetValueByCode("PACSCode", OrganizeId);
            ViewBag.isOpenRationalUse = _sysConfigRepo.GetValueByCode("OpenRationalUse", OrganizeId);//CóKhôngbậthợplýdùngthuốc

            #region Tốc độ truyền
            var frds = _sysConfigRepo.GetValueByCode("frequencyRelDroppingSpeed", OrganizeId);
            frds = string.IsNullOrWhiteSpace(frds) ? "Truyền tĩnh mạch" : frds;
            ViewBag.frequencyRelDroppingSpeed = frds;
            #endregion

            //CóKhôngbậttrongthảo dượcY lệnh
            ViewBag.ISOpenzcyyzSwitch = _sysConfigRepo.GetBoolValueByCode("ISOpenzcyyzSwitch", OrganizeId, false);
            return View();
        }

        #region Chế độ xemreturn
        public IActionResult AdviceList()
        {
            ViewBag.isopenPriorReview = _sysConfigRepo.GetValueByCode("OpenPriorReview", OrganizeId);//CóKhôngbậtviệctrướcPhê duyệtgiao diện
            ViewBag.isOpenRationalUse = _sysConfigRepo.GetValueByCode("OpenRationalUse", OrganizeId);//CóKhôngbậthợplýdùngthuốc
            return View();
        }

        public IActionResult AdviceListLook()
        {
            return View();
        }
        public IActionResult ZyyzQueryAdviceList()
        {
            return View();
        }

        public IActionResult AdviceStop()
        {
            return View();
        }

        public IActionResult AdviceLeaveHospitalStop()
        {
            return View();
        }

        public IActionResult ContinuePrint()
        {
            return View();
        }

        public IActionResult TemplatePresForm()
        {
            return View();
        }
        public IActionResult HistoricalOrders()
        {
            return View();
        }
        public IActionResult PatientHistoricalOrders()
        {
            return View();
        }
        public IActionResult TransferWardStop()
        {
            return View();
        }
        #endregion

        #region Y lệnhMẫu

        public IActionResult saveAsTemplate(InpatientOrderPackageEntity mbObj, List<InpatientOrderPackageVO> mxList)
        {
            if (mbObj.tcfw == (int)EnumTcfw.Dept)//Khoa
            {
                mbObj.DeptCode = UserIdentity.DepartmentCode;

            }
            if (mbObj.tcfw == (int)EnumTcfw.Ward)//Khu bệnh
            {
                mbObj.WardCode = _iBaseDataDmnService.GetWardListByStaffGh(UserIdentity.rygh, OrganizeId).FirstOrDefault().bqCode;
            }
            _inpatientOrderPackageDmnService.saveAsTemplate(mbObj, mxList, OrganizeId);
            return Success();
        }


        /// <summary>
        /// mẫucây
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreeList(int tcfw, string yzlx)
        {
            var treeList = new List<TreeViewModel>();
            int linqyzlx = int.Parse(yzlx);
            var alldata = _inpatientOrderPackageRepo.IQueryable().Where(p => p.tcfw == tcfw && p.tclx == linqyzlx && p.OrganizeId == OrganizeId && p.zt == "1").OrderBy(a => a.CreateTime).ThenBy(a => a.tcmc).ToList();
            switch (tcfw)
            {
                case (int)EnumTcfw.Person:
                    alldata.Where(p => p.ysgh == UserIdentity.rygh);
                    break;
                case (int)EnumTcfw.Dept:
                    alldata.Where(p => p.DeptCode == UserIdentity.DepartmentCode);
                    break;
                case (int)EnumTcfw.Ward:
                    alldata.Where(p => p.WardCode == _iBaseDataDmnService.GetWardListByStaffGh(UserIdentity.rygh, OrganizeId).FirstOrDefault().bqCode);
                    break;
                case (int)EnumTcfw.Hosp:
                    alldata.Where(p => p.ysgh == "*" && p.DeptCode == "*" && p.WardCode == "*");
                    break;
                default: break;
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

        public IActionResult GetMBDetailByMainId(string Id)
        {
            var data = _inpatientOrderPackageDmnService.GetMBDetailByMainId(Id, OrganizeId);
            return Content(data.ToJson());
        }
        public IActionResult GetHistoricalOrders(string zyh, string type, string kssj, string jssj, string cqorls)
        {
            var data = _inpatientOrderPackageDmnService.GetHistoricalOrders(zyh, type, kssj, jssj, cqorls, OrganizeId);
            return Content(data.ToJson());
        }
        public IActionResult GetPatientQuery(string zyh, string type, string kssj, string jssj, string cqorls)
        {
            var data = _inpatientOrderPackageDmnService.GetPatientQuery(zyh, OrganizeId);
            return Content(data.ToJson());
        }
        public IActionResult GetMBDetailByDetailId(string idList)
        {
            var data = _inpatientOrderPackageDmnService.GetMBDetailByDetailId(idList, OrganizeId);
            return Content(data.ToJson());
        }

        #endregion

        #region Y lệnhTrích dẫn

        public IActionResult GetHistoricalOrdersById(string yzlistId)
        {
            var data = _inpatientOrderPackageDmnService.GetHistoricalOrdersById(yzlistId, OrganizeId);
            return Content(data.ToJson());
        }
        public IActionResult GetHistoricalRegist(string zyh, string curzyh, string yzlistId)
        {
            var data = _inpatientOrderPackageDmnService.GetHistoricalRegist(zyh, curzyh, yzlistId, OrganizeId, this.UserIdentity.UserCode);
            return Success("", data);
        }



        #endregion

        #region  Y lệnhQuản lý
        /// <summary>
        /// Y lệnhTra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public IActionResult AdviceGridView(Pagination pagination, AdviceListRequestVO req)
        {
            req.orgId = OrganizeId;
            var data = new
            {
                rows = _iDoctorserviceDmnService.AdviceGridView(pagination, req),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Dừng、Hủy bỏY lệnh
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="tzsj"></param>
        /// <returns></returns>
        public IActionResult StopSubmitForm(string yzId, DateTime? tzsj, string yzlx, string zyh, string iszt)
        {
            _iDoctorserviceDmnService.AdviceStop(yzId, tzsj, UserIdentity.rygh, OrganizeId, yzlx, zyh, iszt);
            return Success();
        }


        /// <summary>
        /// XóachưaPhê duyệtcủaY lệnh
        /// </summary>
        /// <param name="yzId"></param>
        /// <returns></returns>
        public IActionResult DelForm(string yzId, string yzlx, string zyh, string iszt, string conflinktoOR)
        {
            conflinktoOR = Newtouch.CIS.Compat.AppConfig.Get("EnableLinkToOR");
            _iDoctorserviceDmnService.AdviceDel(yzId, OrganizeId, yzlx, zyh, iszt, conflinktoOR);
            return Success();
        }

        /// <summary>
        /// Hủy DC
        /// </summary>
        /// <param name="yzId"></param>
        /// <returns></returns>
        public IActionResult advicedc(string yzId, string yzlx, string zyh, string isFeeGroup)
        {
            _iDoctorserviceDmnService.Advicedc(yzId, OrganizeId, yzlx, zyh, isFeeGroup);
            return Success();
        }

        /// <summary>
        /// Xuất việntoànDừng
        /// </summary>
        /// <returns></returns>
        public IActionResult AdviceLeaveHospitalStopSubmit(string zyh, DateTime tzsj)
        {
            _iDoctorserviceDmnService.AdviceLeaveHospitalStopSubmit(zyh, tzsj, OrganizeId, UserIdentity.rygh);
            return Success();
        }

        /// <summary>
        /// chuyển khutoànDừng
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult AdviceTransferWardStopSubmit(string zyh, string bq, DateTime? kssj)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("ThiếuSố nhập viện");
            }
            if (string.IsNullOrWhiteSpace(bq) || bq == "==Vui lòng chọn==")
            {
                throw new FailedException("ThiếuKhu bệnh");
            }
            if (kssj == DateTime.MaxValue || kssj == DateTime.MinValue || kssj == null)
            {
                throw new FailedException("ThiếuThời gian bắt đầu");
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
        /// đãDừngY lệnhsao chépTạo mới
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="yzId"></param>
        /// <param name="yzlb"></param>
        /// <returns></returns>
        public IActionResult RegistTzyz(string zyh, string yzId, string yzlb)
        {
            _iDoctorserviceDmnService.RegistTzyz(yzId, OrganizeId, yzlb, zyh);
            return Success();
        }

        #endregion

        #region Y lệnhGói

        /// <summary>
        /// kiểm trahiện tạibệnh ngườiCóKhôngtồn tạiởchưaPhê duyệtcủaY lệnh dài hạn
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult boolwsh(string zyh)
        {
            int wshzt = (int)EnumYzzt.Ds;
            var wsh = _inpatientLongTermOrderRepo.IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == OrganizeId && p.zt == "1" && p.yzzt == wshzt);
            if (wsh != null && wsh.Count() > 0)
            {
                return Error("Tồn tại y lệnh chưa duyệt");
            }
            return Success();
        }

        /// <summary>
        /// Quay lạigóiphạm vi
        /// </summary>
        /// <returns></returns>
        public IActionResult GettcfwContainWard()
        {
            SysStaffVEntity sysStaffEntity = _sysStaffRepo.GetValidStaffByGh(this.UserIdentity.rygh, this.OrganizeId);
            int mbqx = Convert.ToInt32((sysStaffEntity.mbqx == null || sysStaffEntity.mbqx == "") ? "1" : sysStaffEntity.mbqx);
            var data = new List<object>();
            var qxz = new
            {
                id = "",
                text = "==Vui lòng chọn=="
            };
            data.Add(qxz);
            bool containbq = _sysConfigRepo.GetBoolValueByCode("DoctorServiceTemplate", OrganizeId).ToBool();
            foreach (EnumTcfw item in Enum.GetValues(typeof(EnumTcfw)))
            {
                if ((!containbq) && (item == EnumTcfw.Ward || mbqx < (int)item))
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
        /// chọngóichi tiết
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTemplatePresDetailByMbId(string mbId)
        {
            return View();
        }

        /// <summary>
        /// Y lệnhTra cứutạoKiểu y lệnhhộp thả xuốngkhi，cấu hìnhcủaXét nghiệmKiểm traHiển thị
        /// </summary>
        /// <returns></returns>
        public IActionResult GetyzlxContain()
        {
            var data = new List<object>();
            var qxz = new
            {
                id = "",
                text = "Tất cả"
            };
            data.Add(qxz);
            bool containbq = _sysConfigRepo.GetBoolValueByCode("OpenZYJyJcSwitch", OrganizeId, false).ToBool();
            //ăn uốngY lệnh
            bool containss = _sysConfigRepo.GetBoolValueByCode("ISOpenSsyzSwitch", OrganizeId, false).ToBool();
            //trongthảo dượcY lệnh
            bool containzcy = _sysConfigRepo.GetBoolValueByCode("ISOpenzcyyzSwitch", OrganizeId, false).ToBool();
            foreach (EnumYzlx item in Enum.GetValues(typeof(EnumYzlx)))
            {
                if (((!containbq) && (item == EnumYzlx.jc || item == EnumYzlx.jy)) || item == EnumYzlx.oper || (!containss && item == EnumYzlx.ssyz) || (!containzcy && item == EnumYzlx.zcy))
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

        #endregion

        #region Quyết toánThông tin
        public IActionResult SettInfoShow()
        {
            return View();
        }

        #endregion


        #region  lis pacs Báo cáo
        /// <summary>
        /// //TheoSố nhập việnLấyLIS/PACSbáo cáoHoàn thànhSố lượng
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult CountLISztzy(string zyh)
        {
            var num = _iDoctorserviceDmnService.CountLISztzy(OrganizeId, zyh);
            var data = new
            {
                num = num
            };
            return Content(data.ToJson());
        }
        #endregion
    }
}