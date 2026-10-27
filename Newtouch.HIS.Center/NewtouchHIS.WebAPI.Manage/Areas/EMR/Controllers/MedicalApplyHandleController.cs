using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Domain.IDomainService.EMR;
using NewtouchHIS.Domain.IDomainService.MRQC;
using NewtouchHIS.Domain.InterfaceObjets.EMR;
using NewtouchHIS.Domain.InterfaceObjets.MRQC;
using NewtouchHIS.DomainService.EMR;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;

namespace NewtouchHIS.WebAPI.Manage.Areas.EMR.Controllers
{
    /// <summary>
    /// Bệnh ánĐăng kýPhê duyệt
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalApplyHandleController : ApiBaseController
    {
        private readonly IMedicalApplyHandleDmnService _applyhandleDmn;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="applyhandleDmn"></param>
        public MedicalApplyHandleController(IHttpClientHelper httpClient, 
           IMedicalApplyHandleDmnService applyhandleDmn)
           : base(httpClient)
        {
            _applyhandleDmn = applyhandleDmn;
        }
        /// <summary>
        /// Đăng kýPhê duyệt/ Phê duyệt,Bác bỏ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyApplyApprove")]
        public async Task<BusResult<bool>> ModifyApplyApproveAsync(Request<MedicalApplyApproveVo> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Tổ chứcId là bắt buộc" };
            }
            var data = await _applyhandleDmn.ApplyApprove(request.Data,request.OrganizeId, DBEnum.EmrDb);

            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, Data = data };

        }
    }
}
