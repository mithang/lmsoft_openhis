using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.CIS.API.Envelope;
using Newtouch.CIS.API.Requests;
using Newtouch.CIS.API.Services;
using Newtouch.Domain.IDomainServices.Inpatient;

namespace Newtouch.CIS.API.Controllers
{
    /// <summary>
    /// Port của HospitalizedBusinessController (api/Hospitalized).
    /// SỬA BUG GỐC: _deptMedicineApplyNoDmnService ở bản Framework không bao giờ
    /// được gán (ctor chỉ nhận IComponentContext) — bản Core inject qua DI nên
    /// cả hai action cập nhật trạng thái đều chạy thật.
    /// Thông điệp lỗi gốc (tiếng Trung) đã dịch sang tiếng Việt; trường msg của
    /// DefaultResponse gốc được map sang sub_msg của envelope bản port.
    /// Hai action này GHI dữ liệu — code biên dịch và chạy thật nhưng KHÔNG live-test
    /// ghi trong task này (chính sách không ghi DB), chỉ live-test envelope/token.
    /// </summary>
    [ApiController]
    [Route("api/Hospitalized")]
    [DefaultAuthorize]
    public class HospitalizedBusinessController : ApiControllerBase
    {
        private readonly IDeptMedicineApplyNoDmnService _deptMedicineApplyNoDmnService;

        public HospitalizedBusinessController(TokenStore tokenStore,
            IDeptMedicineApplyNoDmnService deptMedicineApplyNoDmnService)
            : base(tokenStore)
        {
            _deptMedicineApplyNoDmnService = deptMedicineApplyNoDmnService;
        }

        [HttpPost("UpdateApplyNoStatus")]
        public IActionResult UpdateApplyNoStatus([FromBody] DeptApplyNoRequest dto)
        {
            var response = CommonExecute<DeptApplyNoRequest, DefaultResponse>((req, resp) =>
            {
                var valdata = _deptMedicineApplyNoDmnService.SelectApplyNoStatus(req.OrganizeId, req.SqdArray, req.UserCode);
                if (valdata.Count > 0)
                {
                    valdata = valdata.Where(p => p.sqzt != "1").ToList();
                    if (valdata.Count == 0)
                    {
                        var cnt = _deptMedicineApplyNoDmnService.UpdateApplyNoStatus(req.OrganizeId, req.SqdArray, req.UserCode);
                        resp.code = ResponseResultCode.SUCCESS;
                        if (cnt == 0)
                        {
                            resp.code = ResponseResultCode.FAIL;
                            resp.sub_msg = "Cập nhật trạng thái phiếu đề nghị thất bại, phiếu đề nghị không hợp lệ";
                        }
                    }
                    else
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.sub_msg = "Cập nhật trạng thái phiếu đề nghị thất bại, chỉ được trả lại phiếu đang ở trạng thái 'Đã đề nghị'!";
                    }
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_msg = "Không tìm thấy số phiếu đề nghị hợp lệ!";
                }
            }, dto);
            return CreateResponse(response);
        }

        [HttpPost("UpdateReturnApplyNoStatus")]
        public IActionResult UpdateReturnApplyNoStatus([FromBody] DeptApplyNoRequest dto)
        {
            var response = CommonExecute<DeptApplyNoRequest, DefaultResponse>((req, resp) =>
            {
                var valdata = _deptMedicineApplyNoDmnService.SelectKcthApplyNoStatus(req.OrganizeId, req.SqdArray, req.UserCode);
                if (valdata != null)
                {
                    if (valdata != "1")
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.sub_msg = "Cập nhật trạng thái phiếu đề nghị thất bại, chỉ được trả lại phiếu đang ở trạng thái 'Đã đề nghị'!";
                    }
                    else
                    {
                        var cnt = _deptMedicineApplyNoDmnService.UpdateReturnApplyNoStatus(req.OrganizeId, req.SqdArray, req.UserCode);
                        resp.code = ResponseResultCode.SUCCESS;
                        if (cnt == 0)
                        {
                            resp.code = ResponseResultCode.FAIL;
                            resp.sub_msg = "Cập nhật trạng thái phiếu đề nghị thất bại, phiếu đề nghị không hợp lệ";
                        }
                    }
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_msg = "Không tìm thấy số phiếu đề nghị này, phiếu đề nghị không hợp lệ!";
                }
            }, dto);
            return CreateResponse(response);
        }
    }
}
