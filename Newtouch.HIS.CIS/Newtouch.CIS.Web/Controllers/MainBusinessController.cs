using System;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.Application.Interface;
using Newtouch.Domain.DTO;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;

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

        public MainBusinessController(ISysConfigRepo sysConfigRepo, ISysOrganizeDmnService organizeDmnService, ISysUserDmnService sysUserDmnService, IPrescriptionDmnService prescriptionDmnService, ISysStaffRepo sysStaffRepo, IOutpatientCmmManagerApp outpatientCmmManagerApp)
        {
            this._sysConfigRepo = sysConfigRepo;
            this._organizeDmnService = organizeDmnService;
            this._sysUserDmnService = sysUserDmnService;
            this._prescriptionDmnService = prescriptionDmnService;
            this._sysStaffRepo = sysStaffRepo;
            this._outpatientCmmManagerApp = outpatientCmmManagerApp;
        }


        public override IActionResult Index()
        {
            //Đăng nhập ngườiCóKhôngCóĐiều dưỡng
            ViewBag.IsNurse = _sysUserDmnService.CheckStaffIsBelongDuty(this.UserIdentity.StaffId, "Nurse");

            //Công tắc:Điều dưỡngnhậpdấu hiệu sinh tồn
            ViewBag.IsVitalSignsSwitch = _sysConfigRepo.GetValueByCode("vitalSignsSwitch", this.OrganizeId);
            //Công tắc:Có mở khôngXét nghiệmKiểm tra
            ViewBag.IsOpenJyJcSwitch = _sysConfigRepo.GetBoolValueByCode("openJyJcSwitch", this.OrganizeId);
            //Công tắc:Ngoại trúCó mở khôngPhục hồi chức năngĐơn thuốc
            ViewBag.ISOpenKfcf = _sysConfigRepo.GetBoolValueByCode("openKfcf", this.OrganizeId);
            //Công tắc:Ngoại trúCó mở khôngHạng mục thông thườngĐơn thuốc
            ViewBag.ISOpenCgxmcf = _sysConfigRepo.GetBoolValueByCode("openCgxmcf", this.OrganizeId);
            ViewBag.isOpenRationalUse = _sysConfigRepo.GetValueByCode("OpenRationalUse", OrganizeId);//CóKhôngbậthợplýdùngthuốc
            //Công tắc:Ngoại trúCó mở khôngThuốc Đông yĐơn thuốc
            ViewBag.ISOpenZycf = _sysConfigRepo.GetBoolValueByCode("openZycf", this.OrganizeId);
            ViewBag.ISOpenZycfnew = _sysConfigRepo.GetBoolValueByCode("openZycfnew", this.OrganizeId);
            //Công tắc:Ngoại trúCó mở khôngydùngVật tư tiêu hao
            ViewBag.ISOpenWzhc = _sysConfigRepo.GetBoolValueByCode("openWzhccf", this.OrganizeId);
            //Công tắc:ThuốcCóKhôngNhà thuốc liên quanTồn kho
            ViewBag.ISMedicineSearchRelatedKC = _sysConfigRepo.GetBoolValueByCode("IS_MedicineSearchRelatedKC", OrganizeId);
            //Phục hồi chức năngĐơn thuốcInCông tắc
            ViewBag.ISPrintRehabPres = _sysConfigRepo.GetBoolValueByCode("IS_PrintRehabPres", OrganizeId, true);
            //Điều trịphiếuInCông tắc
            ViewBag.ISPrintRehabTreatment = _sysConfigRepo.GetBoolValueByCode("IS_PrintRehabTreatment", OrganizeId);
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

            //Công tắc:đặt hẹnđăng ký khám
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
            //Bảo hiểm y tếđiều khiểncode
            ViewBag.ControlbrxzCode = _sysConfigRepo.GetValueByCode("ControlbrxzCode", OrganizeId);
            //Lưu ýxạCách dùngđiều khiển
            ViewBag.ControlzsyfCode = _sysConfigRepo.GetValueByCode("zsyfpz", OrganizeId);
            //khí dungCách dùngđiều khiển
            ViewBag.ControlwhyfCode = _sysConfigRepo.GetValueByCode("whyfpz", OrganizeId);


            //Hình ảnhcấu hình
            ViewBag.PACSCode = _sysConfigRepo.GetValueByCode("PACSCode", OrganizeId);
            ViewBag.yysz_hyfqURL = Newtouch.CIS.Compat.AppConfig.Get("yysz_hyfqURL");
            //Hình ảnhgọiđọctrìnhthứ tựVị trícấu hình
            ViewBag.exelink_pacs = _sysConfigRepo.GetValueByCode("exelink_pacs", OrganizeId);
            //xatrìnhy tế
            ViewBag.OrganizeCodeSd = _organizeDmnService.GetCodeByOrgId(OrganizeId);
            //ViewBag.OrganizeCodeSd = Newtouch.CIS.Compat.AppConfig.Get("OrganizeCodeSd");
            ViewBag.RemoteTreatRPTURL = Newtouch.CIS.Compat.AppConfig.Get("RemoteTreatRPTURL");
          

            #region Tốc độ truyền
            var frds = _sysConfigRepo.GetValueByCode("frequencyRelDroppingSpeed", OrganizeId);
            frds = string.IsNullOrWhiteSpace(frds) ? "Truyền tĩnh mạch" : frds;
            ViewBag.frequencyRelDroppingSpeed = frds;
            #endregion
            return View();
        }

        #region Giai đoạn 3 Tần Hoàng Đảo
        /// <summary>
        /// Bảo hiểm y tếBệnh nhânPhê duyệtMục thu phíCóKhôngchứatự chi trả
        /// </summary>
        /// <param name="sfxmCode"></param>
        /// <returns></returns>
        public IActionResult ValidateMedicalInsurance(string cfDto)
        {
            var cfDtoList = Newtouch.CIS.Compat.Tools.Json.ToList<PrescriptionHtmlDTO>(cfDto);
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

        #region Phòng khám YHCT

        /// <summary>
        /// trongyquánbộphútxemhình
        /// </summary>
        /// <returns></returns>
        public IActionResult TcmHis()
        {
            return View();
        }

        /// <summary>
        /// đẩyThông tin bệnh nhân
        /// </summary>
        /// <param name="jzxx"></param>
        /// <returns></returns>
        public IActionResult TcmHis01(TreatEntityObj jzxx)
        {
            var result = _outpatientCmmManagerApp.TcmHis01(jzxx, OrganizeId, UserIdentity.UserCode);
            return "1".Equals(result.code) ? Success() : Error(result.desc);
        }

        /// <summary>
        /// điện tửHồ sơtậpthànhmẫukhối
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="jzys">Khám bệnhBác sĩMã nhân viên</param>
        /// <returns></returns>
        public IActionResult GetIntegrateEmrUrl(string mzh, string jzys)
        {
            string response;
            var result = _outpatientCmmManagerApp.GetIntegrateEmrUrl(mzh, jzys, OrganizeId, out response);
            return string.IsNullOrWhiteSpace(result) ? Success("", response) : Error(result);
        }

        /// <summary>
        /// biệnchứngluậnđiều trịmẫukhối
        /// </summary>
        /// <param name="aeRequest"></param>
        /// <returns></returns>
        public IActionResult GetIntegrateAeUrl(GetAeRequestDto aeRequest)
        {
            string response;
            if (aeRequest == null) aeRequest = new GetAeRequestDto();
            aeRequest.organizeId = OrganizeId;
            if (string.IsNullOrWhiteSpace(aeRequest.jzys)) aeRequest.jzys = UserIdentity.UserCode;
            var result = _outpatientCmmManagerApp.GetIntegrateAeUrl(aeRequest, out response);
            return string.IsNullOrWhiteSpace(result) ? Success("", response) : Error(result);
        }

        /// <summary>
        /// nhắclấyđiện tửHồ sơ
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <returns></returns>
        public IActionResult ExtractIntegrateEmr(string mzh)
        {
            var result = _outpatientCmmManagerApp.ExtractIntegrateEmr(mzh, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// nhắclấyChẩn đoánthông tin
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <returns></returns>
        public IActionResult ExtractIntegrateDiagnosis(string mzh)
        {
            var result = _outpatientCmmManagerApp.ExtractIntegrateDiagnosis(mzh, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// trongyquánbậtkhung
        /// </summary>
        /// <returns></returns>
        public IActionResult CmmAlertForm()
        {
            return View();
        }

        /// <summary>
        /// đẩythảo dược
        /// </summary>
        /// <returns></returns>
        public IActionResult PushMedicineInfo()
        {
            var result = _outpatientCmmManagerApp.PushMedicineInfo(OrganizeId, UserIdentity.UserCode);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// nhắclấyĐơn thuốc
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="mzzybz">Dấu hiệu ngoại trú/nội trú 0-Kho thuốc  1-Ngoại trú  2-Nhập viện  3-Chung(lấyĐơn vị ngoại trú) </param>
        /// <returns></returns>
        public IActionResult ExtractIntegrateRp(string mzh, string mzzybz)
        {
            var result = _outpatientCmmManagerApp.ExtractIntegrateRp(mzh, OrganizeId, mzzybz);
            return Success("", result.ToJson());
        }

        /// <summary>
        /// biếtbiếtkhoHệ thốngmẫukhối
        /// </summary>
        /// <returns></returns>
        public IActionResult GetIntegrateKUrl()
        {
            var response = "";
            var result = _outpatientCmmManagerApp.GetIntegrateKUrl(out response);
            return string.IsNullOrWhiteSpace(result) ? Success("", response) : Error(result);
        }

        /// <summary>
        /// tậpthànhđiều trịchưabệnhmẫukhối
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="mzh"></param>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <returns></returns>
        public IActionResult GetIntegrateHEALUrl(string blh, string mzh, string zjlx, string zjh)
        {
            string response;
            var result = _outpatientCmmManagerApp.GetIntegrateHEALUrl(blh, mzh, zjlx, zjh, OrganizeId, out response);
            return string.IsNullOrWhiteSpace(result) ? Success("", response) : Error(result);
        }

        /// <summary>
        /// LấyBệnh nhânThông tin cơ bản
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="mzh"></param>
        /// <param name="jzys"></param>
        /// <returns></returns>
        public JsonResult GetIntegrateRcRequsetParam(string blh, string mzh, string jzys)
        {
            if (string.IsNullOrWhiteSpace(jzys)) jzys = UserIdentity.UserCode;
            var result = _outpatientCmmManagerApp.GetIntegrateRCRequsetParam(blh, mzh, jzys, OrganizeId);
            return Json(result);
        }
        #endregion
    }
}