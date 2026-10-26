using DotNetDBF;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.ReportManage;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportController : ControllerBase
    {
        private readonly IReportCommonDmnService _reportCommonDmnService;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IHospFeeDmnService _hospFeeDmnService;
        private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;
	    private readonly IRptrptMzRjbRepo _rptrptMzRjbRepo;
        private readonly ISysConfigRepo _sysConfigRepo;

        #region

        public ActionResult RevenueDetailesByPatientReportCompare() {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Báo cáo tháng
        /// </summary>
        /// <returns></returns>
        public ActionResult MonthReportIndex()
        {
            return View();
        }

        public ActionResult MonthlyProfit() {
            ReportingServiceCom();
            return View();
        }

        public ActionResult TherapistPerformance() {
            ReportingServiceCom();
            return View();
        }

        public ActionResult PatientDataDetail() {
            ReportingServiceCom();
            return View();
        }
        public ActionResult ChargeTypeQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult OutpatientWorkloadQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult OutpatientSituationQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult OutpatientQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult NormalMedicineInsuranceQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult AllMedicineInsuranceQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult GetStaffByDutyCode(string orgId)
        {
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId ?? OrganizeId, "RehabDoctor");//Lấy kỹ thuật viên trị liệu
            return Content(data.ToJson());
        }
        /// <summary>
        /// Lấy dữ liệu hộp thả xuống kho thuốc/nhà thuốc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSFDLCodeSelectJson()
        {
            var data = _reportCommonDmnService.GetSFDLCodeSelectJson(OrganizeId);
            var treeList = new List<TreeSelectModel>
            {
                new TreeSelectModel{
                id="",
                text="未选择"}
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
        /// Báo cáo tháng Yêu cầu tra cứu Thời gian thực của Dữ liệu
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult MonthReportRealTimeData(int year, int month)
        {
            var data = _reportCommonDmnService.GetMonthReportRealTimeData(this.OrganizeId, new DateTime(year, month, 1), new DateTime(year, month, 1).AddMonths(1));
            return Success(null, data);
        }

        /// <summary>
        /// Báo cáo tháng Cố định hóa Điều chỉnh
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="calcZje"></param>
        /// <returns></returns>
        public ActionResult MonthReportDataConfirmPreview()
        {
            return View();
        }

        public ActionResult NewOutpatientFeeStatistics()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// Báo cáo tháng Cố định hóa Xác nhận
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="calcZje"></param>
        /// <param name="resultZje"></param>
        /// <param name="details"></param>
        /// <param name="balance"></param>
        /// <param name="balanceReason"></param>
        /// <param name="isregenerate">Tạo lại Cờ</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult SubmitMonthReportData(int year, int month, decimal calcZje, decimal resultZje
            , IList<MonthReportMajorCateStatisticsVO> details, decimal balance = 0, string balanceReason = null
            , bool isregenerate = false)
        {
            if (resultZje <= 0)
            {
                throw new FailedException("费用合计结果应为正数，请确认");
            }
            if (calcZje + balance != resultZje)
            {
                throw new FailedException("金额计算错误，请确认<br/>（费用+补差!=结果）");
            }
            _reportCommonDmnService.SubmitMonthReportData(this.OrganizeId, this.UserIdentity.UserCode, year, month, calcZje, resultZje
                , details, balance, balanceReason
                , isregenerate);

            return Success("报表生成成功");
        }

        public ActionResult RehabilitationCategoryTreatDetail()
        {
            ReportingServiceCom();
            return View();
        }

        public ActionResult RevenueDetailesByTherapistReportCompare()
        {
            ReportingServiceCom();
            return View();
        }
        #endregion

        #region 治疗师工作统计

        /// <summary>
        /// Thống kê công việc kỹ thuật viên trị liệu
        /// </summary>
        /// <returns></returns>
        public ActionResult TherapeWorkStatistics()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region 分类清单 - 住院病人

        /// <summary>
        /// Danh sách phân loại - Bệnh nhân nội trú
        /// </summary>
        /// <returns></returns>
        public ActionResult InpatientClassification()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region 分类清单 - 门诊病人

        /// <summary>
        /// Danh sách phân loại - Bệnh nhân ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientClassification()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region 结算单 - 住院病人

        /// <summary>
        /// Phiếu quyết toán - Bệnh nhân nội trú
        /// </summary>
        /// <returns></returns>
        public ActionResult InpatientSett()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region 结算单 - 门诊病人

        /// <summary>
        /// Phiếu quyết toán - Bệnh nhân ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientSett()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// Phiếu quyết toán - Bệnh nhân ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientSettFl()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region 逐单收入报表

        /// <summary>
        /// Báo cáo thu nhập theo từng đơn
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientFeeStatistics()
        {
            ReportingServiceCom();

            return View();
        }

        public ActionResult GetdotorcByDutyCode(string orgId)
        {
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId ?? OrganizeId, "Doctor");//Lấy bác sĩ
            return Content(data.ToJson());
        }

        #endregion

        #region 费用一日清

        /// <summary>
        /// Bảng kê chi phí trong ngày
        /// </summary>
        /// <returns></returns>
        public ActionResult InpatientDayFee()
        {
            
            //_hospFeeDmnService.SyncPatFee(OrganizeId, "", 0);//Đồng bộ chi phí khoản mục
            //_hospFeeDmnService.SyncPatFee(OrganizeId, "", 1);//Đồng bộ chi phí thuốc
            ReportingServiceCom();

            return View();
        }

        public ActionResult GetInpatientcryrq(string zyh)
        {
            var hospPatientBasic = _patientBasicInfoDmnService.GetSysBasicVagueByZHY(zyh, this.OrganizeId);
            return Content(hospPatientBasic.ToJson());
        }

        [HttpPost]
        public ActionResult SynInpatientDayFee(string zyh)
        {
            if(!string.IsNullOrWhiteSpace(zyh))
            {
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 0);//Đồng bộ chi phí khoản mục
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 1);//Đồng bộ chi phí thuốc
            }
            return Success();
        }
       

        /// <summary>
        /// Chỉ số hiệu quả（Chi phí Lượt người）
        /// </summary>
        /// <returns></returns>
        public ActionResult PerformanceIndicatorFee()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region 费用结算清单
        public ActionResult InpatientPatFeeDetail()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region 执行计划查询
        public ActionResult TreatmentplanQuery()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region 执行计划查询门诊
        public ActionResult TreatmentplanQueryOutpatient()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region 门诊日结报表
        public ActionResult ChargeDailysettlement()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region 住院医保费用统计报表
        public ActionResult HealthInsuranceReport()
        {
            ReportingServiceCom();

            return View();
        }
		#endregion

		#region 医院垫付金额明细
		public ActionResult HosAdvanceAmount()
		{
			ReportingServiceCom();
			return View();
		}
		#endregion

		#region 病人就诊信息（就诊次数 等）
		/// <summary>
		/// Thông tin khám bệnh của bệnh nhân（Số lần khám bệnh v.v.）
		/// </summary>
		/// <returns></returns>
		public ActionResult OutpatientTreatmentInfo()
		{
			ReportingServiceCom();
			return View();
		}
        #endregion

        public ActionResult HosOutpatientFees()
        {
            return View();
        }

        /// <summary>
        /// Lợi nhuận mã dịch vụ
        /// </summary>
        /// <returns></returns>
        public ActionResult RevenueDetailesReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// Lợi nhuận mã dịch vụ（Nhóm kỹ thuật viên trị liệu）
        /// </summary>
        /// <returns></returns>
        public ActionResult RevenueDetailesByTherapistReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// Đồng bộ lợi nhuận mã dịch vụ
        /// </summary>
        /// <returns></returns>
        public ActionResult SyncRevenueDetailesReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// Đồng bộ lợi nhuận mã dịch vụ（Nhóm kỹ thuật viên trị liệu）
        /// </summary>
        /// <returns></returns>
        public ActionResult SyncRevenueDetailesByTherapistReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// Tổng quan thu phí bệnh nhân
        /// </summary>
        /// <returns></returns>
        public ActionResult PatientRevenue()
        {
            ReportingServiceCom();

            return View();
        }

        public ActionResult PatientRevenueTB()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// Bảng tổng quan bệnh nhân ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientListChart()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// Bảng tổng quan bệnh nhân nội trú
        /// </summary>
        /// <returns></returns>
        public ActionResult InpatientListChart()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// Thả xuống kỹ thuật viên trị liệu
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRehabDoctorList(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                orgId = this.OrganizeId;
            }
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId, "RehabDoctor");
            return Content(data.ToJson());
        }

		

		/// <summary>
		/// Báo cáo thực hiện thu phí bệnh nhân
		/// </summary>
		/// <returns></returns>
		public ActionResult PatientChargeExecution()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// Bảng kê khai quyết toán hàng tháng（Bảo hiểm y tế）
        /// </summary>
        /// <returns></returns>
        public ActionResult SettMonthForYB()
        {
            ReportingServiceCom();

            return View();
        }
        public ActionResult GetdoctorByDutyCode(string orgId)
        {
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId ?? OrganizeId, "Doctor");//Lấy bác sĩ
            return Content(data.ToJson());
        }

        /// <summary>
        /// Báo cáo toàn viện nội trú
        /// </summary>
        /// <returns></returns>
        public ActionResult InpatientWhileHospital()
        {
            ReportingServiceCom();
            //return View();
            return View();
        }

        /// <summary>
        /// Báo cáo toàn viện ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientWhileHospital()
        {
            ReportingServiceCom();
            //return View();
            return View();
        }

        public ActionResult PrintCryRegisterReport() {
            ReportingServiceCom();
            //return View();
            return View();
        }
       

        public ActionResult OutpWorkStacDeptReport () {
            ReportingServiceCom();
            return View();
        }

        //View bảng kê chi phí trong ngày
        public ActionResult PrintReport()
        {
            ReportingServiceCom();

           return View();
        }


        //View danh sách ngày tổng hợp phân loại
        public ActionResult SubtotalsReport()
        {
            ReportingServiceCom();

            return View();
        }

        //View chi tiết chi phí nội trú
        public ActionResult Hospitalizationcost() {
            ReportingServiceCom();

            return View();
        }

        //Danh sách phân loại bệnh nhân nội trú
        public ActionResult Hospitalizationclassification()
        {
            ReportingServiceCom();

            return View();
        }





        #region 新农合申报
        /// <summary>
        /// Chi tiết kê khai bảo hiểm y tế nông thôn mới ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientXnhBcmxsqd()
        {
            ReportingServiceCom();
            ViewBag.usercode = UserIdentity.UserCode;
            return View();
        }

        public ActionResult OutpatientXnhBcsqd()
        {
            ReportingServiceCom();
            ViewBag.usercode = UserIdentity.UserCode;
            return View();
        }

        public ActionResult InpatientXnhBcsqd()
        {
            ReportingServiceCom();
            ViewBag.usercode = UserIdentity.UserCode;
            return View();
        }

        public ActionResult InpatientXnhBcmxsqd()
        {
            ReportingServiceCom();
            ViewBag.usercode = UserIdentity.UserCode;
            return View();
        }
        #endregion

        #region
        /// <summary>
        /// Bảng quyết toán ngày ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientDailyFee()
        {
            ReportingServiceCom();
            var DailyFeeHbOpen = _sysConfigRepo.GetValueByCode("DailyFeeHbOpen",this.OrganizeId);//Báo cáo quyết toán ngày có gộp tất cả bản ghi thu phí của nhân viên thu phí hay không
            RptrptMzRjbEntity mzRjbEntity= _rptrptMzRjbRepo.GetLastMzrjEntity(this.OrganizeId, DailyFeeHbOpen == "ON"? "":this.UserIdentity.UserCode);
            ViewBag.usercode = DailyFeeHbOpen == "ON" ?"": UserIdentity.UserCode;
			ViewBag.kssj = mzRjbEntity.jssj.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.isfirst = string.IsNullOrWhiteSpace(mzRjbEntity.Id) ?"True" :"False";
            ViewBag.OrgId = this.OrganizeId;
            return View();
        }

	    public ActionResult turnInFee(DateTime kssj,DateTime jssj)
	    {
			_reportCommonDmnService.turnInFee(this.OrganizeId,this.UserIdentity.UserCode,kssj,jssj);
		    return Success();
	    }
	    public ActionResult turnInFeeSelect(string keyword)
	    {
            var DailyFeeHbOpen = _sysConfigRepo.GetValueByCode("DailyFeeHbOpen", this.OrganizeId);
            var rjList = _rptrptMzRjbRepo.GetLastMzrjEntityList(this.OrganizeId, DailyFeeHbOpen == "ON" ? "" : this.UserIdentity.UserCode, keyword);
			return Content(rjList.ToJson());
	    }
		/// <summary>
		/// Bảng thông tin chi tiết quyết toán ngoại trú
		/// </summary>
		/// <returns></returns>
		public ActionResult OutpatientSettDetailsInfo()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Bảng tổng hợp hạch toán tài chính khoa
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentFinancialAccSummary()
        {
            ReportingServiceCom();
            return View();
        }

        public ActionResult HosPovertyVisitSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Bảng thống kê thu nhập nghiệp vụ khoa
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentBusinessIncomeSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Bảng thống kê phân loại thuốc
        /// </summary>
        /// <returns></returns>
        public ActionResult MedicineClassificationSettedSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Bảng thống kê khối lượng công việc nhân viên thu phí ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientTollCollectorWorkloadSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Bảng thống kê khối lượng công việc bác sĩ ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientDoctorWorkloadSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Bảng thống kê phân loại khoản mục
        /// </summary>
        /// <returns></returns>
        public ActionResult ChargeItemClassificationSettedSummary()
        {
            ReportingServiceCom();
            return View();
        }

        public ActionResult InpatientCrytjb()
        {
            ReportingServiceCom();
            return View();
        }

	    public ActionResult OutpatientKsstjb()
	    {
		    ReportingServiceCom();
		    return View();
	    }

	    public ActionResult HosMedicalIncome()
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
            ViewBag.topOrgId = Constants.TopOrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //Có phải là quản trị viên bệnh viện hay không
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }

		#endregion

		#region 医保
	    public ActionResult OutpatientCqYbDz()
	    {
		    ReportingServiceCom();
		    return View();
	    }
        #endregion
        #region 医保自费科室结算费用统计
        public ActionResult HosExpenseSettDeatilInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosExpenseSettInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosYbPatientDrYbSettInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosYbPatientNoInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosYbPatientSettDetailInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosYbPatientSettInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosYbSettApplyInfo()
        {
            ReportingServiceCom();
            return View();
        }

        public ActionResult HosAcceptingGold()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosMedicalBill()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosFeeSettSummry()
        {
            ReportingServiceCom();
            return View();
        }
        #endregion

         //Chi tiết thu nhập khoa
        public ActionResult Department_income_statement() {
            ReportingServiceCom();

            return View();
        }

        //Giá vốn thuốc của khoa
        public ActionResult Drugcostprice() {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// Chi tiết thu phí hàng tháng
        /// </summary>
        /// <returns></returns>
        public ActionResult MonthlyFeeDetails()
        {
            ReportingServiceCom();

            return View();
        }
        
        /// <summary>
        /// Lấy tên loại lớn
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDlMc()
		{
			var data = _reportCommonDmnService.GetDlMc(this.OrganizeId);
			return Content(data.ToJson());
		}

        #region 特病门诊统计
        public ActionResult TBStatistics()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion

		/// <summary>
		/// Phân tích chi phí bảo hiểm thành thị
		/// </summary>
		/// <returns></returns>
		public ActionResult MZCBFYFX()
		{
			ReportingServiceCom();

			return View();
		}
		/// <summary>
		/// Phân tích chi phí bảo hiểm cư dân
		/// </summary>
		/// <returns></returns>
		public ActionResult MZJBFYFX()
		{
			ReportingServiceCom();

			return View();
		}

		public ActionResult FYXMMXQuery()
		{
			ReportingServiceCom();

			return View();
		}
		
		/// <summary>
		/// Tổng hợp báo cáo ngày chi phí ngoại trú
		/// </summary>
		/// <returns></returns>
		public ActionResult MZFYRBHZ_Query()
		{
			ReportingServiceCom();

			return View();
		}

		#region 医保申报上传报表
		/// <summary>
		/// Danh sách tổng hợp kê khai bảo hiểm y tế
		/// </summary>
		/// <returns></returns>
		public ActionResult YBSBHZQDQuery()
		{
			ReportingServiceCom();

			return View();
		}

		/// <summary>
		/// Tải lên kê khai bảo hiểm y tế cho công nhân viên
		/// </summary>
		/// <returns></returns>
		public ActionResult YBSBSC_ZGQuery()
		{
			ReportingServiceCom();

			return View();
		}


		/// <summary>
		/// Tải lên kê khai bảo hiểm y tế cho cư dân
		/// </summary>
		/// <returns></returns>
		public ActionResult YBSBSC_JMQuery()
		{
			ReportingServiceCom();

			return View();
		}

		/// <summary>
		/// Kê khai bảo hiểm y tế tương trợ giúp đỡ khó khăn
		/// </summary>
		/// <returns></returns>
		public ActionResult YBSBSC_HZBKQuery()
		{
			ReportingServiceCom();

			return View();
		}

		/// <summary>
		/// Kê khai bảo hiểm y tế ngoại tỉnh
		/// </summary>
		/// <returns></returns>
		public ActionResult YBSBSC_YDQuery()
		{
			ReportingServiceCom();

			return View();
		}

		/// <summary>
		/// Xuất tệp kê khai bảo hiểm y tế dbf Định dạng Và trả về địa chỉ tải xuống tệp Bật tải xuống tại trang frontend
		/// </summary>
		/// <returns></returns>
		public ActionResult DCQuery_dbf(string types, string ksrq, string jsrq)
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
			var uri = Request.Url.AbsoluteUri;
			string filepath = Server.MapPath("~/医保申报上传文件/" + pathname);
			var Scheme = Request.Url.Scheme;
			var Host = Request.Url.Authority;
			
			var refdata = new
			{
				path =""
			};
			//"D:\\Tải lên tệp kê khai bảo hiểm y tế" + "\\" + "YNE" + year + Month + types + ".dbf";
			if (System.IO.File.Exists(Path.GetFullPath(filepath)))
			{
				System.IO.File.Delete(Path.GetFullPath(filepath));
			}
			//Lấy dữ liệu Xác định có dữ liệu cần xuất để tải xuống hay không
			var list = _reportCommonDmnService.yBSBMX_DCdbfs(types, ksrq, jsrq, OrganizeId);
			IEnumerable<YBSBMX> data = null;
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

			#region  dbf文件FileStream读取不到文件 未找到原因 现使用拼接地址前端下载
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
			//	Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(pathname, System.Text.Encoding.UTF8));
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
			//return File(new FileStream(filepath, FileMode.OpenOrCreate), "application/octet-stream", pathname);//“text/plain”là tệp MIME Loại
			//return File(filepath, "application/x-dbf", pathname);
			#endregion

			var refpath = Scheme + "://" + Host + "/医保申报上传文件/" + pathname;
			refdata = new
			{
				path = refpath
			};
			return Content(refdata.ToJson());
		}

        #endregion
        #region 获取收费项目
        /// <summary>
        /// Lấy tên loại lớn
        /// </summary>
        /// <returns></returns>
        public ActionResult Getsfxm(Pagination pagination, string xzstr,string py,string sfdl)
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
        #endregion 医保上报报表
        #region
        /// <summary>
        /// Tổng số trang
        /// </summary>
        /// <returns></returns>
        public ActionResult YBXXQueryUpload()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// Kho bản ghi giao dịch
        /// </summary>
        /// <returns></returns>
        public ActionResult JYJLQueryUpload()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// Kho đăng ký khám bệnh nặng ngoại trú/cấp cứu
        /// </summary>
        /// <returns></returns>
        public ActionResult DBGHQueryUpload()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// Kho thu phí giường tại nhà ngoại trú/cấp cứu
        /// </summary>
        /// <returns></returns>
        public ActionResult JCSFQueryUpload()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// Lấy thông tin bản ghi giao dịch
        /// </summary>
        /// <returns></returns>
        public ActionResult GetJyjlData(Pagination pagination, string ksrq, string jsrq)
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
        public ActionResult Ybsjsctotxt(string ksrq, string jsrq)
        {
            StringBuilder sb = new StringBuilder();
            var rows = _reportCommonDmnService.GetJyjlDatatotxt(ksrq, jsrq, this.OrganizeId);
            return Content(rows.ToJson()); 
        }
        /// <summary>
        /// Lấy kho đăng ký khám bệnh nặng ngoại trú/cấp cứu
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMjzdbghData(Pagination pagination, string ksrq, string jsrq)
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
        public ActionResult YbMjzdbghtxt(string ksrq, string jsrq)
        {
            StringBuilder sb = new StringBuilder();
            var rows = _reportCommonDmnService.GetYbMjzdbghtxt(ksrq, jsrq, this.OrganizeId);
            return Content(rows.ToJson());
        }
        /// <summary>
        /// Lấy dữ liệu thu phí thêm giường ngoại trú/cấp cứu
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMjzjcsfData(Pagination pagination, string ksrq, string jsrq)
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
        public ActionResult GetMjzjcsfDatatxt(string ksrq, string jsrq)
        {
            StringBuilder sb = new StringBuilder();
            var rows = _reportCommonDmnService.GetMjzjcsfDatatxt(ksrq, jsrq, this.OrganizeId);
            return Content(rows.ToJson());
        }
        #endregion
    }
}