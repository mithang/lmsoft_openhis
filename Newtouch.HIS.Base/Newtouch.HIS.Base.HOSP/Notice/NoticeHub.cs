using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtouch.Core.Redis;
using Newtouch.HIS.Base.HOSP.Notice;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    [HubName("noticeHub")]
    public class noticeHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections =
           new ConnectionMapping<string>();
        private readonly static string _appId = System.Configuration.ConfigurationManager.AppSettings["AppId"].ToString();

        public void SendChatMessage(string who, string message)
        {
            string name = Context.User.Identity.Name;

            foreach (var connectionId in _connections.GetConnections(who))
            {
                Clients.Client(connectionId).addChatMessage(name + ": " + message);
            }
        }

        /// <summary>
        /// Tất cả client đã kết nối。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendToAll(string name, string message)
        {
            await Clients.All.addNewMessageToPage(name, message);
        }
        /// <summary>
        /// Chỉ client gọi。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendToCaller(string name, string message)
        {
            await Clients.Caller.addContosoChatMessageToPage(name, message);
        }
        /// <summary>
        /// Tất cả client trừ client gọi。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendToOtherUsers(string name, string message)
        {
            await Clients.Others.addContosoChatMessageToPage(name, message);
        }
        /// <summary>
        /// Theo kết nối ID Client cụ thể được xác định。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task SendToUserbyConId(string name, string message, string connectionId)
        {
            await Clients.Client(connectionId).addContosoChatMessageToPage(name, message);
        }
        /// <summary>
        /// Tất cả client đã kết nối trong nhóm được chỉ định（Trừ client gọi）。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task SendToGroup(string name, string message, string groupName)
        {
            await Clients.OthersInGroup(groupName).addContosoChatMessageToPage(name, message);
        }
        /// <summary>
        /// Theo userId Người dùng cụ thể được xác định。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task SendToUser(string name, string message, string user)
        {
            await Clients.User(user).addContosoChatMessageToPage(name, message);
        }
        /// <summary>
        /// Kết nối ID Tất cả client và nhóm trong danh sách。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task SendToRangeOfConIds(string name, string message, List<string> connectionIds)
        {
            await Clients.Clients(connectionIds).broadcastMessage(name, message);
        }
        /// <summary>
        /// Danh sách nhóm
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="connectionIds"></param>
        /// <returns></returns>
        public async Task SendToGroups(string name, string message, List<string> groupIds)
        {
            await Clients.Groups(groupIds).broadcastMessage(name, message);
        }
        /// <summary>
        /// SignalR 2.1 Danh sách tên người dùng
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public async Task SendToUsers(string name, string message, IList<string> users)
        {
            await Clients.Users(users).broadcastMessage(name, message);
        }


        #region init
        public Task JoinGroup(string groupName)
        {
            var user = GetUser();
            if (user != null)
            {
                user.group = groupName;
            }
            SetUser(user);
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }
        #endregion

        #region 生命周期
        public override Task OnConnected()
        {
            //string name = Context.User.Identity.Name;
            NoticeHubUser user = new NoticeHubUser();
            if (ExistsUser())
            {
                user = GetUser();
                if(!user.connectionId.Exists(p=>p==Context.ConnectionId))
                {
                    user.connectionId.Add(Context.ConnectionId);
                }
                user.connStu = (int)EnumNoticeUserConnStu.conn;                
            }
            else
            {
                user = new NoticeHubUser
                {
                    appId = Context.QueryString["appid"],
                    usercode = Context.QueryString["user"],
                    orgId = Context.QueryString["orgid"],
                    connStu = (int)EnumNoticeUserConnStu.conn,
                    connectionId = new List<string> { Context.ConnectionId }
                };
            }
            SetUser(user);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            RemoveUser();
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            NoticeHubUser user = new NoticeHubUser();
            if (ExistsUser())
            {
                user = GetUser();
                if (!user.connectionId.Exists(p => p == Context.ConnectionId))
                {
                    user.connectionId.Add(Context.ConnectionId);
                }
                user.connStu = (int)EnumNoticeUserConnStu.reconn;
            }
            else
            {
                user = new NoticeHubUser
                {
                    appId = Context.QueryString["appid"],
                    usercode = Context.QueryString["user"],
                    orgId = Context.QueryString["orgid"],
                    connStu = (int)EnumNoticeUserConnStu.reconn,
                    connectionId = new List<string> { Context.ConnectionId }
                };
            }
            SetUser(user);
            return base.OnReconnected();
        }
        #endregion


        #region private
        private bool ExistsUser()
        {
            return RedisHelper.Exists($"{string.Format(CacheKey.NoticeConnectionKey,Context.QueryString["orgid"], "*", Context.QueryString["user"])}");
        }
        private NoticeHubUser GetUser()
        {
            return RedisHelper.Get<NoticeHubUser>($"{string.Format(CacheKey.NoticeConnectionKey, Context.QueryString["orgid"], "*", Context.QueryString["user"])}");
        }
        private bool RemoveUser()
        {
            return RedisHelper.Remove($"{string.Format(CacheKey.NoticeConnectionKey, Context.QueryString["orgid"], "*", Context.QueryString["user"])}");
        }
        private bool SetUser(NoticeHubUser user)
        {
            return RedisHelper.Set($"{string.Format(CacheKey.NoticeConnectionKey, Context.QueryString["orgid"], "*", Context.QueryString["user"])}",user);
        }

        #endregion
    }

    public class NoticeHubUser
    {
        public string orgId { get; set; }
        public string appId { get; set; }
        public List<string> connectionId { get; set; }
        public string usercode { get; set; }
        public string username { get; set; }
        public string group { get; set; }
        /// <summary>
        /// Trạng thái kết nối
        /// </summary>
        public int connStu { get; set; } = (int)EnumNoticeUserConnStu.conn;

    }
}
