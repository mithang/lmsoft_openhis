using Newtouch.MRQC.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Application.Interface;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Web.Core.HttpModules;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Newtouch.MRQC.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// khởi động ứng dụng
        /// </summary>
        protected void Application_Start()
        {
            //gọi AreaRegistration.RegisterAllAreas phương thức giúpMVCsau khi khởi động, ứng dụng sẽ tìm tất cả các lớp kế thừa từ AreaRegistration lớp，và gọi chúng cho mỗi lớp như vậy RegisterArea phương thức
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            App_Start.DependencyResolver.RegisteWebApiComponent();

            //đăng ký vào bảng ngữ cảnh
            DefaultDbContext.PartialTBRegister(DefaultDbContextTBRegister.Registe);

            //giải phóng kết nối cơ sở dữ liệu
            AspNetObjectContextDisposalModule.Register(() =>
            {
                Newtouch.Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IDefaultDatabaseFactory, DefaultDbContext>();
            });

            //đăng ký xác minh quyền xuống tầng dưới
            HandlerAuthorizeAttribute.Register((IList<string> roleIdList, string moduleId, string action) =>
            {
                return DependencyDefaultInstanceResolver.GetInstance<IRoleAuthorizeApp>().ActionValidate(roleIdList, action);
            });


        }

    }
}