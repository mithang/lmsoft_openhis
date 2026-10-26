using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Sett.API.App_Start;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DBContext.Infrastructure;
using System;
using System.Web;
using System.Web.Http;

namespace Newtouch.HIS.Sett.API
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // tới ngữ cảnh đ ăng ký bảng 
            DefaultDbContext.PartialTBRegister(DefaultDbContextTBRegister.Registe);

            // đăng ký giải phóng liên kết kho dữ liệu
            FrameworkBase.API.ApiControllerBaseEx.RegisterDBContextDisposer(() => {
                //Giải phóng DefaultDbContext
                Newtouch.Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IDefaultDatabaseFactory, DefaultDbContext>();
            });

            IEntityEx.RegisterCurUserCodeGetter(() =>
            {
                //Tại lớp cơ sở xác thực thân phận người dùng khi viết ở trên
                var account = HttpContext.Current.Items["API_UserIdentity_Account"];
                return account == null ? null : account.ToString();
            });

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}