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
    /// quản lý thông tin bệnh nhân
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
        /// theo họ tên/lấy thông tin bệnh nhân theo số chứng minh nhân dân
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
        /// bệnh nhân tự trả tự lập hồ sơ
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
        /// truy vấn thông tin nội trú bệnh nhân：bysố nội trú/số thẻ/họ tên
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

        #region LIS 报告
        /// <summary>
        /// danh sách báo cáo xét nghiệm nội trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutPatientLisReport")]
        [HttpPost]
        public async Task<ResponseBase> OutPatientLisReportAsync(RequestBus<LisReportMzRequest> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data!.mzh) && string.IsNullOrWhiteSpace(request.Data!.kh)))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(门诊号/卡号)不可为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            request.Data!.sqdzt = ((int)EnumReportStu.ywc).ToString();
            request.Data!.ywlx = ((int)EnumBusType.mz).ToJson();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{PatientAnonRoute}/PatientLisReport", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// danh sách báo cáo xét nghiệm nội trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("InHosPatientLisReport")]
        [HttpPost]
        public async Task<ResponseBase> InHosPatientLisReportAsync(RequestBus<LisReportZyRequest> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data!.zyh) && string.IsNullOrWhiteSpace(request.Data!.kh)))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(住院号/卡号)不可为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            request.Data!.sqdzt = ((int)EnumReportStu.ywc).ToString();
            request.Data!.ywlx = ((int)EnumBusType.zy).ToJson();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{PatientAnonRoute}/PatientLisReport", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// truy vấn chi tiết báo cáo xét nghiệm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("PatientLisReportDetail")]
        [HttpPost]
        public async Task<ResponseBase> PatientLisReportDetailAsync(RequestBus<LisReportMzRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.Data!.sqdh) || string.IsNullOrWhiteSpace(request.Data!.ywlx))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(sqdh、ywlx)不可为空" };
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
