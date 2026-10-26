using FrameworkBase.MultiOrg.Application.Interface;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Domain.DBContext.Infrastructure;
using Newtouch.HIS.Web.Core.HttpModules;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Newtouch.CIS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Khởi động ứng dụng
        /// </summary>
        protected void Application_Start()
        {
            //Gọi AreaRegistration.RegisterAllAreas Phương pháp làm choMVCỨng dụng sau khi khởi động sẽ tìm tất cả các lớp kế thừa từ AreaRegistration lớp, và gọi của chúng cho mỗi lớp như vậy RegisterArea Phương pháp
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            App_Start.DependencyResolver.RegisteWebApiComponent();

            //Đăng ký vào bảng ngữ cảnh
            DefaultDbContext.PartialTBRegister(DefaultDbContextTBRegister.Registe);

            //Giải phóng liên kết cơ sở dữ liệu
            AspNetObjectContextDisposalModule.Register(() =>
            {
                Newtouch.Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IDefaultDatabaseFactory, DefaultDbContext>();
            });

            //Xác thực quyền đăng ký xuống tầng dưới
            HandlerAuthorizeAttribute.Register((IList<string> roleIdList, string moduleId, string action) =>
            {
                return DependencyDefaultInstanceResolver.GetInstance<IRoleAuthorizeApp>().ActionValidate(roleIdList, action);
            });

        }

    }
}