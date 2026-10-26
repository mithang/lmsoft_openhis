using Newtouch.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.DBContext.Infrastructure;
using Newtouch.HIS.Web.Core.HttpModules;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FrameworkBase.MultiOrg.Application.Interface;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Web
{
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// khởi động ứng dụng chương trình
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteRegistration.RegisterRoutes(RouteTable.Routes);

            App_Start.DependencyResolver.RegisteWebApiComponent();

            //tới ngữ cảnh đăng ký bảng
            DefaultDbContext.PartialTBRegister(DefaultDbContextTBRegister.Registe);

            //数据库链接释放
            AspNetObjectContextDisposalModule.Register(() =>
            {
                Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IDefaultDatabaseFactory, DefaultDbContext>();
                //Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IBaseDatabaseFactory, BaseDatabaseFactory>();
            });

            //权限验证向下层注册
            HandlerAuthorizeAttribute.Register((roleIdList, moduleId, action) => DependencyDefaultInstanceResolver.GetInstance<IRoleAuthorizeApp>().ActionValidate(roleIdList, action));

            //运行全局脚本 chương trình khởi động giờ thực hiện một lần
            try
            {
                var pars = new List<SqlParameter>
                {
                    new SqlParameter("@orgId", ""),
                    new SqlParameter("@topOrgId", Constants.TopOrganizeId),
                };
                DbHelperSQL.ExecuteNonQuery("exec usp_sysinit @orgId, @topOrgId", pars.ToArray());
            }
            catch
            {
                // ignored
            }
        }

    }
}