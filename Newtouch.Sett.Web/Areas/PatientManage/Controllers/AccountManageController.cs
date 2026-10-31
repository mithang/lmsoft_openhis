using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.PatientManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("PatientManage")]

    public class AccountManageController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IAccountManageApp _accountApp;
        private readonly ISysPatientBasicInfoRepo _sysPatientBasicInfoRepo;
        private readonly ISysAccountRepo _sysAccountRepo;
        private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;

        public static string accountConfig = "";
        // Port fix: the Framework source had a parameterless ctor reading
        // _sysConfigRepo (never assigned there either - shipped-source
        // inconsistency). Core port uses proper DI; accountConfig is
        // refreshed per request at action time (OrganizeId is only safe
        // once HttpContext is attached, i.e. NOT in the ctor).
        public AccountManageController(IAccountManageApp accountApp, ISysPatientBasicInfoRepo sysPatientBasicInfoRepo, ISysAccountRepo sysAccountRepo, IPatientBasicInfoDmnService patientBasicInfoDmnService, ISysConfigRepo sysConfigRepo)
        {
            _accountApp = accountApp;
            _sysPatientBasicInfoRepo = sysPatientBasicInfoRepo;
            _sysAccountRepo = sysAccountRepo;
            _patientBasicInfoDmnService = patientBasicInfoDmnService;
            _sysConfigRepo = sysConfigRepo;
        }

        /// <summary>
        /// Giao diện quản lý tài khoản
        /// </summary> 
        /// <returns></returns>  
        public IActionResult PatAccountInfo()
        {
            return View();
        }

        /// <summary>
        /// Giao diện quản lý tài khoản
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="kh">Số thẻ</param>
        /// <param name="xm">Họ tên</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PatAccountInfo(string zyh, string kh, string xm)
        {
            ReserveDto hospatDto = _accountApp.GetHosPatAccInfo(zyh.ToInt());
            return Success("", hospatDto);
        }

        /// <summary>
        /// phântrangsốdữ liệuthêmtải
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="brzybz">bệnhngườitạiviệntiêuchí:null/Emptyxếptrừlàmhủylấyhủyvàoviện;của nóanh tađốiphảizybzEnum,nhiềucáidùngtrêusốphâncắt</param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult PatSearchInfo(Pagination pagination, string zyh, string xm, string brzybzType = null)
        {
            var data = new
            {
                rows = _patientBasicInfoDmnService.GetPatSearchList(pagination, this.OrganizeId, zyh,xm, brzybzType),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// ngườiviêntratra cứucộtbảng chỉbệnhlịchsố
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult PatOnlyBlhSearchInfo(Pagination pagination, string blh, string xm)
        {
            var data = new
            {
                rows = _sysPatientBasicInfoRepo.GetPatOnlyBlhSearchList(pagination,this.OrganizeId, blh, xm),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy thông tin tài khoản bệnh nhân
        /// </summary>
        /// <param name="zh">Tài khoản</param>
        /// <returns></returns>
        public IActionResult GetBasicZhInfoJson(int zh)
        {
            List<PatAccPayVO> list = _accountApp.GetPayList(zh,null);
            return Success("", list);
        }
        /// <summary>
        /// Thêm tài khoản tiền tạm ứng mới
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public IActionResult AddReserveAccount(int patid,string zhxz=null)
        {
            if (string.IsNullOrWhiteSpace(patid.ToString()))
            {
                throw new FailedException("Thiếu mã nội bộ bệnh nhân");
            }

            int? xz = null;
            accountConfig = _sysConfigRepo.GetValueByCode("EnableHospitalAccount", this.OrganizeId);
            if (accountConfig == "ON" && !string.IsNullOrWhiteSpace(zhxz))
            {
                xz = Convert.ToInt16(zhxz);
                var zyzh = _sysAccountRepo.IQueryable().Where(p =>
                    p.patid == patid && p.zhxz == xz && p.OrganizeId == OrganizeId &&
                    p.zt == "1");
                if (zyzh != null && zyzh.Count() > 0)
                {
                    throw new FailedException("này bệnh nhân Đã Tồn tại tạm ứng Tài khoản，Vui lòng xác nhận");
                }
            }
            else
            {
                var zh = _sysAccountRepo.IQueryable().Where(p => p.patid == patid && p.OrganizeId == OrganizeId && p.zt == "1");
                if (zh != null && zh.Count() > 0)
                {
                    throw new FailedException("này bệnh nhân Đã Tồn tại tạm ứng Tài khoản，Vui lòng xác nhận");
                }
            }
            SysAccountEntity entity = new SysAccountEntity()
            {
                patid = patid,
                OrganizeId = OrganizeId,
                zhye = 0,
                zhxz = xz,
                zt = "1",
                zhCode = SettKeyHelper.GetNewFieldUniqueValue("xt_zh", this.OrganizeId).ToInt()
            };
            entity.Create(true);
            _sysAccountRepo.Insert(entity);
            return Success();
        }

        #region Số chứng từ
        /// <summary>
        /// thêmthêmchứngchứngsốcửa sổmiệng
        /// </summary>
        /// <returns></returns>
        public IActionResult ChooseReceipt()
        {
            return View();
        }

        /// <summary>
        /// Lấy số chứng từ biên lai
        /// </summary>
        /// <returns></returns>
        public IActionResult GetReceipt()
        {
            string sjpzh = _accountApp.GetFinRepSJPZH();
            return Success("", sjpzh);
        }

        /// <summary>
        /// kiểmchứngchứngchứngsố
        /// </summary>
        /// <param name="pzh">Số chứng từ</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CheckReceipt(string pzh)
        {
            string type = string.Empty;
            FinanceReceiptEntity sjEntity = new FinanceReceiptEntity();
            bool flag = _accountApp.CheckSJPZH(pzh, out sjEntity, out type);
            return Success("", flag);
        }
        #endregion

        #region tạm ứng nạp tiền
        /// <summary>
        /// Nạp tiền
        /// </summary>
        /// <param name="depDto">Tập dữ liệu nạp tiền</param>
        /// <returns></returns>
        public IActionResult PayDepositPost(DeposDto depDto)
        {
            string szId = "";
            bool flag = _accountApp.PayDepositPost(depDto, out szId);
            var data = new
            {
                flag = flag,
                szId = szId
            };
            return Success("", data);
        }


        /// <summary>
        /// Nạp tiền
        /// </summary>
        /// <param name="depDto">Tập dữ liệu nạp tiền</param>
        /// <returns>trảquaymớitài khoảnhộthông tintin</returns>
        public IActionResult PayDepositPostAcnt(DeposDto depDto)
        {
            var ety = _accountApp.PayDepositPostAcnt(depDto);
            return Success("", ety);
        }
        /// <summary>
        /// Hoàn tiền
        /// </summary>
        /// <param name="depDto">Tập dữ liệu hoàn tiền</param>
        /// <returns></returns>
        public IActionResult ExitDepositPost(DeposDto depDto)
        {
            bool flag = _accountApp.ExitDepositPost(depDto);
            return Success("", flag);
        }

        /// <summary>
        /// Hoàn toàn bộ số dư
        /// </summary>
        /// <param name="depDto"></param>
        /// <returns></returns>
        public IActionResult RefundAccount(DeposDto depDto) {
            _accountApp.RefundAccount(depDto);
            return Success();
        }

        /// <summary>
        /// đượclấytài khoảnhộcòn lạimức
        /// </summary>
        /// <param name="patid">patid</param>
        /// <returns></returns>
        public IActionResult GetZhyeByPatid(int patid,string zhxz=null)
        {
            if (!string.IsNullOrWhiteSpace(zhxz))
            {
                var zhInfo = _accountApp.GetZhyeByPatid(patid, Convert.ToInt16(zhxz));
                return Success(null, zhInfo);
            }
            else
            {
                var zhye = _accountApp.GetZhyeByPatid(patid);
                return Success(null, zhye);
            }
            
        }

        #endregion

        #region  tra cứu nhân viên Danh sách

        public IActionResult PatSearchView(string from = "")
        {
            return View();
        }
        #endregion

        #region tra cứu nhân viên Danh sách chỉ Số hồ sơ bệnh án
        public IActionResult PatOnlyBlhSearchView(string from = "")
        {
            return View();
        }
        #endregion
    }
}
