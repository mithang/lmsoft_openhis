using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Newtouch.MRQC.Web.App_Start.Startup))]

namespace Newtouch.MRQC.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // thông tin chi tiết về cách cấu hình ứng dụng，vui lòng truy cập https://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}
