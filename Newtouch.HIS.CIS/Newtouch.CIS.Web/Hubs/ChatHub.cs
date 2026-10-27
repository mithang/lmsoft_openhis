using Microsoft.AspNetCore.SignalR;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Newtouch.CIS.Web.Hubs
{
    /// <summary>
    /// Chat hub, ported from SignalR 2 (OWIN MapSignalR) to ASP.NET Core SignalR.
    /// The JS client must call connection.invoke("Send", name, message) and listen
    /// for "addNewMessageToPage" (same event name as the original).
    /// </summary>
    public class ChatHub : Hub
    {
        public async Task Send(string name, string message)
        {
            await Clients.All.SendAsync("addNewMessageToPage", name, message);
        }
    }
}
