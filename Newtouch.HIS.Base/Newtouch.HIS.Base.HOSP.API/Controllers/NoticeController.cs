using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtouch.HIS.Base.HOSP.API.Envelope;
using Newtouch.HIS.Base.HOSP.API.Hubs;
using Newtouch.HIS.Base.HOSP.API.Services;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>Thông báo đẩy (port Core của NoticeController Web API 2).
    /// QA PHASE 2 (API-05): sendNoticeToUser trước đây là action trần không auth,
    /// không envelope — body rỗng ném Exception và trả HTTP 500 kèm full stack
    /// trace (đường dẫn source server). Nay action chạy qua CommonExecute với
    /// [DefaultAuthorize] (token HOSP.API qua GetToken) và trả envelope chuẩn:
    /// thiếu token → 40004 TOKEN_IS_REQUIRED, thiếu orgid/user → 40004
    /// {FIELD}_IS_REQUIRED, không còn 500/stack trace. Lưu ý đổi contract:
    /// trước đây trả thẳng chuỗi notice.message, nay trả envelope
    /// {code, sub_code, sub_msg, data} với data = message; không tìm thấy
    /// caller nào trong cây repo dùng route này (chỉ có bản copy ở HOSP Web),
    /// nên đổi này được chấp nhận cho vận hành thật.
    /// GET getmsg giữ nguyên như gốc (không thuộc phạm vi API-05) nhưng cũng là
    /// bề mặt broadcast không-auth — cần quyết định riêng trước khi vận hành
    /// thật (xem báo cáo qa-phase2/api-auth.md).
    /// Không dùng [ApiController] ở controller này (theo tiền lệ
    /// HospitalizationController của PDS.API): để body null/rỗng vẫn vào
    /// CommonExecute và trả envelope thay vì HTTP 400 ProblemDetails của
    /// [ApiController].
    /// </summary>
    [Route("api/notice")]
    public class NoticeController : ApiControllerBase
    {
        private readonly IHubContext<NoticeHub> _hub;

        public NoticeController(IHubContext<NoticeHub> hub, TokenStore tokenStore)
            : base(tokenStore)
        {
            _hub = hub;
        }

        [HttpGet("getmsg/{nick}/{id}")]
        public async Task<string> Get(string nick, string id)
        {
            await _hub.Clients.All.SendAsync("showMessage", nick + "," + id + "," + DateTime.UtcNow.ToString());
            return "value";
        }

        [HttpPost("sendNoticeToUser")]
        [DefaultAuthorize]
        public ContentResult SendNoticeToUser([FromBody] NoticeModel notice)
        {
            var response = CommonExecute<NoticeModel, DefaultResponse>((req, resp) =>
            {
                string[] users = req.user.Split(',');
                foreach (var u in users)
                {
                    var userNoticeData = NoticeHub.FindUser(req.orgid, u);
                    if (userNoticeData != null
                        && userNoticeData.connStu != (int)EnumNoticeUserConnStu.disconn
                        && userNoticeData.connectionId.Count > 0)
                    {
                        foreach (var connectionId in userNoticeData.connectionId)
                        {
                            _hub.Clients.Client(connectionId).SendAsync("sendToUser", req.message, req).GetAwaiter().GetResult();
                        }
                    }
                }
                resp.data = req.message;
                resp.code = ResponseResultCode.SUCCESS;
            }, notice);
            return CreateResponse(response);
        }
    }

    /// <summary>Body của sendNoticeToUser. Kế thừa RequestBase để qua CommonExecute
    /// (Token/TokenType/Timestamp/AppId + Validate [Required]).
    /// Chú ý: field `appid` cũ của model gốc nay map vào RequestBase.AppId
    /// (Newtonsoft deserialize không phân biệt hoa/thường nên client gửi `appid`
    /// hay `AppId` đều vào AppId); field `appid` riêng đã bỏ để tránh xung đột
    /// hai property chỉ khác hoa/thường khi deserialize.</summary>
    public class NoticeModel : RequestBase
    {
        [Required]
        public string orgid { get; set; }
        [Required]
        public string user { get; set; }
        public string message { get; set; }
        public string msgid { get; set; }
    }
}
