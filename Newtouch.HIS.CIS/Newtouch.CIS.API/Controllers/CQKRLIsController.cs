using Microsoft.AspNetCore.Mvc;
using Newtouch.CIS.API.Envelope;
using Newtouch.CIS.API.Requests;
using Newtouch.CIS.API.Services;

namespace Newtouch.CIS.API.Controllers
{
    /// <summary>
    /// STUB CÓ CHỦ ĐÍCH — KHÔNG GỌI LIS NGOÀI.
    ///
    /// Bản gốc (api/CQkrlis/GetLisReport) gán vào request một connection string
    /// OLEDB trỏ tới máy chủ LIS ngoài (địa chỉ + tài khoản hardcode trong source
    /// gốc — KHÔNG copy sang bản port) rồi gọi LisRequest.GetMzReport/GetZyReport/
    /// CreateJpg_C/CreatePdf_C (tích hợp LIS qua OLEDB + DLL native, chỉ chạy trên
    /// Windows). Trên Linux không có OLEDB/native tương đương, máy chủ LIS ngoài
    /// cũng không có trong deployment này, nên endpoint luôn trả lỗi tường minh
    /// thay vì 404 hay dữ liệu giả.
    /// </summary>
    [ApiController]
    [Route("api/CQkrlis")]
    public class CQKRLIsController : ApiControllerBase
    {
        public CQKRLIsController(TokenStore tokenStore)
            : base(tokenStore)
        {
        }

        [HttpPost("GetLisReport")]
        public IActionResult GetLisReport([FromBody] LISResultDTO request)
        {
            var response = CommonExecute<LISResultDTO, DefaultResponse>((req, resp) =>
            {
                resp.code = ResponseResultCode.FAIL;
                resp.sub_code = "LIS_NOT_AVAILABLE";
                resp.sub_msg = "Tích hợp LIS ngoài chưa khả dụng trên bản Linux (bản gốc dùng OLEDB + DLL native Windows trỏ tới máy chủ LIS ngoài, hiện không có trong deployment này). Endpoint không trả dữ liệu giả.";
            }, request);
            return CreateResponse(response);
        }
    }
}
