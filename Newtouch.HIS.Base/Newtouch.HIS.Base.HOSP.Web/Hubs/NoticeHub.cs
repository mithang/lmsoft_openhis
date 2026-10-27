using Microsoft.AspNetCore.SignalR;
using Newtouch.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newtouch.HIS.Base.HOSP.Web.Hubs
{
    /// <summary>
    /// Port of the SignalR 2 noticeHub (Notice/NoticeHub.cs) to ASP.NET Core SignalR.
    /// Method names and client callback names are preserved so existing callers keep
    /// working at the protocol level; the Redis user store is replaced by an
    /// in-memory map (single-instance demo; the old RedisHelper API has no net6.0
    /// equivalent). NOTE: legacy SignalR 2 jQuery clients cannot speak the Core
    /// SignalR protocol — browser pages need the @microsoft/signalr client to use
    /// this hub live. The endpoint itself exists (no 404).
    /// </summary>
    public class NoticeHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections =
           new ConnectionMapping<string>();
        private readonly static ConcurrentDictionary<string, NoticeHubUser> _users =
            new ConcurrentDictionary<string, NoticeHubUser>();

        public async Task SendChatMessage(string who, string message)
        {
            string name = Context.User?.Identity?.Name;
            foreach (var connectionId in _connections.GetConnections(who))
            {
                await Clients.Client(connectionId).SendAsync("addChatMessage", name + ": " + message);
            }
        }

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
            var user = GetUser();
            if (user != null)
            {
                user.group = groupName;
            }
            SetUser(user);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public override async Task OnConnectedAsync()
        {
            NoticeHubUser user;
            if (ExistsUser())
            {
                user = GetUser();
                if (!user.connectionId.Exists(p => p == Context.ConnectionId))
                {
                    user.connectionId.Add(Context.ConnectionId);
                }
                user.connStu = (int)EnumNoticeUserConnStu.conn;
            }
            else
            {
                user = new NoticeHubUser
                {
                    appId = Query("appid"),
                    usercode = Query("user"),
                    orgId = Query("orgid"),
                    connStu = (int)EnumNoticeUserConnStu.conn,
                    connectionId = new List<string> { Context.ConnectionId }
                };
            }
            SetUser(user);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            RemoveUser();
            await base.OnDisconnectedAsync(exception);
        }

        private string Query(string key)
        {
            var http = Context.GetHttpContext();
            return http != null && http.Request.Query.TryGetValue(key, out var v) ? v.ToString() : null;
        }

        private string UserKey()
        {
            return $"{Query("orgid")}|{Query("user")}";
        }

        /// <summary>Shared lookup for NoticeController (replaces the old Redis user store).</summary>
        public static NoticeHubUser FindUser(string orgId, string user)
        {
            _users.TryGetValue($"{orgId}|{user}", out var u);
            return u;
        }

        private bool ExistsUser()
        {
            return _users.ContainsKey(UserKey());
        }

        private NoticeHubUser GetUser()
        {
            _users.TryGetValue(UserKey(), out var user);
            return user;
        }

        private bool RemoveUser()
        {
            return _users.TryRemove(UserKey(), out _);
        }

        private bool SetUser(NoticeHubUser user)
        {
            if (user == null)
            {
                return false;
            }
            _users[UserKey()] = user;
            return true;
        }
    }

    public class NoticeHubUser
    {
        public string orgId { get; set; }
        public string appId { get; set; }
        public List<string> connectionId { get; set; }
        public string usercode { get; set; }
        public string username { get; set; }
        public string group { get; set; }
        public int connStu { get; set; } = (int)EnumNoticeUserConnStu.conn;
    }

}
