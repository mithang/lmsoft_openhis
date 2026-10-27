using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;
using NewtouchHIS.WebAPI.Manage.Models;
using NewtouchHIS.WebAPI.Manage.Models.Patient;
using static NewtouchHIS.Base.Domain.EnumExtend.BusEnum;

namespace NewtouchHIS.WebAPI.Manage.Areas.Sett.Controllers
{
    /// <summary>
    /// Quản lý thông tin bệnh nhân
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ApiBaseController
    {
        private readonly string BookingMethodRoute = "api/OutpBook/BookingESB";
        public PatientController(IHttpClientHelper httpClient) : base( httpClient)
        {
        }
        /// <summary>
        /// TheoHọ tên/Lấy thông tin bệnh nhân theo họ tên/số CMND
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetPatient")]
        [HttpPost]
        public async Task<ResponseBase> GetPatientAsync(RequestBus<SearchPatientRequest> request)
        {
            var apiReq = InitController.BuildBookRequest(request, "Y004", "0").Adapt<BookingRequest>();
            //var apiReq = _mapper.Map<BookingRequest>(InitController.BuildBookRequest(request, "Y004", "0"));
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// Tự chi trảBệnh nhânTự lập hồ sơ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("PatientSelfReg")]
        [HttpPost]
        public async Task<ResponseBase> PatientSelfRegAsync(RequestBus<PatientRegisterBasic> request)
        {
            var apiReq = InitController.BuildBookRequest(request, "Y004", "1").Adapt<BookingRequest>();
            //var apiReq = _mapper.Map<BookingRequest>(InitController.BuildBookRequest(request, "Y004", "1"));
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }

        /// <summary>
        /// Bệnh nhânTruy vấn thông tin nội trú của bệnh nhân:bySố nhập viện/Số thẻ/Họ tên
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("InHosPatientQuery")]
        [HttpPost]
        public async Task<ResponseBase> InHosPatientQueryAsync(RequestBus<SearchPatientRequest> request)
        {
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            request.Data!.ywlx = EnumBusType.zy.ToString();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{PatientAnonRoute}/InHosPatientQuery", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }

        #region LIS Báo cáo
        /// <summary>
        /// Danh sách báo cáo xét nghiệm nội trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutPatientLisReport")]
        [HttpPost]
        public async Task<ResponseBase> OutPatientLisReportAsync(RequestBus<LisReportMzRequest> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data!.mzh) && string.IsNullOrWhiteSpace(request.Data!.kh)))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "Thông tin quan trọng(Số ngoại trú/Số thẻ) không được để trống" };
            }
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            request.Data!.sqdzt = ((int)EnumReportStu.ywc).ToString();
            request.Data!.ywlx = ((int)EnumBusType.mz).ToJson();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{PatientAnonRoute}/PatientLisReport", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// Danh sách báo cáo xét nghiệm nội trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("InHosPatientLisReport")]
        [HttpPost]
        public async Task<ResponseBase> InHosPatientLisReportAsync(RequestBus<LisReportZyRequest> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data!.zyh) && string.IsNullOrWhiteSpace(request.Data!.kh)))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "Thông tin quan trọng(Số nhập viện/Số thẻ) không được để trống" };
            }
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            request.Data!.sqdzt = ((int)EnumReportStu.ywc).ToString();
            request.Data!.ywlx = ((int)EnumBusType.zy).ToJson();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{PatientAnonRoute}/PatientLisReport", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// Truy vấn chi tiết báo cáo xét nghiệm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("PatientLisReportDetail")]
        [HttpPost]
        public async Task<ResponseBase> PatientLisReportDetailAsync(RequestBus<LisReportMzRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.Data!.sqdh) || string.IsNullOrWhiteSpace(request.Data!.ywlx))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "Thông tin quan trọng(sqdh,ywlx) không được để trống" };
            }
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            request.Data!.sqdzt = EnumReportStu.ywc.ToString();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{PatientAnonRoute}/PatientLisReportDetail", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        #endregion
    }
}
