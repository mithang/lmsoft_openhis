using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Lib.Services.SignalR
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("Cors")]
    public class HisNoticeHub : Hub
    {
        #region máy khách gọi
        public void SendNotice(string name, string message)
        {
            Clients.All.SendAsync(name, message);
        }

        /// <summary>
        /// ghi lại kết nối của người dùng hiện tạiID，để tiện sau này đẩy thông báo tin nhắn trong hệ thống tới người dùng máy khách。
        /// OnConnectedAsyncđược gọi khi thiết lập kết nối mới với hub。 
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var user = Context.UserIdentifier;
            //var userId = _currentUserService.GetUserId();
            //await _userService.SaveWebSocketIdAsync(userId, Context.ConnectionId);
            //Console.WriteLine($"kết nối mới:{Context.ConnectionId},userId={userId}");
            ////xử lý tin nhắn chưa đọc của người dùng hiện tại
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
        /// thiết lậpSignalRmáy khách củaidvàtokenquan hệ của
        /// </summary>
        public static Dictionary<string, AuthIdentity> UserIdAndCid = new Dictionary<string, AuthIdentity>();
        /// <summary>
        /// phương thức khởi tạo thiết lập kết nối
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
        /// tác dụng chính là，khi người dùngWebSocketsau khi ngắt kết nối，xóa kết nối của nóID。
        /// OnDisconnectedAsyncđược gọi khi kết nối với hub bị chấm dứt。
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            //var userId = _currentUserService.GetUserId();
            //Console.WriteLine($"ngắt kết nối:{Context.ConnectionId},userId={userId}");
            //await _userService.SaveWebSocketIdAsync(userId, string.Empty);
            if (UserIdAndCid.Keys.Contains(Context.ConnectionId))
            {
                //UserData userData = UserIdAndCid[Context.ConnectionId];
                //if (userData != null)
                //{
                //    userData.Timer.Stop();  //dừng bộ hẹn giờ
                //    userData.Timer.Dispose();  //giải phóng bộ hẹn giờ

                //}
                UserIdAndCid.Remove(Context.ConnectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }



    }


}
