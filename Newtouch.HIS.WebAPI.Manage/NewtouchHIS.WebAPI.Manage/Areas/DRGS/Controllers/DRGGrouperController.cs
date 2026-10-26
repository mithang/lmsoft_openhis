using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.DRG;
using NewtouchHIS.Lib.Services.DrgGroup;

namespace NewtouchHIS.WebAPI.Manage.Areas.DRGS.Controllers
{
    /// <summary>
    /// DRGgiao diện bộ nhóm
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
        /// truy vấn qua thông tin bệnh ánDRGtình hình nhóm
        /// tham số đầu vào làString，định dạng như"22058878,2,88,32460,,13040503,94,1,K80.302|K80.305|K83.109|K72.905|Z90.408|E14.900x001,51.8803|51.8701|54.5100x005|45.1301"
        /// sẽMedicalRecordlớp của11một thuộc tính nối bằng dấu phẩy，trong đó：zdList、ssListkiểu của làString[]，nhiều phần tử dùng|phân tách；remarktrường có thể chọn
        /// </summary>
        /// <returns>GroupResult</returns>
        [HttpPost]
        [Route("DrgGroupByMedRecord")]
        public async Task<BusResult<DrgGroupResult>> GroupRecord(RequestBus<DrgAreaMedicalRecord> request)
        {
            if (request == null || request.Data == null)
            {
                return new BusResult<DrgGroupResult> { code = ResponseResultCode.FAIL, msg = "关键病案信息不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.version))
            {
                request.Data.version = ConfigInitHelper.SysConfig.DrgGroupVersion ?? "chs_drg_11";
            }
            return await _drgGroupService.GroupMedRecordAsync(request.Data);

        }
        /// <summary>
        /// truy vấn hàng loạtDRGtình hình nhóm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DrgGroupByMedRecordList")]
        public async Task<BusResult<List<DrgGroupResult>>> GroupRecordList(RequestBus<DrgAreaMedicalRecordS> request)
        {
            if (request == null || request.Data == null || (request.Data.medicalArr.Length == 0 && request.Data.medicalList.Count == 0))
            {
                return new BusResult<List<DrgGroupResult>> { code = ResponseResultCode.FAIL, msg = "关键病案信息不可为空，medicalArr与medicalList至少一项不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.version))
            {
                request.Data.version = ConfigInitHelper.SysConfig.DrgGroupVersion ?? "chs_drg_11";
            }
            return await _drgGroupService.GroupMedRecordAsync(request.Data);

        }
        /// <summary>
        /// truy vấn hàng loạt tình hình nhóm theo tệp bệnh án
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DrgGroupByXls")]
        public async Task<BusResult<DrgGroupFileResult>> GroupFileRecord(RequestBus<DrgAreaFileInfo> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.Data.FileName))
            {
                return new BusResult<DrgGroupFileResult> { code = ResponseResultCode.FAIL, msg = "文件信息不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.version))
            {
                request.Data.version = ConfigInitHelper.SysConfig.DrgGroupVersion ?? "chs_drg_11";
            }
            return await _drgGroupService.GroupFileRecordAsync(request.Data);

        }
    }
}
