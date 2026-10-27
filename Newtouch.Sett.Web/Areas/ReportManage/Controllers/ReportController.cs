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
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.Sett.Web.Areas.ReportManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("ReportManage")]

    public class ReportController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IReportCommonDmnService _reportCommonDmnService;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IHospFeeDmnService _hospFeeDmnService;
        private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;
	    private readonly IRptrptMzRjbRepo _rptrptMzRjbRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        public ReportController(IReportCommonDmnService reportCommonDmnService, ISysUserDmnService sysUserDmnService, IHospFeeDmnService hospFeeDmnService, IPatientBasicInfoDmnService patientBasicInfoDmnService, IRptrptMzRjbRepo rptrptMzRjbRepo, ISysConfigRepo sysConfigRepo)
        {
            _reportCommonDmnService = reportCommonDmnService;
            _sysUserDmnService = sysUserDmnService;
            _hospFeeDmnService = hospFeeDmnService;
            _patientBasicInfoDmnService = patientBasicInfoDmnService;
            _rptrptMzRjbRepo = rptrptMzRjbRepo;
            _sysConfigRepo = sysConfigRepo;
        }


        #region

        public IActionResult RevenueDetailesByPatientReportCompare() {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 月报表
        /// </summary>
        /// <returns></returns>
        public IActionResult MonthReportIndex()
        {
            return View();
        }

        public IActionResult MonthlyProfit() {
            ReportingServiceCom();
            return View();
        }

        public IActionResult TherapistPerformance() {
            ReportingServiceCom();
            return View();
        }

        public IActionResult PatientDataDetail() {
            ReportingServiceCom();
            return View();
        }
        public IActionResult ChargeTypeQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult OutpatientWorkloadQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult OutpatientSituationQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult OutpatientQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult NormalMedicineInsuranceQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult AllMedicineInsuranceQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult GetStaffByDutyCode(string orgId)
        {
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId ?? OrganizeId, "RehabDoctor");//获取治疗师
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取药库药房下拉框数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetSFDLCodeSelectJson()
        {
            var data = _reportCommonDmnService.GetSFDLCodeSelectJson(OrganizeId);
            var treeList = new List<TreeSelectModel>
            {
                new TreeSelectModel{
                id="",
                text="Chưa chọn"}
            };
            if (data != null)
            {
                foreach (SFDLReportVO item in data)
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.dlcode;
                    treeModel.text = item.dlmc;
                    treeList.Add(treeModel);

                }
                return Content(treeList.TreeSelectJson(null));
            }
            else
            {
                return Content("");
            }
        }

        /// <summary>
        /// 月报表 请求查询 实时 的 数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>

        public IActionResult MonthReportRealTimeData(int year, int month)
        {
            var data = _reportCommonDmnService.GetMonthReportRealTimeData(this.OrganizeId, new DateTime(year, month, 1), new DateTime(year, month, 1).AddMonths(1));
            return Success(null, data);
        }

        /// <summary>
        /// 月报表 固化 调整
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="calcZje"></param>
        /// <returns></returns>
        public IActionResult MonthReportDataConfirmPreview()
        {
            return View();
        }

        public IActionResult NewOutpatientFeeStatistics()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 月报表 固化 确认
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="calcZje"></param>
        /// <param name="resultZje"></param>
        /// <param name="details"></param>
        /// <param name="balance"></param>
        /// <param name="balanceReason"></param>
        /// <param name="isregenerate">重新生成 标志</param>
        /// <returns></returns>

        public IActionResult SubmitMonthReportData(int year, int month, decimal calcZje, decimal resultZje
            , IList<MonthReportMajorCateStatisticsVO> details, decimal balance = 0, string balanceReason = null
            , bool isregenerate = false)
        {
            if (resultZje <= 0)
            {
                throw new FailedException("Tổng chi phí kết quả phải là số dương, vui lòng xác nhận");
            }
            if (calcZje + balance != resultZje)
            {
                throw new FailedException("Số tiền tính bị lỗi, vui lòng xác nhận<br/>(Chi phí + Bù chênh lệch != Kết quả)");
            }
            _reportCommonDmnService.SubmitMonthReportData(this.OrganizeId, this.UserIdentity.UserCode, year, month, calcZje, resultZje
                , details, balance, balanceReason
                , isregenerate);

            return Success("báo cáo Tạo Thành công");
        }

        public IActionResult RehabilitationCategoryTreatDetail()
        {
            ReportingServiceCom();
            return View();
        }

        public IActionResult RevenueDetailesByTherapistReportCompare()
        {
            ReportingServiceCom();
            return View();
        }
        #endregion

        #region Kỹ thuật viên trị liệu Công việc Thống kê

        /// <summary>
        /// 治疗师工作统计
        /// </summary>
        /// <returns></returns>
        public IActionResult TherapeWorkStatistics()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region Phân loại bảng - Nhập viện bệnh nhân

        /// <summary>
        /// 分类清单 - 住院病人
        /// </summary>
        /// <returns></returns>
        public IActionResult InpatientClassification()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region Phân loại bảng - Ngoại trú bệnh nhân

        /// <summary>
        /// 分类清单 - 门诊病人
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientClassification()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region quyết toán đơn - Nhập viện bệnh nhân

        /// <summary>
        /// 结算单 - 住院病人
        /// </summary>
        /// <returns></returns>
        public IActionResult InpatientSett()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region quyết toán đơn - Ngoại trú bệnh nhân

        /// <summary>
        /// 结算单 - 门诊病人
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientSett()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// 结算单 - 门诊病人
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientSettFl()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region 逐 đơn Thu nhập báo cáo

        /// <summary>
        /// 逐单收入报表
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientFeeStatistics()
        {
            ReportingServiceCom();

            return View();
        }

        public IActionResult GetdotorcByDutyCode(string orgId)
        {
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId ?? OrganizeId, "Doctor");//获取医生
            return Content(data.ToJson());
        }

        #endregion

        #region Chi phí một ngày

        /// <summary>
        /// 费用一日清
        /// </summary>
        /// <returns></returns>
        public IActionResult InpatientDayFee()
        {
            
            //_hospFeeDmnService.SyncPatFee(OrganizeId, "", 0);//同步项目费用
            //_hospFeeDmnService.SyncPatFee(OrganizeId, "", 1);//同步药品费用
            ReportingServiceCom();

            return View();
        }

        public IActionResult GetInpatientcryrq(string zyh)
        {
            var hospPatientBasic = _patientBasicInfoDmnService.GetSysBasicVagueByZHY(zyh, this.OrganizeId);
            return Content(hospPatientBasic.ToJson());
        }

        [HttpPost]
        public IActionResult SynInpatientDayFee(string zyh)
        {
            if(!string.IsNullOrWhiteSpace(zyh))
            {
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 0);//同步项目费用
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 1);//同步药品费用
            }
            return Success();
        }
       

        /// <summary>
        /// 绩效指标（费用 人次）
        /// </summary>
        /// <returns></returns>
        public IActionResult PerformanceIndicatorFee()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region Chi phí quyết toán bảng
        public IActionResult InpatientPatFeeDetail()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region Thực hiện计划 Tra cứu
        public IActionResult TreatmentplanQuery()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region Thực hiện计划 Tra cứu Ngoại trú
        public IActionResult TreatmentplanQueryOutpatient()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region Ngoại trú ngày结 báo cáo
        public IActionResult ChargeDailysettlement()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region Nhập viện Chi phí BHYT Thống kê báo cáo
        public IActionResult HealthInsuranceReport()
        {
            ReportingServiceCom();

            return View();
        }
		#endregion

		#region Bệnh viện垫付 Số tiền chi tiết
		public IActionResult HosAdvanceAmount()
		{
			ReportingServiceCom();
			return View();
		}
		#endregion

		#region bệnh nhân Thông tin khám bệnh（khám Số lần v.v.）
		/// <summary>
		/// 病人就诊信息（就诊次数 等）
		/// </summary>
		/// <returns></returns>
		public IActionResult OutpatientTreatmentInfo()
		{
			ReportingServiceCom();
			return View();
		}
        #endregion

        public IActionResult HosOutpatientFees()
        {
            return View();
        }

        /// <summary>
        /// 服务代码盈利
        /// </summary>
        /// <returns></returns>
        public IActionResult RevenueDetailesReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// 服务代码盈利（治疗师分组）
        /// </summary>
        /// <returns></returns>
        public IActionResult RevenueDetailesByTherapistReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// 同步服务代码盈利
        /// </summary>
        /// <returns></returns>
        public IActionResult SyncRevenueDetailesReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// 同步服务代码盈利（治疗师分组）
        /// </summary>
        /// <returns></returns>
        public IActionResult SyncRevenueDetailesByTherapistReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// 病人收费总览
        /// </summary>
        /// <returns></returns>
        public IActionResult PatientRevenue()
        {
            ReportingServiceCom();

            return View();
        }

        public IActionResult PatientRevenueTB()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 门诊病人一览表
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientListChart()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 住院病人一览表
        /// </summary>
        /// <returns></returns>
        public IActionResult InpatientListChart()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 治疗师下拉
        /// </summary>
        /// <returns></returns>
        public IActionResult GetRehabDoctorList(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                orgId = this.OrganizeId;
            }
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId, "RehabDoctor");
            return Content(data.ToJson());
        }

		

		/// <summary>
		/// 病人收费执行报表
		/// </summary>
		/// <returns></returns>
		public IActionResult PatientChargeExecution()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 月度申报结算表（医保）
        /// </summary>
        /// <returns></returns>
        public IActionResult SettMonthForYB()
        {
            ReportingServiceCom();

            return View();
        }
        public IActionResult GetdoctorByDutyCode(string orgId)
        {
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId ?? OrganizeId, "Doctor");//获取医生
            return Content(data.ToJson());
        }

        /// <summary>
        /// 住院全院报表
        /// </summary>
        /// <returns></returns>
        public IActionResult InpatientWhileHospital()
        {
            ReportingServiceCom();
            //return View();
            return View();
        }

        /// <summary>
        /// 门诊全院报表
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientWhileHospital()
        {
            ReportingServiceCom();
            //return View();
            return View();
        }

        public IActionResult PrintCryRegisterReport() {
            ReportingServiceCom();
            //return View();
            return View();
        }
       

        public IActionResult OutpWorkStacDeptReport () {
            ReportingServiceCom();
            return View();
        }

        //费用一日清视图
        public IActionResult PrintReport()
        {
            ReportingServiceCom();

           return View();
        }


        //分类汇总日清单视图
        public IActionResult SubtotalsReport()
        {
            ReportingServiceCom();

            return View();
        }

        //住院费用明细视图
        public IActionResult Hospitalizationcost() {
            ReportingServiceCom();

            return View();
        }

        //住院病人分类清单
        public IActionResult Hospitalizationclassification()
        {
            ReportingServiceCom();

            return View();
        }





        #region hợp tác nông thôn mới申报
        /// <summary>
        /// 门诊新农合申报明细
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientXnhBcmxsqd()
        {
            ReportingServiceCom();
            ViewBag.usercode = UserIdentity.UserCode;
            return View();
        }

        public IActionResult OutpatientXnhBcsqd()
        {
            ReportingServiceCom();
            ViewBag.usercode = UserIdentity.UserCode;
            return View();
        }

        public IActionResult InpatientXnhBcsqd()
        {
            ReportingServiceCom();
            ViewBag.usercode = UserIdentity.UserCode;
            return View();
        }

        public IActionResult InpatientXnhBcmxsqd()
        {
            ReportingServiceCom();
            ViewBag.usercode = UserIdentity.UserCode;
            return View();
        }
        #endregion

        #region
        /// <summary>
        /// 门诊日结算表
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientDailyFee()
        {
            ReportingServiceCom();
            var DailyFeeHbOpen = _sysConfigRepo.GetValueByCode("DailyFeeHbOpen",this.OrganizeId);//日结报表是否合并所有收费员收费记录
            RptrptMzRjbEntity mzRjbEntity= _rptrptMzRjbRepo.GetLastMzrjEntity(this.OrganizeId, DailyFeeHbOpen == "ON"? "":this.UserIdentity.UserCode);
            ViewBag.usercode = DailyFeeHbOpen == "ON" ?"": UserIdentity.UserCode;
			ViewBag.kssj = mzRjbEntity.jssj.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.isfirst = string.IsNullOrWhiteSpace(mzRjbEntity.Id) ?"True" :"False";
            ViewBag.OrgId = this.OrganizeId;
            return View();
        }

	    public IActionResult turnInFee(DateTime kssj,DateTime jssj)
	    {
			_reportCommonDmnService.turnInFee(this.OrganizeId,this.UserIdentity.UserCode,kssj,jssj);
		    return Success();
	    }
	    public IActionResult turnInFeeSelect(string keyword)
	    {
            var DailyFeeHbOpen = _sysConfigRepo.GetValueByCode("DailyFeeHbOpen", this.OrganizeId);
            var rjList = _rptrptMzRjbRepo.GetLastMzrjEntityList(this.OrganizeId, DailyFeeHbOpen == "ON" ? "" : this.UserIdentity.UserCode, keyword);
			return Content(rjList.ToJson());
	    }
		/// <summary>
		/// 门诊结算明细信息表
		/// </summary>
		/// <returns></returns>
		public IActionResult OutpatientSettDetailsInfo()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 科室财务核算汇总表
        /// </summary>
        /// <returns></returns>
        public IActionResult DepartmentFinancialAccSummary()
        {
            ReportingServiceCom();
            return View();
        }

        public IActionResult HosPovertyVisitSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 科室业务收入统计表
        /// </summary>
        /// <returns></returns>
        public IActionResult DepartmentBusinessIncomeSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 药品分类统计表
        /// </summary>
        /// <returns></returns>
        public IActionResult MedicineClassificationSettedSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 门诊收费员工作量统计表
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientTollCollectorWorkloadSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 门诊医生工作量统计表
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientDoctorWorkloadSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 项目分类统计表
        /// </summary>
        /// <returns></returns>
        public IActionResult ChargeItemClassificationSettedSummary()
        {
            ReportingServiceCom();
            return View();
        }

        public IActionResult InpatientCrytjb()
        {
            ReportingServiceCom();
            return View();
        }

	    public IActionResult OutpatientKsstjb()
	    {
		    ReportingServiceCom();
		    return View();
	    }

	    public IActionResult HosMedicalIncome()
	    {
		    ReportingServiceCom();
		    return View();
		}

	    #endregion

		#region private methods

		/// <summary>
		/// 
		/// </summary>
		private void ReportingServiceCom()
        {
            
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.topOrgId = SysConfigReader.String("Top_OrganizeId");
            ViewBag.ReportServerHOST = "";
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //是否是医院管理员
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }

		#endregion

		#region BHYT
	    public IActionResult OutpatientCqYbDz()
	    {
		    ReportingServiceCom();
		    return View();
	    }
        #endregion
        #region BHYT tự chi trả Khoa quyết toán Chi phí Thống kê
        public IActionResult HosExpenseSettDeatilInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult HosExpenseSettInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult HosYbPatientDrYbSettInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult HosYbPatientNoInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult HosYbPatientSettDetailInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult HosYbPatientSettInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult HosYbSettApplyInfo()
        {
            ReportingServiceCom();
            return View();
        }

        public IActionResult HosAcceptingGold()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult HosMedicalBill()
        {
            ReportingServiceCom();
            return View();
        }
        public IActionResult HosFeeSettSummry()
        {
            ReportingServiceCom();
            return View();
        }
        #endregion

         //科室收入明细
        public IActionResult Department_income_statement() {
            ReportingServiceCom();

            return View();
        }

        //科室药品成本价
        public IActionResult Drugcostprice() {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 月度收费明细
        /// </summary>
        /// <returns></returns>
        public IActionResult MonthlyFeeDetails()
        {
            ReportingServiceCom();

            return View();
        }
        
        /// <summary>
        /// 获取大类名称
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDlMc()
		{
			var data = _reportCommonDmnService.GetDlMc(this.OrganizeId);
			return Content(data.ToJson());
		}

        #region Khám bệnh đặc biệt Thống kê
        public IActionResult TBStatistics()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion

		/// <summary>
		/// 城保费用分析
		/// </summary>
		/// <returns></returns>
		public IActionResult MZCBFYFX()
		{
			ReportingServiceCom();

			return View();
		}
		/// <summary>
		/// 居保费用分析
		/// </summary>
		/// <returns></returns>
		public IActionResult MZJBFYFX()
		{
			ReportingServiceCom();

			return View();
		}

		public IActionResult FYXMMXQuery()
		{
			ReportingServiceCom();

			return View();
		}
		
		/// <summary>
		/// 门诊费用日报表汇总
		/// </summary>
		/// <returns></returns>
		public IActionResult MZFYRBHZ_Query()
		{
			ReportingServiceCom();

			return View();
		}

		#region BHYT申报 tải lên báo cáo
		/// <summary>
		/// 医保申报汇总清单
		/// </summary>
		/// <returns></returns>
		public IActionResult YBSBHZQDQuery()
		{
			ReportingServiceCom();

			return View();
		}

		/// <summary>
		/// 医保申报上传职工
		/// </summary>
		/// <returns></returns>
		public IActionResult YBSBSC_ZGQuery()
		{
			ReportingServiceCom();

			return View();
		}


		/// <summary>
		/// 医保申报上传居民
		/// </summary>
		/// <returns></returns>
		public IActionResult YBSBSC_JMQuery()
		{
			ReportingServiceCom();

			return View();
		}

		/// <summary>
		/// 医保申报互助帮困
		/// </summary>
		/// <returns></returns>
		public IActionResult YBSBSC_HZBKQuery()
		{
			ReportingServiceCom();

			return View();
		}

		/// <summary>
		/// 医保申报异地
		/// </summary>
		/// <returns></returns>
		public IActionResult YBSBSC_YDQuery()
		{
			ReportingServiceCom();

			return View();
		}

		/// <summary>
		/// 医保申报导出文件dbf格式 并返回下载文件地址 在前端页面进行下载弹出
		/// </summary>
		/// <returns></returns>
		public IActionResult DCQuery_dbf(string types, string ksrq, string jsrq)
		{
			string year = DateTime.Now.ToString("yy");
			int mone= DateTime.Now.Month;
			string Month = mone.ToString();
			if (mone>9)
			{
				switch (mone)
				{
					case 10:
						Month = "A";
						break;
					case 11:
						Month = "B";
						break;
					case 12:
						Month = "C";
						break;
				}
			}
			string pathname = "YNE" + year + Month + types + ".dbf";
			var uri = Request.Scheme + "://" + Request.Host.Value + Request.Path.Value + Request.QueryString.Value;
			string filepath = SettPathHelper.MapPath("~/BHYT申报 tải lên Tệp/" + pathname);
			var Scheme = Request.Scheme;
			var Host = Request.Host.Value;
			
			var refdata = new
			{
				path =""
			};
			//"D:\\医保申报上传文件" + "\\" + "YNE" + year + Month + types + ".dbf";
			if (System.IO.File.Exists(Path.GetFullPath(filepath)))
			{
				System.IO.File.Delete(Path.GetFullPath(filepath));
			}
			//获取数据 判断是否有数据需要导出下载
			var list = _reportCommonDmnService.yBSBMX_DCdbfs(types, ksrq, jsrq, OrganizeId);
			IEnumerable<object> data = null;
			switch (types)
			{
				case "1":
					data = new List<ZGMJZ_Query>();
					data = list.zgmjzvo;
					if (list.zgmjzvo.Count <= 0)
					{
						return Content(refdata.ToJson());
					}
					break;
				case "2":
					data = new List<ZGDBMZ_Qury>();
					data = list.zgdbmzvo;
					if (list.zgdbmzvo.Count <= 0)
					{
						return Content(refdata.ToJson());
					}
					break;
				case "3":
					data = new List<ZGZY_Query>();
					data = list.zgzyvo;
					if (list.zgzyvo.Count <= 0)
					{
						return Content(refdata.ToJson());
					}
					break;
				case "4":
					data = new List<ZGMJZTS_Query>();
					data = list.zgmjztsvo;
					if (list.zgmjztsvo.Count <= 0)
					{
						return Content(refdata.ToJson());
					}
					break;
				case "5":
					data = new List<ZGZY_Query>();
					data = list.zgzyvo;
					if (list.zgzyvo.Count <= 0)
					{
						return Content(refdata.ToJson());
					}
					break;
				case "Q":
					data = new List<JMMJZ_Query>();
					data = list.jmmjzvo;
					if (list.jmmjzvo.Count <= 0)
					{
						return Content(refdata.ToJson());
					}
					break;
				case "R":
					data = new List<JMZY_Query>();
					data = list.jmzyvo;
					if (list.jmzyvo.Count <= 0)
					{
						return Content(refdata.ToJson());
					}
					break;
				case "S":
					data = new List<HZBK_Query>();
					data = list.hzbkvo;
					if (list.hzbkvo.Count <= 0)
					{
						return Content(refdata.ToJson());
					}
					break;
				case "W":
					data = new List<MZYD_Query>();
					data = list.ydmzvo;
					if (list.ydmzvo.Count <= 0)
					{
						return Content(refdata.ToJson());
					}
					break;
				case "E":
					data = new List<ZYYD_Query>();
					data = list.ydzyvo;
					if (list.ydzyvo.Count <= 0)
					{
						return Content(refdata.ToJson());
					}
					break;
			}
			using (System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite))
			using (var writer = new DBFWriter(fs))
			{
				foreach (var item in data)
				{
					System.Reflection.PropertyInfo[] properties = item.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
					DBFField[] fields = new DBFField[properties.Length];
					object[] listval = new object[properties.Length];
					var i = 0;
					foreach (var typeitem in properties)
					{
						var type = NativeDbType.Char;
						var lenth = 50;
						var decimallentg = 0;
						listval[i] = typeitem.GetValue(item, null);
						switch (typeitem.PropertyType.Name.ToLower())
						{
							case "string":
								type = NativeDbType.Char;
								lenth = 32;
								break;
							case "decimal":
								type = NativeDbType.Numeric;
								lenth = 10;
								decimallentg = 2;
								break;
						}
						if (decimallentg > 0)
						{
							fields[i] = new DBFField(typeitem.Name, type, lenth, decimallentg);
						}
						else
						{
							fields[i] = new DBFField(typeitem.Name, type, lenth);
						}

						i++;
					}
					if (writer.Fields == null)
					{
						writer.Fields = fields;
					}
					writer.CharEncoding = System.Text.Encoding.Default;
					writer.WriteRecord(listval);
					listval = null;
				}
			}

			#region  dbf TệpFileStream读 lấy không đến Tệp Chưa tìm thấy lý do 现 Sử dụng拼接 địa chỉ trước phía Tải xuống
			//try
			//{
			//	System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite, 40960,true);

			//	byte[] b = new Byte[fs.Length];
			//	fs.Read(b, 0, b.Length);
			//	fs.Flush();
			//	fs.Close();

			//	//System.IO.File.Delete(SavePdfPath);
			//	Response.Clear();
			//	Response.ClearHeaders();
			//	Response.Clear();
			//	Response.ClearHeaders();
			//	Response.Buffer = false;
			//	Response.ContentType = "application/octet-stream";      //ContentType;
			//	Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Net.WebUtility.UrlEncode(pathname, System.Text.Encoding.UTF8));
			//	Response.AppendHeader("Content-Length", b.Length.ToString());
			//	fs.Close();
			//	fs.Close();
			//	if (b.Length > 0)
			//	{
			//		Response.OutputStream.Write(b, 0, b.Length);
			//	}

			//	Response.Flush();
			//	Response.End();
			//}
			//catch (Exception ex)
			//{
			//	string msg = ex.Message;
			//}
			//return File(new FileStream(filepath, FileMode.OpenOrCreate), "application/octet-stream", pathname);//“text/plain”是文件MIME类型
			//return File(filepath, "application/x-dbf", pathname);
			#endregion

			var refpath = Scheme + "://" + Host + "/BHYT申报 tải lên Tệp/" + pathname;
			refdata = new
			{
				path = refpath
			};
			return Content(refdata.ToJson());
		}

        #endregion
        #region Lấy Khoản thu phí
        /// <summary>
        /// 获取大类名称
        /// </summary>
        /// <returns></returns>
        public IActionResult Getsfxm(Pagination pagination, string xzstr,string py,string sfdl)
        {
            var list = new
            {
                rows = _reportCommonDmnService.Getsfxm(pagination, xzstr, py, sfdl, this.OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        #endregion BHYT trên报 báo cáo
        #region
        /// <summary>
        /// 总页面
        /// </summary>
        /// <returns></returns>
        public IActionResult YBXXQueryUpload()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 交易记录库
        /// </summary>
        /// <returns></returns>
        public IActionResult JYJLQueryUpload()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 门急诊大病挂号库
        /// </summary>
        /// <returns></returns>
        public IActionResult DBGHQueryUpload()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 门急诊家床收费库
        /// </summary>
        /// <returns></returns>
        public IActionResult JCSFQueryUpload()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 获取交易记录信息
        /// </summary>
        /// <returns></returns>
        public IActionResult GetJyjlData(Pagination pagination, string ksrq, string jsrq)
        {
            var list = new
            {
                rows = _reportCommonDmnService.GetJyjlData(pagination, ksrq, jsrq,this.OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        public IActionResult Ybsjsctotxt(string ksrq, string jsrq)
        {
            StringBuilder sb = new StringBuilder();
            var rows = _reportCommonDmnService.GetJyjlDatatotxt(ksrq, jsrq, this.OrganizeId);
            return Content(rows.ToJson()); 
        }
        /// <summary>
        /// 获取门急诊大病挂号库
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMjzdbghData(Pagination pagination, string ksrq, string jsrq)
        {
            var list = new
            {
                rows = _reportCommonDmnService.GetMjzdbghData(pagination, ksrq, jsrq, this.OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        public IActionResult YbMjzdbghtxt(string ksrq, string jsrq)
        {
            StringBuilder sb = new StringBuilder();
            var rows = _reportCommonDmnService.GetYbMjzdbghtxt(ksrq, jsrq, this.OrganizeId);
            return Content(rows.ToJson());
        }
        /// <summary>
        /// 获取门急诊大病加床收费数据
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMjzjcsfData(Pagination pagination, string ksrq, string jsrq)
        {
            var list = new
            {
                rows = _reportCommonDmnService.GetMjzjcsfData(pagination, ksrq, jsrq, this.OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        public IActionResult GetMjzjcsfDatatxt(string ksrq, string jsrq)
        {
            StringBuilder sb = new StringBuilder();
            var rows = _reportCommonDmnService.GetMjzjcsfDatatxt(ksrq, jsrq, this.OrganizeId);
            return Content(rows.ToJson());
        }
        #endregion
    }
}
