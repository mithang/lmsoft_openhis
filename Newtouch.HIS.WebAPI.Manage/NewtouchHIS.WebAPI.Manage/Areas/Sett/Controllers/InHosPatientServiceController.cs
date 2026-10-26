using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;
using NewtouchHIS.WebAPI.Manage.Models;
using NewtouchHIS.WebAPI.Manage.Models.InHosPatient;
using NewtouchHIS.Lib.Base.Extension;
using Mapster;

namespace NewtouchHIS.WebAPI.Manage.Areas.Sett.Controllers
{
    /// <summary>
    /// dịch vụ bệnh nhân nội trú
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InHosPatientServiceController : ApiBaseController
    {        
        public InHosPatientServiceController(IHttpClientHelper httpClient) : base( httpClient)
        {

        }

        /// <summary>
        /// hóa đơn phân loại chờ thanh toán của bệnh nhân nội trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("InHosFeeTypeBillUnpaid")]
        [HttpPost]
        public async Task<ResponseBase> InHosFeeTypeBillUnpaidAsync(RequestBus<InHosPatBillRequest> request)
        {
            if (request.Data == null || (string.IsNullOrWhiteSpace(request.Data.zyh)&& string.IsNullOrWhiteSpace(request.Data.kh)&& string.IsNullOrWhiteSpace(request.Data.zjh)))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息不可为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, null, null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{PatientAnonRoute}/InHosBillUnpaid", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        
    }

}
