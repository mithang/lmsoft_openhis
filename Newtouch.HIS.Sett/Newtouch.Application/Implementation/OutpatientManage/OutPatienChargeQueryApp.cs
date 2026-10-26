using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.Tools;
using System;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using System.Collections;
using Newtouch.HIS.Domain.ReportTemplateVO;
using System.Data;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common.Exceptions;
using Microsoft.Office.Interop.Excel;
using Newtouch.Infrastructure.Model;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Quyết toán ngoại trú tra cứu
    /// </summary>
    public class OutPatienChargeQueryApp : AppBase, IOutPatienChargeQueryApp
    {
        private readonly IOutPatientChargeQueryDmnService _outPatienChargeQueryDmnService;
        private readonly ISysChargeCategoryRepo _sysChargeMajorClassRepo;
        private readonly ISysChargeAdditionalCategoryRepo _sysChargeAdditionalCategoryRepo;
        private readonly ISysPatientChargeAdditionalRepo _sysPatientChargeAdditionalRepo;
        private readonly ISysChargeItemMultiLanguageRepo _sysChargeItemMultiLanguageRepo;
        private readonly ISysPatientBasicInfoRepo _sysPatBasicInfoRepository;
        private readonly IOutpatientSettlementRepo _outpatientSettlementRepo;
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        private readonly IOupatientInvoicePrintRepo _oupatientInvoicePrintRepo;
        private readonly ISysPatientNatureRepo _sysPatientNatureRepo;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysChargeItemRepo _sysChargeItemRepo;
        private readonly ISysRegistSpecialDiseaseRepo _sysRegistSpecialDiseaseRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysStaffRepo _sysStaffRepo;

        #region 单击加载收费明细（收费查询页面）

        /// <summary>
        /// LấyĐ ăng ký khám ngoại trú thu phí chi tiết
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public OutPatientRegChargeDetailDto GetRecordsByJsnm(string jsnm)
        {
            var chargeDetailsList = _outPatienChargeQueryDmnService.GetRecordsByJsnm(jsnm);

            var mzsfcxdlPzStr = _sysConfigRepo.GetValueByCode(Infrastructure.Constants.xtmzpz.MZSFCX_DL, OperatorProvider.GetCurrent().OrganizeId);
            var arrDl = mzsfcxdlPzStr.TrimEnd(',').Split(',');

            var sysDlList = _sysChargeMajorClassRepo.GetLazyList(OperatorProvider.GetCurrent().OrganizeId).Where(p => arrDl.Contains(p.dlCode)).ToList();
            var dlhj = sysDlList.Select(p => new OutPatientRegChargeMajorClassGroupBO
            {
                dl = p.dlCode,
                dlmc = p.dlmc,
                jehj = chargeDetailsList.Where(t => t.dlCode == p.dlCode).Sum(t => t.je)
            });

            return new OutPatientRegChargeDetailDto
            {
                ghRecordsDetails = chargeDetailsList,
                dlhj = dlhj.ToList(),
            };
        }

        /// <summary>
        /// Thu phí chi tiếttra cứu
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public OutPatientRegChargeDetailDto ChargeRecordsQuery(string jsnm)
        {
            var chargeDetail = _outPatienChargeQueryDmnService.GetRecordsByJsnm(jsnm).ToList();
            return new OutPatientRegChargeDetailDto
            {
                ghRecordsDetails = chargeDetail,
                dlhj = CalculationDlhj(chargeDetail)
            };
        }

        /// <summary>
        /// Tính loại lớn tổng cộng 
        /// </summary>
        /// <returns></returns>
        private IList<OutPatientRegChargeMajorClassGroupBO> CalculationDlhj(IList<OutPatientRegChargeDetailsVO> chargeDetail)
        {
            var result = new List<OutPatientRegChargeMajorClassGroupBO>();
            var sysDls = _sysChargeMajorClassRepo.GetLazyList(OperatorProvider.GetCurrent().OrganizeId);
            if (sysDls == null || sysDls.Count <= 0 || chargeDetail.Count <= 0)
            {
                return result;
            }
            var sfdls = sysDls.ToJson().ToObject<List<SysChargeCategoryVEntity>>();
            sysDls.Select(p => new OutPatientRegChargeMajorClassGroupBO
            {
                dl = p.mzprintbillcode
            }).Distinct().ToList().ForEach(p =>
            {
                var dlmc = "";
                decimal jehj = 0;
                var dlgroup = sfdls.Where(n => n.mzprintbillcode == p.dl).ToList();
                var sysChargeCategoryVEntity = sfdls.FirstOrDefault(n => n.dlCode == p.dl);
                if (sysChargeCategoryVEntity != null)
                {
                    dlmc = sysChargeCategoryVEntity.dlmc;

                    #region Calculation jehj
                    var tmp = from dl in dlgroup
                              join item in chargeDetail
                                  on dl.dlCode equals item.dlCode
                              select new OutPatientRegChargeDetailsVO
                              {
                                  dlCode = item.dlCode,
                                  dlmc = item.dlmc,
                                  je = item.je
                              };
                    var tmpArr = tmp as OutPatientRegChargeDetailsVO[] ?? tmp.ToArray();
                    if (tmpArr.Any())
                    {
                        jehj = tmpArr.Sum(t => t.je);
                        result.Add(new OutPatientRegChargeMajorClassGroupBO
                        {
                            dl = p.dl,
                            dlmc = dlmc,
                            jehj = jehj
                        });
                    }
                    #endregion
                }
            });
            return result;
        }

        #endregion



        #region 重打/补打页面进来加载门诊结算记录
        /// <summary>
        /// tải thông tin quyết toán mz_js List
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="jsnm"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public IList<OutPatientReprintOrSuppPrintSettleVO> LoadMzjsRecords(Pagination pagination, string jsnm, DateTime? startDate, DateTime? endDate, string kh, string yfph)
        {
            IList<OutPatientReprintOrSuppPrintSettleVO> list = _outPatienChargeQueryDmnService.LoadMzjsRecords(pagination, jsnm, startDate, endDate, kh, yfph);
            return list;
        }
        #endregion

        #region 单击时加载结算明细（重打/补打页面）
        /// <summary>
        /// tải chi tiết quyết toán mz_jsmx List
        /// </summary>
        /// <param name="jsnmList"></param>
        /// <returns></returns>
        public OutPatientReprintOrSuppPrintSettleDetailDto LoadMzjsMXRecords(string jsnmStr)
        {
            OutPatientReprintOrSuppPrintSettleDetailDto settleDetailDto = new OutPatientReprintOrSuppPrintSettleDetailDto();
            //settleDetailDto.pzmzEntity = _sysConfigRepo.GetByCode(Constants.xtmzpz.SFXM_FWF, OperatorProvider.GetCurrent().OrganizeId);
            if (settleDetailDto.pzmzEntity == null)
            {
                throw new Exception("系统未配置编号为：" + Infrastructure.Constants.xtmzpz.SFXM_FWF + "的系统门诊配置");
            }
            jsnmStr = "0," + jsnmStr;
            jsnmStr = jsnmStr.TrimEnd(',');

            settleDetailDto.mz_ghxmList = _outPatienChargeQueryDmnService.GetOutpatientRegItemList(jsnmStr);
            settleDetailDto.mz_xmList = _outPatienChargeQueryDmnService.GetOutpatientItemList(jsnmStr);
            settleDetailDto.mz_cfmxList = _outPatienChargeQueryDmnService.GetOutpatientPrescriptDetailList(jsnmStr);

            settleDetailDto.IsGh = false;

            if (settleDetailDto.mz_ghxmList != null && settleDetailDto.mz_ghxmList.Count > 0)
            {
                settleDetailDto.IsGh = true;
            }

            return settleDetailDto;

        }
        #endregion



        #region 打印前检查打印数据
        /// <summary>
        /// kiểm tra có hay không có có thể in của dữ liệu
        /// </summary>
        /// <param name="jsnmStr"></param>
        public void CheckPintInfo(string jsnm)
        {
            OutpatientSettlementEntity settlementEntity = _outpatientSettlementRepo.SelectMZJS(Convert.ToInt32(jsnm), this.OrganizeId);
            if (settlementEntity == null)
            {
                throw new FailedCodeException("OUTPAT_PLEASE_SELECT_YOUR_BILLING_INFORMATION");
            }
            if (settlementEntity.ghnm == 0)
            {
                throw new FailedCodeException("OUTPAT_NO_BILLING_DETAILS");
            }
            OutpatientRegistEntity regEntity = _outpatientRegistRepo.SelectOutPatientReg(settlementEntity.ghnm, this.OrganizeId);
            if (regEntity == null)
            {
                throw new FailedCodeException("OUTPAT_NO_BILLING_DETAILS");
            }

        }
        #endregion


        #region 打印明细
        /// <summary>
        /// InChi tiết
        /// </summary>
        /// <param name="jsnmStr"></param>
        public void PrintMxData(string jsnmStr)
        {
            DateTime printDate = DateTime.Now;
            SettleDetailVO settleDetail = null;
            List<PrinLeftRowVO> leftDt = null;
            int leftDlCount = 0;
            List<PrinRightRowVO> rightDt = null;
            int rightDlCount = 0;

            jsnmStr = "0," + jsnmStr;
            jsnmStr = jsnmStr.TrimEnd(',');
            getPrintMxHtml(jsnmStr, printDate, out settleDetail, out leftDt, out leftDlCount, out rightDt, out rightDlCount);

            PrintMxBill(settleDetail, leftDt, rightDt);
        }
        #endregion

        #region 打印明细准备工作
        /// <summary>
        /// hợp và List
        /// </summary>
        /// <returns></returns>
        public List<PrintSettleVO> returnMergeList(OutPatientReprintOrSuppPrintSettleDetailDto SettleDetailDto)
        {
            List<PrintSettleVO> settleVOList = new List<PrintSettleVO>();
            //mz_ghxm
            foreach (var item in SettleDetailDto.mz_ghxmList)
            {
                PrintSettleVO obj = new PrintSettleVO();
                obj.cfh = "";
                obj.CreateTime = item.CreateTime;
                obj.danwei = item.djdw;
                obj.dj = item.dj;
                obj.djandfwfdj = item.djandfwfdj;
                obj.dl = item.dl;
                obj.dlmc = item.dlmc;
                obj.fwfdj = item.fwfdj;
                obj.ghnm = item.ghnm;
                obj.je = item.je;
                obj.patid = item.patid;
                obj.sfxm = item.sfxm;
                obj.sfxmmc = item.sfxmmc;
                obj.sl = item.sl;
                obj.xmnm = item.xmnm;
                obj.yp = "";
                obj.ypmc = "";
                obj.zfbl = item.zfbl;
                obj.zfxz = item.zfxz;
                obj.kh = item.kh;

                settleVOList.Add(obj);
            }
            //mz_xm
            foreach (var item in SettleDetailDto.mz_xmList)
            {
                PrintSettleVO obj = new PrintSettleVO();
                obj.cfh = "";
                obj.CreateTime = item.CreateTime;
                obj.danwei = item.djdw;
                obj.dj = item.dj;
                obj.djandfwfdj = item.djandfwfdj;
                obj.dl = item.dl;
                obj.dlmc = item.dlmc;
                obj.fwfdj = item.fwfdj;
                obj.ghnm = item.ghnm;
                obj.je = item.je;
                obj.patid = item.patid;
                obj.sfxm = item.sfxm;
                obj.sfxmmc = item.sfxmmc;
                obj.sl = item.sl;
                obj.xmnm = item.xmnm;
                obj.yp = "";
                obj.ypmc = "";
                obj.zfbl = item.zfbl;
                obj.zfxz = item.zfxz;
                obj.kh = item.kh;

                settleVOList.Add(obj);
            }
            //mz_cfmx
            foreach (var item in SettleDetailDto.mz_cfmxList)
            {
                PrintSettleVO obj = new PrintSettleVO();
                obj.cfh = item.cfh;
                obj.CreateTime = item.CreateTime;
                obj.danwei = item.djdw;
                obj.dj = item.dj;
                obj.djandfwfdj = item.djandfwfdj;
                obj.dl = item.dl;
                obj.dlmc = item.dlmc;
                obj.fwfdj = item.fwfdj;
                obj.ghnm = item.ghnm;
                obj.je = item.je;
                obj.patid = item.patid;
                obj.sfxm = item.sfxm;
                obj.sfxmmc = item.sfxmmc;
                obj.sl = item.sl;
                obj.xmnm = item.xmnm;
                obj.yp = item.yp;
                obj.ypmc = item.ypmc;
                obj.zfbl = item.zfbl;
                obj.zfxz = item.zfxz;
                obj.kh = item.kh;

                settleVOList.Add(obj);
            }
            return settleVOList;

        }

        /// <summary>
        /// sắp xếpchi tiết nội dung 
        /// </summary>
        /// <param name="jsnmStr"></param>
        /// <param name="printDate"></param>
        /// <param name="settleDetail"></param>
        /// <param name="leftDt"></param>
        /// <param name="leftDlCount"></param>
        /// <param name="rightDt"></param>
        /// <param name="rightDlCount"></param>
        /// <param name="lan"></param>
        public void getPrintMxHtml(string jsnmStr, DateTime printDate, out SettleDetailVO settleDetail,
           out List<PrinLeftRowVO> leftDt, out int leftDlCount, out List<PrinRightRowVO> rightDt, out int rightDlCount, Enumlan lan = Enumlan.Default)
        {
            leftDlCount = 0;
            rightDlCount = 0;

            var SettleDetailDto = LoadMzjsMXRecords(jsnmStr);

            List<PrintSettleVO> settleVOList = returnMergeList(SettleDetailDto);

            if (lan != Enumlan.Default)
            {
                List<SysChargeAdditionalCategoryEntity> fjsfdlList = _sysChargeAdditionalCategoryRepo.SelectALLEffectiveList(this.OrganizeId);
                List<SysPatientChargeAdditionalEntity> brsffjList = _sysPatientChargeAdditionalRepo.SelectALLEffectiveList(this.OrganizeId);
                var dyyList = _sysChargeItemMultiLanguageRepo.SelectALLEffectiveList(OperatorProvider.GetCurrent().OrganizeId);

                foreach (var item in settleVOList)
                {
                    //Hạng mục thu phí tên 
                    string sfxmmc = string.IsNullOrEmpty(item.sfxmmc) ? item.ypmc : item.sfxmmc;
                    string sfxm = string.IsNullOrEmpty(item.sfxm) ? item.yp : item.sfxm;
                    var dyy = dyyList.Where(t => t.sfxmCode == sfxm).FirstOrDefault();
                    if (dyy != null)
                    {
                        if (lan == Enumlan.English)
                        {
                            sfxmmc = dyy.sfxmmcEnglish;
                        }
                        else if (lan == Enumlan.Jpanese)
                        {
                            sfxmmc = dyy.sfxmmcJpan;
                        }
                        else if (lan == Enumlan.Fanti)
                        {
                            sfxmmc = dyy.sfxmmcFanti;
                        }
                    }
                    // Hạng mục thu phí loại lớn (lấy đượcLàxt_fjsfdl trong Củadl Vàdlmc)
                    string dl = item.dl;
                    string dlmc = item.dlmc;
                    SysPatientChargeAdditionalEntity brsffj = brsffjList.Find(t => t.sfxm == sfxm);
                    if (brsffj == null)
                    {
                        brsffj = brsffjList.Find(t => t.dl == dl);
                    }
                    if (brsffj != null)
                    {
                        SysChargeAdditionalCategoryEntity fjsfdl = fjsfdlList.Find(t => t.dl == brsffj.fjxsdl);
                        if (fjsfdl != null)
                        {
                            dl = fjsfdl.dl;
                            dlmc = fjsfdl.dlmc;
                        }
                    }

                    item.sfxm = sfxm;
                    item.sfxmmc = sfxmmc;
                    item.dl = dl;
                    item.dlmc = dlmc;
                }
            }

            // không inchi tiết loại lớn 
            var notPrintMXdlpz = _sysConfigRepo.GetByCode(Infrastructure.Constants.xtmzpz.DL_NOPRINTMX, OperatorProvider.GetCurrent().OrganizeId);
            if (notPrintMXdlpz == null)
            {
                throw new FailedCodeException("SYS_DOES_NOT_EXIST_NOT_PRINT_THE_DETAIL_CATEGROY,PLEASE_MAINTAIN_THE_CONFIGURATION_INFO");
            }
            List<string> notPrintMxdlList = new List<string>(notPrintMXdlpz.Value.Split(','));

            // loại lớn chi phí
            Dictionary<string, decimal> dlfyDict = new Dictionary<string, decimal>();
            //thuốc thảo dược loại lớn hợp và in
            Dictionary<string, decimal> cyfyDict = new Dictionary<string, decimal>();

            DateTime minDate = DateTime.Now;
            DateTime maxDate = DateTime.Now;
            int patid = 0;
            string kh = "";
            int totalCount = 0;
            decimal total = 0m;
            foreach (var item in settleVOList)
            {
                string key = item.dl;
                if (!notPrintMxdlList.Contains(key))
                {
                    totalCount++;
                }
                if (!dlfyDict.ContainsKey(key))
                {
                    dlfyDict.Add(key, 0);
                }
                dlfyDict[key] = dlfyDict[key] + Convert.ToDecimal(item.je.ToString());
                total += Convert.ToDecimal(item.je.ToString());
                if (patid != 0 && patid != item.patid)
                {
                    throw new FailedCodeException("OUTPAT_THE_DETAILS_OF_THE_DIFFERENT_PATIENTS_CAN_NOT_BE_MIXED_PRINT");
                }
                patid = item.patid;
                DateTime CreateTime = item.CreateTime;
                if (minDate > CreateTime)
                {
                    minDate = CreateTime;
                }
                if (maxDate < CreateTime)
                {
                    maxDate = CreateTime;
                }
                if (item.kh.Length == 28)
                {
                    kh = kh.Substring(0, 10);
                }
                else
                {
                    kh = item.kh;
                }

            }

            #region 获取模板参数数据
            SysPatientBasicInfoEntity brjbxx = _sysPatBasicInfoRepository.GetInfoByPatid(patid.ToString(), this.OrganizeId);
            if (brjbxx == null)
            {
                throw new FailedCodeException("OUTPAT_NO_PATIENT_INFORMATION");
            }
            TimeSpan ts = maxDate - minDate;
            settleDetail = new SettleDetailVO();
            settleDetail.Patient_Name = brjbxx.xm;
            settleDetail.DateOfBirth = brjbxx.csny.ToDateString();
            if (brjbxx.xb == ((int)EnumSex.male).ToString())
            {
                settleDetail.Sex = EnumSex.male.ToString();
            }
            else if (brjbxx.xb == ((int)EnumSex.female).ToString())
            {
                settleDetail.Sex = EnumSex.female.ToString();
            }
            else
            {
                settleDetail.Sex = EnumSex.other.ToString();
            }

            settleDetail.MedicalRecordNo = kh;
            settleDetail.jsKsrq = minDate;
            settleDetail.jsJsrq = maxDate;
            settleDetail.Year = printDate.Year.ToString();
            settleDetail.Month = printDate.Month.ToString();
            settleDetail.Date = printDate.Day.ToString();
            settleDetail.SummOfCharges = total.ToString("0.00");

            ArrayList arr = new ArrayList(jsnmStr.Split(','));

            foreach (string jsnm in arr)
            {
                settleDetail.No = settleDetail.No + "-" + jsnm;
            }
            settleDetail.No = settleDetail.No.TrimEnd('-').TrimStart('-');

            #endregion

            string xdlmc = string.Empty;
            int j = 0;
            leftDt = new List<PrinLeftRowVO>();
            rightDt = new List<PrinRightRowVO>();
            foreach (KeyValuePair<string, decimal> dlPair in dlfyDict)
            {
                List<PrintSettleVO> objList = settleVOList.Where(p => p.dl == dlPair.Key).ToList();
                #region 添加项目明细
                for (int i = 0; i < objList.Count; i++)
                {
                    xdlmc = objList[i].dlmc.ToString();
                    if (!notPrintMxdlList.Contains(objList[i].dl.ToString()))
                    {
                        //Nếu làsố chẵnthì trái bên,số lẻthì phải bên
                        if (j % 2 == 0)
                        {
                            PrinLeftRowVO leftNewRow = new PrinLeftRowVO();
                            leftNewRow.sfxm = objList[i].sfxm;
                            leftNewRow.sfxmmc = objList[i].sfxmmc;
                            leftNewRow.dl = objList[i].dl;
                            leftNewRow.dlmc = objList[i].dlmc;
                            leftNewRow.sl = objList[i].sl;
                            leftNewRow.djAndFwfdj = objList[i].djandfwfdj;
                            leftNewRow.je = objList[i].je;
                            leftDt.Add(leftNewRow);
                        }
                        else
                        {
                            PrinRightRowVO rightNewRow = new PrinRightRowVO();
                            rightNewRow.sfxm2 = objList[i].sfxm;
                            rightNewRow.sfxmmc2 = objList[i].sfxmmc;
                            rightNewRow.dl2 = objList[i].dl.ToString();
                            rightNewRow.dlmc2 = objList[i].dlmc;
                            rightNewRow.sl2 = objList[i].sl;
                            rightNewRow.djAndFwfdj2 = objList[i].djandfwfdj;
                            rightNewRow.je2 = objList[i].je;
                            rightDt.Add(rightNewRow);
                        }
                    }
                }
                #endregion

                if (objList.Count > 0)
                {
                    if (j % 2 == 0)
                    {
                        leftDlCount++;
                    }
                    else
                    {
                        rightDlCount++;
                    }

                    if (notPrintMxdlList.Contains(dlPair.Key))
                    {
                        //Thêm loại lớn tổng cộng 
                        if (j % 2 == 0)
                        {
                            PrinLeftRowVO leftNewRow = new PrinLeftRowVO();
                            leftNewRow.sfxm = string.Empty;
                            leftNewRow.sfxmmc = xdlmc;
                            leftNewRow.dl = dlPair.Key;
                            leftNewRow.dlmc = xdlmc;
                            leftNewRow.sl = 0;
                            leftNewRow.djAndFwfdj = 0;
                            leftNewRow.je = dlPair.Value;
                            leftDt.Add(leftNewRow);
                        }
                        else
                        {
                            PrinRightRowVO rightNewRow = new PrinRightRowVO();
                            rightNewRow.sfxm2 = string.Empty;
                            rightNewRow.sfxmmc2 = xdlmc;
                            rightNewRow.dl2 = dlPair.Key;
                            rightNewRow.dlmc2 = xdlmc;
                            rightNewRow.sl2 = 0;
                            rightNewRow.djAndFwfdj2 = 0;
                            rightNewRow.je2 = dlPair.Value;
                            rightDt.Add(rightNewRow);
                        }
                    }
                    j++;
                }
            }

        }

        /// <summary>
        /// InChi tiết quyết toán
        /// </summary>
        public void PrintMxBill(SettleDetailVO detail, List<PrinLeftRowVO> leftDt, List<PrinRightRowVO> rightDt)
        {
            var obj = detail.MapperTo<SettleDetailVO, PrintSettleDetailVO>();
            if (!string.IsNullOrWhiteSpace(detail.Patient_Name))
            {
                obj.Patient_Name = detail.Patient_Name;
            }
            if (!string.IsNullOrWhiteSpace(detail.DateOfBirth))
            {
                obj.DateOfBirth = detail.DateOfBirth;
            }
            if (!string.IsNullOrWhiteSpace(detail.No))
            {
                obj.No = detail.No;
            }
            if (!string.IsNullOrWhiteSpace(detail.Sex))
            {
                obj.Sex = detail.Sex;
            }
            if (!string.IsNullOrWhiteSpace(detail.MedicalRecordNo))
            {
                obj.MedicalRecordNo = detail.MedicalRecordNo;
            }
            if (!string.IsNullOrWhiteSpace(detail.ZLDate))
            {
                obj.ZLDate = detail.ZLDate;
            }
            if (!string.IsNullOrWhiteSpace(detail.Year))
            {
                obj.Year = detail.Year;
            }
            if (!string.IsNullOrWhiteSpace(detail.Month))
            {
                obj.Month = detail.Month;
            }
            if (!string.IsNullOrWhiteSpace(detail.Date))
            {
                obj.Date = detail.Date;
            }
            if (!string.IsNullOrWhiteSpace(detail.SummOfCharges))
            {
                obj.SummOfCharges = detail.SummOfCharges;
            }

            string fpPath = string.Format(@"{0}\17楼明细清单模板.grf", Infrastructure.Constants.ReportTemplateDirUrl);

            var subReportDict = new Dictionary<string, System.Data.DataTable>();
            if (leftDt != null)
            {
                subReportDict.Add("leftSubReport", leftDt.ToDataTable());
            }
            if (rightDt != null)
            {
                subReportDict.Add("rightSubReport", rightDt.ToDataTable());
            }

            //GridppReportHelper.Print<PrintSettleDetailVO>(obj, fpPath, null, subReportDict);
        }
        #endregion



        #region 发票打印
        /// <summary>
        /// In hóa đ ơn
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="Pagefph">biểu mẫu truyền vàocủa số hóa đ ơn</param>
        public void PrintInvoice(string jsnm, bool isGh)
        {
            //Quyết toán ngoại trú
            OutpatientSettlementEntity settlementEntity = _outpatientSettlementRepo.SelectMZJS(Convert.ToInt32(jsnm), this.OrganizeId);
            //Thêm bản ghi in
            SelectInvoicePrintData(settlementEntity, "", Enumdyfs.Print);
            //In hóa đ ơn
            ExecPrintInvoice(Convert.ToInt32(jsnm), settlementEntity.ghnm, isGh, Enumdyfs.Print);

        }

        /// <summary>
        /// In hóa đ ơn tổng số tiền=0,thì không in hóa đ ơn
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="ghnm"></param>
        /// <param name="dyfs"></param>
        public void ExecPrintInvoice(int jsnm, int ghnm, bool isGh, Enumdyfs dyfs)
        {
            //sắp xếpin nội dung 
            PrintInvoiceInfoVO info = GetPrintInvoiceInfo(jsnm, ghnm, isGh, dyfs);
            string fpPath = "";

            // báo cáo con 
            List<ItemSubReportVO> itemSubReportList = new List<ItemSubReportVO>();
            foreach (var item in info.ItemPrice)
            {
                ItemSubReportVO itemSub = new ItemSubReportVO();
                itemSub.item = item.Key;
                itemSub.account = item.Value;

                itemSubReportList.Add(itemSub);
            }
            // báo cáo con 
            List<DetailSubReportVO> detailSubReportList = new List<DetailSubReportVO>();
            foreach (var item in info.ItemDetails)
            {

                DetailSubReportVO detailItem = new DetailSubReportVO();
                detailItem.Code = item["项目编码"];
                detailItem.Name = item["名称"];
                detailItem.Spec = item["规格"];
                detailItem.Num = item["数量"];
                detailItem.Price = item["单价"];
                detailItem.Account = item["金额"];

                detailSubReportList.Add(detailItem);
            }
            var subReportDict = new Dictionary<string, System.Data.DataTable>();
            if (itemSubReportList != null)
            {
                subReportDict.Add("ItemSubReport", itemSubReportList.ToDataTable());
            }
            if (detailSubReportList != null)
            {
                subReportDict.Add("DetailSubReport", detailSubReportList.ToDataTable());
            }


            //Khởi tạo báo cáo nội dung 
            if (info.IsGh)
            {
                PrintInvoiceRegisterVO obj = ReportRegisterInitialize(info);
                fpPath = string.Format(@"{0}\医疗发票套打(挂号).grf", Infrastructure.Constants.ReportTemplateDirUrl);
                if (info.IsBd)
                {
                    fpPath = string.Format(@"{0}\医疗发票套打(挂号补打).grf", Infrastructure.Constants.ReportTemplateDirUrl);
                }

                //GridppReportHelper.Print(obj, fpPath, null, subReportDict, "controlname");

            }
            else
            {
                PrintInvoiceVO obj = ReportInitialize(info);
                fpPath = string.Format(@"{0}\医疗发票套打.grf", Infrastructure.Constants.ReportTemplateDirUrl);
                if (info.IsBd)
                {
                    fpPath = string.Format(@"{0}\医疗发票套打(补打).grf", Infrastructure.Constants.ReportTemplateDirUrl);
                }
                if (false == string.IsNullOrEmpty(info.FpdymbPath))
                    fpPath = info.FpdymbPath;

                //GridppReportHelper.Print(obj, fpPath, null, subReportDict, "controlname");
            }


        }

        
        #endregion

        #region 补打
        /// <summary>
        /// In bù
        /// </summary>
        /// <param name="jsnm"></param>
        public void SupplementPrint(string jsnm, bool isGh)
        {
            //Quyết toán ngoại trú
            OutpatientSettlementEntity settlementEntity = _outpatientSettlementRepo.SelectMZJS(Convert.ToInt32(jsnm), this.OrganizeId);
            //Thêm bản ghi in
            SelectInvoicePrintData(settlementEntity, "", Enumdyfs.BD);
            PrintInvoiceInfoVO info = GetPrintInvoiceInfo(Convert.ToInt32(jsnm), settlementEntity.ghnm, isGh, Enumdyfs.BD);
            string fpPath = "";

            // báo cáo con 
            List<ItemSubReportVO> itemSubReportList = new List<ItemSubReportVO>();
            foreach (var item in info.ItemPrice)
            {
                ItemSubReportVO itemSub = new ItemSubReportVO();
                itemSub.item = item.Key;
                itemSub.account = item.Value;

                itemSubReportList.Add(itemSub);
            }
            // báo cáo con 
            List<DetailSubReportVO> detailSubReportList = new List<DetailSubReportVO>();
            foreach (var item in info.ItemDetails)
            {

                DetailSubReportVO detailItem = new DetailSubReportVO();
                detailItem.Code = item["项目编码"];
                detailItem.Name = item["名称"];
                detailItem.Spec = item["规格"];
                detailItem.Num = item["数量"];
                detailItem.Price = item["单价"];
                detailItem.Account = item["金额"];

                detailSubReportList.Add(detailItem);
            }
            var subReportDict = new Dictionary<string, System.Data.DataTable>();
            if (itemSubReportList != null)
            {
                subReportDict.Add("ItemSubReport", itemSubReportList.ToDataTable());
            }
            if (detailSubReportList != null)
            {
                subReportDict.Add("DetailSubReport", detailSubReportList.ToDataTable());
            }

            //Khởi tạo báo cáo nội dung 
            if (info.IsGh)
            {
                PrintInvoiceRegisterVO obj = ReportRegisterInitialize(info);
                fpPath = string.Format(@"{0}\医疗发票套打(挂号).grf", Infrastructure.Constants.ReportTemplateDirUrl);
                if (info.IsBd)
                {
                    fpPath = string.Format(@"{0}\医疗发票套打(挂号补打).grf", Infrastructure.Constants.ReportTemplateDirUrl);
                }

                //GridppReportHelper.Print(obj, fpPath, null, subReportDict, "controlname");

            }
            else
            {
                PrintInvoiceVO obj = ReportInitialize(info);
                fpPath = string.Format(@"{0}\医疗发票套打.grf", Infrastructure.Constants.ReportTemplateDirUrl);
                if (info.IsBd)
                {
                    fpPath = string.Format(@"{0}\医疗发票套打(补打).grf", Infrastructure.Constants.ReportTemplateDirUrl);
                }
                if (false == string.IsNullOrEmpty(info.FpdymbPath))
                    fpPath = info.FpdymbPath;

                //GridppReportHelper.Print(obj, fpPath, null, subReportDict, "controlname");
            }

        }
        #endregion

        #region 重打
        /// <summary>
        /// In lại
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="pageFph"></param>
        public void RePrint(string jsnm, string pageFph, bool isGh)
        {
            //Quyết toán ngoại trú
            OutpatientSettlementEntity settlementEntity = _outpatientSettlementRepo.SelectMZJS(Convert.ToInt32(jsnm), this.OrganizeId);
            //Thêm bản ghi in
            SelectInvoicePrintData(settlementEntity, pageFph, Enumdyfs.CD);
            //In lại giờ thay đ ổi mz_js Vàcw_fp Củafph
            _outPatienChargeQueryDmnService.UpdateInvoiceNo(settlementEntity, pageFph);

            PrintInvoiceInfoVO info = GetPrintInvoiceInfo(Convert.ToInt32(jsnm), settlementEntity.ghnm, isGh, Enumdyfs.CD);

            string fpPath = "";

            // báo cáo con 
            List<ItemSubReportVO> itemSubReportList = new List<ItemSubReportVO>();
            foreach (var item in info.ItemPrice)
            {
                ItemSubReportVO itemSub = new ItemSubReportVO();
                itemSub.item = item.Key;
                itemSub.account = item.Value;

                itemSubReportList.Add(itemSub);
            }
            // báo cáo con 
            List<DetailSubReportVO> detailSubReportList = new List<DetailSubReportVO>();
            foreach (var item in info.ItemDetails)
            {

                DetailSubReportVO detailItem = new DetailSubReportVO();
                detailItem.Code = item["项目编码"];
                detailItem.Name = item["名称"];
                detailItem.Spec = item["规格"];
                detailItem.Num = item["数量"];
                detailItem.Price = item["单价"];
                detailItem.Account = item["金额"];

                detailSubReportList.Add(detailItem);
            }
            var subReportDict = new Dictionary<string, System.Data.DataTable>();
            if (itemSubReportList != null)
            {
                subReportDict.Add("ItemSubReport", itemSubReportList.ToDataTable());
            }
            if (detailSubReportList != null)
            {
                subReportDict.Add("DetailSubReport", detailSubReportList.ToDataTable());
            }

            //Khởi tạo báo cáo nội dung 
            if (info.IsGh)
            {
                PrintInvoiceRegisterVO obj = ReportRegisterInitialize(info);
                fpPath = string.Format(@"{0}\医疗发票套打(挂号).grf", Infrastructure.Constants.ReportTemplateDirUrl);
                if (info.IsBd)
                {
                    fpPath = string.Format(@"{0}\医疗发票套打(挂号补打).grf", Infrastructure.Constants.ReportTemplateDirUrl);
                }

                //GridppReportHelper.Print(obj, fpPath, null, subReportDict, "controlname");

            }
            else
            {
                PrintInvoiceVO obj = ReportInitialize(info);
                fpPath = string.Format(@"{0}\医疗发票套打.grf", Infrastructure.Constants.ReportTemplateDirUrl);
                if (info.IsBd)
                {
                    fpPath = string.Format(@"{0}\医疗发票套打(补打).grf", Infrastructure.Constants.ReportTemplateDirUrl);
                }
                if (false == string.IsNullOrEmpty(info.FpdymbPath))
                    fpPath = info.FpdymbPath;

                //GridppReportHelper.Print(obj, fpPath, null, subReportDict, "controlname");
            }

        }
        #endregion

        #region 打印准备工作（打印/补打/重打共用）
        /// <summary>
        /// Thêm bản ghi in
        /// </summary>
        /// <param name="js"></param>
        /// <param name="Pagefph">biểu mẫu truyền vàocủa số hóa đ ơn</param>
        /// <param name="dyfs"></param>
        /// <returns></returns>
        public void SelectInvoicePrintData(OutpatientSettlementEntity js, string Pagefph, Enumdyfs dyfs)
        {
            string fph = js.fph;
            // mới số hóa đ ơn
            string xfph = Pagefph;
            if (dyfs == Enumdyfs.Print)
            {
                xfph = fph;
            }
            //Lấygốc hóa đ ơn của mới nhất số hóa đ ơn
            OupatientInvoicePrintEntity lastInvoicePrintEntity = _oupatientInvoicePrintRepo.SelectOutPatientInvoicePrintByJsnm(js.jsnm, this.OrganizeId);
            if (lastInvoicePrintEntity != null)
            {
                fph = lastInvoicePrintEntity.xfph;
            }
            if (dyfs == Enumdyfs.BD)  //In bù mới số hóa đ ơn là cũ số hóa đ ơn
            {
                xfph = fph;
            }
            //Thêm bản ghi in
            _outPatienChargeQueryDmnService.SaveInvoicePrintRecords(js, fph, xfph, dyfs);

        }

        /// <summary>
        /// Lấyin hóa đ ơn nội dung 
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="ghnm"></param>
        /// <param name="isGh"></param>
        /// <param name="dyfs"></param>
        /// <param name="zhye"></param>
        /// <param name="isJz"></param>
        /// <returns></returns>
        public PrintInvoiceInfoVO GetPrintInvoiceInfo(int jsnm, int ghnm, bool isGh, Enumdyfs dyfs = Enumdyfs.Print, decimal zhye = 0m, bool isJz = false)
        {
            #region 初始化及数据验证
            bool isPrintXmmx = true;
            PrintInvoiceInfoVO info = new PrintInvoiceInfoVO();
            if (dyfs == Enumdyfs.BD)
            {
                info.IsBd = true;
            }
            //Quyết toán ngoại trú
            OutpatientSettlementEntity settleEntity = _outpatientSettlementRepo.SelectMZJS(jsnm, this.OrganizeId);
            if (settleEntity == null)
            {
                throw new FailedCodeException("OUTPAT_THE_SETTLEMENT_INFORMATION_DOES_NOT_EXIST");
            }
            //LấyTính chất bệnh nhân
            SysPatientNatureEntity patientNatureEntity = null;
            //SysPatientNatureEntity patientNatureEntity = _sysPatientNatureRepo.SelectBrxzByBrxzbh(settleEntity.brxzbh);
            //&update170720
            if (patientNatureEntity == null)
            {
                throw new FailedCodeException("OUTPAT_PATIENT_NATURE_IS_NOT_EXIST");
            }
            //Lấy thông tin bệnh nhân theo mã nội bộ bệnh nhân
            SysPatientBasicInfoEntity patientBasicInfoEntity = _sysPatBasicInfoRepository.GetInfoByPatid(settleEntity.patid.ToString(), this.OrganizeId);
            if (patientBasicInfoEntity == null)
            {
                throw new FailedCodeException("OUTPAT_NO_PATIENT_INFORMATION");
            }

            //Lấy thu tiền Đ ơn vị

            // không inchi tiết loại lớn 
            var notPrintMXdlpz = _sysConfigRepo.GetByCode(Infrastructure.Constants.xtmzpz.DL_NOPRINTMX, OperatorProvider.GetCurrent().OrganizeId);
            if (notPrintMXdlpz == null)
            {
                throw new FailedCodeException("SYS_DOES_NOT_EXIST_NOT_PRINT_THE_DETAIL_CATEGROY,PLEASE_MAINTAIN_THE_CONFIGURATION_INFO");
            }
            List<string> notPrintMxdlList = new List<string>(notPrintMXdlpz.Value.Split(','));

            //// giư ờng gia đ ình bệnh nhân không inchi tiết

            //Tổng số tiền
            decimal totalAmount = 0m;
            OutpatientRegistEntity registEntity = null;
            SysDepartmentVEntity departmentEntity = null;
            //Hạng mục Số tiền
            List<KeyValuePair<string, string>> ItemPrice = new List<KeyValuePair<string, string>>();
            Dictionary<string, decimal> dlfyDict = new Dictionary<string, decimal>();
            //thuốc thảo dược loại lớn hợp và in
            Dictionary<string, decimal> cyfyDict = new Dictionary<string, decimal>();
            //Hạng mục mã tên quy cách số lư ợng Đ ơn giá số tiền(tệ)
            List<Dictionary<string, string>> ItemDetails = new List<Dictionary<string, string>>();
            Dictionary<string, string> itemDetail = new Dictionary<string, string>();
            //Đ ăng ký khámchi tiết:Tên bệnh nhân:Name bệnh nhân mã nội bộ:ID thông thư ờng hoặc chuyên gia:YS khoa:KS khoa số thứ tự:KsNo ngày:dateTime
            Dictionary<string, string> GhItemDetails = new Dictionary<string, string>();
            //cửa sổthông tin
            List<string> Xwindow = new List<string>();
            // nên thuxxxthu trướcyyy trả lại tiền thừazzz
            string account = string.Empty;
            // phư ơng thức thanh toán như:Tiền mặt:xx tệ,pos:yy tệ
            string PayFuc = string.Empty;
            //Chênh lệch tiền lẻ
            string XjWc = string.Empty;
            #endregion

            #region 显示发票项目
            if (!isGh)
            {
                //// giư ờng gia đ ình không inchi tiết

                //Đ ơn thuốc ngoại trú
                List<OutpatientPrescriptionDetailVO> mz_cfmxList = _outPatienChargeQueryDmnService.GetOutpatientPrescriptDetailList(jsnm);
                #region 获取处方明细
                //Lấy loại lớn chi phí
                foreach (var item in mz_cfmxList)
                {
                    string dl = item.dlmc;
                    if (!dlfyDict.ContainsKey(dl))
                    {
                        dlfyDict.Add(dl, 0m);
                    }
                    dlfyDict[dl] += item.je;
                    totalAmount += item.je;
                    //thuốc thảo dược không inchi tiết
                    if (!notPrintMxdlList.Contains(item.dl))
                    {
                        //Hạng mục mã tên quy cách số lư ợng Đ ơn giá số tiền(tệ)
                        itemDetail = new Dictionary<string, string>();
                        itemDetail.Add("项目编码", string.Empty);
                        itemDetail.Add("名称", item.ypmc);
                        itemDetail.Add("规格", item.ypgg);
                        itemDetail.Add("数量", item.sl.ToString());
                        itemDetail.Add("单价", (item.je + item.fwfdj).ToString());
                        itemDetail.Add("金额", item.je.ToString());
                        ItemDetails.Add(itemDetail);
                    }
                    else
                    {
                        string cydl = item.dlmc;
                        if (!cyfyDict.ContainsKey(dl))
                            cyfyDict.Add(dl, 0m);

                        cyfyDict[dl] += item.je;
                    }


                }
                foreach (KeyValuePair<string, decimal> pair in cyfyDict)
                {
                    itemDetail = new Dictionary<string, string>();
                    itemDetail.Add("项目编码", string.Empty);
                    itemDetail.Add("名称", pair.Key);
                    itemDetail.Add("规格", "");
                    itemDetail.Add("数量", string.Empty);
                    itemDetail.Add("单价", string.Empty);
                    itemDetail.Add("金额", pair.Value.ToString("0.00"));
                    ItemDetails.Add(itemDetail);
                }
                #endregion

                #region 获取门诊项目
                //Hạng mục ngoại trú
                List<OutpatienItemVO> mz_xmList = _outPatienChargeQueryDmnService.GetOutpatientItemList(jsnm);
                //Lấy loại lớn chi phí
                foreach (var item in mz_xmList)
                {
                    string dl = item.dlmc;
                    if (!dlfyDict.ContainsKey(dl))
                        dlfyDict.Add(dl, 0m);

                    dlfyDict[dl] += item.je;
                    totalAmount += item.je;

                    //Hạng mục mã tên quy cách số lư ợng Đ ơn giá số tiền(tệ)
                    itemDetail = new Dictionary<string, string>();
                    itemDetail.Add("项目编码", item.wjdm);
                    itemDetail.Add("名称", item.sfxmmc);
                    itemDetail.Add("规格", "");
                    itemDetail.Add("数量", item.sl.ToString());
                    itemDetail.Add("单价", (item.dj + item.fwfdj).ToString());
                    itemDetail.Add("金额", item.je.ToString());
                    ItemDetails.Add(itemDetail);

                }
                #endregion
                foreach (KeyValuePair<string, decimal> pair in dlfyDict)
                {
                    KeyValuePair<string, string> item = new KeyValuePair<string, string>(pair.Key + "：", pair.Value.ToString("0.00"));
                    ItemPrice.Add(item);
                }
                //Lấycửa sổthông tin
                foreach (var item in mz_cfmxList)
                {
                    if (Xwindow.Contains(item.lyck))
                        continue;

                    Xwindow.Add(item.lyck);
                }
            }

            //Lấykhi trư ớc quyết toán của tất cả các hạng mục thông tin
            if (isGh)
            {
                //LấyĐ ăng ký khám nội dung 
                registEntity = _outpatientRegistRepo.SelectOutPatientReg(ghnm, this.OrganizeId);
                if (registEntity == null)
                {
                    throw new FailedCodeException("OUTPAT_THE_REGISTERED_MESSAGE_DOES_NOT__EXIST");
                }
                // theo Đ ăng ký khámkhoa lấykhoa tên
                departmentEntity = _sysDepartmentRepo.GetEntityByCode(registEntity.ks, OperatorProvider.GetCurrent().OrganizeId);
                if (departmentEntity == null)
                    throw new FailedCodeException("SYS_THERE_IS_NO_DEPARTMENT_IN_THE_SYSTEM_PLEASE_MAINTAIN_THE_DEPARTMENT_INFORMATION");
                // theo Đ ăng ký khám bác sĩ lấy bác sĩ tên
                string ysName = _sysStaffRepo.GetNameByGh(registEntity.ys, OperatorProvider.GetCurrent().OrganizeId);
                #region 获取门诊挂号项目
                //Đ ăng ký khám ngoại trú hạng mục
                List<OutpatientRegistItemVO> registItemList = _outPatienChargeQueryDmnService.GetOutpatientRegItemList(jsnm);
                //Lấy loại lớn chi phí
                foreach (var item in registItemList)
                {
                    if (item.dl == "34")
                    {
                        item.dlmc = "诊查费(自费)";
                    }
                    string dl = item.dlmc;
                    if (!dlfyDict.ContainsKey(dl))
                        dlfyDict.Add(dl, 0m);

                    dlfyDict[dl] += item.je;
                    totalAmount += item.je;
                }
                foreach (KeyValuePair<string, decimal> pair in dlfyDict)
                {
                    KeyValuePair<string, string> item = new KeyValuePair<string, string>(pair.Key + "：", pair.Value.ToString("0.00"));
                    ItemPrice.Add(item);
                }
                #endregion

                //Hệ thống hạng mục thu phí (Đ ăng ký khám tên hạng mục)
                var chargeItemEntity = _sysChargeItemRepo.SelectSysChargeItemBysfxm(registEntity.ghlx, OperatorProvider.GetCurrent().OrganizeId);
                if (chargeItemEntity == null)
                    throw new FailedCodeException("OUTPAT_THE_REGISTERED_ITEM_DATA_DOES_NOT_EXIST");

                // phán đoán có hay không chuyên giaĐ ăng ký khám
                bool isZjgh = false;
                bool isContains = false;
                var pzmz = _sysConfigRepo.GetByCode(Infrastructure.Constants.xtmzpz.GHLX_ZJGH, OperatorProvider.GetCurrent().OrganizeId);
                if (pzmz == null)
                {
                    throw new FailedCodeException("OUTPAT_CONFIGURATIONS_THERE_IS_NO_GHLX_ZJGH_CONFIGURATION_IN_THE_OUTPATIENT_SETTING");
                }
                string pzStr = pzmz.Value;
                if (!string.IsNullOrEmpty(pzStr))
                {
                    List<string> ghlxValues = new List<string>(pzStr.Split(','));
                    string result = ghlxValues.Find(t => t.ToString() == registEntity.ghlx);
                    if (!string.IsNullOrEmpty(result))
                    {
                        isContains = true;
                    }
                }
                if (isContains)
                {
                    isZjgh = true;
                }
                string ysShow = "普通";
                if (!string.IsNullOrEmpty(registEntity.ghzb))
                {
                    SysRegistSpecialDiseaseEntity registSpecialDiseaseEntity = _sysRegistSpecialDiseaseRepo.SelectSysChargeItemByghzb(registEntity.ghzb, this.OrganizeId);
                    if (registSpecialDiseaseEntity == null)
                    {
                        throw new FailedCodeException("OUTPAT_THERE_IS_NO_SPECIAL_DISEASE_DATA");
                        ysShow = registSpecialDiseaseEntity.ghzbmc;
                    }
                }
                else if (isZjgh || chargeItemEntity.sfxmCode == ((int)Enumghlx.TeXu).ToString())
                {
                    //ysShow = "Chuyên gia";
                    ysShow = chargeItemEntity.sfxmmc;
                }
                else if (isZjgh || chargeItemEntity.sfxmCode == ((int)Enumghlx.TeXuV).ToString())
                {
                    //ysShow = "Chuyên gia";
                    ysShow = chargeItemEntity.sfxmmc;
                }
                else
                {
                    //Tuần ngày đ ặc biệt và mục nghỉngày đ ặc biệt (2015-07-01)
                    ysShow = chargeItemEntity.sfxmmc;
                }
                if (registEntity.mjzbz == ((int)EnumOutPatientType.emerDiagnosis).ToString())
                {
                    ysShow += "(急诊)";
                }

                GhItemDetails.Add("Name", patientBasicInfoEntity.xm);
                GhItemDetails.Add("ID", patientBasicInfoEntity.patid.ToString());
                GhItemDetails.Add("YS", ysShow);
                GhItemDetails.Add("KS", departmentEntity.Name);
                GhItemDetails.Add("KsNo", registEntity.jzxh.ToString());
                GhItemDetails.Add("dateTime", registEntity.CreateTime.ToString("yyyy-MM-dd"));
                GhItemDetails.Add("YsName", ysName);
                info.Ks = departmentEntity.Name;
                // thực thu xxx nên thuyyy trả lại tiền thừazzz
                account = string.Format("实收{0}应收{1}找零{2}", settleEntity.xjzf.ToString("0.00"), settleEntity.ysk, settleEntity.zl);
                //Tiền mặt:xxTệ,pos:yyTệ
                List<OutpatientSettlementPaymentModelVO> SettlementPaymentModelList = _outPatienChargeQueryDmnService.GetSettlementPaymentModel(settleEntity.jsnm);
                foreach (var item in SettlementPaymentModelList)
                {
                    PayFuc += item.xjzffsmc + ":" + item.zfje + ",";
                }
                PayFuc = PayFuc.TrimEnd(',');
                //Chênh lệch tiền lẻ
                XjWc = settleEntity.xjwc.ToString("0.00");

            }
            else
            {
                if (isPrintXmmx)
                {
                    //LấyĐ ăng ký khám nội dung 
                    registEntity = _outpatientRegistRepo.SelectOutPatientReg(ghnm, this.OrganizeId);
                    if (registEntity == null)
                    {
                        throw new FailedCodeException("OUTPAT_THE_REGISTERED_MESSAGE_DOES_NOT__EXIST");
                    }
                    // theo Đ ăng ký khámkhoa lấykhoa tên
                    departmentEntity = _sysDepartmentRepo.GetEntityByCode(registEntity.ks, OperatorProvider.GetCurrent().OrganizeId);
                    if (departmentEntity == null)
                    {
                        throw new FailedCodeException("SYS_THERE_IS_NO_DEPARTMENT_IN_THE_SYSTEM_PLEASE_MAINTAIN_THE_DEPARTMENT_INFORMATION");
                    }
                    info.Ks = departmentEntity.Name;
                }
                // thực thu xxx nên thuyyy trả lại tiền thừazzz
                account = string.Format("实收{0}应收{1}找零{2}", settleEntity.xjzf.ToString("0.00"), settleEntity.ysk, settleEntity.zl);
                //Tiền mặt:xxTệ,pos:yyTệ
                List<OutpatientSettlementPaymentModelVO> SettlementPaymentModelList = _outPatienChargeQueryDmnService.GetSettlementPaymentModel(settleEntity.jsnm);
                foreach (var item in SettlementPaymentModelList)
                {
                    PayFuc += item.xjzffsmc + ":" + item.zfje + ",";
                }
                PayFuc = PayFuc.TrimEnd(',');
                //Chênh lệch tiền lẻ
                XjWc = settleEntity.xjwc.ToString("0.00");

            }

            #endregion

            #region 交易金额

            info.PayForSelf = "";
            info.PayForClass = "";
            info.PrivateExpense = settleEntity.zlfy.ToString("0.00");
            info.Cash = settleEntity.xjzf.ToString("0.00");
            info.Pay = "";
            info.TotalStr = settleEntity.zje;
            info.ExtraPay = "";//phụ thêm thanh toán 
            info.CurrentRemain = "";//KhiNămSố dư tài khoản
            info.PastRemain = "";// các năm trư ớc số dư tài khoản
            info.SocialPay = "";//BHYT quỹ chung thanh toán 

            info.No = settleEntity.jsnm.ToString();

            #endregion

            info.Type = patientNatureEntity.brxzmc;
            info.DepartmentType = "占位符";    //Nên là bệnh viện của thuộc tính: bệnh viện V.v. cấp 
            info.Name = patientBasicInfoEntity.xm;
            info.Sex = patientBasicInfoEntity.xb == null ? ""
                : ((EnumSex)(patientBasicInfoEntity.xb.ToInt(-1))).GetDescription("");
            if (registEntity.kh.Length == 28)
            {
                info.Id = registEntity.kh.Substring(0, 10);
            }
            else
            {
                info.Id = registEntity.kh;
            }
            if (settleEntity.jmje > 0)
                info.Memo = "其它费用：" + settleEntity.jmje.ToString("0.00");//Hiển thị số tiền giảm trừ
            info.CompanyName = "占位符";   //Nên là bệnh viện của thuộc tính: hóa đ ơn tiêu đ ề 
            info.Cashier = _sysStaffRepo.GetNameByGh(settleEntity.CreatorCode, OperatorProvider.GetCurrent().OrganizeId);   //Nên là cócái' tạo ngườimã nhân viên' trư ờng 
            info.Date = settleEntity.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            info.ItemPrice = ItemPrice;
            info.GhItemDetails = GhItemDetails;
            info.Xwindow = Xwindow;
            info.account = account;
            info.PayFuc = PayFuc;
            info.XjWc = XjWc;
            info.Fph = settleEntity.fph;
            info.IsGh = isGh;

            //In lại hóa đ ơn hiển thị mới nhất của số hóa đ ơn
            if (dyfs == Enumdyfs.CD)
            {
                OupatientInvoicePrintEntity lastInvoicePrintEntity = _oupatientInvoicePrintRepo.SelectOutPatientInvoicePrintByJsnm(jsnm, this.OrganizeId);

                info.Fph = lastInvoicePrintEntity.xfph;
            }

            //Hạng mụclớn hơn20 dòng,chỉin trư ớc 20 dòng dữ liệu, và nhắc nhở đ ến màn hình cảm ứngtra cứu
            if (ItemDetails.Count > 20)
            {
                for (int i = 0; i < ItemDetails.Count; i++)
                {
                    if (i >= 20)
                        break;

                    info.ItemDetails.Add(ItemDetails[i]);
                }
                info.IsFull = true;
            }
            else
            {
                info.ItemDetails = ItemDetails;
            }

            //thêmgiường không in phải bênhạng mụcchi tiết
            if (isPrintXmmx == false)
            {
                info.ItemDetails = new List<Dictionary<string, string>>();
            }

            return info;

        }

        /// <summary>
        /// Khởi tạo báo cáo nội dung (không phải Đ ăng ký khám)
        /// </summary>
        /// <param name="info"></param>
        private PrintInvoiceVO ReportInitialize(PrintInvoiceInfoVO info)
        {
            // trư ờng tên& trư ờng loại nhất quán có thể sao chépvalue đ ến mới Đ ối tư ợng
            var obj = info.MapperTo<PrintInvoiceInfoVO, PrintInvoiceVO>();
            //Số tiềnviết hoa
            obj.TotalStr = info.TotalStr.ToUpperMoney();


            if (info.Xwindow.Count == 1 && info.Xwindow[0] == "99")
            {
                obj.Xwindow = string.Format("请先取号后拿药。");
            }
            else
            {
                obj.Xwindow = string.Format("请到{0}号窗口拿药。", string.Join(",", info.Xwindow.ToArray()));
            }
            if (!string.IsNullOrWhiteSpace(info.PayFuc))
            {
                obj.PayFuc = info.PayFuc;
            }
            if (!string.IsNullOrWhiteSpace(info.XjWc))
            {
                obj.XjWc = "现金误差：" + info.XjWc;
            }
            if (!string.IsNullOrWhiteSpace(info.account))
            {
                obj.Account = info.account;
            }
            if (!string.IsNullOrWhiteSpace(info.Ks))
            {
                obj.Ks = "(" + info.Ks + ")";
            }
            if (info.IsFull)
            {
                obj.IsFull = "此收据部分项目名称，内容进行了简化。详细信息请在触摸屏等正式公示途径中查询。";
            }

            return obj;
        }

        /// <summary>
        /// bộ in(Đ ăng ký khám)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private PrintInvoiceRegisterVO ReportRegisterInitialize(PrintInvoiceInfoVO info)
        {
            // trư ờng tên& trư ờng loại nhất quán có thể sao chépvalue đ ến mới Đ ối tư ợng
            var obj = info.MapperTo<PrintInvoiceInfoVO, PrintInvoiceRegisterVO>();
            //Số tiềnviết hoa
            obj.TotalStr = info.TotalStr.ToUpperMoney();

            string itemDetailsTxt = string.Empty;
            itemDetailsTxt = string.Format("{0}本科流水号：", itemDetailsTxt);

            obj.P_Name = info.GhItemDetails["Name"];
            obj.P_ID = "ID:" + info.GhItemDetails["ID"];
            obj.P_GH = info.GhItemDetails["YS"];
            obj.P_KS = info.GhItemDetails["KS"];
            obj.P_BKS = itemDetailsTxt;
            obj.P_Date = info.GhItemDetails["dateTime"];
            obj.P_YsName = info.GhItemDetails["YsName"];
            obj.Ks = "(" + info.Ks + ")";
            obj.KsNo = info.GhItemDetails["KsNo"];
            obj.PayFuc = info.PayFuc;
            obj.XjWc = "现金误差：" + info.XjWc;
            obj.Account = info.account;

            return obj;
        }
        #endregion



    }

}