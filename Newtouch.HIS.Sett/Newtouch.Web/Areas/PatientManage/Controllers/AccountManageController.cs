using System;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IRepository;

namespace Newtouch.HIS.Web.Areas.PatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountManageController : ControllerBase
    {
        private readonly IAccountManageApp _accountApp;
        private readonly ISysPatientBasicInfoRepo _sysPatientBasicInfoRepo;
        private readonly ISysAccountRepo _sysAccountRepo;
        private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;

        public static string accountConfig = "";
        public AccountManageController()
        {
            accountConfig = _sysConfigRepo.GetValueByCode("EnableHospitalAccount", this.OrganizeId);
        }

        /// <summary>
        /// Giao diện quản lý tài khoản
        /// </summary> 
        /// <returns></returns>  
        public ActionResult PatAccountInfo()
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
        public ActionResult PatAccountInfo(string zyh, string kh, string xm)
        {
            ReserveDto hospatDto = _accountApp.GetHosPatAccInfo(zyh.ToInt());
            return Success("", hospatDto);
        }

        /// <summary>
        /// Tải dữ liệu phân trang
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="brzybz">Cờ bệnh nhân đang ở viện：null/Empty Loại trừ hủy nhập viện đã vô hiệu；Tương ứng khác zybz Enum，Nhiều mục phân tách bằng dấu phẩy</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult PatSearchInfo(Pagination pagination, string zyh, string xm, string brzybzType = null)
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
        /// Danh sách tra cứu nhân sự Chỉ số bệnh án
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult PatOnlyBlhSearchInfo(Pagination pagination, string blh, string xm)
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
        public ActionResult GetBasicZhInfoJson(int zh)
        {
            List<PatAccPayVO> list = _accountApp.GetPayList(zh,null);
            return Success("", list);
        }
        /// <summary>
        /// Thêm mới tài khoản tạm ứng
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public ActionResult AddReserveAccount(int patid,string zhxz=null)
        {
            if (string.IsNullOrWhiteSpace(patid.ToString()))
            {
                throw new FailedException("缺少病人内码");
            }

            int? xz = null;
            if (accountConfig == "ON" && !string.IsNullOrWhiteSpace(zhxz))
            {
                xz = Convert.ToInt16(zhxz);
                var zyzh = _sysAccountRepo.IQueryable().Where(p =>
                    p.patid == patid && p.zhxz == xz && p.OrganizeId == OrganizeId &&
                    p.zt == "1");
                if (zyzh != null && zyzh.Count() > 0)
                {
                    throw new FailedException("该病人已存在预交金账户，请确认");
                }
            }
            else
            {
                var zh = _sysAccountRepo.IQueryable().Where(p => p.patid == patid && p.OrganizeId == OrganizeId && p.zt == "1");
                if (zh != null && zh.Count() > 0)
                {
                    throw new FailedException("该病人已存在预交金账户，请确认");
                }
            }
            SysAccountEntity entity = new SysAccountEntity()
            {
                patid = patid,
                OrganizeId = OrganizeId,
                zhye = 0,
                zhxz = xz,
                zt = "1",
                zhCode = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("xt_zh", this.OrganizeId).ToInt()
            };
            entity.Create(true);
            _sysAccountRepo.Insert(entity);
            return Success();
        }

        #region 凭证号
        /// <summary>
        /// Cửa sổ thêm số chứng từ
        /// </summary>
        /// <returns></returns>
        public ActionResult ChooseReceipt()
        {
            return View();
        }

        /// <summary>
        /// Lấy số chứng từ biên lai
        /// </summary>
        /// <returns></returns>
        public ActionResult GetReceipt()
        {
            string sjpzh = _accountApp.GetFinRepSJPZH();
            return Success("", sjpzh);
        }

        /// <summary>
        /// Kiểm tra số chứng từ
        /// </summary>
        /// <param name="pzh">Số chứng từ</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckReceipt(string pzh)
        {
            string type = string.Empty;
            FinanceReceiptEntity sjEntity = new FinanceReceiptEntity();
            bool flag = _accountApp.CheckSJPZH(pzh, out sjEntity, out type);
            return Success("", flag);
        }
        #endregion

        #region 预交金充值
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
        /// Nạp tiền
        /// </summary>
        /// <param name="depDto">Tập hợp dữ liệu nạp tiền</param>
        /// <returns>Trả về thông tin tài khoản mới</returns>
        public ActionResult PayDepositPostAcnt(DeposDto depDto)
        {
            var ety = _accountApp.PayDepositPostAcnt(depDto);
            return Success("", ety);
        }
        /// <summary>
        /// Hoàn tiền
        /// </summary>
        /// <param name="depDto">Tập hợp dữ liệu hoàn tiền</param>
        /// <returns></returns>
        public ActionResult ExitDepositPost(DeposDto depDto)
        {
            bool flag = _accountApp.ExitDepositPost(depDto);
            return Success("", flag);
        }

        /// <summary>
        /// Hoàn toàn bộ số dư
        /// </summary>
        /// <param name="depDto"></param>
        /// <returns></returns>
        public ActionResult RefundAccount(DeposDto depDto) {
            _accountApp.RefundAccount(depDto);
            return Success();
        }

        /// <summary>
        /// Lấy số dư tài khoản
        /// </summary>
        /// <param name="patid">patid</param>
        /// <returns></returns>
        public ActionResult GetZhyeByPatid(int patid,string zhxz=null)
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

        #region  人员查询列表

        public ActionResult PatSearchView(string from = "")
        {
            return View();
        }
        #endregion

        #region 人员查询列表 仅病历号
        public ActionResult PatOnlyBlhSearchView(string from = "")
        {
            return View();
        }
        #endregion
    }
}