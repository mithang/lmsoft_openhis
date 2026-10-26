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
    /// dịch vụ bệnh nhân ngoại trú
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
        /// danh sách đăng ký khám hiệu lực trong ngày
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutpRegTodayQuery")]
        [HttpPost]
        public async Task<ResponseBase> OutpRegTodayQueryAsync(RequestBus<OutpRegistQueryRequest> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data.kh) && string.IsNullOrWhiteSpace(request.Data.mzh)))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(kh/mzh)不可全部为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y027", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// danh sách đăng ký khám bệnh nhân
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutpRegQuery")]
        [HttpPost]
        public async Task<ResponseBase> OutpRegRecordsAsync(RequestBus<OutpRegistQueryRequest> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data.kh) && string.IsNullOrWhiteSpace(request.Data.mzh)))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(kh/mzh)不可全部为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{PatientAnonRoute}/OutpatRegRecord", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }

        /// <summary>
        /// hóa đơn chờ thanh toán ngoại trúbysố khám ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutpBillUnpaidByMzh")]
        [HttpPost]
        public async Task<ResponseBase> OutpBillUnpaidByMzhAsync(RequestBus<OutpRegistQueryRequest> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data.kh) || string.IsNullOrWhiteSpace(request.Data.mzh)))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(kh、mzh)不可为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y028", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// chi tiết hạng mục hóa đơn ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutpBillItemDetail")]
        [HttpPost]
        public async Task<ResponseBase> OutpBillItemDetailAsync(RequestBus<OutpPresInfoRequest> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data.mzh) || request.Data.PresIds == null))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(PresIds、mzh)不可为空" };
            }
            request.Data.PresId = string.Join(",", request.Data.PresIds);
            var apiReq = InitController.BuildBookRequest(request, "Y029", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data.Adapt<OutpPresInfoHisApiRequest>();
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// thanh toán hóa đơnstep1：tạo đơn hàng thanh toán
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("BillOrderCreate")]
        [HttpPost]
        [Obsolete("方法已过期，请使用PayOrder")]
        public async Task<ResponseBase> OutpBillOrderCreateAsync(RequestBus<OutpBillRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.Data.mzh) || string.IsNullOrWhiteSpace(request.Data.kh))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(kh、mzh)不可为空" };
            }

            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = new OrderApplyRequest { CardNo = request.Data.kh, VisitNo = request.Data.mzh, OrderType = (int)EnumBusType.mz };
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{OrderMethodRoute}/OrderCreate", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }

        /// <summary>
        /// thanh toán hóa đơnstep2：đăng ký khóa đơn hàng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("BillOrderLockApply")]
        [HttpPost]
        [Obsolete("方法已过期，请使用PayOrder")]
        public async Task<ResponseBase> BillOrderLockApplyAsync(RequestBus<OutpBillRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.Data.OrderNo) || request.Data.OrderAmt <= 0 || string.IsNullOrWhiteSpace(request.Data.kh))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(OrderNo、kh)不可为空" };
            }

            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = new OrderInfoBaseVO { CardNo = request.Data.kh, OrderNo = request.Data.OrderNo, OrderType = (int)EnumBusType.mz, OrderAmt = request.Data.OrderAmt };
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{OrderMethodRoute}/LockerApply", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// thanh toán hóa đơnstep3：thanh toán đơn hàng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("BillOrderPay")]
        [HttpPost]
        [Obsolete("方法已过期，请使用PayOrder")]
        public async Task<ResponseBase> BillOrderPayAsync(RequestBus<OrderPayRequest> request)
        {
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{OrderMethodRoute}/PayOrder", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// thanh toán hóa đơnsteptest：kiểm tra đẩy
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

        #region 挂号
        /// <summary>
        /// đăng ký khám theo hẹn
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("BookOutpatientRegist")]
        [HttpPost]
        public async Task<ResponseBase> BookOutpatientRegistAsync(RequestBus<OutpatientRegistRequest> request)
        {
            if (request.Data == null || (request.Data.kh == null && request.Data.BookId == null))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "预约挂号BookId&kh不可为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y024", "0").Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// đăng ký khám ngoại trú thông thường
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutpatientRegist")]
        [HttpPost]
        public async Task<ResponseBase> OutpatientRegistAsync(RequestBus<OutpatientRegistRequest> request)
        {
            if (request.Data == null || (request.Data.kh == null && request.Data.ScheduId == null))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "普通挂号ScheduId&kh不可为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y026", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        #endregion
    }
}
