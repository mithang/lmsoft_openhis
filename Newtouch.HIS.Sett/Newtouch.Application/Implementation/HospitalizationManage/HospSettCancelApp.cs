using Newtouch.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.DTO.OutputDto;
using FrameworkBase.MultiOrg.Application;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Hủy quyết toán nhập viện 实现
    /// </summary>
    public class HospSettCancelApp : AppBase, IHospSettCancelApp
    {
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly IHosPatAccDmnService _hosPatAccDmnService;
        private readonly ISysCashPaymentModelRepo _sysCashPaymentModelRepo;
        private readonly IHospSettDmnService _hospSettDmnService;
        private readonly IHospSettlementRepo _hospSettlementRepo;
        private readonly IHospSettlementDetailRepo _hospSettlementDetailRepo;

        /// <summary>
        /// Hủy quyết toán nhập viện,XemBệnh nhânNhập viện Trạng tháiThông tin
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        public HospSettCancelPatStatusDetailDto GetPatHospStatusDetail(string zyh, string kh)
        {
            //Lấy thông tin bệnh nhân quyết toán nhập viện(cơ bản thông tin, tài khoản, khoaV.v.)
            var settpatinfo = getHospPatBasicInfo(ref zyh, kh);

            // theo số nhập viện lấy (chưa hoàn)danh sách quyết toán, 取 của nó nhất sau một dòng
            var lastSett = _hospSettlementRepo.GetValidList(zyh, this.OrganizeId).OrderByDescending(p => p.CreateTime).FirstOrDefault();

            var resultDto = new HospSettCancelPatStatusDetailDto()
            {
                SettPatInfo = settpatinfo,
                LastUnCancelledSett = lastSett,
            };

            return resultDto;
        }

        /// <summary>
        /// Hủy quyết toán nhập viện GửiLưuThao tác
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsnm"></param>
        /// <param name="cancelReason"></param>
        public void DoCancel(string zyh, int expectedjsnm, string cancelReason)
        {
            //Lấy thông tin bệnh nhân quyết toán nhập viện(cơ bản thông tin, tài khoản, khoaV.v.)
            var settpatinfo = getHospPatBasicInfo(ref zyh, "");

            // theo số nhập viện lấy (chưa hoàn)danh sách quyết toán, 取 của nó nhất sau một dòng
            var lastSett = _hospSettlementRepo.GetValidList(zyh, this.OrganizeId).OrderByDescending(p => p.CreateTime).FirstOrDefault();

            if (lastSett.jsnm != expectedjsnm)
            {
                throw new FailedCodeException("HOSP_CANCELSETT_JSNM_IS_CHANGED");
            }

            var sqlVO = new HospCancelSettDBDataUpdateCollectVO() {
                cx_zyjsmxList = new List<HospSettlementDetailEntity>(),
                cx_xt_brzhszjlList = new List<SysPatientAccountRevenueAndExpenseEntity>(),
                cx_zy_jszffsList = new List<HospSettlementPaymentModelEntity>(),

            };

            //BHYT giao dịch hủy quyết toán

            sqlVO.zyh = zyh;
            /********* chú thích tại đ ây Củazybz nên là bản hệ thống trạng thái thay đ ổi là"Chờ thanh toán", đ ẩy cho khác hệ thống(繁云), sau đ ó (繁云)hệ thống thay đ ổi là"Khu bệnh trong ", như vậy, này bệnh nhân才 có thể ghi sổ. tại đ ây trư ớc gán giá trị thủ công *************/
            sqlVO.zybz = ((int)EnumZYBZ.Bqz).ToString();
            //sqlVO.zybz = ((int)EnumZYBZ.Djz).ToString();
            sqlVO.cyrq = null;

            //Quyết toán nhập viện Tư ơng ứng của hủy bỏ bản ghi 
            var cxjs = lastSett.Clone();
            sqlVO.cxjs = cxjs;
            cxjs.jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_js");
            cxjs.cxjsnm = lastSett.jsnm;
            cxjs.jszt = ((int)EnumJieSuanZT.YT).ToString();
            cxjs.cxjsyy = cancelReason;
            cxjs.CreatorCode = this.UserIdentity.UserCode;
            cxjs.CreateTime = DateTime.Now;
            cxjs.zt = "1";

            //Quyết toán nhập việnchi tiết Tư ơng ứng của hủy bỏ bản ghi 
            var zyjsmxList = _hospSettlementDetailRepo.IQueryable().Where(p => p.jsnm == lastSett.jsnm).ToList();
            foreach (var zyjsmx in zyjsmxList)
            {
                var cxjsmx = zyjsmx.Clone();
                cxjsmx.jsnm = cxjs.jsnm;
                cxjsmx.CreatorCode = this.UserIdentity.UserCode;
                cxjsmx.CreateTime = DateTime.Now;
                cxjsmx.zt = "1";
                sqlVO.cx_zyjsmxList.Add(cxjsmx);
            }

            //Hệ thống bệnh nhânthu chi tài khoản bản ghi 
            if (!settpatinfo.zyyjjzhbh.HasValue || !settpatinfo.zyyjjzh.HasValue)
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_YJJ_ACCOUNT_IS_NOT_FOUND");
            }
            sqlVO.cx_xt_brzhszjlList.Add(new SysPatientAccountRevenueAndExpenseEntity()
            {
                OrganizeId = this.OrganizeId,
                zh = settpatinfo.zyyjjzh ?? 0,
                patid = settpatinfo.patid.Value,
                szje = lastSett.xjzf,
                zhye = lastSett.xjzf,
                pzh = string.Empty,
                szxz = ((int)AccSzxz.ZYJS).ToString(),
                xjzffs = "",    //&update170720
                jsnm = cxjs.jsnm,
                zyh = zyh,
                CreatorCode = this.UserIdentity.UserCode,
                CreateTime = DateTime.Now,
                zt = "1",
            });

            //Quyết toán nhập viện phư ơng thức thanh toán 
            sqlVO.cx_zy_jszffsList.Add(new HospSettlementPaymentModelEntity()
            {
                jsnm = cxjs.jsnm,
                OrganizeId = this.OrganizeId,
                xjzffs = Constants.xtzffs.ZYYJZHZF,
                zfje = lastSett.xjzf,
                ssry = this.UserIdentity.UserCode,
                ssrq = DateTime.Now,
                CreatorCode = this.UserIdentity.UserCode,
                CreateTime = DateTime.Now,
                zt = "1",
            });

            _hospSettDmnService.CancelSettSyncToDB(sqlVO, this.OrganizeId);

            //########Đ ồng bộ trạng thái tới kho trung gian

            return;
        }

        #region private methods

        /// <summary>
        /// Hủy quyết toán nhập viện lấy thông tin cơ bản bệnh nhân(cơ bản thông tin, tài khoản, khoaV.v.) xác minh trạng thái đang nằm viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        private HospSettPatInfoVO getHospPatBasicInfo(ref string zyh, string kh)
        {
            if (string.IsNullOrWhiteSpace(zyh) && !string.IsNullOrWhiteSpace(kh))
            {
                var zyList = _hospPatientBasicInfoRepo.IQueryable().Where(p => kh.Equals(p.kh) && p.OrganizeId == this.OrganizeId).ToList();
                if (zyList.Count == 0)
                {
                    throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
                }
                //zyh = zyList.Where(p => p.zybz == ((int)EnumZYBZ.Ycy).ToString())
                //      .OrderByDescending(p => p.CreateTime).Select(p => p.zyh).First();
                //if (string.IsNullOrWhiteSpace(zyh)) //说明 không có có thể hủy quyết toán của bản ghi 
                //{
                //    zyh = zyList.OrderByDescending(p => p.CreateTime).Select(p => p.zyh).First();
                //}
                zyh = zyList.OrderByDescending(p => p.CreateTime).Select(p => p.zyh).First();
            }
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            var settpatinfoList = _hosPatAccDmnService.GetHospSettPatInfo(zyh, this.OrganizeId);
            if (settpatinfoList == null || settpatinfoList.Count == 0)
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (settpatinfoList.Count > 1)
            {
                throw new FailedCodeException("HOSP_MATCHED_ERROR_MULTI_MATCHED");
            }
            var settpatinfo = settpatinfoList.First();
            if (settpatinfo.zybz != ((int)EnumZYBZ.Ycy).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_NOT_YCY_CANNOT_DO_CANCEL_SETTLEMENT");
            }
            else if (settpatinfo.patid == 0)
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (settpatinfo.ryrq <= new DateTime(1990, 01, 01))
            {
                throw new FailedCodeException("HOSP_ERROR_THERE_IS_NO_RYRQ");
            }
            else if (string.IsNullOrWhiteSpace(settpatinfo.brxz))
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (string.IsNullOrWhiteSpace(settpatinfo.ksCode))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_DEPT_IS_NO_FOUND");
            }
            else if (settpatinfo.zyyjjzh <= 0)
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_THERE_IS_NO_YJJ_ACCOUNT");
            }

            settpatinfo.zyts = DateTimeHelper.GetInHospDays(settpatinfo.ryrq.Value, settpatinfo.cyrq.Value);

            return settpatinfo;
        }

        #endregion

    }

}
