using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;













namespace Newtouch.Sett.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("OutpatientManage")]

    public class OutpatientAccountingController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IOutPatientDmnService _outPatientDmnService;
        private readonly IOutPatChargeApp _outPatChargeApp;
        private readonly IOutPatChargeDmnService _outPatChargeDmSer; // thu phí đăng ký khámService 
        private readonly IRefundDmnService _refundService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IOutPatChargeDmnService _outChargeDmnService;
        private readonly ISyncTreatmentServiceRecordDmnService _syncTreatmentServiceRecordDmnService;
        private readonly ISyncTreatmentServiceRecordRepo _syncTreatmentServiceRecordRepo;
        private readonly IOutPatChargeDmnService _outPatChargeDmnService;

        public OutpatientAccountingController(IOutPatientDmnService outPatientDmnService, IOutPatChargeApp outPatChargeApp, IOutPatChargeDmnService outPatChargeDmSer, IRefundDmnService refundService, ISysConfigRepo sysConfigRepo, ISysUserDmnService sysUserDmnService, IOutPatChargeDmnService outChargeDmnService, ISyncTreatmentServiceRecordDmnService syncTreatmentServiceRecordDmnService, ISyncTreatmentServiceRecordRepo syncTreatmentServiceRecordRepo, IOutPatChargeDmnService outPatChargeDmnService)
        {
            _outPatientDmnService = outPatientDmnService;
            _outPatChargeApp = outPatChargeApp;
            _outPatChargeDmSer = outPatChargeDmSer;
            _refundService = refundService;
            _sysConfigRepo = sysConfigRepo;
            _sysUserDmnService = sysUserDmnService;
            _outChargeDmnService = outChargeDmnService;
            _syncTreatmentServiceRecordDmnService = syncTreatmentServiceRecordDmnService;
            _syncTreatmentServiceRecordRepo = syncTreatmentServiceRecordRepo;
            _outPatChargeDmnService = outPatChargeDmnService;
        }

        #region Khởi tạo trang

        /// <summary>
        /// thêm mớiđăng ký
        /// </summary>
        /// <returns></returns>

        public IActionResult SysPatEntitiesblhView(string from = "")
        {
            ViewBag.from = from;
            return View();
        }

        public override IActionResult Index()
        {
            ViewBag.IsUpperLimitReminder = _sysConfigRepo.GetByCode("xt_UpperLimitReminder", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetValueByCode("xt_UpperLimitReminder", this.OrganizeId);
            ViewBag.IsBlh = _sysConfigRepo.GetByCode("IsBlh", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("IsBlh", OrganizeId).Value;
            ViewBag.sfxm_dj = _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId).Value;
            var duty = _sysUserDmnService.CheckStaffIsBelongDuty(this.UserIdentity.StaffId, "RehabDoctor");
            if (duty)
            {
                ViewBag.CurYsStaffId = UserIdentity.StaffId;
                ViewBag.CurYs = UserIdentity.rygh;
                ViewBag.CurYsmc = UserIdentity.UserName;
                ViewBag.CurKs = UserIdentity.DepartmentCode;
                ViewBag.CurKsmc = UserIdentity.DepartmentName;
            }

            return View();
        }

        public IActionResult AddOutpatientInfo()
        {
            // ReSharper disable once Mvc.ViewNotResolved
            return View();
        }

        /// <summary>
        /// ghi sổkhoản mụctrang chi tiết
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientAccountingDetail()
        {
            return View();
        }

        public IActionResult AccountingInOptima()
        {
            ViewBag.IsUpperLimitReminder = _sysConfigRepo.GetByCode("xt_UpperLimitReminder", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetValueByCode("xt_UpperLimitReminder", this.OrganizeId);
            ViewBag.sfxm_dj = _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId).Value;
            var duty = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "RehabDoctor");
            if (duty)
            {
                ViewBag.CurYsStaffId = UserIdentity.StaffId;
                ViewBag.CurYs = UserIdentity.rygh;
                ViewBag.CurYsmc = UserIdentity.UserName;
                ViewBag.CurKs = UserIdentity.DepartmentCode;
                ViewBag.CurKsmc = UserIdentity.DepartmentName;
            }
            return View();
        }

        /// <summary>
        /// thu phí ngoại trú
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatCharge()
        {
            ViewBag.IsBlh = _sysConfigRepo.GetByCode("IsBlh", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("IsBlh", OrganizeId).Value;
            ViewBag.sfxm_dj = _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId).Value;
            var duty = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "RehabDoctor");
            if (duty)
            {
                ViewBag.CurYsStaffId = UserIdentity.StaffId;
                ViewBag.CurYs = UserIdentity.rygh;
                ViewBag.CurYsmc = UserIdentity.UserName;
                ViewBag.CurKs = UserIdentity.DepartmentCode;
                ViewBag.CurKsmc = UserIdentity.DepartmentName;
            }
            return View();
        }

        #endregion

        #region Ghi sổ ngoại trú Vision-1.1 phiên bản đầu tiên của ghi sổ ngoại trú
        /// <summary>
        /// tảithông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="blh">số hồ sơ bệnh án</param>
        /// <returns></returns>
        public IActionResult GetOutpatientBasicInfo(string IsBlh, string mzh)
        {
            IsBlh = IsBlh == "true" ? _sysConfigRepo.GetByCode("IsBlh", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("IsBlh", OrganizeId).Value : null;
            var patChargeInfo = _outPatChargeApp.GetOutPatBasicInfoInAcc(IsBlh, mzh);
            if (patChargeInfo.Count == 1)
            {
                return Success("Gửi thành công", patChargeInfo.FirstOrDefault());
            }
            else if (patChargeInfo.Count > 1)
            {
                return Success("Gửi thành công", patChargeInfo);
            }
            return Error("Thất bại");
        }

        /// <summary>
        /// lưungoại trúghi sổ khám
        /// </summary>
        /// <param name="bacDto"></param>
        /// <param name="xmList"></param>
        /// <returns></returns>
        public IActionResult SaveAcountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> xmList)
        {

            _outPatientDmnService.SaveOutpatientAccountInfo(bacDto, xmList, OrganizeId);
            return Success("Gửi thành công");
        }

        public IActionResult PatSearchInfo(Pagination pagination, string blh, string xm,string zjh=null,string zjlx=null)
        {
            var data = new
            {
                rows = _refundService.GetBasicInfoSearchListInRegister(pagination, blh, xm, OrganizeId,zjh,zjlx),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        #endregion

        #region OptimaGhi sổ Vision-1.2 phiên bản thứ hai của ghi sổ ngoại trú（bao gồm ghi sổ ngoại trú và ghi sổ nhập viện）
        /// <summary>
        /// lấynơicóbản ghi trị liệu bệnh nhân
        /// </summary>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetIssueGridJson(string type, string kssj, string jssj)
        {
            if (type == "2" && string.IsNullOrWhiteSpace(kssj) && string.IsNullOrWhiteSpace(jssj))
            {
                throw new FailedException("Ngày bắt đầu và ngày kết thúc không được cùng trống");
            }
            var rygh = this.UserIdentity.rygh;
            var data = _syncTreatmentServiceRecordRepo.GetUnitList(OrganizeId, type, rygh, kssj, jssj).Where(p => p.hidden == 1);
            return Content(data.ToJson());
        }

        /// <summary>
        /// tảithông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="mzh">số khám ngoại trú</param>
        /// <returns></returns>
        public IActionResult GetpatientInfoInOptima(string admsnum, string type, string kssj, string jssj)
        {
            var resultDto = new OutpatOptimAccInfoDto()
            {
                OutpatAccInfoDto = null,
                OptimAccInfoDto = null,
            };
            string rygh = UserIdentity.rygh;
            resultDto.OutpatAccInfoDto = _outChargeDmnService.GetPatInfoInOptima(admsnum, OrganizeId, type);
            if (type == "Ngoại trú")
            {
                resultDto.OptimAccInfoDto = _outChargeDmnService.GetOutAccountInfo(admsnum, OrganizeId, kssj, jssj, rygh).OrderBy(p => p.jzsj).ToList();
            }
            else if (type == "Nhập viện")
            {

                resultDto.OptimAccInfoDto = _outChargeDmnService.GetintAccountInfo(admsnum, OrganizeId, kssj, jssj, rygh).OrderBy(p => p.jzsj).ToList();
            }
            return Success("Gửi thành công", resultDto);
        }

        /// <summary>
        /// tra cứuđồng bộbản ghi trị liệu
        /// </summary>
        /// <returns></returns>
        public IActionResult GetUnConfirmedSyncedTreatmentRecord(string zyh, string mzh)
        {
            var admsnum = mzh;
            var resultDto = new OutpatOptimInfoDto()
            {
                OutpatAccInfoDto = null,
                SyncTreatmentServiceRecordVO = null,
            };

            var type = "Ngoại trú";
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                type = "Nhập viện";
                admsnum = zyh;
            }
            resultDto.OutpatAccInfoDto = _outChargeDmnService.GetPatInfoInOptima(admsnum, OrganizeId, type);
            resultDto.SyncTreatmentServiceRecordVO = (List<SyncTreatmentServiceRecordVO>)_syncTreatmentServiceRecordDmnService.GetList(OrganizeId, 1, mzh: mzh, zyh: zyh, zlsgh: UserIdentity.rygh);

            return Success(null, resultDto);
        }
        public IActionResult SaveOptimaAcountInfo(string patientType, OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> xmList)
        {
            _outPatientDmnService.CommitAccounting(patientType, bacDto, xmList, OrganizeId);
            return Success("Gửi thành công");
        }
        #endregion

        #region Thu phí ngoại trú Vision-1.3 phiên bản thứ ba của ghi sổ ngoại trú
        /// <summary>
        /// lấynơicóbệnh nhânđăng ký khámthông tin
        /// </summary>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetRegisterGridJson(string begindate, string enddate)
        {
            var data = _outChargeDmnService.GetRegisterGridJson(begindate, enddate, OrganizeId, UserIdentity.UserCode);
            return Content(data.ToJson());
        }

        /// <summary>
        /// lấybản ghi lịch sử ghi sổ
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetpatientAccountInfo(string IsBlh, string mzh)
        {
            var resultDto = new OutpatOptimAccInfoDto()
            {
                OutpatAccInfoDto = null,
                OptimAccInfoDto = null,
            };
            var paiInfoList = _outPatChargeApp.GetOutPatBasicInfoInAcc(IsBlh, mzh);
            if (paiInfoList != null && paiInfoList.Count > 0)
            {
                if (paiInfoList.Count > 1)
                {
                    return Success("Gửi thành công", paiInfoList);
                }
                else
                {
                    resultDto.OutpatAccInfoDto = paiInfoList.FirstOrDefault();
                    resultDto.OptimAccInfoDto = _outPatChargeApp.GetInfo(resultDto.OutpatAccInfoDto.mzh);
                    return Success("Gửi thành công", resultDto);
                }
            }
            return Error("Tra cứu thất bại");
        }



        /// <summary>
        /// lưungoại trúghi sổ khám
        /// </summary>
        /// <param name="bacDto"></param>
        /// <param name="xmList"></param>
        /// <returns></returns>
        public IActionResult SavepatientAcountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> xmList)
        {
            _outPatientDmnService.SavepatientAccountInfo(bacDto, xmList, OrganizeId);
            return Success("Gửi thành công");
        }
        #endregion

        #region Ghi sổ ngoại trú，phiên bản thứ tư
        /// <summary>
        /// lấybản ghi lịch sử ghi sổ
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetpatientAccountInfoV4(string mzh)
        {
            var resultDto = new OutpatOptimAccInfoDto()
            {
                OutpatAccInfoDto = null,
                OptimAccInfoDto = null,
            };
            var paiInfoList = _outPatChargeApp.GetOutPatBasicInfoInAcc("OFF", mzh);
            if (paiInfoList != null && paiInfoList.Count > 0)
            {
                if (paiInfoList.Count > 1)
                {
                    return Success("Gửi thành công", paiInfoList);
                }
                else
                {
                    resultDto.OutpatAccInfoDto = paiInfoList.FirstOrDefault();
                    resultDto.OptimAccInfoDto = _outPatChargeDmnService.GetpatientAccountInfoV4(mzh, OrganizeId);
                    return Success("Gửi thành công", resultDto);
                }
            }
            return Error("Tra cứu thất bại");
        }
        #endregion
    }
}
