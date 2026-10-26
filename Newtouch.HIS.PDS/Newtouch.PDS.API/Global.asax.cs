using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.API.Common.HttpModules;
using Newtouch.HIS.Domain.DBContext.Infrastructure;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebGrease.Configuration;

namespace Newtouch.PDS.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            //Đăng ký vào sổ đăng ký ngữ cảnh
            DefaultDbContext.PartialTBRegister(DefaultDbContextTBRegister.Registe);

            //Đăng ký giải phóng kết nối cơ sở dữ liệu
            FrameworkBase.API.ApiControllerBaseEx.RegisterDBContextDisposer(() =>
            {
                //Giải phóngDefaultDbContext
                var key = "AspNetObjectContextDisposalModule_Dispose_Flag_" + typeof(DefaultDbContext).Name;
                if (HttpContext.Current == null && HttpContext.Current.Items[key] == null) return;
                var dbFactory = DependencyDefaultInstanceResolver.GetInstance<IDefaultDatabaseFactory>();
                if (dbFactory != null)
                {
                    dbFactory.Dispose();
                }
            });

            IEntityEx.RegisterCurUserCodeGetter(() =>
            {
                //Được viết khi lớp cơ sở xác thực danh tính người dùng
                var account = HttpContext.Current.Items["API_UserIdentity_Account"];
                return account == null ? null : account.ToString();
            });
        }
    }
}
