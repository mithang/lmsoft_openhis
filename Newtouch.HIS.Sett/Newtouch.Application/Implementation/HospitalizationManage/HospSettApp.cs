using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Quyết toán nhập viện 实现
    /// </summary>
    public class HospSettApp : AppBase, IHospSettApp
    {
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly IHospFeeDmnService _hospFeeDmnService;
        private readonly IHosPatAccDmnService _hosPatAccDmnService;
        private readonly ISysPatientComprehensiveNatureRepo _sysPatientComprehensiveNatureRepo;
        private readonly ISysPatientChargeRangeRepo _sysPatientChargeRangeRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysPatientChargeAlgorithmRepo _sysPatientChargeAlgorithmRepo;
        private readonly ISysPatientNatureRepo _sysPatientNatureRepo;
        private readonly IOutPatientDmnService _outPatientDmnService;
        private readonly ISysCashPaymentModelRepo _sysCashPaymentModelRepo;
        private readonly IHospSettDmnService _hospSettDmnService;
        private readonly IIntegratedMaterialDmnService _integratedMaterialDmnService;

        /// <summary>
        /// Quyết toán nhập viện,XemBệnh nhânNhập viện Trạng tháiThông tin
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        public HosSettPatStatusDetailDto GetPatHospStatusDetail(string zyh, string kh)
        {
            //Lấy thông tin bệnh nhân quyết toán nhập viện(cơ bản thông tin, tài khoản, khoaV.v.)
            var settpatinfo = getHospSettPatInfo(ref zyh, kh);

            //Bệnh nhân交 khoản lịch sử 
            var chargeList = _hosPatAccDmnService.GetAccPayInfo((settpatinfo.zyyjjzh ?? 0), this.OrganizeId, zyh);
            if (chargeList.OrderByDescending(p => p.CreateTime).Select(p => p.zhye).FirstOrDefault() != chargeList.Select(p => p.szje).Sum())
            {
                // phán đoán này lần nhập viện có tạm ứng nạp tiền bản ghi 
                throw new FailedException("预交金账户出现收支与余额不一致<br/>请先核查");
            }

            // theo số nhập viện lấy tính phí hạng mục(Đ ã theo ' loại lớn 'Phân loại)
            var dlFeeList = GetHospFeeClassifyWithDLBOList(zyh);
            if (dlFeeList == null || dlFeeList.Count == 0)
            {
                throw new FailedCodeException("HOSP_SETTLEMENTITEM_IS_NULL");
            }

            //Xác minh thu phí BHYT,phải sử dụng hạng mục/dư ợc phẩm BHYT/Thuốc
            checkFeeValidWithBRXZ(dlFeeList, settpatinfo.brxz);

            var resultDto = new HosSettPatStatusDetailDto()
            {
                HospSettPatInfo = settpatinfo,
                ChargeList = chargeList,
                DLFeeList = dlFeeList,
                zhyjj = chargeList.Select(p => p.szje).Sum(),
                jsje = dlFeeList.Select(p => p.jsje).Sum(),
            };
            return resultDto;
        }

        /// <summary>
        /// Quyết toán nhập viện bệnh nhân phân loạithu phí 预览
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsje">Số tiền quyết toán ngăn chặn quá trình trong củachi phí thay đ ổi (chưa loại trừ giảm trừ của)</param>
        public HosSettPatClassifyChargePreviewDto GetHospSettPatClassifyChargePreview(string zyh, DateTime expectedcyrq, decimal expectedjsje)
        {
            //Lấy thông tin bệnh nhân quyết toán nhập viện(cơ bản thông tin, tài khoản, khoaV.v.)
            var settpatinfo = getHospSettPatInfo(ref zyh, "");

            if (settpatinfo.cyrq.Value.Date != expectedcyrq.Date)
            {
                throw new FailedCodeException("HOSP_OUTHOSPITAL_CYRQ_IS_CHANGED");
            }

            //Nhập viện tài khoản thông tin tài khoản tạm ứng số dư //Bệnh nhân交 khoản lịch sử 
            var zhLastCharge = _hosPatAccDmnService.GetAccPayInfo((settpatinfo.zyyjjzh ?? 0), this.OrganizeId, zyh).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            if (zhLastCharge == null)
            {
                // phán đoán này lần nhập viện có tạm ứng nạp tiền bản ghi 
                throw new FailedCodeException("HOSP_PATIENTACCOUNT_REVENUEANDEXPENSE_IS_NULL");
            }

            var resultDto = new HosSettPatClassifyChargePreviewDto();

            resultDto.expectedcyrq = settpatinfo.cyrq.Value;

            //need organizeId
            resultDto.fph = _outPatientDmnService.GetInvoiceListByEmpNo(this.UserIdentity.UserCode, this.OrganizeId);

            // theo số nhập viện lấy tính phí hạng mục(Đ ã theo ' loại lớn 'Phân loại)
            var dlFeeList = GetHospFeeClassifyWithDLBOList(zyh);
            if (dlFeeList == null || dlFeeList.Count == 0)
            {
                throw new FailedCodeException("HOSP_SETTLEMENTITEM_IS_NULL");
            }

            if (dlFeeList.Select(p => p.jsje).Sum() != expectedjsje)
            {
                throw new FailedCodeException("HOSP_CHARGEITEM_IS_CHANGED");
            }
            resultDto.expectedjsje = expectedjsje;
            resultDto.zyh = zyh;

            //Xác minh thu phí BHYT,phải sử dụng hạng mục/dư ợc phẩm BHYT/Thuốc
            checkFeeValidWithBRXZ(dlFeeList, settpatinfo.brxz);

            // nhóm (tính chất bệnh nhân)quyết toán
            var zyjyjsList = getHospFeeJieSuanGroupedByBrxzList(settpatinfo, dlFeeList);

            //得 ghi sổ, tự trả theo phân loại, tự trả, tự trả, khác, số tiền phải thu, trả lại tiền thừa, 舍入差额, số tiền thực thu
            //Tính chất bệnh nhân决定zyjyjsList.Count
            foreach (HospFeeItemOrMedicinListGroupedByBrxzBO zyjyjs in zyjyjsList)
            {
                // tức là Tư ơng ứng một giao dịch 'zy_jyjs'
                //######### chỉ tự trả logic 
                foreach (var fee in zyjyjs.FeeList)
                {
                    resultDto.yingshoukuan += fee.sfjsfymx.total;   // sẽ 因là kết hợp tính chất bệnh nhân hạng mục trùng lặp tính？
                    resultDto.jzfy += fee.sfjsfymx.jzfy;
                    resultDto.flzf += fee.sfjsfymx.flzf;
                    resultDto.zifu += fee.sfjsfymx.sfzf;
                    resultDto.zifei += fee.sfjsfymx.zl;
                    resultDto.jmje += fee.sfjsfymx.jmje;
                }
                resultDto.yingshoukuan = resultDto.yingshoukuan + zyjyjs.jzce;    //Tiền mặt thêm vào ghi sổ超额một phần
                resultDto.jzfy = resultDto.jzfy - zyjyjs.jzce;    // có thể phí ghi sổ减去超额một phần
                resultDto.zifu = resultDto.zifu + zyjyjs.jzce;    //Tự trả thêm vào ghi sổ超额một phần
                resultDto.srce = 0; //Nhập viện không có tiền mặt分币误差
            }

            resultDto.ssk = resultDto.zhye = Convert.ToDecimal(zhLastCharge.zhye);  //Số tiền thực thu
            //Nếu nhập viện tài khoản số dư <thanh toán tiền mặt,thì còn lại một phần dùng khác thanh toán;nếu>Thanh toán tiền mặt,thì nhiều 余một phần dùng cho trả lại tiền thừa

            resultDto.zhaoling = resultDto.zhye - resultDto.yingshoukuan;

            resultDto.HospSettPatInfo = settpatinfo;

            return resultDto;
        }

        /// <summary>
        /// Quyết toán nhập viện GửiQuyết toán,LưuKết quả
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsje">Số tiền quyết toán ngăn chặn quá trình trong củachi phí thay đ ổi (chưa loại trừ giảm trừ của)</param>
        /// <param name="expectedzhaoling"></param>
        /// <param name="xjzfListStr"></param>
        public SettSaveSuccessResultDto SaveSett(string zyh, DateTime expectedcyrq, string fph, decimal expectedyjjzhye, decimal expectedjsje, decimal expectedzhaoling, string xjzfListStr, decimal shishoukuan)
        {
            if (string.IsNullOrWhiteSpace(fph))
            {
                throw new FailedCodeException("HOSP_PLEASE_CHOOSE_FINO");
            }
            if (_outPatientDmnService.ExistsForInvoiceNo(fph, this.OrganizeId))
            {
                throw new FailedCodeException("HOSP_FINO_IS_USED");
            }
            if (expectedzhaoling < 0)
            {
                throw new FailedCodeException("HOSP_ZHAOLING_IS_NEGATIVE");
            }

            //Lấy thông tin bệnh nhân quyết toán nhập viện(cơ bản thông tin, tài khoản, khoaV.v.)
            var settpatinfo = getHospSettPatInfo(ref zyh, "");

            if (settpatinfo.cyrq.Value.Date != expectedcyrq.Date)
            {
                throw new FailedCodeException("HOSP_OUTHOSPITAL_CYRQ_IS_CHANGED");
            }

            //Nhập viện tài khoản thông tin tài khoản tạm ứng số dư //Bệnh nhân交 khoản lịch sử 
            var zhLastCharge = _hosPatAccDmnService.GetAccPayInfo((settpatinfo.zyyjjzh ?? 0), this.OrganizeId, zyh).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            if (zhLastCharge == null)
            {
                throw new FailedCodeException("HOSP_PATIENTACCOUNT_REVENUEANDEXPENSE_IS_NULL");
            }

            if (zhLastCharge.zhye != expectedyjjzhye)
            {
                throw new FailedCodeException("HOSP_PATIENTACCOUNT_BALANCE_IS_CHANGED");
            }

            // theo số nhập viện lấy tính phí hạng mục(Đ ã theo ' loại lớn 'Phân loại)
            var dlFeeList = GetHospFeeClassifyWithDLBOList(zyh);
            if (dlFeeList == null || dlFeeList.Count == 0)
            {
                throw new FailedCodeException("HOSP_SETTLEMENTITEM_IS_NULL");
            }

            if (dlFeeList.Select(p => p.jsje).Sum() != expectedjsje)
            {
                throw new FailedCodeException("HOSP_CHARGEITEM_IS_CHANGED");
            }

            //Xác minh thu phí BHYT,phải sử dụng hạng mục/dư ợc phẩm BHYT/Thuốc
            checkFeeValidWithBRXZ(dlFeeList, settpatinfo.brxz);

            // nhóm (tính chất bệnh nhân)quyết toán
            var zyjyjsList = getHospFeeJieSuanGroupedByBrxzList(settpatinfo, dlFeeList);

            //Dữ liệu kho thay đ ổi Đ ối tư ợng
            var sqlVO = getSettDBUpdateVO(settpatinfo, fph, expectedyjjzhye, expectedzhaoling, zyjyjsList, xjzfListStr, zhLastCharge.xjzffs);

            if ((sqlVO.zy_js.zl ?? 0) != expectedzhaoling)
            {
                throw new FailedException("找零金额发生变更，请重新结算");
            }

            //Lưu vào cơ sở dữ liệu
            _hospSettDmnService.SettSyncToDB(sqlVO, this.OrganizeId);

            var reslt = new SettSaveSuccessResultDto()
            {
                yingshoukuan = sqlVO.zy_js.zje.ToString("0.00"),
                ssk = shishoukuan.ToString("0.00"),
                srce = sqlVO.zy_js.xjwc.ToString("0.00"),
                zhaoling = (sqlVO.zy_js.zl ?? 0).ToString("0.00"),
            };

            return reslt;
        }

        /// <summary>
        /// theo số nhập viện lấy lần này quyết toán tính phí theo loại lớn (phân loại hạng mục)
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<HospFeeClassifyWithDLBO> GetHospFeeClassifyWithDLBOList(string zyh)
        {
            var itemsList = getZyItemsVOList(zyh);
            var ypList = getZyMedicinVOList(zyh);

            var itemBOList = itemsList.GroupBy(p => new { p.dl, p.dlmc }).Select(p => new HospFeeClassifyWithDLBO()
            {
                dl = p.Key.dl,
                dlmc = p.Key.dlmc,
                jsje = p.Select(i => (i.fwfdj + i.dj) * i.sl).Sum(),    //Số tiền quyết toán
                ItemFeeList = p.ToList()
            });

            var medicinBOList = ypList.GroupBy(p => new { p.dl, p.dlmc }).Select(p => new HospFeeClassifyWithDLBO()
            {
                dl = p.Key.dl,
                dlmc = p.Key.dlmc,
                jsje = p.Select(i => (i.dj) * i.sl).Sum(),
                MedicinFeeList = p.ToList()
            });

            return itemBOList.Union(medicinBOList).ToList();
        }

        #region private method

        /// <summary>
        /// LấyTính phí hạng mụcDanh sách
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        private IList<HospItemFeeDetailVO> getZyItemsVOList(string zyh)
        {
            return _hospFeeDmnService.GetAllItemFeeDetailVOList(zyh, this.OrganizeId);
        }

        /// <summary>
        /// Lấy thuốc tính phí danh sách
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        private IList<HospMedicinFeeDetailVO> getZyMedicinVOList(string zyh)
        {
            return _hospFeeDmnService.GetAllMedicinFeeDetailVOList(zyh, this.OrganizeId);
        }

        /// <summary>
        /// Quyết toán nhập viện lấy thông tin cơ bản bệnh nhân(cơ bản thông tin, tài khoản, khoaV.v.) xác minh trạng thái đang nằm viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        private HospSettPatInfoVO getHospSettPatInfo(ref string zyh, string kh)
        {
            if (string.IsNullOrWhiteSpace(zyh) && !string.IsNullOrWhiteSpace(kh))
            {
                var zyList = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.OrganizeId == this.OrganizeId && kh.Equals(p.kh)).ToList();
                if (zyList.Count == 0)
                {
                    throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
                }
                if (zyList.Count(p => p.zybz != ((int)EnumZYBZ.Wry).ToString()
                    && p.zybz != ((int)EnumZYBZ.Ycy).ToString()) > 1)
                {
                    throw new FailedCodeException("HOSP_CARDNO_MULTI_ZYH_PLEASE_USE_ZYH");
                }
                else
                {
                    // tại đ ây có thể quyết toán bản ghi Đ ã sẽ không nhiều 余 một dòng
                    zyh = zyList.Where(p => p.zybz != ((int)EnumZYBZ.Wry).ToString()
                            && p.zybz != ((int)EnumZYBZ.Ycy).ToString()).Select(p => p.zyh).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(zyh)) //说明 không có có thể quyết toán của bản ghi 
                    {
                        zyh = zyList.OrderByDescending(p => p.CreateTime).Select(p => p.zyh).First();
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            var settPatinfoList = _hosPatAccDmnService.GetHospSettPatInfo(zyh, this.OrganizeId);
            if (settPatinfoList == null || settPatinfoList.Count == 0)
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (settPatinfoList.Count > 1)
            {
                throw new FailedCodeException("HOSP_MATCHED_ERROR_MULTI_MATCHED");
            }
            var patinfo = settPatinfoList.First();
            if (patinfo.zybz == ((int)EnumZYBZ.Wry).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_WRY_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (patinfo.zybz == ((int)EnumZYBZ.Bqz).ToString())
            {
                //############ từ khác hệ thống(繁云)Đ ồng bộ tại viện trạng thái giả sử Đ ồng bộ thành công có thể xuất viện lấy đ ến 'cybz'Là1, và 'Thời gian xuất viện'
                patinfo.zybz = ((int)EnumZYBZ.Djz).ToString(); //Chờ thanh toán
                //throw new FailedCodeException("HOSP_CURRENTSTATUS_BQZ_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (patinfo.zybz == ((int)EnumZYBZ.Ycy).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_YCY_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (patinfo.patid == 0)
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (!patinfo.ryrq.HasValue || patinfo.ryrq <= new DateTime(1990, 01, 01))
            {
                throw new FailedCodeException("HOSP_ERROR_THERE_IS_NO_RYRQ");
            }
            else if (string.IsNullOrWhiteSpace(patinfo.brxz))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_NATURE_IS_NO_FOUND");
            }
            else if (string.IsNullOrWhiteSpace(patinfo.ksmc))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_DEPT_IS_NO_FOUND");
            }
            else if (patinfo.zyyjjzh <= 0)
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_THERE_IS_NO_YJJ_ACCOUNT");
            }
            ////############ từ khác hệ thống(繁云)Đ ồng bộ tại viện trạng thái giả sử Đ ồng bộ thành công có thể xuất viện lấy đ ến 'cybz'Là1, và 'Thời gian xuất viện'
            //patinfo.zybz = ((int)EnumZYBZ.Djz).ToString(); //Chờ thanh toán

            // không CầnĐ ồng bộ đ ến dữ liệu kho trong // không Đ ồng thời辰, sẽ có khác nhau của'Nhập viện số ngày'
            patinfo.cyrq = Convert.ToDateTime(DateTime.Now.AddMinutes(-30).ToString());

            patinfo.zyts = DateTimeHelper.GetInHospDays(patinfo.ryrq.Value, patinfo.cyrq.Value);

            //只có mới nhập viện và chờ quyết toán帐trạng thái才允许结账
            if (patinfo.zybz != ((int)EnumZYBZ.Xry).ToString()
                && patinfo.zybz != ((int)EnumZYBZ.Djz).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_NOT_XRY_OR_DJX_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }

            if (patinfo.ybjylx == ((int)EnumYBJYLX.ybjylx6).ToString())
            {
                if (patinfo.ryrq.Value.Year != patinfo.cyrq.Value.Year)
                {
                    throw new FailedCodeException("HOSP_NB_PATIENT_CANNOT_CROSS_YEAR_DO_SETTLEMENT");
                }
            }
            return patinfo;
        }

        /// <summary>
        /// Xác minh thu phí BHYT,phải sử dụng hạng mục/dư ợc phẩm BHYT/Thuốc
        /// </summary>
        /// <param name="dlFeeList"></param>
        /// <param name="brxz"></param>
        private void checkFeeValidWithBRXZ(IList<HospFeeClassifyWithDLBO> dlFeeList, string brxz)
        {
            if (dlFeeList == null || dlFeeList.Count == 0 || string.IsNullOrWhiteSpace(brxz))
            {
                return;
            }
            //Nhập việnthu phí BHYTtính chất bệnh nhân, thu phí BHYT phải sử dụng hạng mục/dư ợc phẩm BHYT/Thuốc
            var pzzyZYYBSFBRXZ = _sysConfigRepo.GetValueByCode(Constants.xtzypz.ZYYBSFBRXZ, this.OrganizeId);
            if (!string.IsNullOrWhiteSpace(pzzyZYYBSFBRXZ) && pzzyZYYBSFBRXZ.Contains(brxz))
            {
                if (dlFeeList.Any(p => (
                 (p.ItemFeeList != null && p.ItemFeeList.Any(i => string.IsNullOrWhiteSpace(i.ybbm)))
                 || (p.MedicinFeeList != null && p.MedicinFeeList.Any(i => string.IsNullOrWhiteSpace(i.ybbm)))
                 )))
                {
                    throw new FailedCodeException("HOSP_YB_PATIENT_CANNOT_BILLING_NOTYB");
                }
            }
            else
            {
                // không phải thu phí BHYTbệnh nhân
            }
        }

        /// <summary>
        /// Lấy nhập việnchi phí quyết toán nhóm by tính chất bệnh nhân => nhóm (tính chất bệnh nhân)quyết toán
        /// </summary>
        /// <param name="settPatInfoVO">Thông tin bệnh nhân(bao gồm thông tin cơ bản nhập viện, hệ thống cơ bản thông tin, tài khoản, khoaV.v.)</param>
        /// <param name="classifyFeeList">Tính phí hạng mục, phân loại tính phí </param>
        /// <returns></returns>
        private IList<HospFeeItemOrMedicinListGroupedByBrxzBO> getHospFeeJieSuanGroupedByBrxzList(HospSettPatInfoVO settPatInfoVO, IList<HospFeeClassifyWithDLBO> classifyFeeList)
        {
            //Bệnh nhân(nhiều)性质 List, tức là kết hợp tính chất bệnh nhân // tại đ ây của sắp xếp 很重 cần.OrderByDescending(p => p.jssx)
            var brxzList = _sysPatientComprehensiveNatureRepo.GetListByZbrxz(settPatInfoVO.brxz, this.OrganizeId);
            if (brxzList == null || brxzList.Count == 0)
            {
                brxzList.Add(new SysPatientComprehensiveNatureEntity()
                {
                    zbrxz = settPatInfoVO.brxz,
                    cbrxz = settPatInfoVO.brxz
                });
            }

            //Hạng mục/Thuốc tính phí ' theo tính chất bệnh nhân' nhóm quyết toán danh sách(只là nhóm tính phí hạng mục/Thuốc)
            var feeGedByBrxzList = getHospFeeListGroupedByBrxzBO(brxzList, classifyFeeList);

            // cho hạng mục/Thuốc bảng tính phí Detail thêm vào 'Có hay khôngBHYT'Của bit dấu hiệu (vị gán giá trị)
            divisionToYBandFYB(feeGedByBrxzList);

            // lần lư ợt tính mỗi hạng mục/Thuốc củachi phí
            jifeiToBrxzFeeList(feeGedByBrxzList);

            // mức khởi trả ############ tạm thời chưa liên quan mức khởi trả 

            //Xử lý thanh toán BHYT giới hạn trên 
            getJZCE(settPatInfoVO.ryzdicd10, feeGedByBrxzList);

            return feeGedByBrxzList;
        }

        /// <summary>
        /// Hạng mục/Thuốc tính phí ' theo tính chất bệnh nhân' nhóm quyết toán danh sách.'Nếu là đ ơn lẻ tính chất bệnh nhân(không phải kết hợp tính chất bệnh nhân), sẽ quay lại một dòng'
        /// </summary>
        /// <returns></returns>
        private IList<HospFeeItemOrMedicinListGroupedByBrxzBO> getHospFeeListGroupedByBrxzBO(IList<SysPatientComprehensiveNatureEntity> brxzList, IList<HospFeeClassifyWithDLBO> classifyFeeList)
        {
            var brxzFeeGroupList = new List<HospFeeItemOrMedicinListGroupedByBrxzBO>();
            foreach (var zhbrxzEntity in brxzList)
            {
                var brxzFeeGroup = new HospFeeItemOrMedicinListGroupedByBrxzBO()
                {
                    brxzzhEntity = zhbrxzEntity,
                    brxzEntity = _sysPatientNatureRepo.IQueryable().Where(p => p.brxz == zhbrxzEntity.cbrxz && p.zt == "1").FirstOrDefault(),
                    FeeList = new List<HospFeeItemOrMedicinDetailBO>(),
                };
                if (brxzFeeGroup.brxzEntity == null)
                {
                    throw new FailedCodeException("HOSP_PATIENT_NATURE_IS_NOT_EXIST");
                }
                //Lấy hiệu lực của bệnh nhânthu phí phạm vi xt_brsffw
                var patChargeRangeList = _sysPatientChargeRangeRepo.IQueryable().Where(p => p.brxz == zhbrxzEntity.cbrxz && p.OrganizeId == this.OrganizeId && p.zt == "1").ToList();
                // vì vậy này cấu hình很关键,容易导致hạng mục không tính phí 

                foreach (var dlFee in classifyFeeList)
                {
                    if (dlFee.ItemFeeList != null && dlFee.ItemFeeList.Count > 0)
                    {
                        foreach (var itemFee in dlFee.ItemFeeList)
                        {
                            if (zhbrxzEntity.sfpc == "1" && (
                                brxzFeeGroup.FeeList.Any(p => p.Item != null && p.Item.jfbbh == itemFee.jfbbh)
                                || brxzFeeGroupList.Any(p => p.FeeList.Any(i => i.Item != null && i.Item.jfbbh == itemFee.jfbbh))
                                ))
                            {
                                continue;   //以 kế hoạch tính và cấu hình là không trùng lặp tính
                            }
                            if (patChargeRangeList == null || patChargeRangeList.Count == 0)
                            {
                                brxzFeeGroup.FeeList.Add(new HospFeeItemOrMedicinDetailBO()
                                {
                                    dl = itemFee.dl,
                                    dlmc = itemFee.dlmc,
                                    Item = itemFee
                                });
                            }
                            //Có匹配của hạng mục thu phí
                            else
                            {
                                //sfxm nhất quán hoặc dl một 样,fw cấu hình Củasfxm không 作限制
                                if (patChargeRangeList.Any(p => p.sfxm == itemFee.sfxm) || patChargeRangeList.Any(p => p.dl == itemFee.dl && (string.IsNullOrEmpty(p.sfxm) || p.sfxm == "0")))
                                {
                                    brxzFeeGroup.FeeList.Add(new HospFeeItemOrMedicinDetailBO()
                                    {
                                        dl = itemFee.dl,
                                        dlmc = itemFee.dlmc,
                                        Item = itemFee
                                    });
                                }
                                //Tính chất bệnh nhân có thu phí phạm vi cấu hình,nhưng này 项(hạng mục thu phí/Thuốc)chưa cấu hình,thì không tính này 项
                            }
                        }
                    }
                    if (dlFee.MedicinFeeList != null && dlFee.MedicinFeeList.Count > 0)
                    {
                        foreach (var medicinFee in dlFee.MedicinFeeList)
                        {
                            if (zhbrxzEntity.sfpc == "1" && (
                                brxzFeeGroup.FeeList.Any(p => p.Medicin != null && p.Medicin.jfbbh == medicinFee.jfbbh)
                               || brxzFeeGroupList.Any(p => p.FeeList.Any(i => i.Medicin != null && i.Medicin.jfbbh == medicinFee.jfbbh))
                               ))
                            {
                                continue;
                            }
                            if (patChargeRangeList == null || patChargeRangeList.Count == 0)
                            {
                                brxzFeeGroup.FeeList.Add(new HospFeeItemOrMedicinDetailBO()
                                {
                                    dl = medicinFee.dl,
                                    dlmc = medicinFee.dlmc,
                                    Medicin = medicinFee
                                });
                            }
                            //Có匹配của hạng mục thu phí
                            else
                            {
                                //sfxm nhất quán hoặc dl một 样,fw cấu hình Củasfxm không 作限制
                                if (patChargeRangeList.Any(p => p.dl == medicinFee.dl && (string.IsNullOrEmpty(p.sfxm) || p.sfxm == "0")))
                                {
                                    brxzFeeGroup.FeeList.Add(new HospFeeItemOrMedicinDetailBO()
                                    {
                                        dl = medicinFee.dl,
                                        dlmc = medicinFee.dlmc,
                                        Medicin = medicinFee
                                    });
                                }
                                //Tính chất bệnh nhân có thu phí phạm vi cấu hình,nhưng này 项(hạng mục thu phí/Thuốc)chưa cấu hình,thì không tính này 项
                            }
                        }
                    }
                }
                brxzFeeGroupList.Add(brxzFeeGroup);
            }
            return brxzFeeGroupList;
        }

        /// <summary>
        /// cho 'Có hay không cầnBHYT giao dịch ' bit dấu hiệu gán giá trị 
        /// </summary>
        /// <param name="boList"></param>
        private void divisionToYBandFYB(IList<HospFeeItemOrMedicinListGroupedByBrxzBO> brxzFeeGroupList)
        {
            //Lấy ngoại trú cấu hình: không tải lênBHYT loại lớn 
            var pzzyBSCYBStr = _sysConfigRepo.GetValueByCode(Constants.xtzypz.DL_BSCYB, this.OrganizeId) ?? "";

            string[] arrDl = pzzyBSCYBStr.TrimEnd(',').Split(',').Where(p => !string.IsNullOrWhiteSpace(pzzyBSCYBStr)).ToArray();
            Dictionary<string, string> dlDict = new Dictionary<string, string>();
            foreach (string dl in arrDl)
            {
                if (dlDict.ContainsKey(dl))
                {
                    continue;
                }
                dlDict.Add(dl, dl);
            }

            foreach (var brxzFeeGroup in brxzFeeGroupList)
            {
                foreach (var fee in brxzFeeGroup.FeeList)
                {
                    if (dlDict.Count == 0)
                    {
                        fee.isNeedYBJY = true;
                    }
                    else if (fee.Item != null && dlDict.ContainsKey(fee.dl))
                    {
                        fee.isNeedYBJY = false;
                    }
                    else if (fee.Medicin != null && dlDict.ContainsKey(fee.dl))
                    {
                        fee.isNeedYBJY = false;
                    }
                    else
                    {
                        fee.isNeedYBJY = true;
                    }
                }
            }
        }

        /// <summary>
        /// riêng lẻ tính mỗi hạng mục/Thuốc củachi phí => Ghi sổ, tự trả theo phân loại, tự trả, tự trả, khác
        /// </summary>
        /// <param name="brxzFeeGroupList"></param>
        private void jifeiToBrxzFeeList(IList<HospFeeItemOrMedicinListGroupedByBrxzBO> brxzFeeGroupList)
        {
            // một lần 性材料 loại lớn cấu hình
            var pzzyYCXCLDMValue = _sysConfigRepo.GetValueByCode(Constants.xtzypz.YCXCLDM, this.OrganizeId);
            string[] arrYCXCLDl = pzzyYCXCLDMValue.Split(',').Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();

            foreach (var brxzFeeGroup in brxzFeeGroupList)
            {
                foreach (var fee in brxzFeeGroup.FeeList)
                {
                    jifeiToHospFeeItemOrMedicinDetailBO(fee, arrYCXCLDl);   // này một 步và khi trư ớc bệnh nhân không có关
                }
                // áp dụng thu phí thuật toán (tính chất bệnh nhân) // thuật toán danh sách sắp xếp theo cấp bậc 
                var brxzSfsfList = _sysPatientChargeAlgorithmRepo.IQueryable().Where(p => p.brxz == brxzFeeGroup.brxzzhEntity.cbrxz && p.OrganizeId == this.OrganizeId && p.zt == "1").OrderByDescending(p => p.sfjb).ToList();
                foreach (var fee in brxzFeeGroup.FeeList)
                {
                    jifeiWithBrsfsfToHospFeeItemOrMedicinDetailBO(fee, brxzSfsfList);
                }
            }
        }

        /// <summary>
        /// 简 cần tính => Số tiền tự chi trả(tính chất tự trả là tự trả), có thể thanh toán số tiền, tự trả theo phân loại
        /// </summary>
        /// <param name="fee"></param>
        /// <param name="arrYCXCLDl"> một lần 性材料 loại lớn </param>
        private void jifeiToHospFeeItemOrMedicinDetailBO(HospFeeItemOrMedicinDetailBO fee, string[] arrYCXCLDl)
        {
            if (fee.Item != null)
            {
                var xm = fee.Item;  //Tính phí hạng mục
                fee.jmhje = xm.je - xm.jmje;    //Số tiền sau giảm trừ

                #region 一次性材料收费项目判断
                if (arrYCXCLDl != null && arrYCXCLDl.Contains(fee.dl))
                {
                    var sfclxmzhEntity = _integratedMaterialDmnService.GetSfclxmzhEntity(fee.Item.sfxm, this.OrganizeId);
                    if (sfclxmzhEntity != null)
                    {
                        fee.sfclxmzhEntity = sfclxmzhEntity;
                        fee.zlfy = fee.jmhje;   // trư ớc tất cả ghi vào tự chi trả
                        return;
                    }
                }
                #endregion

                // theo hạng mụcthu phí性质
                if (xm.zfxz == ((int)EnumZiFuXingZhi.ZF).ToString())
                {
                    fee.zlfy = fee.jmhje;   //Tự trả thì số tiền sau giảm trừ thì là số tiền tự chi trả
                }
                else if (xm.zfxz == ((int)EnumZiFuXingZhi.KB).ToString())
                {
                    fee.kbje = fee.jmhje;   //Số tiền có thể ghi sổ
                }
                else
                {
                    //Có thể thanh toán:Chi phí ×(1－tỷ lệ tự trả)
                    //Tự trả theo phân loại:Chi phí ×tỷ lệ tự trả
                    if (xm.zfbl >= 0)
                    {
                        fee.kbje = (fee.jmhje * (1 - xm.zfbl));    //Số tiền có thể ghi sổ
                        //Nếu một loại 药là乙10%,Đ ơn giá100 tệ, ý nghĩa thì là说, này 药 đ ầu tiên 10%là cần cá nhân chịu của
                        // này 10 tệ 钱 thì là'Tự trả theo phân loại'
                        //另90 tệ còn chưa là cái gọi là của'Tự trả', cần theo 个人của tỷ lệ thanh toán BHYT BHYTvà个人 theo tỷ lệ chịu 
                        fee.flzf = (fee.jmhje * xm.zfbl);  //Số tiền tự trả theo phân loại
                    }
                    //Khi tỷ lệ tự trả là số âm giờ,biểu thị tự trả đ ịnh mức
                    else
                    {
                        if (xm.jmbl < 0)
                        {
                            throw new FailedException(xm.jfbbh + "定额自负，减免比例不能为负数，请重新配置");
                        }
                        //Tỷ lệ giảm trừ thông thư ờng 0, vì vậy flzf thông thư ờng đ ều là Đ ịnh mức*Số lư ợng
                        //Số tiền tự trả theo phân loại: Đ ịnh mức * (1-tỷ lệ giảm trừ)* Số lư ợng
                        // có thể 说明.jmbl là tại tự trả đ ịnh mức giờ 才có意义
                        fee.flzf = (-xm.zfbl) * (1 - xm.jmbl) * xm.sl;
                        fee.kbje = fee.jmhje - fee.flzf;    //Số tiền có thể ghi sổ
                    }
                }
            }
            else if (fee.Medicin != null)
            {
                var medicin = fee.Medicin;
                fee.jmhje = medicin.je - medicin.jmje;
                if (medicin.zfxz == ((int)EnumZiFuXingZhi.ZF).ToString())
                {
                    fee.zlfy = fee.jmhje;
                }
                else if (medicin.zfxz == ((int)EnumZiFuXingZhi.KB).ToString())
                {
                    fee.kbje = fee.jmhje;
                }
                else
                {
                    if (medicin.zfbl >= 0)
                    {
                        fee.kbje = (fee.jmhje * (1 - medicin.zfbl));
                        fee.flzf = (fee.jmhje * medicin.zfbl);
                    }
                    else
                    {
                        if (medicin.jmbl < 0)
                        {
                            throw new FailedException(medicin.jfbbh + "定额自负，减免比例不能为负数，请重新配置");
                        }
                        fee.flzf = (-medicin.zfbl) * (1 - medicin.jmbl) * medicin.sl;
                        fee.kbje = fee.jmhje - fee.flzf;
                    }
                }
            }

        }

        /// <summary>
        /// theo hạng mục thu phí tổng cộng và bệnh nhân''''Thu phí thuật toán ''''Tính
        /// </summary>
        /// <param name="fee"></param>
        /// <param name="brxzSfsfList">Tính chất bệnh nhân Tư ơng ứng củathu phí thuật toán danh sách</param>
        private void jifeiWithBrsfsfToHospFeeItemOrMedicinDetailBO(HospFeeItemOrMedicinDetailBO fee, IList<SysPatientChargeAlgorithmEntity> brxzSfsfList)
        {
            //Hạng mục(thuốc)匹配củathu phí thuật toán 
            IList<SysPatientChargeAlgorithmEntity> sfsfList = null;
            if (fee.Item != null)
            {
                var xm = fee.Item;  //Tính phí hạng mục
                sfsfList = brxzSfsfList.Where(p => p.sfxm == xm.sfxm).ToList();
            }
            if (sfsfList == null || sfsfList.Count == 0)
            {
                sfsfList = brxzSfsfList.Where(p => p.sfxm == "" && (p.dl == fee.dl || (p.dl ?? "").Trim() == "*")).ToList();
            }

            if (sfsfList == null || sfsfList.Count == 0)    //Nếu không có bệnh nhânthu phí thuật toán 
            {
                // không có đặt thuật toán tổng chi phí = có thể ghi sổ＋tự trả theo phân loại＋tự chi trả
                fee.sfjsfymx.total = fee.kbje + fee.flzf + fee.zlfy;
                //Tự trả theo phân loại
                fee.sfjsfymx.flzf = fee.flzf;
                //Phí ghi sổ
                fee.sfjsfymx.jzfy = fee.kbje;
                //Chi phí tự chi trả
                fee.sfjsfymx.zl = fee.zlfy;
                //Tiền mặt
                fee.sfjsfymx.xj = fee.flzf + fee.zlfy;  //Thanh toán tiền mặt = Tự trả theo phân loại + Tự trả
            }
            else
            {
                if (sfsfList.Select(p => p.fyfw).Distinct().Count() > 1)
                {
                    throw new FailedCodeException("HOSP_PATIENT_NATURE_AND_CATEGORY_THE_SUANFALIST_IS_NOT_SAME_RANGE");
                }

                #region 费用合计    根据病人不同的收费算法，=>现金、记账费用、分类自负、自理
                if (sfsfList[0].fyfw == Constants.xtbrsfsffyfw.fyfw0)
                {
                    //Tổng số tiền giao dịch = Tổng chi phí trong phạm vi quyết toán BHYT - Tự trả theo phân loại;Tiền mặt:Tự trả theo phân loại+Tự chi trả+Tự trả
                    fee.sfjsfymx.jzfy = fee.kbje; //Phí ghi sổ:Có thể ghi sổ
                    fee.sfjsfymx.flzf = fee.flzf; //Tự trả theo phân loại
                    fee.sfjsfymx.zl = fee.zlfy;   //Chi phí tự chi trả
                    fee.sfjsfymx.xj = fee.flzf + fee.zlfy;    //Tiền mặt
                }
                else if (sfsfList[0].fyfw == Constants.xtbrsfsffyfw.fyfw1)
                {
                    //Tổng số tiền giao dịch = Tổng chi phí trong phạm vi quyết toán BHYT;Tiền mặt:Tự chi trả+Tự trả
                    fee.sfjsfymx.jzfy = fee.kbje + fee.flzf;  //Phí ghi sổ:Có thể ghi sổ + Tự trả theo phân loại
                    fee.sfjsfymx.flzf = 0m;   //Tự trả theo phân loại
                    fee.sfjsfymx.zl = fee.zlfy;   //Chi phí tự chi trả
                    fee.sfjsfymx.xj = fee.zlfy;   //Tiền mặt
                }
                else if (sfsfList[0].fyfw == Constants.xtbrsfsffyfw.fyfw2)
                {
                    //Tổng số tiền giao dịch = Tổng chi phí trong phạm vi quyết toán BHYT;Tiền mặt:Tự trả
                    fee.sfjsfymx.jzfy = fee.kbje + fee.flzf + fee.zlfy;   //Phí ghi sổ:Có thể ghi sổ + Tự trả theo phân loại + Tự chi trả
                    fee.sfjsfymx.flzf = 0m;   //Tự trả theo phân loại
                    fee.sfjsfymx.zl = 0m; //Chi phí tự chi trả
                    fee.sfjsfymx.xj = 0m; //Tiền mặt
                }
                else if (sfsfList[0].fyfw == Constants.xtbrsfsffyfw.fyfw3)
                {
                    //Tổng số tiền giao dịch = Tổng chi phí trong phạm vi quyết toán BHYT;Tiền mặt:Tự trả
                    fee.sfjsfymx.jzfy = fee.kbje + fee.flzf + fee.zlfy;   //Phí ghi sổ:Có thể ghi sổ + Tự trả theo phân loại + Tự chi trả + Hoàn toàn tự trả
                    fee.sfjsfymx.flzf = 0m;   //Tự trả theo phân loại
                    fee.sfjsfymx.zl = 0m; //Chi phí tự chi trả
                    fee.sfjsfymx.xj = 0m; //Tiền mặt
                }
                #endregion

                #region 费用计算
                decimal jsAmount = fee.sfjsfymx.jzfy;
                decimal jsedAmount = 0m;    // theo chi phí phạm vi cần tính của số tiền
                decimal sfzf = 0m;  // thuật toán tự trả(tính chất tự trả là tự chi trả giờ ghi vào ' thuật toán tự chi trả')
                //Nếu có chi phí giới hạn trên,thì theo thuật toán cấp bậc tính(thực tế 只需tínhchi phí tự chi trả) 
                foreach (var sfEntity in sfsfList)
                {
                    var sl = (fee.Item != null ? fee.Item.sl : fee.Medicin.sl);
                    decimal sffysx = sfEntity.fysx * sl;   //Chi phí giới hạn trên 
                    if (sffysx > fee.sfjsfymx.jzfy)
                    {
                        sfzf += jsAmount == 0 ? 0
                                  : (sfEntity.zfbl >= 0 ? jsAmount * sfEntity.zfbl : (-sfEntity.zfbl * sl));
                        jsedAmount += jsAmount;
                        jsAmount = fee.sfjsfymx.jzfy - jsedAmount;
                        break;
                    }
                    else
                    {
                        if (sffysx > jsAmount)
                        {
                            jsedAmount += sffysx - jsedAmount;
                            sfzf += jsAmount == 0 ? 0
                                : (sfEntity.zfbl >= 0 ? jsAmount * sfEntity.zfbl : (-sfEntity.zfbl * sl));
                        }
                        else
                        {
                            if (sffysx == 0)
                            {
                                sfzf += jsAmount == 0 ? 0
                                    : (sfEntity.zfbl >= 0 ? jsAmount * sfEntity.zfbl : (-sfEntity.zfbl * sl));
                                jsedAmount = jsAmount;
                                break;
                            }
                            else
                            {
                                jsedAmount += sffysx;
                                sfzf += sffysx == 0 ? 0
                                     : (sfEntity.zfbl >= 0 ? sffysx * sfEntity.zfbl : (-sfEntity.zfbl * sl));
                            }
                        }
                        jsAmount = fee.sfjsfymx.jzfy - jsedAmount;
                    }
                }
                if (jsedAmount != fee.sfjsfymx.jzfy)
                {
                    sfzf += fee.sfjsfymx.jzfy - jsedAmount;
                }
                #endregion

                if (sfsfList[0].zfxz == ((int)EnumSuanFaZFXZ.ZL).ToString())
                {
                    fee.sfjsfymx.sfzl += sfzf;  //Tính chất tự trả là tự chi trả
                }
                else
                {
                    fee.sfjsfymx.sfzf += sfzf;
                }
                if (sfsfList[0].fyfw == Constants.xtbrsfsffyfw.fyfw3)
                {
                    //Nếu là hoàn toàn tự trả,ghi sổ số tiền là tự chi trả
                    fee.sfjsfymx.zl += fee.sfjsfymx.jzfy - sfzf;
                    fee.sfjsfymx.jzfy = 0m;
                    fee.sfjsfymx.xj += fee.sfjsfymx.zl;
                }
                else
                {
                    // có thể phí ghi sổ
                    fee.sfjsfymx.jzfy = fee.sfjsfymx.jzfy - sfzf;
                }
                //Tiền mặt
                fee.sfjsfymx.xj += sfzf;
                //Có thuật toán tổng chi phí = tiền mặt
                fee.sfjsfymx.total = fee.sfjsfymx.xj;
            }
            fee.ybjyje = fee.sfjsfymx.jzfy;
            fee.ybjyfwje = fee.sfjsfymx.jzfy + fee.sfjsfymx.flzf;

            // một lần 性材料 //放tại này thứ tự không 问题？
            if (fee.sfclxmzhEntity != null)
            {
                //.jmhje fee.zlfy  fee.sfjsfymx.zl
                if (fee.sfjsfymx.zl > fee.sfclxmzhEntity.kbfwsx)
                {
                    fee.sfjsfymx.zl = fee.sfjsfymx.zl - fee.sfclxmzhEntity.kbfwsx;
                    fee.sfjsfymx.jzfy += fee.sfclxmzhEntity.kbfwsx;
                }
                else
                {
                    fee.sfjsfymx.zl = 0;
                    fee.sfjsfymx.jzfy += fee.sfjsfymx.zl; //全报
                }
            }
        }

        /// <summary>
        /// Ghi sổ超额
        /// </summary>
        /// <param name="icd10">Chẩn đoán nhập viện Của icd10</param>
        /// <param name="brxzFeeGroupList"></param>
        private void getJZCE(string icd10, IList<HospFeeItemOrMedicinListGroupedByBrxzBO> brxzFeeGroupList)
        {
            // theo tính chất bệnh nhân và chẩn đoán phán đoán, có thể 保chi phí của giới hạn trên,如少儿基金,thứ một chẩn đoán là急性xx阑尾, thanh toán BHYT giới hạn trên là1100
            // ví dụ:[{"brxz":"18","icd10":"A01.001","bxsx":"1100"}]
            var pzzyBRXZZDBXSXValue = _sysConfigRepo.GetValueByCode(Constants.xtzypz.ZY_BRXZ_ZD_BXSX, this.OrganizeId);
            if (string.IsNullOrWhiteSpace(pzzyBRXZZDBXSXValue))
            {
                return;
            }
            var pzedBxsxList = Json.ToObject<List<BrxzZdBxsxBO>>(pzzyBRXZZDBXSXValue);

            foreach (var brxzFeeGroup in brxzFeeGroupList)
            {
                BrxzZdBxsxBO matchedXZPZ = null;
                if (pzedBxsxList == null ||
                    (matchedXZPZ = pzedBxsxList.FirstOrDefault(p => p.brxz == brxzFeeGroup.brxzzhEntity.cbrxz && p.icd10 == icd10)) == null
                    )
                {
                    continue;
                }
                var jzjeHj = getBrxzFeeListJZHJ(brxzFeeGroup);   //Tính出phí ghi sổ tổng cộng (có thể thanh toán số tiền)
                if (jzjeHj > matchedXZPZ.bxsx)
                {
                    brxzFeeGroup.jzce = jzjeHj - matchedXZPZ.bxsx;
                }
            }
        }

        /// <summary>
        /// Lấy phí ghi sổ tổng cộng 
        /// </summary>
        /// <param name="brxzFeeGroup"></param>
        /// <returns></returns>
        private decimal getBrxzFeeListJZHJ(HospFeeItemOrMedicinListGroupedByBrxzBO brxzFeeGroup)
        {
            decimal hj = 0;
            foreach (var fee in brxzFeeGroup.FeeList)
            {
                if (fee.isNeedYBJY) //FYBCójzfy么
                {
                    hj += fee.sfjsfymx.jzfy;
                }
                ////########### mức khởi trả số tiền ？？ BHYTcó BHYTcủa mức khởi trả số tiền？？ //Ghi sổ超额 ？？
            }
            return hj;
        }

        /// <summary>
        /// Lấy dữ liệu kho thay đ ổi tập hợp Đ ối tư ợng(đ ể tại事物 trong một lần gửi)
        /// </summary>
        /// <returns></returns>
        private OutHospSettDBDataUpdateCollectVO getSettDBUpdateVO(HospSettPatInfoVO settpatinfo, string fph, decimal zhye, decimal zhaoling, IList<HospFeeItemOrMedicinListGroupedByBrxzBO> zyjyjsList, string xjzfListStr, string defaultxjzffs)
        {
            var sqlVO = new OutHospSettDBDataUpdateCollectVO()
            {
                zy_jsmxList = new List<HospSettlementDetailEntity>(),
                zy_jyjsList = new List<HospTransactionSettlementEntity>(),
                zy_jyjsmxList = new List<HospTransactionSettlementDetailEntity>(),

                zy_jszffsList = new List<HospSettlementPaymentModelEntity>(),

                xt_brzhszjlList = new List<SysPatientAccountRevenueAndExpenseEntity>(),

                zy_jsdlList = new List<HospSettlementCategoryEntity>(),
            };

            //zy_js
            var zy_js = new HospSettlementEntity()
            {
                jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_js"),
                OrganizeId = this.OrganizeId,
                zyh = settpatinfo.zyh,
                brxz = settpatinfo.brxz,
                zyts = DateTimeHelper.GetInHospDays(settpatinfo.ryrq.Value, settpatinfo.cyrq.Value),
                fph = fph,
                jszt = ((int)EnumJieSuanZT.YJ).ToString(),
                jsksrq = settpatinfo.ryrq,
                jsjsrq = settpatinfo.cyrq,
                jylx = settpatinfo.ybjylx,
                CreatorCode = this.UserIdentity.UserCode,
                CreateTime = DateTime.Now,
                zt = "1",
                zl = zhaoling,   //Trả lại tiền thừa
                xjzffs = string.Empty,
            };

            foreach (HospFeeItemOrMedicinListGroupedByBrxzBO zyjyjs in zyjyjsList)
            {
                //(kết hợp tính chất bệnh nhân) tức là Tư ơng ứng một giao dịch 'zy_jyjs'
                var zy_jyjs = new HospTransactionSettlementEntity()
                {
                    jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_jyjs"),
                    OrganizeId = this.OrganizeId,
                    gljsnm = zy_js.jsnm,
                    zyh = settpatinfo.zyh,
                    sbrxz = zyjyjs.brxzEntity.brxz,
                    zyts = zy_js.zyts,
                    xjwc = 0,
                    xjzffs = string.Empty,
                    fph = zy_js.fph,
                    jszt = ((int)EnumJieSuanZT.YJ).ToString(),
                    jsksrq = settpatinfo.ryrq,
                    jsjsrq = settpatinfo.cyrq,
                    fpdm = string.Empty,
                    jylx = settpatinfo.ybjylx,
                    CreatorCode = this.UserIdentity.UserCode,
                    CreateTime = DateTime.Now,
                    zt = "1",
                };

                //######### chỉ tự trả logic 

                foreach (var fee in zyjyjs.FeeList)
                {
                    //zy_js
                    zy_js.zje += fee.jmhje; //Tổng số tiền tức là số tiền sau giảm trừ总và
                    zy_js.zlfy += fee.zlfy; //Chi phí tự chi trả thuật toán trư ớc 
                    zy_js.zffy += fee.sfjsfymx.sfzf; //Chi phí tự trả thuật toán tự trả
                    zy_js.flzffy += fee.sfjsfymx.flzf;  //Chi phí tự trả theo phân loại
                    zy_js.jzfy += fee.sfjsfymx.jzfy;    //Phí ghi sổ
                    zy_js.xjzf += fee.sfjsfymx.total;   //Thanh toán tiền mặt
                    zy_js.jmje += fee.sfjsfymx.jmje; //Số tiền giảm trừ
                    //zy_js.ysk //Tạm thu //CS không gán giá trị 
                    //zy_js.zl //Trả lại tiền thừa //CS không gán giá trị 

                    //zy_jyjs
                    zy_jyjs.zlfy += fee.zlfy;   //Chi phí tự chi trả
                    zy_jyjs.zffy += fee.sfjsfymx.sfzf;  //Chi phí tự trả
                    zy_jyjs.flzffy = fee.sfjsfymx.flzf;     //Chi phí tự trả theo phân loại
                    zy_jyjs.jzfy += fee.sfjsfymx.jzfy;  //Phí ghi sổ
                    zy_jyjs.xjzf += fee.sfjsfymx.total; //Thanh toán tiền mặt
                    zy_jyjs.jmje += fee.sfjsfymx.jmje;  //Số tiền giảm trừ
                    //zy_jyjs.ysk //Tạm thu //CS không gán giá trị 
                    //zy_jyjs.zl //Trả lại tiền thừa //CS không gán giá trị 

                    //a new zy_jsmx
                    var zyjsmx = new HospSettlementDetailEntity()
                    {
                        jsnm = zy_js.jsnm,  //Quyết toán nhập viện của nội dung 
                        OrganizeId = this.OrganizeId,
                        jyfwje = fee.ybjyfwje,
                        jyje = fee.ybjyje,
                        xmjfbbh = fee.Item != null ? fee.Item.jfbbh : 0,
                        ypjfbbh = fee.Medicin != null ? fee.Medicin.jfbbh : 0,
                        yzlx = (fee.Item != null ? (int)EnumYiZhuXZ.XM : (fee.Medicin != null ? (int)EnumYiZhuXZ.YP : 0)).ToString(),
                        CreatorCode = this.UserIdentity.UserCode,
                        CreateTime = DateTime.Now,
                        zt = "1",
                    };
                    sqlVO.zy_jsmxList.Add(zyjsmx);

                    //a new zy_yjjsmx
                    var zyjyjsmx = new HospTransactionSettlementDetailEntity()
                    {
                        jsnm = zy_jyjs.jsnm,    //Nhập viện giao dịch quyết toán của mã nội bộ
                        OrganizeId = this.OrganizeId,
                        xmjfbbh = fee.Item != null ? fee.Item.jfbbh : 0,
                        ypjfbbh = fee.Medicin != null ? fee.Medicin.jfbbh : 0,
                        yzlx = (fee.Item != null ? (int)EnumYiZhuXZ.XM : (fee.Medicin != null ? (int)EnumYiZhuXZ.YP : 0)).ToString(),
                        jyfwje = fee.ybjyfwje,
                        jyje = fee.ybjyje,
                        CreatorCode = this.UserIdentity.UserCode,
                        CreateTime = DateTime.Now,
                        zt = "1",
                    };
                    sqlVO.zy_jyjsmxList.Add(zyjyjsmx);
                }

                //Tổng số tiền:Chi phí tự chi trả+Chi phí tự trả+Chi phí tự trả theo phân loại+Phí ghi sổ
                zy_jyjs.zje = zy_jyjs.zlfy + zy_jyjs.zffy + zy_jyjs.flzffy + zy_jyjs.jzfy;
                sqlVO.zy_jyjsList.Add(zy_jyjs);

                //Quyết toán nhập viện loại lớn 

                var ybjsfwze = zyjyjs.FeeList.Sum(p => p.ybjyfwje);

                var dlList = zyjyjs.FeeList.Select(p => p.dl).Distinct().ToList();
                foreach (var dl in dlList)
                {
                    var zy_jsdl = new HospSettlementCategoryEntity()
                    {
                        jsnm = zy_jyjs.jsnm,    //Là liên kết 'Nhập viện giao dịch quyết toán',而 không phải 'Quyết toán nhập viện'
                        OrganizeId = this.OrganizeId,
                        dl = dl,
                        jsrq = DateTime.Now,
                        CreatorCode = this.UserIdentity.UserCode,
                        CreateTime = DateTime.Now,
                        zt = "1",
                    };
                    var feeList = zyjyjs.FeeList.Where(p => p.dl == dl && p.isNeedYBJY);
                    if (feeList != null && feeList.Count() > 0)
                    {
                        //Chi phí bảo hiểm y tế
                        if (ybjsfwze == 0)
                        {
                            zy_jsdl.zffy = 0m;
                            zy_jsdl.kbfy = 0m;
                        }
                        else
                        {
                            //yb.jzfy + yb.flzf/ returnValue.ybjsfwze chỉ dùng 做tính trăm 分比
                            decimal percent = feeList.Sum(p => p.ybjyfwje) / ybjsfwze;
                            zy_jsdl.zffy = percent * zy_jyjs.zffy;//Số tiền theo phân loại/Tổng số tiền theo phân loại*Số tiền tự trả
                            zy_jsdl.kbfy = percent * zy_jyjs.jzfy;//Số tiền theo phân loại/Tổng số tiền theo phân loại*Ghi sổ số tiền
                        }
                    }
                    else
                    {
                        feeList = zyjyjs.FeeList.Where(p => p.dl == dl);
                        // không phải chi phí bảo hiểm y tế
                        zy_jsdl.zffy = feeList.Sum(p => p.sfjsfymx.sfzf); // thuật toán tự trả
                        zy_jsdl.kbfy = feeList.Sum(p => p.sfjsfymx.jzfy);   //Có thể thanh toánChi phí
                    }
                    zy_jsdl.jmfy = feeList.Sum(p => p.sfjsfymx.jmje);   //Số tiền giảm trừ
                    zy_jsdl.flzffy = feeList.Sum(p => p.sfjsfymx.flzf); //Chi phí tự trả theo phân loại
                    zy_jsdl.zlfy = feeList.Sum(p => p.sfjsfymx.zl); //Chi phí tự chi trả
                    //Tổng số tiền
                    zy_jsdl.zje = zy_jsdl.flzffy + zy_jsdl.zlfy + zy_jsdl.jmfy + zy_jsdl.zffy + zy_jsdl.kbfy;
                    sqlVO.zy_jsdlList.Add(zy_jsdl);
                }
            }

            #region 系统病人账户收支记录
            var xjzffsList = _sysCashPaymentModelRepo.GetLazyList();
            //Hệ thống bệnh nhânthu chi tài khoản bản ghi 
            var newPaymentBOList = Json.ToObject<IList<HospSettPaymentRecordBO>>(xjzfListStr);
            if (!newPaymentBOList.Any(p => p.xjzffs == Constants.xtzffs.ZYYJZHZF))
            {
                throw new FailedCodeException("HOSP_YJK_PAYMENTMODEL_MISSSING_YJK");
            }
            foreach (var pBo in newPaymentBOList)
            {
                if (pBo.xjzffs == Constants.xtzffs.ZYYJZHZF)
                {
                    continue;
                }
                var xt_brzhszjl = new SysPatientAccountRevenueAndExpenseEntity()
                {
                    OrganizeId = this.OrganizeId,
                    zh = settpatinfo.zyyjjzh ?? 0,
                    patid = settpatinfo.patid ?? 0,
                    szje = pBo.zfje,
                    zhye = (zhye += pBo.zfje),
                    pzh = string.Empty,
                    szxz = ((int)AccSzxz.ZYBJK).ToString(),
                    //xjzffsbh = xjzffsList.Where(p => (p.xjzffs ?? "").Equals(pBo.xjzffs)).Select(p => p.xjzffsbh).FirstOrDefault(),
                    xjzffs = "",    //&update170720
                    jsnm = zy_js.jsnm,
                    zyh = settpatinfo.zyh,
                    CreatorCode = this.UserIdentity.UserCode,
                    CreateTime = DateTime.Now,
                    zt = "1",
                };

                sqlVO.xt_brzhszjlList.Add(xt_brzhszjl);
            }
            // một dòng nhập viện支出消费của số tiền
            sqlVO.xt_brzhszjlList.Add(new SysPatientAccountRevenueAndExpenseEntity()
            {
                OrganizeId = this.OrganizeId,
                zh = settpatinfo.zyyjjzh ?? 0,
                patid = settpatinfo.patid ?? 0,
                szje = (-zy_js.xjzf),
                zhye = (zhye -= zy_js.xjzf),
                pzh = string.Empty,
                szxz = ((int)AccSzxz.ZYJS).ToString(),
                //xjzffsbh = xjzffsList.Any(p => p.xjzffs == defaultxjzffs) ? xjzffsList.Where(p => p.xjzffs == defaultxjzffs).Select(p => p.xjzffsbh).FirstOrDefault() : xjzffsList.Where(p => p.xjzffs == Constants.xtzffs.XJZF).Select(p => p.xjzffsbh).FirstOrDefault(),
                xjzffs = "",    //&update170720
                jsnm = zy_js.jsnm,
                zyh = settpatinfo.zyh,
                CreatorCode = this.UserIdentity.UserCode,
                CreateTime = DateTime.Now,
                zt = "1",
            });
            if (zhye != zhaoling)
            {
                throw new FailedCodeException("HOSP_FEE_IS_CHANGED");
            }
            //Thêm một dòng trả lại tiền thừa của收支 bản ghi 
            sqlVO.xt_brzhszjlList.Add(new SysPatientAccountRevenueAndExpenseEntity()
            {
                OrganizeId = this.OrganizeId,
                zh = settpatinfo.zyyjjzh ?? 0,
                patid = settpatinfo.patid ?? 0,
                szje = (-zhaoling),
                zhye = (zhye -= zhaoling),  //0
                pzh = string.Empty,
                szxz = ((int)AccSzxz.ZYJS).ToString(),
                //xjzffsbh = xjzffsList.Any(p => p.xjzffs == defaultxjzffs) ? xjzffsList.Where(p => p.xjzffs == defaultxjzffs).Select(p => p.xjzffsbh).FirstOrDefault() : xjzffsList.Where(p => p.xjzffs == Constants.xtzffs.XJZF).Select(p => p.xjzffsbh).FirstOrDefault(),
                xjzffs = "",    //&update170720
                jsnm = zy_js.jsnm,
                zyh = settpatinfo.zyh,
                CreatorCode = this.UserIdentity.UserCode,
                CreateTime = DateTime.Now,
                zt = "1",
            });
            #endregion

            # region 住院结算支付方式
            var zy_jszffs = new HospSettlementPaymentModelEntity()
            {
                jsnm = zy_js.jsnm,
                OrganizeId = this.OrganizeId,
                xjzffs = Constants.xtzffs.ZYYJZHZF,
                zfje = zy_js.xjzf,
                ssry = this.UserIdentity.UserCode,
                ssrq = DateTime.Now,
                zh = settpatinfo.zyyjjzh,
                CreatorCode = this.UserIdentity.UserCode,
                CreateTime = DateTime.Now,
                zt = "1",
            };
            #endregion

            sqlVO.zy_jszffsList.Add(zy_jszffs);

            sqlVO.zy_js = zy_js;

            sqlVO.zyh = settpatinfo.zyh;
            sqlVO.zybz = ((int)EnumZYBZ.Ycy).ToString();
            sqlVO.cyrq = settpatinfo.cyrq;
            sqlVO.fph = fph;

            return sqlVO;
        }

        #endregion

    }
}
