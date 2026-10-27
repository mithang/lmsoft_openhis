using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Base.HOSP.API.Hubs
{
    public class NoticeHubUser
    {
        public string appId { get; set; }
        public string usercode { get; set; }
        public string orgId { get; set; }
        public int connStu { get; set; }
        public List<string> connectionId { get; set; } = new List<string>();
    }

    /// <summary>
    /// Port Core SignalR của Models/Notice/noticeHub (SignalR 2) bên HOSP.API.
    /// Logic OnConnected/OnDisconnected giữ nguyên (connStu qua EnumNoticeUserConnStu
    /// của Infra); OnReconnected không có trong Core nên gộp vào OnConnected.
    /// GIỚI HẠN (ghi rõ): bản gốc lưu connection qua Redis (cross-process); bản port
    /// dùng dict tĩnh cùng process (giống hub bên HOSP web). Client nối vào hub của
    /// HOSP.API sẽ nhận được message từ NoticeController của chính host này; client
    /// nối vào hub web (5233) thì không (khác process). Endpoint hub tồn tại, không 404.
    /// Client SignalR 2 (jQuery) cũ không nói được protocol Core — cần client
    /// @microsoft/signalr mới để push trực tiếp.
    /// </summary>
    public class NoticeHub : Hub
    {
        private static readonly ConcurrentDictionary<string, NoticeHubUser> _users
            = new ConcurrentDictionary<string, NoticeHubUser>();

        private static string Key(string orgId, string usercode) => (orgId ?? "") + ":" + (usercode ?? "");

        public static NoticeHubUser FindUser(string orgId, string usercode)
        {
            _users.TryGetValue(Key(orgId, usercode), out var u);
            return u;
        }

        private string Q(string name) => Context.GetHttpContext()?.Request.Query[name].FirstOrDefault();

        public async Task SendToAll(string name, string message)
        {
            await Clients.All.SendAsync("addNewMessageToPage", name, message);
        }

        public async Task SendToCaller(string name, string message)
        {
            await Clients.Caller.SendAsync("addContosoChatMessageToPage", name, message);
        }

        public async Task SendToOtherUsers(string name, string message)
        {
            await Clients.Others.SendAsync("addContosoChatMessageToPage", name, message);
        }

        public async Task SendToUserbyConId(string name, string message, string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("addContosoChatMessageToPage", name, message);
        }

        public async Task SendToGroup(string name, string message, string groupName)
        {
            await Clients.OthersInGroup(groupName).SendAsync("addContosoChatMessageToPage", name, message);
        }

        public async Task SendToUser(string name, string message, string user)
        {
            await Clients.User(user).SendAsync("addContosoChatMessageToPage", name, message);
        }

        public async Task SendToRangeOfConIds(string name, string message, List<string> connectionIds)
        {
            await Clients.Clients(connectionIds).SendAsync("broadcastMessage", name, message);
        }

        public async Task SendToGroups(string name, string message, List<string> groupIds)
        {
            await Clients.Groups(groupIds).SendAsync("broadcastMessage", name, message);
        }

        public async Task SendToUsers(string name, string message, IList<string> users)
        {
            await Clients.Users(users).SendAsync("broadcastMessage", name, message);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public override async Task OnConnectedAsync()
        {
            var orgId = Q("orgid");
            var usercode = Q("user");
            var key = Key(orgId, usercode);
            var user = _users.GetOrAdd(key, _ => new NoticeHubUser
            {
                appId = Q("appid"),
                usercode = usercode,
                orgId = orgId,
            });
            lock (user)
            {
                if (!user.connectionId.Contains(Context.ConnectionId))
                    user.connectionId.Add(Context.ConnectionId);
                user.connStu = (int)EnumNoticeUserConnStu.conn;
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var orgId = Q("orgid");
            var usercode = Q("user");
            if (_users.TryGetValue(Key(orgId, usercode), out var user))
            {
                lock (user)
                {
                    user.connectionId.Remove(Context.ConnectionId);
                    user.connStu = (int)EnumNoticeUserConnStu.disconn;
                }
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
