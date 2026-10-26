using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface.HospitalizationManage;
using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.HospitalizationManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Application.Implementation.HospitalizationManage
{
    /// <summary>
    /// 
    /// </summary>
    public class DischargeSettleApp : AppBase, IDischargeSettleApp
    {
        private readonly IDischargeSettleDmnService _dischargeSettleDmnService;
        private readonly IHospSettlementRepo _hospSettlementRepo;
        private readonly IHospSettlementDetailRepo _hospSettlementDetailRepo;
        private readonly IHospAccountingPlanDetailRepo _hospAccountingPlanDetailRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IHospSettlementGAYBFeeRepo _hospSettlementGAYBFeeRepo;
        private readonly IHospSettlementGAXNHFeeRepo _hospSettlementGAXNHFeeRepo;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
	    private readonly ICqybSett05Repo _cqybSett05Repo;
        private readonly IBookkeepInHosDmnService _BookkeepInHosDmnService;
        private readonly IHospItemBillingRepo _hospItemBillingRepo;
        #region 出院结算

        /// <summary>
        /// Số nhập việntra cứu dữ liệu(bao gồm thông tin bệnh nhân và tính phí chi tiết) zyh hoặc kh +cardType hoặc sfz +cardType
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="sfz"></param>
        /// <param name="kh"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public InpatientSettPatStatusDetailDto GetInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType,string jslx,string ver)
        {
            // Lấy thông tin bệnh nhân
            var settpatInfo = GetInpatientSettlePatInfo(ref zyh, sfz, kh, cardType, jslx);
            // Lấy tính phí chi tiết(bao gồm:Hạng mục và thuốc)
            //var settleItemsBo = GetInpatientSettleItemsDetailList(zyh);
            if (settpatInfo.cyrq.HasValue)
            {
                settpatInfo.zyts = DateTimeHelper.GetInHospDays(settpatInfo.ryrq.Value, settpatInfo.cyrq.Value);
            }
            var resultDto = new InpatientSettPatStatusDetailDto()
            {
                InpatientSettPatInfo = settpatInfo,
                //InpatientSettleItemBO = settleItemsBo
                GroupFeeVOList = _dischargeSettleDmnService.GetHospGroupFeeVOList(zyh, this.OrganizeId,ver),
            };
            return resultDto;
        }


        /// <summary>
        /// Thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public InpatientSettPatInfoVO GetInpatientSettlePatInfo(ref string zyh, string sfz = null, string kh = null, string cardType = null,string jslx=null)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                if (!string.IsNullOrWhiteSpace(kh) && !string.IsNullOrWhiteSpace(cardType))
                {
                    // theo số thẻ và loại thẻ đ ổi lấy số nhập viện
                    zyh = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.kh == kh && p.CardType == cardType &&p.zt=="1" && p.OrganizeId==this.OrganizeId && p.zybz!=((int)EnumZYBZ.Wry).ToString()).OrderByDescending(p => p.CreateTime).Select(p => p.zyh).FirstOrDefault();
                }
                else if (!string.IsNullOrWhiteSpace(sfz) && !string.IsNullOrWhiteSpace(cardType))
                {
                    // theo CMNDvà loại thẻ đ ổi lấy số nhập viện
                    var zjlx = ((int)EnumZJLX.sfz).ToString();
                    zyh = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.zjlx == zjlx && p.zjh == sfz && p.CardType == cardType && p.zt=="1" && p.OrganizeId==this.OrganizeId && p.zybz!= ((int)EnumZYBZ.Wry).ToString()).OrderByDescending(p => p.CreateTime).Select(p => p.zyh).FirstOrDefault();
                }
            }

            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }

            var settPatinfoList = _dischargeSettleDmnService.SelectInpatientSettPatInfo(zyh, this.OrganizeId);
            if (settPatinfoList == null || settPatinfoList.Count == 0)
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (settPatinfoList.Count > 1)
            {
                throw new FailedCodeException("HOSP_MATCHED_ERROR_MULTI_MATCHED");
            }
            var settpatinfo = settPatinfoList.First();

            if (settpatinfo == null)
            {
                throw new FailedException("患者住院信息未找到");
            }

            if (settpatinfo.zybz == ((int)EnumZYBZ.Ycy).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_YCY_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (settpatinfo.patId == 0)
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (!settpatinfo.ryrq.HasValue || settpatinfo.ryrq <= new DateTime(1990, 01, 01))
            {
                throw new FailedCodeException("HOSP_ERROR_THERE_IS_NO_RYRQ");
            }
            //else if (string.IsNullOrWhiteSpace(settpatinfo.brxz))
            //{
            //    throw new FailedCodeException("HOSP_ERROR_PATIENT_NATURE_IS_NO_FOUND");
            //}
            else if (string.IsNullOrWhiteSpace(settpatinfo.ksmc))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_DEPT_IS_NO_FOUND");
            }
            //settpatinfo.zyts = Common.DateTimeHelper.GetInHospDays(settpatinfo.ryrq.Value, settpatinfo.cyrq.Value);
                //zybz có hay không có thể quyết toán khu bệnh trong của bệnh nhân
                var isSettContainsBQZ = _sysConfigRepo.GetBoolValueByCode("HOSP_Sett_Contains_BQZ", this.OrganizeId, false).Value;
                var brzybzType = ((int)EnumZYBZ.Djz).ToString();
                if (isSettContainsBQZ)
                {
                    brzybzType += "," + ((int)EnumZYBZ.Bqz).ToString();
                }
                if (!brzybzType.Contains(settpatinfo.zybz) && (string.IsNullOrEmpty(jslx) || (!string.IsNullOrEmpty(jslx) && jslx != "mnjs")))
                {
                    throw new FailedException(((EnumZYBZ)(Convert.ToInt32(settpatinfo.zybz))).GetDescription() + "状态不能结算");
                }
            

            return settpatinfo;
        }

        /// <summary>
        /// Lấy(tính phí và đã hoàn tổng cộng của) tính phí chi tiết(bao gồm hạng mục điều trị và thuốc)
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        private InpatientSettleItemBO GetInpatientSettleItemsDetailList(string zyh, DateTime? kssj, DateTime? jssj)
        {
            if (string.IsNullOrEmpty(zyh))
            {
                return null;
            }
            //zy_xmjfb
            var treatmentItemsList = _dischargeSettleDmnService.SelectTreatmentItemList(zyh, this.OrganizeId, kssj,jssj);
            //zy_ypjfb
            var drugList = _dischargeSettleDmnService.SelectDrugList(zyh, this.OrganizeId, kssj, jssj);
            // không phải điều trị hạng mục thu phí
            //var non_treatmentItemsList = _dischargeSettleDmnService.SelectNonTreatmentItemList(zyh, this.OrganizeId);

            var resultBo = new InpatientSettleItemBO()
            {
                TreatmentItemList = treatmentItemsList,
                DrugList = drugList
                //Non_treatmentItemList = non_treatmentItemsList
            };
            return resultBo;
        }
        #region  医保数据传输
        public InpatientSettPatStatusDetailDto GetZyUploadDetail(string zyh,string jslx)
        {
            //Lấy thông tin bệnh nhân
            var settpatInfo = GetInpatientSettlePatInfo(ref zyh, null, null, null, jslx);
            // Lấy tính phí số tiền(bao gồm:Hạng mục và thuốc)
            //var settleItemsBo = GetInpatientSettleJe(zyh,kssj,jssj,sczt);
            if (settpatInfo.cyrq.HasValue)
            {
                settpatInfo.zyts = DateTimeHelper.GetInHospDays(settpatInfo.ryrq.Value, settpatInfo.cyrq.Value);
            }
            var resultDto = new InpatientSettPatStatusDetailDto()
            {
                //InpatIentFeeSum= settleItemsBo,
                InpatientSettPatInfo = settpatInfo
            };
            return resultDto;

        }
        /// <summary>
        /// Chi phí bảo hiểm y tế truyền tảizje
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public InpatIentFeeSumVo GetInpatientSettleJe(string zyh, string sczt, DateTime? kssj, DateTime jssj, string xmmc)
        {
            if (string.IsNullOrEmpty(zyh))
            {
                return null;
            }
            var FeeVo = _dischargeSettleDmnService.GetInpatSettFeeSum(zyh, this.OrganizeId, sczt, xmmc, kssj, jssj);
            var resultVo = new InpatIentFeeSumVo
            {
                zje =FeeVo
            };
            return resultVo;
        }

        #endregion
        /// <summary>
        /// LưuQuyết toán
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedcyrq"></param>
        /// <param name="fph"></param>
        /// <param name="feeRelated">Số tiền và thanh toán thông tin</param>
        /// <param name="ybfeeRelated">BHYT liên quan chi phí thông tin</param>
        public void SaveSett(string zyh, DateTime expectedcyrq, string fph, InpatientSettFeeRelatedDTO feeRelated
            , CQZyjs05Dto ybfeeRelated, S14OutResponseDTO xnhfeeRelated
            , string outTradeNo, string jslx, out int jsnm)
        {
            if (string.IsNullOrEmpty(zyh))
            {
                throw new FailedCodeException("HOSP_ZYH_IS_EMPTY");
            }

            // kiểm tra có hay không có chưa hoàn thành và chưa chấm dứt của kế hoạch thực hiện 
            //_hospAccountingPlanDetailRepo.CheckAccountingPlanDetailStatus(zyh, this.OrganizeId);

            // Lấy thông tin bệnh nhân
            var settpatInfo = GetInpatientSettlePatInfo(ref zyh);
            //Lấy nhất sau một lần quyết toán loại trừ quyết toán giữa chừng
            var dbDt = _BookkeepInHosDmnService.GetLastValidSettTime(zyh, this.OrganizeId);

            // Lấy tính phí chi tiết(bao gồm:Hạng mục và thuốc)
            var settleItemsBo = GetInpatientSettleItemsDetailList(zyh, dbDt, null);

            //Lưu đ ến dữ liệu kho 
            _dischargeSettleDmnService.SaveSett(settpatInfo, settleItemsBo, expectedcyrq, this.OrganizeId, fph, feeRelated, ybfeeRelated, xnhfeeRelated, outTradeNo, jslx,out jsnm);

        }
        #endregion


        #region 取消出院结算

        /// <summary>
        /// Tra cứu
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public InpatientCancelSettPatStatusDetailDto GetCancelSettleStatusDetail(string zyh, string sfz, string kh, string cardType)
        {
            // Lấy thông tin bệnh nhân
            var settpatInfo = GetCancelSettlePatInfo(ref zyh, sfz, kh, cardType);
            // theo số nhập viện lấy (chưa hoàn)danh sách quyết toán, lấy của nó nhất sau một dòng
            var lastSett = _hospSettlementRepo.GetValidList(zyh, this.OrganizeId).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            var medicalInsurance = _sysConfigRepo.GetValueByCode("Inpatient_MedicalInsurance", this.OrganizeId);
            HospSettlementGAYBFeeEntity ybFee = null;
            HospSettlementGAXNHFeeEntity xnhFee = null;
	        CqybSett05Entity cqYbFee = null;
			if (settpatInfo.brxz=="1" && settpatInfo.CardType== ((int)EnumCardType.YBJYK).ToString())//BHYTBệnh nhân
            { 
                if (medicalInsurance == "guian")
                {
                    ybFee = lastSett != null ? _hospSettlementGAYBFeeRepo.IQueryable().Where(p => p.jsnm == lastSett.jsnm).FirstOrDefault() : null;
                }
	            //if (medicalInsurance == "chongqing")
	            //{
		           // cqYbFee = lastSett != null ? _cqybSett05Repo.IQueryable().Where(p => p.jsnm == lastSett.jsnm && p.zt=="1").FirstOrDefault() : null;
	            //}
			}
            if (settpatInfo.brxz == "8" && settpatInfo.CardType == ((int)EnumCardType.XNHJYK).ToString())// BHYT nông thôn mới bệnh nhân
            {
                if (medicalInsurance == "guian")
                {
                    xnhFee = lastSett != null ? _hospSettlementGAXNHFeeRepo.IQueryable().Where(p => p.jsnm == lastSett.jsnm).FirstOrDefault() : null;
                }
            }
            var resultDto = new InpatientCancelSettPatStatusDetailDto()
            {
                InpatientSettPatInfo = settpatInfo,
                LastUnCancelledSett = lastSett,
                YbFee = ybFee,
                XnhFee= xnhFee
	            //CqYbFee= cqYbFee
			};

            return resultDto;
        }

        /// <summary>
        /// Hủy quyết toán nhập viện lấy thông tin cơ bản bệnh nhân(cơ bản thông tin, tài khoản, khoaV.v.) xác minh trạng thái đang nằm viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public InpatientSettPatInfoVO GetCancelSettlePatInfo(ref string zyh, string sfz = null, string kh = null, string cardType = null)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                if (!string.IsNullOrWhiteSpace(kh) && !string.IsNullOrWhiteSpace(cardType))
                {
                    // theo số thẻ và loại thẻ đ ổi lấy số nhập viện
                    zyh = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.kh == kh && p.CardType == cardType).OrderByDescending(p => p.CreateTime).Select(p => p.zyh).FirstOrDefault();
                }
                else if (!string.IsNullOrWhiteSpace(sfz) && !string.IsNullOrWhiteSpace(cardType))
                {
                    // theo CMNDvà loại thẻ đ ổi lấy số nhập viện
                    var zjlx = ((int)EnumZJLX.sfz).ToString();
                    zyh = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.zjlx == zjlx && p.zjh == sfz && p.CardType == cardType).OrderByDescending(p => p.CreateTime).Select(p => p.zyh).FirstOrDefault();
                }
            }

            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            var settpatinfoList = _dischargeSettleDmnService.SelectInpatientSettPatInfo(zyh, this.OrganizeId);
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
            else if (settpatinfo.patId == 0)
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (settpatinfo.ryrq <= new DateTime(1990, 01, 01))
            {
                throw new FailedCodeException("HOSP_ERROR_THERE_IS_NO_RYRQ");
            }
            //else if (string.IsNullOrWhiteSpace(settpatinfo.brxz))
            //{
            //    throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            //}
            else if (string.IsNullOrWhiteSpace(settpatinfo.ksCode))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_DEPT_IS_NO_FOUND");
            }
            settpatinfo.zyts = DateTimeHelper.GetInHospDays(settpatinfo.ryrq.Value, settpatinfo.cyrq.Value);

            return settpatinfo;
        }

        /// <summary>
        /// HủyQuyết toán xuất viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsnm"></param>
        /// <param name="cancelReason"></param>
        public void DoCancel(string zyh, int expectedjsnm, string cancelReason, string cancelyblsh, out string outTradeNo, out decimal refundAmount)
        {
            refundAmount = 0;
            outTradeNo = null;

            // Lấy thông tin bệnh nhân
            var settpatInfo = GetCancelSettlePatInfo(ref zyh);
            // theo số nhập viện lấy (chưa hoàn)danh sách quyết toán, lấy của nó nhất sau một dòng(zy_js)
            var lastSett = _hospSettlementRepo.GetValidList(zyh, this.OrganizeId).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            if (lastSett.jsnm != expectedjsnm)
            {
                throw new FailedCodeException("HOSP_CANCELSETT_JSNM_IS_CHANGED");
            }
            //zy_jsmx
            var zyjsmxList = _hospSettlementDetailRepo.IQueryable().Where(p => p.jsnm == lastSett.jsnm).ToList();

            //Tính số tiền mặt hoàn lại // điều kiện tiên quyết: chỉ một phư ơng thức thanh toán // chỉ một phư ơng thức thanh toán js.xjzffs sẽ có giá trị 
            if (!string.IsNullOrWhiteSpace(lastSett.xjzffs) && lastSett.xjzf > 0)
            {
                refundAmount = lastSett.xjzf;  //Nênlấy thanh toán của
                outTradeNo = lastSett.OutTradeNo;
            }

            //LưuHủy
            _dischargeSettleDmnService.DoCancel(lastSett, zyjsmxList, cancelReason, cancelyblsh, this.OrganizeId);

        }

        #endregion

        #region 模拟结算（GA）



        #endregion

        #region 重庆中途结算
        public void SavePartialSettle(string zyh, DateTime startTime, DateTime endTime, string fph, InpatientSettFeeRelatedDTO feeRelated
            , CQZyjs05Dto ybfeeRelated, string outTradeNo, string jslx, out int jsnm)
        {
             jsnm = 0;
            // Lấy thông tin bệnh nhân
            var settpatInfo = GetPartialInpatientSettlePatInfo(ref zyh);

            var ypxmlist = GetInpatientSettleItemsDetailList(zyh, startTime, endTime);
            //List<DrugFeeDetailVO> ypjfbList=null;
            //if (list!=null&&list.DrugList!=null&&list.DrugList.Count>0)
            //{
            //    ypjfbList = list.DrugList.ToList();
            //}
            //Lấy nhất sau một lần quyết toán kết thúc ngày quyết toán
            var dbDt = _BookkeepInHosDmnService.GetLastValidMidwaySettTime(zyh, this.OrganizeId);
            if (dbDt.HasValue && dbDt.Value != startTime)
            {
                throw new FailedException("过程中结算状态发生变更，请重试!");
            }

            _dischargeSettleDmnService.commitPartialSettle(zyh, this.OrganizeId, startTime, endTime, feeRelated, ybfeeRelated,jslx, ypxmlist, out jsnm);
        }

        public InpatientSettPatStatusDetailDto GetPartialInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx,string ver)
        {
            // Lấy thông tin bệnh nhân
            var settpatInfo = GetPartialInpatientSettlePatInfo(ref zyh, sfz, kh, cardType, jslx);
            // Lấy tính phí chi tiết(bao gồm:Hạng mục và thuốc)
            //var settleItemsBo = GetInpatientSettleItemsDetailList(zyh);
                settpatInfo.zyts = DateTimeHelper.GetInHospDays(settpatInfo.ryrq.Value, DateTime.Now);
            var resultDto = new InpatientSettPatStatusDetailDto()
            {
                InpatientSettPatInfo = settpatInfo,
                //InpatientSettleItemBO = settleItemsBo
                GroupFeeVOList = _dischargeSettleDmnService.GetHospGroupFeeVOList(zyh, this.OrganizeId,ver),
            };
            return resultDto;
        }

        /// <summary>
        /// Quyết toán giữa chừngLấy thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public InpatientSettPatInfoVO GetPartialInpatientSettlePatInfo(ref string zyh, string sfz = null, string kh = null, string cardType = null, string jslx = null)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                if (!string.IsNullOrWhiteSpace(kh) && !string.IsNullOrWhiteSpace(cardType))
                {
                    // theo số thẻ và loại thẻ đ ổi lấy số nhập viện
                    zyh = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.kh == kh && p.CardType == cardType).OrderByDescending(p => p.CreateTime).Select(p => p.zyh).FirstOrDefault();
                }
                else if (!string.IsNullOrWhiteSpace(sfz) && !string.IsNullOrWhiteSpace(cardType))
                {
                    // theo CMNDvà loại thẻ đ ổi lấy số nhập viện
                    var zjlx = ((int)EnumZJLX.sfz).ToString();
                    zyh = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.zjlx == zjlx && p.zjh == sfz && p.CardType == cardType).OrderByDescending(p => p.CreateTime).Select(p => p.zyh).FirstOrDefault();
                }
            }

            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }

            var settPatinfoList = _dischargeSettleDmnService.SelectInpatientSettPatInfo(zyh, this.OrganizeId);
            if (settPatinfoList == null || settPatinfoList.Count == 0)
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (settPatinfoList.Count > 1)
            {
                throw new FailedCodeException("HOSP_MATCHED_ERROR_MULTI_MATCHED");
            }
            var settpatinfo = settPatinfoList.First();

            if (settpatinfo == null)
            {
                throw new FailedException("患者住院信息未找到");
            }

            else if (settpatinfo.patId == 0)
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (!settpatinfo.ryrq.HasValue || settpatinfo.ryrq <= new DateTime(1990, 01, 01))
            {
                throw new FailedCodeException("HOSP_ERROR_THERE_IS_NO_RYRQ");
            }
            else if (string.IsNullOrWhiteSpace(settpatinfo.ksmc))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_DEPT_IS_NO_FOUND");
            }

            return settpatinfo;
        }

        /// <summary>
        /// chuyển quyết toán xuất viện tiền xử lý
        /// </summary>
        /// <param name="zyh"></param>
        public string PreCYsettle(string zyh) {
            //1.Chưa dừng của y lệnh phán đoán 
            _hospAccountingPlanDetailRepo.CheckAccountingPlanDetailStatus(zyh, this.OrganizeId);
            //2.Tại viện trạng thái phán đoán 
            GetInpatientSettlePatInfo(ref zyh);
            //3.Có hay khôngphát sinh mới củachi phí phán đoán CheckIsExistUnSetted
            DateTime? startTime = null;
            startTime = _BookkeepInHosDmnService.GetLastValidMidwaySettTime(zyh, this.OrganizeId);
            if (!startTime.HasValue)
            {
                startTime = new DateTime(1970, 01, 01);  //Chưa quyết toán qua thì từ nhập viện bắt đ ầu tính tất cả các hạng mục
            }
            var jsItemList = _hospItemBillingRepo.GetItemFeeEntityListByTime(zyh, this.OrganizeId, startTime.Value, DateTime.Now);
            if (jsItemList.Count()>0)
            {
                throw new FailedException("存在未结费用，不能出院");
            }

            //4.Cập nhậtThông tin cơ bản bệnh nhân
            _dischargeSettleDmnService.UpdatePatientInfo(zyh, this.OrganizeId);
            var jsnm = _dischargeSettleDmnService.GetCQLastJsnm(zyh, this.OrganizeId);
            var jylsh = _dischargeSettleDmnService.GetCQLastLsh(jsnm, this.OrganizeId);
            return jylsh;
        }
        #endregion



    }
}
