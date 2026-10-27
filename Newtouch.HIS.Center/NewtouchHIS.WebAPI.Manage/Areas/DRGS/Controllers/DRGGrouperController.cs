using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.DRG;
using NewtouchHIS.Lib.Services.DrgGroup;

namespace NewtouchHIS.WebAPI.Manage.Areas.DRGS.Controllers
{
    /// <summary>
    /// DRGGiao diện bộ phân nhóm
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DRGGrouperController : ControllerBase
    {
        private readonly IDrgGroupService _drgGroupService;
        public DRGGrouperController(IDrgGroupService drgGroupService)
        {
            _drgGroupService = drgGroupService;
        }
        /// <summary>
        /// Truy vấn qua thông tin bệnh ánDRGTình hình phân nhóm
        /// Tham số đầu vào làString,Định dạng như"22058878,2,88,32460,,13040503,94,1,K80.302|K80.305|K83.109|K72.905|Z90.408|E14.900x001,51.8803|51.8701|54.5100x005|45.1301"
        /// GhépMedicalRecordcủa lớp11thuộc tính bằng dấu phẩy, trong đó,:zdList,ssListkiểu làString[],nhiều phần tử phân tách bằng|;remarktrường tùy chọn
        /// </summary>
        /// <returns>GroupResult</returns>
        [HttpPost]
        [Route("DrgGroupByMedRecord")]
        public async Task<BusResult<DrgGroupResult>> GroupRecord(RequestBus<DrgAreaMedicalRecord> request)
        {
            if (request == null || request.Data == null)
            {
                return new BusResult<DrgGroupResult> { code = ResponseResultCode.FAIL, msg = "Thông tin bệnh án quan trọng không được để trống" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.version))
            {
                request.Data.version = ConfigInitHelper.SysConfig.DrgGroupVersion ?? "chs_drg_11";
            }
            return await _drgGroupService.GroupMedRecordAsync(request.Data);

        }
        /// <summary>
        /// Truy vấn hàng loạtDRGTình hình phân nhóm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DrgGroupByMedRecordList")]
        public async Task<BusResult<List<DrgGroupResult>>> GroupRecordList(RequestBus<DrgAreaMedicalRecordS> request)
        {
            if (request == null || request.Data == null || (request.Data.medicalArr.Length == 0 && request.Data.medicalList.Count == 0))
            {
                return new BusResult<List<DrgGroupResult>> { code = ResponseResultCode.FAIL, msg = "Thông tin bệnh án quan trọng không được để trống,medicalArr và medicalListÍt nhất một mục không được để trống" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.version))
            {
                request.Data.version = ConfigInitHelper.SysConfig.DrgGroupVersion ?? "chs_drg_11";
            }
            return await _drgGroupService.GroupMedRecordAsync(request.Data);

        }
        /// <summary>
        /// Truy vấn hàng loạt tình hình phân nhóm theo tệp bệnh án
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DrgGroupByXls")]
        public async Task<BusResult<DrgGroupFileResult>> GroupFileRecord(RequestBus<DrgAreaFileInfo> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.Data.FileName))
            {
                return new BusResult<DrgGroupFileResult> { code = ResponseResultCode.FAIL, msg = "Thông tin tệp không được để trống" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.version))
            {
                request.Data.version = ConfigInitHelper.SysConfig.DrgGroupVersion ?? "chs_drg_11";
            }
            return await _drgGroupService.GroupFileRecordAsync(request.Data);

        }
    }
}
