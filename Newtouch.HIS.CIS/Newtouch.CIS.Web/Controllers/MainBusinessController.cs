using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Application.Interface;
using Newtouch.Domain.DTO;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class MainBusinessController : OrgControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysOrganizeDmnService _organizeDmnService;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IPrescriptionDmnService _prescriptionDmnService;
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly IOutpatientCmmManagerApp _outpatientCmmManagerApp;


        public override ActionResult Index()
        {
            //Người đăng nhập có phải là y tá không
            ViewBag.IsNurse = _sysUserDmnService.CheckStaffIsBelongDuty(this.UserIdentity.StaffId, "Nurse");

            //Công tắc: y tá nhập dấu hiệu sinh tồn
            ViewBag.IsVitalSignsSwitch = _sysConfigRepo.GetValueByCode("vitalSignsSwitch", this.OrganizeId);
            //Công tắc: có mở xét nghiệm kiểm tra không
            ViewBag.IsOpenJyJcSwitch = _sysConfigRepo.GetBoolValueByCode("openJyJcSwitch", this.OrganizeId);
            //Công tắc: ngoại trú có mở đơn thuốc phục hồi chức năng không
            ViewBag.ISOpenKfcf = _sysConfigRepo.GetBoolValueByCode("openKfcf", this.OrganizeId);
            //Công tắc: ngoại trú có mở đơn thuốc hạng mục thông thường không
            ViewBag.ISOpenCgxmcf = _sysConfigRepo.GetBoolValueByCode("openCgxmcf", this.OrganizeId);
            ViewBag.isOpenRationalUse = _sysConfigRepo.GetValueByCode("OpenRationalUse", OrganizeId);//Có bật sử dụng thuốc hợp lý không
            //Công tắc: ngoại trú có mở đơn thuốc Đông y không
            ViewBag.ISOpenZycf = _sysConfigRepo.GetBoolValueByCode("openZycf", this.OrganizeId);
            ViewBag.ISOpenZycfnew = _sysConfigRepo.GetBoolValueByCode("openZycfnew", this.OrganizeId);
            //Công tắc: ngoại trú có mở vật tư y tế không
            ViewBag.ISOpenWzhc = _sysConfigRepo.GetBoolValueByCode("openWzhccf", this.OrganizeId);
            //Công tắc: thuốc có liên kết tồn kho nhà thuốc không
            ViewBag.ISMedicineSearchRelatedKC = _sysConfigRepo.GetBoolValueByCode("IS_MedicineSearchRelatedKC", OrganizeId);
            //Công tắc in đơn thuốc phục hồi chức năng
            ViewBag.ISPrintRehabPres = _sysConfigRepo.GetBoolValueByCode("IS_PrintRehabPres", OrganizeId) ?? true;
            //Công tắc in phiếu điều trị
            ViewBag.ISPrintRehabTreatment = _sysConfigRepo.GetBoolValueByCode("IS_PrintRehabTreatment", OrganizeId);
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

            //Công tắc: hẹn đăng ký khám
            ViewBag.ISOpenBespeakRegister = _sysConfigRepo.GetBoolValueByCode("IS_BespeakRegister", OrganizeId);
            var defualtFrequency = _sysConfigRepo.GetValueByCode("RehabItemDefaultFrequency", OrganizeId);
            if (!string.IsNullOrWhiteSpace(defualtFrequency))
            {
                var arr = defualtFrequency.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length == 2)
                {
                    ViewBag.RehabItemDefaultFrequencyName = arr[0];
                    ViewBag.RehabItemDefaultFrequencyCode = arr[1];
                }
            }
            //Kiểm soát bảo hiểm y tếcode
            ViewBag.ControlbrxzCode = _sysConfigRepo.GetValueByCode("ControlbrxzCode", OrganizeId);
            //Kiểm soát cách dùng tiêm
            ViewBag.ControlzsyfCode = _sysConfigRepo.GetValueByCode("zsyfpz", OrganizeId);
            //Kiểm soát cách dùng khí dung
            ViewBag.ControlwhyfCode = _sysConfigRepo.GetValueByCode("whyfpz", OrganizeId);


            //Cấu hình hình ảnh
            ViewBag.PACSCode = _sysConfigRepo.GetValueByCode("PACSCode", OrganizeId);
            ViewBag.yysz_hyfqURL = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("yysz_hyfqURL");
            //Cấu hình vị trí chương trình xem hình ảnh
            ViewBag.exelink_pacs = _sysConfigRepo.GetValueByCode("exelink_pacs", OrganizeId);
            //Y tế từ xa
            ViewBag.OrganizeCodeSd = _organizeDmnService.GetCodeByOrgId(OrganizeId);
            //ViewBag.OrganizeCodeSd = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("OrganizeCodeSd");
            ViewBag.RemoteTreatRPTURL = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("RemoteTreatRPTURL");
          

            #region 滴速
            var frds = _sysConfigRepo.GetValueByCode("frequencyRelDroppingSpeed", OrganizeId);
            frds = string.IsNullOrWhiteSpace(frds) ? "静滴" : frds;
            ViewBag.frequencyRelDroppingSpeed = frds;
            #endregion
            return View();
        }

        #region 秦皇岛三期
        /// <summary>
        /// Bệnh nhân bảo hiểm y tế duyệt hạng mục thu phí có bao gồm tự trả không
        /// </summary>
        /// <param name="sfxmCode"></param>
        /// <returns></returns>
        public ActionResult ValidateMedicalInsurance(string cfDto)
        {
            var cfDtoList = Tools.Json.ToList<PrescriptionHtmlDTO>(cfDto);
            Dictionary<int, string> sfxmdic = new Dictionary<int, string>();
            foreach (var item in cfDtoList)
            {

                var sfxmlist = "";
                foreach (var cf in item.cfHtml)
                {
                    if (cf.sfbz == false)
                    {
                        sfxmlist = sfxmlist + "," + (cf.xmCode ?? cf.ypCode);
                        var cflx = item.cflx == "kfcf" ? (int)EnumCflx.RehabPres : item.cflx == "cgxmcf" ? (int)EnumCflx.RegularItemPres : item.cflx == "xycf" ? (int)EnumCflx.WMPres : item.cflx == "zycf" ? cf.cflx = (int)EnumCflx.TCMPres : item.cflx == "jycf" ? (int)EnumCflx.InspectionPres : item.cflx == "jccf" ? (int)EnumCflx.ExaminationPres : 0;
                        if (sfxmdic.ContainsKey(cflx))
                        {
                            sfxmdic.Remove(cflx);
                        }
                        sfxmdic.Add(cflx, sfxmlist);
                    }
                }
            }
            var noybdmList = _prescriptionDmnService.ValidateMedicalInsurance(sfxmdic, OrganizeId);
            var noybdstr = string.Join(",", noybdmList.ToArray());
            return Success("", noybdstr);
        }
        #endregion

        #region 中医馆

        /// <summary>
        /// Khung nhìn một phần của phòng khám Đông y
        /// </summary>
        /// <returns></returns>
        public ActionResult TcmHis()
        {
            return View();
        }

        /// <summary>
        /// Đẩy thông tin bệnh nhân
        /// </summary>
        /// <param name="jzxx"></param>
        /// <returns></returns>
        public ActionResult TcmHis01(TreatEntityObj jzxx)
        {
            var result = _outpatientCmmManagerApp.TcmHis01(jzxx, OrganizeId, UserIdentity.UserCode);
            return "1".Equals(result.code) ? Success() : Error(result.desc);
        }

        /// <summary>
        /// Mô-đun tích hợp bệnh án điện tử
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="jzys">Mã nhân viên bác sĩ khám</param>
        /// <returns></returns>
        public ActionResult GetIntegrateEmrUrl(string mzh, string jzys)
        {
            string response;
            var result = _outpatientCmmManagerApp.GetIntegrateEmrUrl(mzh, jzys, OrganizeId, out response);
            return string.IsNullOrWhiteSpace(result) ? Success("", response) : Error(result);
        }

        /// <summary>
        /// Mô-đun biện chứng luận trị
        /// </summary>
        /// <param name="aeRequest"></param>
        /// <returns></returns>
        public ActionResult GetIntegrateAeUrl(GetAeRequestDto aeRequest)
        {
            string response;
            if (aeRequest == null) aeRequest = new GetAeRequestDto();
            aeRequest.organizeId = OrganizeId;
            if (string.IsNullOrWhiteSpace(aeRequest.jzys)) aeRequest.jzys = UserIdentity.UserCode;
            var result = _outpatientCmmManagerApp.GetIntegrateAeUrl(aeRequest, out response);
            return string.IsNullOrWhiteSpace(result) ? Success("", response) : Error(result);
        }

        /// <summary>
        /// Trích xuất bệnh án điện tử
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <returns></returns>
        public ActionResult ExtractIntegrateEmr(string mzh)
        {
            var result = _outpatientCmmManagerApp.ExtractIntegrateEmr(mzh, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Trích xuất thông tin chẩn đoán
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <returns></returns>
        public ActionResult ExtractIntegrateDiagnosis(string mzh)
        {
            var result = _outpatientCmmManagerApp.ExtractIntegrateDiagnosis(mzh, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Hộp thoại phòng khám Đông y
        /// </summary>
        /// <returns></returns>
        public ActionResult CmmAlertForm()
        {
            return View();
        }

        /// <summary>
        /// Đẩy thảo dược
        /// </summary>
        /// <returns></returns>
        public ActionResult PushMedicineInfo()
        {
            var result = _outpatientCmmManagerApp.PushMedicineInfo(OrganizeId, UserIdentity.UserCode);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Trích xuất đơn thuốc
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="mzzybz">Dấu hiệu ngoại trú/nhập viện 0-Kho thuốc  1-Khám ngoại trú  2-Nhập viện  3-Chung(Lấy đơn vị ngoại trú) </param>
        /// <returns></returns>
        public ActionResult ExtractIntegrateRp(string mzh, string mzzybz)
        {
            var result = _outpatientCmmManagerApp.ExtractIntegrateRp(mzh, OrganizeId, mzzybz);
            return Success("", result.ToJson());
        }

        /// <summary>
        /// Mô-đun hệ thống kho tri thức
        /// </summary>
        /// <returns></returns>
        public ActionResult GetIntegrateKUrl()
        {
            var response = "";
            var result = _outpatientCmmManagerApp.GetIntegrateKUrl(out response);
            return string.IsNullOrWhiteSpace(result) ? Success("", response) : Error(result);
        }

        /// <summary>
        /// Mô-đun tích hợp trị vị bệnh
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="mzh"></param>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <returns></returns>
        public ActionResult GetIntegrateHEALUrl(string blh, string mzh, string zjlx, string zjh)
        {
            string response;
            var result = _outpatientCmmManagerApp.GetIntegrateHEALUrl(blh, mzh, zjlx, zjh, OrganizeId, out response);
            return string.IsNullOrWhiteSpace(result) ? Success("", response) : Error(result);
        }

        /// <summary>
        /// Lấy thông tin cơ bản của bệnh nhân
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="mzh"></param>
        /// <param name="jzys"></param>
        /// <returns></returns>
        public JsonResult GetIntegrateRcRequsetParam(string blh, string mzh, string jzys)
        {
            if (string.IsNullOrWhiteSpace(jzys)) jzys = UserIdentity.UserCode;
            var result = _outpatientCmmManagerApp.GetIntegrateRCRequsetParam(blh, mzh, jzys, OrganizeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}