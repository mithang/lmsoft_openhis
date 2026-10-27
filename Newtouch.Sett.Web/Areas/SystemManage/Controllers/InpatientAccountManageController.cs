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

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{

    [Area("SystemManage")]

    public class InpatientAccountManageController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IInpatientAccountManageApp _accountApp;
        private readonly IInpatientAccountRepo _inpatientAccountRepo;
        private readonly ISysConfigRepo _sysConfigRepo;

        private readonly IPayApp _payApp;

        public static string accountConfig = "";
        public InpatientAccountManageController(IInpatientAccountManageApp accountApp, IInpatientAccountRepo inpatientAccountRepo, ISysConfigRepo sysConfigRepo, IPayApp payApp)
        {
            _accountApp = accountApp;
            _inpatientAccountRepo = inpatientAccountRepo;
            _sysConfigRepo = sysConfigRepo;
            _payApp = payApp;
        }


        // GET: SystemManage/InpatientAccountManage
        public IActionResult Index()
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
        public IActionResult PatAccountInfo(string zyh, string zhxz)
        {
            InpatientReserveDto dto = new InpatientReserveDto();
            if (!string.IsNullOrWhiteSpace(zhxz))
            {
                dto = _accountApp.GetHosPatAccInfo(zyh, zhxz);
            }
            else
            {
                dto = _accountApp.GetHosPatAccInfo(zyh,((int)EnumXTZHXZ.ZYYJKZH).ToString());
            }
            return Success("", dto);
        }

        public IActionResult GetZyAccount(string zyh,string zhxz)
        {
            var dto = _inpatientAccountRepo.FindEntity(p =>
     p.zyh == zyh && p.zhxz.ToString() == zhxz && p.OrganizeId == OrganizeId && p.zt == "1");
            return Success("", dto);
        }
        /// <summary>
        /// 获取病人账户信息
        /// </summary>
        /// <param name="zh">账户</param>
        /// <returns></returns>
        public IActionResult GetBasicZhInfoJson(int zh)
        {
            List<InpatientPatAccPayVO> list = _accountApp.GetPayList(zh, null);
            return Success("", list);
        }


        /// <summary>
        /// 新增预交金账户
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public IActionResult AddReserveAccount(int patid,string zyh , string zhxz = null)
        {
            accountConfig = _sysConfigRepo.GetValueByCode("EnableHospitalAccount", this.OrganizeId);
            if (string.IsNullOrWhiteSpace(zyh.ToString()))
            {
                throw new FailedException("Thiếu mã nhập viện");
            }

            int? xz = null;
            if (accountConfig == "ON" && !string.IsNullOrWhiteSpace(zhxz))
            {
                xz = Convert.ToInt16(zhxz);
                var zyzh = _inpatientAccountRepo.IQueryable().Where(p =>
                    p.zyh == zyh && p.zhxz == xz && p.OrganizeId == OrganizeId &&
                    p.zt == "1");
                if (zyzh != null && zyzh.Count() > 0)
                {
                    throw new FailedException("này bệnh nhân Đã Tồn tại tạm ứng Tài khoản，Vui lòng xác nhận");
                }
            }
            else
            {
                xz = Convert.ToInt16(zhxz);
                var zh = _inpatientAccountRepo.IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == OrganizeId && p.zt == "1");
                if (zh != null && zh.Count() > 0)
                {
                    throw new FailedException("này bệnh nhân Đã Tồn tại tạm ứng Tài khoản，Vui lòng xác nhận");
                }
            }
            InpatientAccountEntity entity = new InpatientAccountEntity()
            {
                patid=patid,
                zyh = zyh,
                OrganizeId = OrganizeId,
                zhye = 0,
                zhxz = xz,
                zt = "1",
                zhCode = SettKeyHelper.GetNewFieldUniqueValue("zy_zh", this.OrganizeId, initFormat: "{0:D5}").ToInt()
        };
            entity.Create(true);
            _inpatientAccountRepo.Insert(entity);
            return Success();
        }



        #region Tài khoản Thao tác
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
        /// 退款
        /// </summary>
        /// <param name="depDto">退款数据集合</param>
        /// <returns></returns>
        public IActionResult ReturnDepositPost(DeposDto depDto)
        {
            bool flag = _accountApp.ReturnDepositPost(depDto);
            return Success("", flag);
        }
        /// <summary>
        /// 余额全退
        /// </summary>
        /// <param name="depDto"></param>
        /// <returns></returns>
        public IActionResult RefundAccount(DeposDto depDto)
        {
            bool flag = _accountApp.RefundAccount(depDto);
            string errorMsg = "";
            if (depDto.zffsbh == 13)
            {
                if (flag)
                {
                    var refundReuslt = _payApp.TradeRefund(depDto.outTradeNo, depDto.zfje, "tạm ứng Hoàn toàn bộ", "", out errorMsg);
                    if (refundReuslt != 0)
                        errorMsg = "đường cũ Hoàn phí thất bại，" + errorMsg;
                }
            }
            return Success(errorMsg, flag);
        }
        #endregion
    }
}
