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
        /// Báo cáo tháng
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
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId ?? OrganizeId, "RehabDoctor");//đượclấyđiều trịđiều trịsư
            return Content(data.ToJson());
        }
        /// <summary>
        /// đượclấythuốckhothuốcphòngdướikéokhungsốdữ liệu
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
        /// Báo cáo tháng vui lòngyêu cầutratra cứu thựcthời gian của dữ liệu
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
        /// Báo cáo tháng Cố định Điều chỉnh
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
        /// Báo cáo tháng Cố định xác nhận
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="calcZje"></param>
        /// <param name="resultZje"></param>
        /// <param name="details"></param>
        /// <param name="balance"></param>
        /// <param name="balanceReason"></param>
        /// <param name="isregenerate">Tạo lại dấu hiệu</param>
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
        /// điều trịđiều trịsưcônglàmthốngtính
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
        /// Danh sách phân loại - Bệnh nhân nhập viện
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
        /// Danh sách phân loại - Bệnh nhân ngoại trú
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
        /// Phiếu quyết toán - Bệnh nhân nhập viện
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
        /// Phiếu quyết toán - Bệnh nhân ngoại trú
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientSett()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// Phiếu quyết toán - Bệnh nhân ngoại trú
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientSettFl()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region từng đơn Thu nhập báo cáo

        /// <summary>
        /// từngphiếuthuvàobáo cáobảng
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientFeeStatistics()
        {
            ReportingServiceCom();

            return View();
        }

        public IActionResult GetdotorcByDutyCode(string orgId)
        {
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId ?? OrganizeId, "Doctor");//Lấy bác sĩ
            return Content(data.ToJson());
        }

        #endregion

        #region Chi phí một ngày

        /// <summary>
        /// Chi phí một ngày rõ ràng
        /// </summary>
        /// <returns></returns>
        public IActionResult InpatientDayFee()
        {
            
            //_hospFeeDmnService.SyncPatFee(OrganizeId, "", 0);//Đồng bộ phí mục
            //_hospFeeDmnService.SyncPatFee(OrganizeId, "", 1);//Đồng bộ phí thuốc
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
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 0);//Đồng bộ phí mục
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 1);//Đồng bộ phí thuốc
            }
            return Success();
        }
       

        /// <summary>
        /// Chỉ số hiệu quả（Chi phí lượt người）
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
        #region Thực hiệnKế hoạch Tra cứu
        public IActionResult TreatmentplanQuery()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region Thực hiệnKế hoạch Tra cứu Ngoại trú
        public IActionResult TreatmentplanQueryOutpatient()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region Ngoại trú ngàykết báo cáo
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

		#region Bệnh việnứng trướctrả Số tiền chi tiết
		public IActionResult HosAdvanceAmount()
		{
			ReportingServiceCom();
			return View();
		}
		#endregion

		#region bệnh nhân Thông tin khám bệnh（khám Số lần v.v.）
		/// <summary>
		/// bệnhngườithìkhámthông tintin（thìkhámlầnsố v.v.）
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
        /// Lợi nhuận mã dịch vụ
        /// </summary>
        /// <returns></returns>
        public IActionResult RevenueDetailesReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// Lợi nhuận mã dịch vụ（Nhóm trị liệu sư）
        /// </summary>
        /// <returns></returns>
        public IActionResult RevenueDetailesByTherapistReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// Đồng bộ lợi nhuận mã dịch vụ
        /// </summary>
        /// <returns></returns>
        public IActionResult SyncRevenueDetailesReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// Đồng bộ lợi nhuận mã dịch vụ（Nhóm trị liệu sư）
        /// </summary>
        /// <returns></returns>
        public IActionResult SyncRevenueDetailesByTherapistReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// bệnhngườithuphítổngxem
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
        /// ngoại trúkhámbệnhngườimộtxembảng
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientListChart()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// nhập việnviệnbệnhngườimộtxembảng
        /// </summary>
        /// <returns></returns>
        public IActionResult InpatientListChart()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// điều trịđiều trịsưdướikéo
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
		/// bệnhngườithuphíthực hiệnthực hiệnbáo cáobảng
		/// </summary>
		/// <returns></returns>
		public IActionResult PatientChargeExecution()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// thángđộkhaibáo cáokếttínhbảng（BHYT）
        /// </summary>
        /// <returns></returns>
        public IActionResult SettMonthForYB()
        {
            ReportingServiceCom();

            return View();
        }
        public IActionResult GetdoctorByDutyCode(string orgId)
        {
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId ?? OrganizeId, "Doctor");//Lấy bác sĩ
            return Content(data.ToJson());
        }

        /// <summary>
        /// nhập việnviệntất cảviệnbáo cáobảng
        /// </summary>
        /// <returns></returns>
        public IActionResult InpatientWhileHospital()
        {
            ReportingServiceCom();
            //return View();
            return View();
        }

        /// <summary>
        /// ngoại trúkhámtất cảviệnbáo cáobảng
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

        //phídùngmộtngàyxóaxembiểu đồ
        public IActionResult PrintReport()
        {
            ReportingServiceCom();

           return View();
        }


        //phânloạitổng hợptổngngàyxóaphiếuxembiểu đồ
        public IActionResult SubtotalsReport()
        {
            ReportingServiceCom();

            return View();
        }

        //nhập việnviệnphídùngrõchi tiếtxembiểu đồ
        public IActionResult Hospitalizationcost() {
            ReportingServiceCom();

            return View();
        }

        //nhập việnviệnbệnhngườiphânloạixóaphiếu
        public IActionResult Hospitalizationclassification()
        {
            ReportingServiceCom();

            return View();
        }





        #region hợp tác nông thôn mớiKhai báo
        /// <summary>
        /// ngoại trúkhámmớinônghợpkhaibáo cáorõchi tiết
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
        /// ngoại trúkhámngàykếttínhbảng
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientDailyFee()
        {
            ReportingServiceCom();
            var DailyFeeHbOpen = _sysConfigRepo.GetValueByCode("DailyFeeHbOpen",this.OrganizeId);//ngàykếtbáo cáobảnglàkhônghợpvànơicóthuphíviênthuphíghighi
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
		/// ngoại trúkhámkếttínhrõchi tiếtthông tintinbảng
		/// </summary>
		/// <returns></returns>
		public IActionResult OutpatientSettDetailsInfo()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// khoaphòngtài chínhvụkiểm tratínhtổng hợptổngbảng
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
        /// khoaphòngnghiệpvụthuvàothốngtínhbảng
        /// </summary>
        /// <returns></returns>
        public IActionResult DepartmentBusinessIncomeSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// thuốcphẩmphânloạithốngtínhbảng
        /// </summary>
        /// <returns></returns>
        public IActionResult MedicineClassificationSettedSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// ngoại trúkhámthuphíviêncônglàmlượngthốngtínhbảng
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientTollCollectorWorkloadSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// ngoại trúkhámy tếsinhcônglàmlượngthốngtínhbảng
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientDoctorWorkloadSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// hạngmụcphânloạithốngtínhbảng
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
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //làkhônglày tếviệnquảnlýviên
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

         //khoaphòngthuvàorõchi tiết
        public IActionResult Department_income_statement() {
            ReportingServiceCom();

            return View();
        }

        //khoaphòngthuốcphẩmthànhnàygiá
        public IActionResult Drugcostprice() {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// thángđộthuphírõchi tiết
        /// </summary>
        /// <returns></returns>
        public IActionResult MonthlyFeeDetails()
        {
            ReportingServiceCom();

            return View();
        }
        
        /// <summary>
        /// Lấy tên loại lớn
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
		/// thành phốbảo hiểmphídùngphânphân tích
		/// </summary>
		/// <returns></returns>
		public IActionResult MZCBFYFX()
		{
			ReportingServiceCom();

			return View();
		}
		/// <summary>
		/// cư trúbảo hiểmphídùngphânphân tích
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
		/// ngoại trúkhámphídùngngàybáo cáobảngtổng hợptổng
		/// </summary>
		/// <returns></returns>
		public IActionResult MZFYRBHZ_Query()
		{
			ReportingServiceCom();

			return View();
		}

		#region BHYTKhai báo tải lên báo cáo
		/// <summary>
		/// y tếbảo hiểmkhaibáo cáotổng hợptổngxóaphiếu
		/// </summary>
		/// <returns></returns>
		public IActionResult YBSBHZQDQuery()
		{
			ReportingServiceCom();

			return View();
		}

		/// <summary>
		/// y tếbảo hiểmkhaibáo cáotrênchuyểnchứccông
		/// </summary>
		/// <returns></returns>
		public IActionResult YBSBSC_ZGQuery()
		{
			ReportingServiceCom();

			return View();
		}


		/// <summary>
		/// y tếbảo hiểmkhaibáo cáotrênchuyểncư trúdân
		/// </summary>
		/// <returns></returns>
		public IActionResult YBSBSC_JMQuery()
		{
			ReportingServiceCom();

			return View();
		}

		/// <summary>
		/// y tếbảo hiểmkhaibáo cáolẫn nhautrợgiúpkhó
		/// </summary>
		/// <returns></returns>
		public IActionResult YBSBSC_HZBKQuery()
		{
			ReportingServiceCom();

			return View();
		}

		/// <summary>
		/// y tếbảo hiểmkhaibáo cáokhácđịa
		/// </summary>
		/// <returns></returns>
		public IActionResult YBSBSC_YDQuery()
		{
			ReportingServiceCom();

			return View();
		}

		/// <summary>
		/// y tếbảo hiểmkhaibáo cáodẫnravănhạngdbfĐịnh dạng vàtrảquaydướitảivănhạngđịađịa chỉ tạitrướcđầutrangmặtvàothực hiệndướitảibậtra
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
			string filepath = SettPathHelper.MapPath("~/医保申报上传文件/" + pathname);
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
			//đượclấysốdữ liệu xétđoạnlàkhôngcósốdữ liệucầncầndẫnradướitải
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

			#region  dbf TệpFileStreamđọc lấy không đến Tệp Chưa tìm thấy lý do hiện Sử dụngGhép địa chỉ trước phía Tải xuống
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
			//return File(new FileStream(filepath, FileMode.OpenOrCreate), "application/octet-stream", pathname);//“text/plain”làvănhạngMIMELoại
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
        #region Lấy Khoản thu phí
        /// <summary>
        /// Lấy tên loại lớn
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
        #endregion BHYT trênbáo cáo báo cáo
        #region
        /// <summary>
        /// tổngtrangmặt
        /// </summary>
        /// <returns></returns>
        public IActionResult YBXXQueryUpload()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// nộpdễghighikho
        /// </summary>
        /// <returns></returns>
        public IActionResult JYJLQueryUpload()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// ngoại trúcấp cứukhámlớnbệnhtreosốkho
        /// </summary>
        /// <returns></returns>
        public IActionResult DBGHQueryUpload()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// ngoại trúcấp cứukhámnhàgiườngthuphíkho
        /// </summary>
        /// <returns></returns>
        public IActionResult JCSFQueryUpload()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// đượclấynộpdễghighithông tintin
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
        /// đượclấyngoại trúcấp cứukhámlớnbệnhtreosốkho
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
        /// đượclấyngoại trúcấp cứukhámlớnbệnhthêmgiườngthuphísốdữ liệu
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
