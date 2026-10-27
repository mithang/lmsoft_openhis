using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Domain.Entity.PatientCenter;
using NewtouchHIS.Domain.IDomainService.PatientCenter;
using NewtouchHIS.Domain.InterfaceObjets;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Domain.InterfaceObjets.PatientCenter;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.WebAPI.Manage.Areas.PatientCenter.Controllers
{
    /// <summary>
    /// Bệnh nhân基础信息管理(自费优先)
    /// </summary>
    [Route("api/patient")]
    [ApiController]
    public class PatBasicInfoController : ControllerBase
    {
        public readonly IPatientInfoDmnService _patientInfoDmn;
        public readonly IPatientAddressDmnService _patientAddressDmn;
        public PatBasicInfoController(IPatientInfoDmnService patientInfoDmn, IPatientAddressDmnService patientAddressDmn)
        {
            _patientInfoDmn = patientInfoDmn;
            _patientAddressDmn = patientAddressDmn;
        }
        /// <summary>
        /// Bệnh nhân基本信息查询 
        /// 自费优先
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List</returns>
        [HttpPost]
        [Route("PatientQuerySelfPay")]
        public async Task<BusResult<SysPatInfoVO>> PatientQueryAsync(Request<SysPatIndexVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<SysPatInfoVO> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và bệnh nhân không được để trống" };
            }
            request.Data.OrganizeId = request.OrganizeId;
            var pat = await _patientInfoDmn.PatientQuery(request.Data, (int)Domain.Enum.HisEnum.EnumBrxz.zf);
            if (pat == null)
            {
                return new BusResult<SysPatInfoVO> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy thông tin bệnh nhân liên quan" };
            }
            return new BusResult<SysPatInfoVO> { code = ResponseResultCode.SUCCESS, Data = pat };
        }

        /// <summary>
        /// Bệnh nhân地址信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientAddressQuery")]
        public async Task<BusResult<SysPatientAddressEntity>> PatientAddressQuery(Request<SysPatientAddressIndexVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và bệnh nhân không được để trống" };
            }
            request.Data.OrganizeId = request.OrganizeId;
            var pat = await _patientAddressDmn.PatientAddressQuery(request.Data.patid, request.OrganizeId);
            if (pat == null)
            {
                return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy thông tin bệnh nhân liên quan" };
            }
            return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.SUCCESS, Data = pat };
        }

        /// <summary>
        /// Bệnh nhân地址信息更新
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientAddressUpdate")]
        public async Task<BusResult<bool>> PatientAddressUpdate(Request<SysPatientAddressInfoVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và thông tin đăng ký quan trọng không được để trống" };
            }
            if (request.Data.patid==0)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "patId không được để trống" };
            }
            SysPatientAddressEntity entity = request.Data.Adapt<SysPatientAddressEntity>();
            var response = await _patientAddressDmn.PatientAddressUpdate(entity, request.AppId);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "Cập nhật thông tin địa chỉ bệnh nhân thành công", Data = response };
        }

        /// <summary>
        /// Bệnh nhân地址信息添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientAddressAdd")]
        public async Task<BusResult<bool>> PatientAddressAdd(Request<SysPatientAddressInfoVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và thông tin đăng ký quan trọng không được để trống" };
            }
            if (request.Data.patid == 0)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "patId không được để trống" };
            }
            SysPatientAddressEntity entity = request.Data.Adapt<SysPatientAddressEntity>();
            var response = await _patientAddressDmn.PatientAddressAdd(entity, request.AppId);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "Thêm thông tin địa chỉ bệnh nhân thành công", Data = true };
        }

        /// <summary>
        /// Bệnh nhân地址信息删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientAddressDelete")]
        public async Task<BusResult<bool>> PatientAddressDelete(Request<SysPatientAddressInfoVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và thông tin đăng ký quan trọng không được để trống" };
            }
            if (request.Data.patid == 0)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "patId không được để trống" };
            }
            SysPatientAddressEntity entity = request.Data.Adapt<SysPatientAddressEntity>();
            var response = await _patientAddressDmn.PatientAddressDelete(entity, request.AppId);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "Xóa thông tin địa chỉ bệnh nhân thành công", Data = response };
        }

        /// <summary>
        /// 获取订单收件人信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientOrderAddressQuery")]
        public async Task<BusResult<SysPatientAddressEntity>> PatientOrderAddressQuery(Request<SysPatientOrderAddressIndexVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và bệnh nhân không được để trống" };
            }
            if (request.Data == null || string.IsNullOrWhiteSpace(request.Data.visitNo))
            {
                return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.FAIL, msg = "Số ngoại trú không được để trống" };
            }
            if ( request.Data.ks!= "00000080")
            {
                return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.FAIL, msg = "Đơn hàng này không phải khám chữa bệnh từ xa" };
            }
            request.Data.OrganizeId = request.OrganizeId;
            var pat = await _patientAddressDmn.PatientOrderAddressQuery(request.Data.visitNo,request.Data.ks,request.OrganizeId);
            if (pat == null)
            {
                return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy thông tin bệnh nhân liên quan" };
            }
            return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.SUCCESS, Data = pat };
        }


    }
}
