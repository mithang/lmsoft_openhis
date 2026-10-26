using Autofac;
using Autofac.Integration.WebApi;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.Core.NLogger;
using Newtouch.Core.Redis.Cache;
using Newtouch.HIS.API.Common.Interface;
using Newtouch.HIS.API.Common.Models;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Newtouch.MRQC.API.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class DependencyResolver
    {
        /// <summary>
        /// tiêm WebAPI bộ điều khiển/bộ lọc
        /// </summary>
        public static void RegisteWebApiComponent()
        {
            var builder = new ContainerBuilder();

            // đăng kýWebAPI bộ điều khiển
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //đăng ký mặc địnhEFngữ cảnh
            builder.RegisterType<DefaultDatabaseFactory>().As<IDefaultDatabaseFactory>().InstancePerLifetimeScope();

            //đăng ký nhật ký
            builder.RegisterType<NLogger>().As<ILogger>().InstancePerLifetimeScope();

            //bộ nhớ đệm
            builder.RegisterType<SERedisCache>().As<ICache>().InstancePerLifetimeScope();

            //API bộ phân tích danh tính người dùng
            builder.RegisterType<UserIdentityResolver>().As<IUserIdentityResolver<Identity>>().InstancePerLifetimeScope();

            #region IOCĐăng ký tầng dưới
            builder.RegisterAssemblyTypes(typeof(RepositoryBase<>).Assembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(DmnServiceBase).Assembly)
                .Where(t => t.Name.EndsWith("DmnService"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            #endregion

            #region IOC, then registe yours
            //DLLđường dẫn tuyệt đối nơi đặt
            var repoAssembly = Assembly.Load("Newtouch.MRQC.Repository");
            builder.RegisterAssemblyTypes(repoAssembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            var dmnserviceAssembly = Assembly.Load("Newtouch.MRQC.DomainServices");
            builder.RegisterAssemblyTypes(dmnserviceAssembly)
                .Where(t => t.Name.EndsWith("DmnService"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            #endregion

            //đăng kýAutofacnhà cung cấp bộ lọc.
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            // đặt bộ phân tích phụ thuộc-Dependency Resolver
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            // Configure Deafult Instance Resolver
            DependencyDefaultInstanceResolver.Register(interfaceType =>
            {
                return FrameworkBase.API.ApiControllerBaseEx.ResolveService(interfaceType);
            });

        }
    }
}