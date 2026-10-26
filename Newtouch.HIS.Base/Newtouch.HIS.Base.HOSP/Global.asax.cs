using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DBContext.Infrastructure;
using Newtouch.HIS.Web.Core.HttpModules;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Newtouch.HIS.Base.HOSP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Khởi động ứng dụng
        /// </summary>
        protected void Application_Start()
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteRegistration.RegisterRoutes(RouteTable.Routes);

            App_Start.DependencyResolver.RegisteWebApiComponent();

            //Giải phóng kết nối cơ sở dữ liệu
            AspNetObjectContextDisposalModule.Register(() =>
            {
                Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IBaseDatabaseFactory, BaseDbContext>();
            });

            //Đăng ký xác minh quyền xuống tầng dưới
            HandlerAuthorizeAttribute.Register((IList<string> roleIdList, string moduleId, string action) =>
            {
                return DependencyDefaultInstanceResolver.GetInstance<IRoleAuthorizeApp>().ActionValidate(roleIdList, moduleId, action);
            });

            //Chạy tập lệnh toàn cục Thực hiện một lần khi chương trình khởi động
            try
            {
                var pars = new List<SqlParameter>() {
                    new SqlParameter("@orgId", ""),
                    new SqlParameter("@topOrgId", Constants.TopOrganizeId),
                };
                DbHelperSQL.ExecuteNonQuery("exec usp_sysinit @orgId, @topOrgId", pars.ToArray());
            }
            catch { }
        }

    }
}