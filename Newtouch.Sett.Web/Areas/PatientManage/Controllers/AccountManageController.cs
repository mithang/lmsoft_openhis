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
        /// 账户管理界面
        /// </summary> 
        /// <returns></returns>  
        public IActionResult PatAccountInfo()
        {
            return View();
        }

        /// <summary>
        /// 账户管理界面
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="kh">卡号</param>
        /// <param name="xm">姓名</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PatAccountInfo(string zyh, string kh, string xm)
        {
            ReserveDto hospatDto = _accountApp.GetHosPatAccInfo(zyh.ToInt());
            return Success("", hospatDto);
        }

        /// <summary>
        /// 分页数据加载
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="brzybz">病人在院标志：null/Empty排除作废取消入院；其他对应zybz枚举，多个用逗号分割</param>
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
        /// 人员查询列表 仅病历号
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
        /// 获取病人账户信息
        /// </summary>
        /// <param name="zh">账户</param>
        /// <returns></returns>
        public IActionResult GetBasicZhInfoJson(int zh)
        {
            List<PatAccPayVO> list = _accountApp.GetPayList(zh,null);
            return Success("", list);
        }
        /// <summary>
        /// 新增预交金账户
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
        /// 添加凭证号窗口
        /// </summary>
        /// <returns></returns>
        public IActionResult ChooseReceipt()
        {
            return View();
        }

        /// <summary>
        /// 获取收据凭证号
        /// </summary>
        /// <returns></returns>
        public IActionResult GetReceipt()
        {
            string sjpzh = _accountApp.GetFinRepSJPZH();
            return Success("", sjpzh);
        }

        /// <summary>
        /// 验证凭证号
        /// </summary>
        /// <param name="pzh">凭证号</param>
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
        /// 充值
        /// </summary>
        /// <param name="depDto">充值数据集合</param>
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
        /// 充值
        /// </summary>
        /// <param name="depDto">充值数据集合</param>
        /// <returns>返回新账户信息</returns>
        public IActionResult PayDepositPostAcnt(DeposDto depDto)
        {
            var ety = _accountApp.PayDepositPostAcnt(depDto);
            return Success("", ety);
        }
        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="depDto">退款数据集合</param>
        /// <returns></returns>
        public IActionResult ExitDepositPost(DeposDto depDto)
        {
            bool flag = _accountApp.ExitDepositPost(depDto);
            return Success("", flag);
        }

        /// <summary>
        /// 余额全退
        /// </summary>
        /// <param name="depDto"></param>
        /// <returns></returns>
        public IActionResult RefundAccount(DeposDto depDto) {
            _accountApp.RefundAccount(depDto);
            return Success();
        }

        /// <summary>
        /// 获取账户余额
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
