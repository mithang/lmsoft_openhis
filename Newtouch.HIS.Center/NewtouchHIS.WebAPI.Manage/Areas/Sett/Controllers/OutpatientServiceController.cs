using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;
using NewtouchHIS.WebAPI.Manage.Models;
using NewtouchHIS.WebAPI.Manage.Models.OrderCenter;
using NewtouchHIS.WebAPI.Manage.Models.OutPatient;
using static NewtouchHIS.Base.Domain.EnumExtend.BusEnum;

namespace NewtouchHIS.WebAPI.Manage.Areas.Sett.Controllers
{
    /// <summary>
    /// Ngoại trúBệnh nhânDịch vụ
    /// </summary>
    [Route("api/Outpatient")]
    [ApiController]
    public class OutpatientServiceController : ApiBaseController
    {
        private readonly string BookingMethodRoute = "api/OutpBook/BookingESB";
        //private readonly string OrderMethodRoute = "api/OrderCenter";
        public OutpatientServiceController(IHttpClientHelper httpClient) : base(httpClient)
        {

        }
        /// <summary>
        /// Danh sách đăng ký khám hiệu lực trong ngày
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutpRegTodayQuery")]
        [HttpPost]
        public async Task<ResponseBase> OutpRegTodayQueryAsync(RequestBus<OutpRegistQueryRequest> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data.kh) && string.IsNullOrWhiteSpace(request.Data.mzh)))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "Thông tin quan trọng(kh/mzh) không được để trống tất cả" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y027", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// Bệnh nhânDanh sách đăng ký khám
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutpRegQuery")]
        [HttpPost]
        public async Task<ResponseBase> OutpRegRecordsAsync(RequestBus<OutpRegistQueryRequest> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data.kh) && string.IsNullOrWhiteSpace(request.Data.mzh)))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "Thông tin quan trọng(kh/mzh) không được để trống tất cả" };
            }
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{PatientAnonRoute}/OutpatRegRecord", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }

        /// <summary>
        /// Hóa đơn ngoại trú chờ thanh toánbySố ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutpBillUnpaidByMzh")]
        [HttpPost]
        public async Task<ResponseBase> OutpBillUnpaidByMzhAsync(RequestBus<OutpRegistQueryRequest> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data.kh) || string.IsNullOrWhiteSpace(request.Data.mzh)))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "Thông tin quan trọng(kh,mzh) không được để trống" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y028", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// Chi tiết hạng mục hóa đơn ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutpBillItemDetail")]
        [HttpPost]
        public async Task<ResponseBase> OutpBillItemDetailAsync(RequestBus<OutpPresInfoRequest> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data.mzh) || request.Data.PresIds == null))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "Thông tin quan trọng(PresIds,mzh) không được để trống" };
            }
            request.Data.PresId = string.Join(",", request.Data.PresIds);
            var apiReq = InitController.BuildBookRequest(request, "Y029", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data.Adapt<OutpPresInfoHisApiRequest>();
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// Thanh toán hóa đơnstep1:Tạo đơn hàng thanh toán
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("BillOrderCreate")]
        [HttpPost]
        [Obsolete("Phương thức đã hết hạn,Vui lòng dùngPayOrder")]
        public async Task<ResponseBase> OutpBillOrderCreateAsync(RequestBus<OutpBillRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.Data.mzh) || string.IsNullOrWhiteSpace(request.Data.kh))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "Thông tin quan trọng(kh,mzh) không được để trống" };
            }

            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = new OrderApplyRequest { CardNo = request.Data.kh, VisitNo = request.Data.mzh, OrderType = (int)EnumBusType.mz };
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{OrderMethodRoute}/OrderCreate", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }

        /// <summary>
        /// Thanh toán hóa đơnstep2:Đăng kýKhóa đơn hàng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("BillOrderLockApply")]
        [HttpPost]
        [Obsolete("Phương thức đã hết hạn,Vui lòng dùngPayOrder")]
        public async Task<ResponseBase> BillOrderLockApplyAsync(RequestBus<OutpBillRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.Data.OrderNo) || request.Data.OrderAmt <= 0 || string.IsNullOrWhiteSpace(request.Data.kh))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "Thông tin quan trọng(OrderNo,kh) không được để trống" };
            }

            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = new OrderInfoBaseVO { CardNo = request.Data.kh, OrderNo = request.Data.OrderNo, OrderType = (int)EnumBusType.mz, OrderAmt = request.Data.OrderAmt };
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{OrderMethodRoute}/LockerApply", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// Thanh toán hóa đơnstep3:Quyết toán đơn hàng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("BillOrderPay")]
        [HttpPost]
        [Obsolete("Phương thức đã hết hạn,Vui lòng dùngPayOrder")]
        public async Task<ResponseBase> BillOrderPayAsync(RequestBus<OrderPayRequest> request)
        {
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{OrderMethodRoute}/PayOrder", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// Thanh toán hóa đơnsteptest:Đẩy kiểm thử
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("PayPushTest")]
        [HttpPost]
        public async Task<ResponseBase> BillOrderPayPushAsync(RequestBus<OrderPayRequest> request)
        {
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{OrderMethodRoute}/test", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }

        #region Đăng ký khám
        /// <summary>
        /// Đặt lịch đăng ký khám
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("BookOutpatientRegist")]
        [HttpPost]
        public async Task<ResponseBase> BookOutpatientRegistAsync(RequestBus<OutpatientRegistRequest> request)
        {
            if (request.Data == null || (request.Data.kh == null && request.Data.BookId == null))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "Đặt lịch đăng ký khámBookId&kh không được để trống" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y024", "0").Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// Đăng ký khám ngoại trú thông thường
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutpatientRegist")]
        [HttpPost]
        public async Task<ResponseBase> OutpatientRegistAsync(RequestBus<OutpatientRegistRequest> request)
        {
            if (request.Data == null || (request.Data.kh == null && request.Data.ScheduId == null))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "Đăng ký khám thườngScheduId&kh không được để trống" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y026", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        #endregion
    }
}
