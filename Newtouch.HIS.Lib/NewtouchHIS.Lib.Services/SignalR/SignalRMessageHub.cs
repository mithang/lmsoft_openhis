using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Lib.Services.SignalR
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("Cors")]
    public class HisNoticeHub : Hub
    {
        #region Lời gọi từ máy khách
        public void SendNotice(string name, string message)
        {
            Clients.All.SendAsync(name, message);
        }

        /// <summary>
        /// Ghi lại ID kết nối của người dùng hiện tại, để tiện sau này đẩy thông báo tin nhắn nội bộ cho người dùng máy khách.
        /// OnConnectedAsyncĐược gọi khi thiết lập kết nối mới với hub. 
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var user = Context.UserIdentifier;
            //var userId = _currentUserService.GetUserId();
            //await _userService.SaveWebSocketIdAsync(userId, Context.ConnectionId);
            //Console.WriteLine($"Kết nối mới:{Context.ConnectionId},userId={userId}");
            ////Xử lý tin nhắn chưa đọc của người dùng hiện tại
            //var unreadMessage = await _messageService.GetMyMessageUnReadNumberAsync(userId);
            //var message = new
            //{
            //    UnReadNumber = unreadMessage,
            //    MessageList = new List<MessageDTO>()
            //};
            //await Clients.Client(Context.ConnectionId).SendAsync(MesssageCenter.NewMessageNotify, message);
            await base.OnConnectedAsync();
        }

        #endregion
        //public readonly LoginManagerService _LoginManagerService;
        //public SignalRMessageHub(LoginManagerService adminUserService)
        //{
        //    _LoginManagerService = adminUserService;
        //}
        /// <summary>
        /// Thiết lập quan hệ giữa id máy khách SignalR và token
        /// </summary>
        public static Dictionary<string, AuthIdentity> UserIdAndCid = new Dictionary<string, AuthIdentity>();
        /// <summary>
        /// Phương thức khởi tạo thiết lập kết nối
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        //public async Task Init(string token)
        //{
        //    var login = _LoginManagerService.GetLoginUserInRedisByToken(token);
        //    UserIdentity userData = new UserIdentity()
        //    {
        //        SignalConnId = Context.ConnectionId,
        //        Token = token,
        //        UserName = login.user_name,
        //        Account = login.user_full_name
        //    };
        //    UserIdAndCid.Add(Context.ConnectionId, userData);
        //}





        /// <summary>
        /// Tác dụng chính là, khi WebSocket của người dùng ngắt kết nối, xóa ID kết nối của anh ta.
        /// OnDisconnectedAsyncĐược gọi khi kết nối với hub bị chấm dứt.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            //var userId = _currentUserService.GetUserId();
            //Console.WriteLine($"Ngắt kết nối:{Context.ConnectionId},userId={userId}");
            //await _userService.SaveWebSocketIdAsync(userId, string.Empty);
            if (UserIdAndCid.Keys.Contains(Context.ConnectionId))
            {
                //UserData userData = UserIdAndCid[Context.ConnectionId];
                //if (userData != null)
                //{
                //    userData.Timer.Stop();  //Dừng bộ hẹn giờ
                //    userData.Timer.Dispose();  //Giải phóng bộ hẹn giờ

                //}
                UserIdAndCid.Remove(Context.ConnectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }



    }


}
