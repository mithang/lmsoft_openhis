using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.WebAPI.Manage.Models.EMR;
using NewtouchHIS.Domain.IDomainService.EMR;
using NewtouchHIS.Domain.InterfaceObjets;
using NewtouchHIS.Lib.Base.Model.DRG;

namespace NewtouchHIS.WebAPI.Manage.Areas.EMR.Controllers
{
    /// <summary>
    /// trang đầu bệnh án
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalHomeController : ApiBaseController
    {
        private readonly IMedicalHomeRDmnService _medicalHomeRDmn;
        public MedicalHomeController(IHttpClientHelper httpClient, IMedicalHomeRDmnService medicalHomeRDmn) : base(httpClient)
        {
            _medicalHomeRDmn = medicalHomeRDmn;
        }

        /// <summary>
        /// truy vấn thông tin trang đầu bệnh án
        /// truy vấn thông tin trọng điểm trang đầu bệnh án bệnh nhân（with：chẩn đoán、phẫu thuật）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("MedicalHomeRecord")]
        [HttpPost]
        public async Task<BusResult<MedicalHomeVO>> MedicalHomeRecordAsync(RequestBus<MedicalHomeRecordRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<MedicalHomeVO> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            if (request == null || request.Data == null || (string.IsNullOrWhiteSpace(request.Data.zyh) && string.IsNullOrWhiteSpace(request.Data.orgId)))
            {
                return new BusResult<MedicalHomeVO> { code = ResponseResultCode.FAIL, msg = "请录入查询条件（住院号/机构编码）" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.orgId))
            {
                request.Data.orgId = request.OrganizeId;
            }
            var data = await _medicalHomeRDmn.MedicalHomeQuery(request.Data.zyh, request.Data.orgId, DBEnum.MrmsDb);
            return new BusResult<MedicalHomeVO> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        /// <summary>
        /// thông tin bệnh án bệnh nhân(DRGđịnh dạng nhóm）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("MedicalHomeRecordFormtDrg")]
        [HttpPost]
        public async Task<BusResult<DrgMedicalRecord>> MedicalHomeRecordFormtDrgAsync(RequestBus<MedicalHomeRecordRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<DrgMedicalRecord> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            if (request == null || request.Data == null || (string.IsNullOrWhiteSpace(request.Data.zyh) && string.IsNullOrWhiteSpace(request.Data.orgId)))
            {
                return new BusResult<DrgMedicalRecord> { code = ResponseResultCode.FAIL, msg = "请录入查询条件（住院号/机构编码）" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.orgId))
            {
                request.Data.orgId = request.OrganizeId;
            }
            var data = await _medicalHomeRDmn.MedicalHomeRecordFormtDrg(request.Data.zyh, request.Data.orgId, DBEnum.EmrDb);
            return new BusResult<DrgMedicalRecord> { code = ResponseResultCode.SUCCESS, Data = data };
        }
    }
}
