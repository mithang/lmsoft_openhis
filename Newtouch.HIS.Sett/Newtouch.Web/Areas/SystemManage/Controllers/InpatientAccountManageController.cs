using FrameworkBase.MultiOrg.Web;
using Newtouch.HIS.Application;
using System.Web.Mvc;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Application.Interface.PatientManage;
using Newtouch.Core.Common.Exceptions;
using System;
using Newtouch.HIS.Domain.IRepository.PatientManage;
using System.Linq;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using Newtouch.HIS.Domain.DTO.InputDto;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using System.Collections.Generic;
using Newtouch.HIS.Application.Interface;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class InpatientAccountManageController : OrgControllerBase
    {
        private readonly IInpatientAccountManageApp _accountApp;
        private readonly IInpatientAccountRepo _inpatientAccountRepo;
        private readonly ISysConfigRepo _sysConfigRepo;

        private readonly IPayApp _payApp;

        public static string accountConfig = "";
        public InpatientAccountManageController()
        {
            accountConfig = _sysConfigRepo.GetValueByCode("EnableHospitalAccount", this.OrganizeId);
        }


        // GET: SystemManage/InpatientAccountManage
        public ActionResult Index()
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
        public ActionResult PatAccountInfo(string zyh, string zhxz)
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

        public ActionResult GetZyAccount(string zyh,string zhxz)
        {
            var dto = _inpatientAccountRepo.FindEntity(p =>
     p.zyh == zyh && p.zhxz.ToString() == zhxz && p.OrganizeId == OrganizeId && p.zt == "1");
            return Success("", dto);
        }
        /// <summary>
        /// Lấy thông tin tài khoản bệnh nhân
        /// </summary>
        /// <param name="zh">Tài khoản</param>
        /// <returns></returns>
        public ActionResult GetBasicZhInfoJson(int zh)
        {
            List<InpatientPatAccPayVO> list = _accountApp.GetPayList(zh, null);
            return Success("", list);
        }


        /// <summary>
        /// Thêm mới tài khoản tạm ứng
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public ActionResult AddReserveAccount(int patid,string zyh , string zhxz = null)
        {
            if (string.IsNullOrWhiteSpace(zyh.ToString()))
            {
                throw new FailedException("缺少住院号");
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
                    throw new FailedException("该病人已存在预交金账户，请确认");
                }
            }
            else
            {
                xz = Convert.ToInt16(zhxz);
                var zh = _inpatientAccountRepo.IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == OrganizeId && p.zt == "1");
                if (zh != null && zh.Count() > 0)
                {
                    throw new FailedException("该病人已存在预交金账户，请确认");
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
                zhCode = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zy_zh", this.OrganizeId, initFormat: "{0:D5}").ToInt()
        };
            entity.Create(true);
            _inpatientAccountRepo.Insert(entity);
            return Success();
        }



        #region 账户操作
        /// <summary>
        /// Nạp tiền
        /// </summary>
        /// <param name="depDto">Tập hợp dữ liệu nạp tiền</param>
        /// <returns></returns>
        public ActionResult PayDepositPost(DeposDto depDto)
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
        /// Hoàn tiền
        /// </summary>
        /// <param name="depDto">Tập hợp dữ liệu hoàn tiền</param>
        /// <returns></returns>
        public ActionResult ReturnDepositPost(DeposDto depDto)
        {
            bool flag = _accountApp.ReturnDepositPost(depDto);
            return Success("", flag);
        }
        /// <summary>
        /// Hoàn toàn bộ số dư
        /// </summary>
        /// <param name="depDto"></param>
        /// <returns></returns>
        public ActionResult RefundAccount(DeposDto depDto)
        {
            bool flag = _accountApp.RefundAccount(depDto);
            string errorMsg = "";
            if (depDto.zffsbh == 13)
            {
                if (flag)
                {
                    var refundReuslt = _payApp.TradeRefund(depDto.outTradeNo, depDto.zfje, "预交金全退", "", out errorMsg);
                    if (refundReuslt != 0)
                        errorMsg = "原路退费失败，" + errorMsg;
                }
            }
            return Success(errorMsg, flag);
        }
        #endregion
    }
}