using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtouch.HIS.Base.HOSP.Web.Hubs;
using Newtouch.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    /// <summary>
    /// Port of the Web API 2 NoticeController (Notice/NoticeController.cs) to a
    /// Core MVC controller with attribute routes. Hub access goes through
    /// IHubContext; user-connection lookup uses the hub's in-memory store
    /// (replacing the old Redis store). Routes preserved: api/notice/getmsg,
    /// api/notice/sendNoticeToUser.
    /// </summary>
    [Route("api/notice")]
    public class NoticeController : Controller
    {
        private readonly IHubContext<NoticeHub> _hub;

        public NoticeController(IHubContext<NoticeHub> hub)
        {
            _hub = hub;
        }

        [HttpGet("getmsg/{nick}/{id}")]
        public async Task<string> Get(string nick, string id)
        {
            await _hub.Clients.All.SendAsync("showMessage", nick + "," + id + "," + DateTime.Now.ToUniversalTime().ToString());
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
                        await _hub.Clients.Client(connectionId).SendAsync("sendToUser", notice.message);
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
    }
}
