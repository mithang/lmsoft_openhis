using Newtouch.EMR.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Application.Interface;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Web.Core.HttpModules;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Newtouch.EMR.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Khởi động ứng dụng
        /// </summary>
        protected void Application_Start()
        {
            //Gọi phương thức AreaRegistration.RegisterAllAreas để ứng dụng MVC sau khi khởi động sẽ tìm tất cả các lớp kế thừa từ AreaRegistration và gọi phương thức RegisterArea cho từng lớp như vậy
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            App_Start.DependencyResolver.RegisteWebApiComponent();

            //Đăng ký vào sổ đăng ký ngữ cảnh
            DefaultDbContext.PartialTBRegister(DefaultDbContextTBRegister.Registe);

            //Giải phóng liên kết cơ sở dữ liệu
            AspNetObjectContextDisposalModule.Register(() =>
            {
                Newtouch.Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IDefaultDatabaseFactory, DefaultDbContext>();
            });

            //Đăng ký xác minh quyền xuống tầng dưới
            HandlerAuthorizeAttribute.Register((IList<string> roleIdList, string moduleId, string action) =>
            {
                return DependencyDefaultInstanceResolver.GetInstance<IRoleAuthorizeApp>().ActionValidate(roleIdList, action);
            });


        }

    }
}