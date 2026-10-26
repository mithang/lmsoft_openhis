using Newtouch.MR.ManageSystem.API.App_Start;
using Newtouch.MR.ManageSystem.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using System;
using System.Web;
using System.Web.Http;

namespace Newtouch.MR.ManageSystem.API
{
    /// <summary>
    /// 
    /// </summary>
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

            //Đăng ký bảng vào ngữ cảnh
            DefaultDbContext.PartialTBRegister(DefaultDbContextTBRegister.Registe);

            //Đăng ký, giải phóngCơ sở dữ liệuLiên kết
            FrameworkBase.API.ApiControllerBaseEx.RegisterDBContextDisposer(()=> {
                //Giải phóngDefaultDbContext
                var key = "AspNetObjectContextDisposalModule_Dispose_Flag_" + typeof(DefaultDbContext).Name;
                if (HttpContext.Current != null || HttpContext.Current.Items[key] != null)
                {
                    var dbFactory = DependencyDefaultInstanceResolver.GetInstance<IDefaultDatabaseFactory>();
                    if (dbFactory != null)
                    {
                        dbFactory.Dispose();
                    }
                }
            });

            IEntityEx.RegisterCurUserCodeGetter(() =>
            {
                //tạiLớp cơ sở xác thực danh tính người dùnggiờđã ghi
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