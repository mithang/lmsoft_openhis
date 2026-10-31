using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.API.Models;
using Newtouch.Sett.API.Services;

namespace Newtouch.Sett.API.Controllers
{
    /// <summary>
    /// Nhóm A: BookingESB. Phục vụ cả hai prefix — api/OutpBook (8016 thực
    /// sự gọi) và api/Booking (route gốc của Sett.API) — để không phải đổi
    /// config 8016. Không dùng [ApiController] để body lỗi vẫn trả envelope
    /// HTTP 200 thay vì 400 (8016 coi mọi HTTP lỗi là "không phản hồi").
    /// Không yêu cầu xác thực: toàn bộ route gốc đều [IgnoreTokenDecrypt] và
    /// 8016 không gửi token (xem BookingModels).
    /// </summary>
    [Route("api/OutpBook")]
    [Route("api/Booking")]
    public class OutpBookController : ControllerBase
    {
        private readonly BookingService _svc;

        public OutpBookController(BookingService svc) { _svc = svc; }

        [HttpPost("BookingESB")]
        public ResponseBaseOld BookingESB([FromBody] BookingRequest req)
        {
            if (req == null) return ResponseBaseOld.Fail("Request rỗng hoặc không hợp lệ");
            try
            {
                return _svc.Dispatch(req);
            }
            catch (Exception ex)
            {
                return ResponseBaseOld.Error(ex.Message);
            }
        }
    }

    /// <summary>Nhóm B: api/PatientAnon/* (gốc PatientApiController).</summary>
    [Route("api/PatientAnon")]
    public class PatientAnonController : ControllerBase
    {
        private readonly PatientAnonService _svc;

        public PatientAnonController(PatientAnonService svc) { _svc = svc; }

        private ResponseBaseOld Run(string action, BookingRequest req)
        {
            if (req == null) return ResponseBaseOld.Fail("Request rỗng hoặc không hợp lệ");
            try
            {
                return _svc.Dispatch(action, req, new ParaData(req.paradata));
            }
            catch (Exception ex)
            {
                return ResponseBaseOld.Error(ex.Message);
            }
        }

        [HttpPost("OutpatRegRecord")]
        public ResponseBaseOld OutpatRegRecord([FromBody] BookingRequest req) { return Run("OutpatRegRecord", req); }

        [HttpPost("InHosBillUnpaid")]
        public ResponseBaseOld InHosBillUnpaid([FromBody] BookingRequest req) { return Run("InHosBillUnpaid", req); }

        [HttpPost("InHosPatientQuery")]
        public ResponseBaseOld InHosPatientQuery([FromBody] BookingRequest req) { return Run("InHosPatientQuery", req); }

        [HttpPost("PatientLisReport")]
        public ResponseBaseOld PatientLisReport([FromBody] BookingRequest req) { return Run("PatientLisReport", req); }

        [HttpPost("PatientLisReportDetail")]
        public ResponseBaseOld PatientLisReportDetail([FromBody] BookingRequest req) { return Run("PatientLisReportDetail", req); }
    }

    /// <summary>Nhóm C: api/OrderCenter/* (hợp 2 controller gốc).</summary>
    [Route("api/OrderCenter")]
    public class OrderCenterController : ControllerBase
    {
        private readonly OrderService _svc;

        public OrderCenterController(OrderService svc) { _svc = svc; }

        private ResponseBaseOld Run(string action, BookingRequest req)
        {
            if (req == null) return ResponseBaseOld.Fail("Request rỗng hoặc không hợp lệ");
            try
            {
                return _svc.Dispatch(action, req, new ParaData(req.paradata));
            }
            catch (Exception ex)
            {
                return ResponseBaseOld.Error(ex.Message);
            }
        }

        [HttpPost("OrderQuery")]
        public ResponseBaseOld OrderQuery([FromBody] BookingRequest req) { return Run("OrderQuery", req); }

        [HttpPost("OrderDetailQuery")]
        public ResponseBaseOld OrderDetailQuery([FromBody] BookingRequest req) { return Run("OrderDetailQuery", req); }

        [HttpPost("OrderCreate")]
        public ResponseBaseOld OrderCreate([FromBody] BookingRequest req) { return Run("OrderCreate", req); }

        [HttpPost("LockerApply")]
        public ResponseBaseOld LockerApply([FromBody] BookingRequest req) { return Run("LockerApply", req); }

        [HttpPost("PayOrder")]
        public ResponseBaseOld PayOrder([FromBody] BookingRequest req) { return Run("PayOrder", req); }

        [HttpPost("PayOutpOrder")]
        public ResponseBaseOld PayOutpOrder([FromBody] BookingRequest req) { return Run("PayOutpOrder", req); }

        [HttpPost("PayInHosOrder")]
        public ResponseBaseOld PayInHosOrder([FromBody] BookingRequest req) { return Run("PayInHosOrder", req); }

        [HttpPost("test")]
        public ResponseBaseOld Test([FromBody] BookingRequest req) { return Run("test", req); }
    }
}
