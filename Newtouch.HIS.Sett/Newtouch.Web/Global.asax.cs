using FrameworkBase.MultiOrg.Application.Interface;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DBContext.Infrastructure;
using Newtouch.HIS.Web.Core.HttpModules;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Newtouch.HIS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Ứng dụng khởi động
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            App_Start.DependencyResolver.RegisteWebApiComponent();

            //Sổ đăng ký ngữ cảnh tới
            DefaultDbContext.PartialTBRegister(DefaultDbContextTBRegister.Registe);

            //Giải phóng liên kết cơ sở dữ liệu
            AspNetObjectContextDisposalModule.Register(() =>
            {
                Newtouch.Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IDefaultDatabaseFactory, DefaultDbContext>();
            });

            //Đăng ký xuống tầng dưới xác minh quyền
            HandlerAuthorizeAttribute.Register((IList<string> roleIdList, string moduleId, string action) =>
            {
                return DependencyDefaultInstanceResolver.GetInstance<IRoleAuthorizeApp>().ActionValidate(roleIdList, action);
            });

        }

    }
}