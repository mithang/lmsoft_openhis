using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtouch.HIS.Base.HOSP.API.Hubs;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>Thông báo đẩy (port Core của NoticeController Web API 2).</summary>
    [Route("api/notice")]
    [ApiController]
    public class NoticeController : ControllerBase
    {
        private readonly IHubContext<NoticeHub> _hub;

        public NoticeController(IHubContext<NoticeHub> hub)
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
        public async Task<string> SendNoticeToUser([FromBody] NoticeModel notice)
        {
            if (notice == null || string.IsNullOrWhiteSpace(notice.orgid) || string.IsNullOrWhiteSpace(notice.user))
            {
                throw new Exception("Thông tin người dùng và cơ sở không được để trống!");
            }
            string[] users = notice.user.Split(',');
            foreach (var u in users)
            {
                var userNoticeData = NoticeHub.FindUser(notice.orgid, u);
                if (userNoticeData != null
                    && userNoticeData.connStu != (int)EnumNoticeUserConnStu.disconn
                    && userNoticeData.connectionId.Count > 0)
                {
                    foreach (var connectionId in userNoticeData.connectionId)
                    {
                        await _hub.Clients.Client(connectionId).SendAsync("sendToUser", notice.message, notice);
                    }
                }
            }
            return notice.message;
        }
    }

    public class NoticeModel
    {
        public string appid { get; set; }
        public string orgid { get; set; }
        public string user { get; set; }
        public string message { get; set; }
        public string msgid { get; set; }
    }
}
