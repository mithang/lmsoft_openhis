using Newtouch.EMR.API.App_Start;
using Newtouch.EMR.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using System;
using System.Web;
using System.Web.Http;

namespace Newtouch.EMR.API
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

            //Đăng ký vào sổ đăng ký ngữ cảnh
            DefaultDbContext.PartialTBRegister(DefaultDbContextTBRegister.Registe);

            //Đăng ký và giải phóng liên kết cơ sở dữ liệu
            FrameworkBase.API.ApiControllerBaseEx.RegisterDBContextDisposer(()=> {
                //Giải phóng DefaultDbContext
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
                //Được ghi khi xác thực danh tính người dùng ở lớp cơ sở
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