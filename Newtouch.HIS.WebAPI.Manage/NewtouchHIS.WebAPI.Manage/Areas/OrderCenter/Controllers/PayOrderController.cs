using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Domain.Entity.PatientCenter;
using NewtouchHIS.Domain.IDomainService.PatientCenter;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;
using NewtouchHIS.WebAPI.Manage.Models;
using NewtouchHIS.WebAPI.Manage.Models.InHosPatient;
using NewtouchHIS.WebAPI.Manage.Models.OrderCenter;
using static NewtouchHIS.Base.Domain.EnumExtend.BusEnum;

namespace NewtouchHIS.WebAPI.Manage.Areas.OrderCenter.Controllers
{
    /// <summary>
    /// trung tâm đơn hàng                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PayOrderController : ApiBaseController
    {
        public readonly IPatientAddressDmnService _patientAddressDmn;
        //private readonly string OrderMethodRoute = "api/OrderCenter";
        public PayOrderController(IHttpClientHelper httpClient, IPatientAddressDmnService patientAddressDmnService) : base(httpClient)
        {
            _patientAddressDmn = patientAddressDmnService;

        }

        /// <summary>
        /// truy vấn thông tin đơn hàng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OrderQuery")]
        [HttpPost]
        public async Task<ResponseBase> OrderQueryAsync(RequestBus<OrderInfoBaseVO> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data.OrderNo) && string.IsNullOrWhiteSpace(request.Data.CardNo) && string.IsNullOrWhiteSpace(request.Data.VisitNo)))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(OrderNo、CardNo、VisitNo)至少一项不可为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{OrderMethodRoute}/OrderQuery", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// truy vấn chi tiết đơn hàng ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OrderDetail")]
        [HttpPost]
        public async Task<ResponseBase> OrderDetailAsync(RequestBus<OrderInfoBaseVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.Data.OrderNo))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(OrderNo)不可为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{OrderMethodRoute}/OrderDetailQuery", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }

        /// <summary>
        /// thanh toán hóa đơnstep1：tạo đơn hàng thanh toán
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("BillOrderCreate")]
        [HttpPost]
        public async Task<ResponseBase> BillOrderCreateAsync(RequestBus<PatBillRequest> request)
        {
            if (request.Data == null || request.Data.OrderType == null || string.IsNullOrWhiteSpace(request.Data.kh))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(kh、OrderType)不可为空" };
            }
            else if (request.Data.OrderType == (int)EnumBusType.mz && string.IsNullOrWhiteSpace(request.Data.mzh))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(mzh)不可为空" };
            }
            else if (request.Data.OrderType == (int)EnumBusType.zy && string.IsNullOrWhiteSpace(request.Data.zyh))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(zyh)不可为空" };
            }
            else if (request.Data.OrderType != (int)EnumBusType.mz && request.Data.OrderType != (int)EnumBusType.zy)
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "订单类型异常" };
            }
            else if (request.OrganizeId == null)
            {

                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(组织机构)不可为空" };
            }

            //tạo đơn hàng
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            var visitNo = request.Data.OrderType == (int)EnumBusType.mz ? request.Data.mzh : request.Data.zyh;
            apiReq.paradata = new OrderApplyRequest
            {
                CardNo = request.Data.kh,
                VisitNo = visitNo ?? "",
                OrderType = request.Data.OrderType,
            };
            //thông tin địa chỉ nhận của bệnh nhân（không cần thiết）
            if (request.Data.addressInfo != null)
            {
                var address = request.Data.addressInfo.Adapt<SysPatientAddressEntity>();
                address.patid = request.Data.addressInfo.patid;
                address.OrganizeId = request.OrganizeId;

                var addressResponse = await _patientAddressDmn.PatientAddressAdd(address, request.AppId);
                apiReq.paradata = new OrderApplyRequest
                {
                    CardNo = request.Data.kh,
                    VisitNo = visitNo ?? "",
                    OrderType = request.Data.OrderType,
                    patId = addressResponse.patid,
                    dzId = addressResponse.Id
                };
            }

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
        public async Task<ResponseBase> BillOrderLockApplyAsync(RequestBus<InHosPatBillRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.Data.OrderNo) || request.Data.OrderAmt <= 0 || string.IsNullOrWhiteSpace(request.Data.kh))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(OrderNo、kh)不可为空" };
            }

            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = new OrderInfoBaseVO { CardNo = request.Data.kh, OrderNo = request.Data.OrderNo, OrderType = (int)EnumBusType.zy, OrderAmt = request.Data.OrderAmt };
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
        public async Task<ResponseBase> BillOrderPayAsync(RequestBus<OrderPayRequest> request)
        {
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            ResponseBaseOld resp = new ResponseBaseOld();
            if (request.Data!.OrderType == (int)EnumBusType.mz)
            {
                resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{OrderMethodRoute}/PayOutpOrder", apiReq.ToJson());
            }
            else if (request.Data!.OrderType == (int)EnumBusType.zy)
            {
                resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{OrderMethodRoute}/PayInHosOrder", apiReq.ToJson());
            }
            else
            {
                resp.code = ResponseResultCode.ERROR;
                resp.msg = "订单类型异常";
            }
            return InitController.BuildResponseBase(resp);
        }
    }
}
