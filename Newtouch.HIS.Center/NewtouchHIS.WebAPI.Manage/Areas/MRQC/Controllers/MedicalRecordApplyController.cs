using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Domain.IDomainService.MRQC;
using NewtouchHIS.Domain.InterfaceObjets.MRQC;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;

namespace NewtouchHIS.WebAPI.Manage.Areas.MRQC.Controllers
{
    /// <summary>
    /// 病历Đăng ký
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordApplyController : ApiBaseController
    {
        private readonly IMedicalRecordApplyDmnService _medicalrecordapplyDmn;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="mapper"></param>
        /// <param name="medicalrecordapplyDmn"></param>
        public MedicalRecordApplyController(IHttpClientHelper httpClient,
           IMedicalRecordApplyDmnService medicalrecordapplyDmn)
           : base(httpClient)
        {
            _medicalrecordapplyDmn = medicalrecordapplyDmn;
        }

        /// <summary>
        /// 质控病历Đăng ký
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMedicalApplyRecord")]
        public async Task<BusResult<MedicalBlApplyResponse>> AddMedicalApplyRecordAsync(Request<MedicalBlApplyVo> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<MedicalBlApplyResponse> { code = ResponseResultCode.FAIL, msg = "Tổ chứcId là bắt buộc" };
            }
            request.Data.OrganizeId = request.OrganizeId;
            var data = await _medicalrecordapplyDmn.AddMedicalApplyRecord(request.Data);
            if (data==null)
            {
                return new BusResult<MedicalBlApplyResponse> { code = ResponseResultCode.FAIL, msg = "Đăng ký bệnh án kiểm soát chất lượng thất bại，Vui lòng đăng ký lại" };
            }
            return new BusResult<MedicalBlApplyResponse> { code = ResponseResultCode.SUCCESS, msg = "Thêm thành công", Data = data };

        }
    }
}
