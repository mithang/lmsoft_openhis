using Newtouch.MR.ManageSystem.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Application.Interface;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Web.Core.HttpModules;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Newtouch.MR.ManageSystem.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Khởi động ứng dụng
        /// </summary>
        protected void Application_Start()
        {
            //Gọi phương thức AreaRegistration.RegisterAllAreas để ứng dụng MVCtạisau khi khởi động sẽ tìm tất cả các lớp kế thừa từ AreaRegistration, và gọi phương thức RegisterArea cho mỗi lớp như vậy
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            App_Start.DependencyResolver.RegisteWebApiComponent();

            //Đăng ký bảng vào ngữ cảnh
            DefaultDbContext.PartialTBRegister(DefaultDbContextTBRegister.Registe);

            //Cơ sở dữ liệuGiải phóng liên kết
            AspNetObjectContextDisposalModule.Register(() =>
            {
                Newtouch.Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IDefaultDatabaseFactory, DefaultDbContext>();
            });

            //Đăng ký xác thực quyền xuống tầng dưới
            HandlerAuthorizeAttribute.Register((IList<string> roleIdList, string moduleId, string action) =>
            {
                return DependencyDefaultInstanceResolver.GetInstance<IRoleAuthorizeApp>().ActionValidate(roleIdList, action);
            });


        }

    }
}