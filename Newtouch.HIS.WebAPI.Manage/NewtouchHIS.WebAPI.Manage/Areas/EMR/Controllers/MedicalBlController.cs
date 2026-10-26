using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Domain.IDomainService.EMR;
using NewtouchHIS.Domain.InterfaceObjets.EMR;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;
using NewtouchHIS.WebAPI.Manage.Models.EMR;

namespace NewtouchHIS.WebAPI.Manage.Areas.EMR.Controllers
{
    /// <summary>
    /// giao diện mẫu loại bệnh án：lời gọi kiểm soát chất lượng
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalBlController : ApiBaseController
    {
        private readonly IMedicalBlDmnService _medicalblDmn;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="medicalblDmn"></param>
        public MedicalBlController(IHttpClientHelper httpClient, IMedicalBlDmnService medicalblDmn) : base(httpClient)
        {
            _medicalblDmn = medicalblDmn;
        }
        /// <summary>
        /// loại bệnh án
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("MedicalbllxRecord")]
        [HttpPost]
        public async Task<BusResult<MedicalBllxItemVo>> MedicalbllxRecordAsync(RequestBus<MedicalTypeItemRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<MedicalBllxItemVo> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            var data = await _medicalblDmn.MedicalbllxRecord(request.OrganizeId, DBEnum.EmrDb);
            return new BusResult<MedicalBllxItemVo> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        /// <summary>
        /// mẫu bệnh án
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("MedicalblmbRecord")]
        [HttpPost]
        public async Task<BusResult<MedicalBlMbItemVo>> MedicalblmbRecordgAsync(RequestBus<MedicalTypeItemRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<MedicalBlMbItemVo> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            var data = await _medicalblDmn.MedicalblmbRecord(request.OrganizeId, DBEnum.EmrDb);
            return new BusResult<MedicalBlMbItemVo> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        /// <summary>
        /// cây mẫu loại bệnh án
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("MedicalbllxTreeRecord")]
        [HttpPost]
        public async Task<BusResult<MedicalBllxMbTreeVo>> MedicalblmbTreeRecordgAsync(RequestBus<MedicalTypeItemRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<MedicalBllxMbTreeVo> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            var data = await _medicalblDmn.MedicalbllxTreeRecord(request.OrganizeId, DBEnum.EmrDb);
            return new BusResult<MedicalBllxMbTreeVo> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        /// <summary>
        /// danh sách bệnh nhân nội trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("MedicalCenterPatInfo")]
        [HttpPost]
        public async Task<BusResult<List<MedicalPatientVo>>> MedicalCenterPatInfo(RequestBus<MedicalPatientRequest> request)
        {
            if (request == null || request.Data == null)
            {
                return new BusResult<List<MedicalPatientVo>> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<MedicalPatientVo>> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.ksrq.ToString()))
            {
                return new BusResult<List<MedicalPatientVo>> { code = ResponseResultCode.FAIL, msg = "日期开始时间不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.jsrq.ToString()))
            {
                return new BusResult<List<MedicalPatientVo>> { code = ResponseResultCode.FAIL, msg = "日期结束时间不可为空" };
            }
            var data = await _medicalblDmn.MedicalCenterPatInfo(request.Data.brbz, request.Data.ksrq, request.Data.jsrq, request.Data.srz, request.OrganizeId, DBEnum.EmrDb);
            return new BusResult<List<MedicalPatientVo>> { code = ResponseResultCode.SUCCESS, Data = data };
        }

        /// <summary>
        /// danh sách chẩn đoán
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("MedicalCenterDiaglist")]
        [HttpPost]
        public async Task<BusResult<List<MedDiagListVo>>> MedicalCenterDiaglistAsync(RequestBus<MedicalHomeRequest> request)
        {
            if (request == null || request.Data == null)
            {
                return new BusResult<List<MedDiagListVo>> { code = ResponseResultCode.FAIL, msg = "机构编码/住院号不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<MedDiagListVo>> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.zyh))
            {
                return new BusResult<List<MedDiagListVo>> { code = ResponseResultCode.FAIL, msg = "住院号不可为空" };
            }
            var data = await _medicalblDmn.MedicalCenterDiaglist(request.Data.zyh, request.OrganizeId, DBEnum.EmrDb);
            return new BusResult<List<MedDiagListVo>> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        /// <summary>
        /// văn bản bệnh ántree 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("MedicalCenterBlwsTree")]
        [HttpPost]
        public async Task<BusResult<List<MedRecordTree>>> MedicalCenterBlwsTreeAsync(RequestBus<MedicalHomeRequest> request)
        {
            if (request == null || request.Data == null)
            {
                return new BusResult<List<MedRecordTree>> { code = ResponseResultCode.FAIL, msg = "机构编码/住院号不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<MedRecordTree>> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.zyh))
            {
                return new BusResult<List<MedRecordTree>> { code = ResponseResultCode.FAIL, msg = "住院号不可为空" };
            }
            var data = await _medicalblDmn.MedicalCenterBlwsTree(request.Data.zyh, request.OrganizeId, DBEnum.EmrDb);
            return new BusResult<List<MedRecordTree>> { code = ResponseResultCode.SUCCESS, Data = data };
        }
    }
}
