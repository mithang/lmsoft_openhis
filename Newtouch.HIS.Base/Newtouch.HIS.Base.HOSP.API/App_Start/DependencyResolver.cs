using Autofac;
using Autofac.Integration.WebApi;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.Core.NLogger;
using Newtouch.Core.Redis.Cache;
using Newtouch.HIS.API.Common.Interface;
using Newtouch.HIS.API.Common.Models;
using Newtouch.HIS.Application.Implementation;
using Newtouch.HIS.Domain.DBContext.Infrastructure;
using Newtouch.HIS.DomainServices;
using Newtouch.HIS.Repository;
using Newtouch.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Newtouch.HIS.Base.HOSP.API.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class DependencyResolver
    {
        /// <summary>
        /// Inject controller/bộ lọc Web API
        /// </summary>
        /// <param name="config">HttpConfiguration</param>
        public static void RegisteWebApiComponent()
        {
            var builder = new ContainerBuilder();

            // Đăng ký controller WebAPI
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Đăng ký EF context mặc định
            builder.RegisterType<BaseDatabaseFactory>().As<IBaseDatabaseFactory>().InstancePerLifetimeScope();

            //Đăng ký nhật ký
            builder.RegisterType<NLogger>().As<ILogger>().InstancePerLifetimeScope();

            //Bộ nhớ đệm
            builder.RegisterType<SERedisCache>().As<ICache>().InstancePerLifetimeScope();

            //Trình phân tích danh tính người dùng API
            builder.RegisterType<HISAPIUserIdentityResolver>().As<IUserIdentityResolver<Identity>>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(SysUserRoleRepo).Assembly)
                .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(SysUserDmnService).Assembly)
                .Where(t => t.Name.EndsWith("DmnService"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(SysUserApp).Assembly)
                .Where(t => t.Name.EndsWith("App"))
                .Where(t => !string.IsNullOrEmpty(t.Namespace))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            //Đăng ký nhà cung cấp bộ lọc Autofac.
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            // Thiết lập trình phân giải phụ thuộc - Dependency Resolver
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