using Autofac;
using Autofac.Integration.WebApi;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.Core.NLogger;
using Newtouch.Core.Redis.Cache;
using Newtouch.HIS.Repository;
using System.Reflection;
using System.Web.Http;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.API.Common.Interface;
using Newtouch.HIS.API.Common.Models;

namespace Newtouch.PDS.API.App_Start
{
    public class DependencyResolver
    {
        /// <summary>
        /// TiêmWeb APIBộ điều khiển/Bộ lọc
        /// </summary>
        public static void RegisteWebApiComponent()
        {
            var builder = new ContainerBuilder();

            //Đăng ký bộ điều khiển
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Đăng ký mặc địnhEFNgữ cảnh
            builder.RegisterType<DefaultDatabaseFactory>().As<IDefaultDatabaseFactory>().InstancePerLifetimeScope();

            //Đăng ký nhật ký
            //LoggerHelper.InitLog4net("/Configs/log4net.config");
            builder.RegisterType<NLogger>().As<ILogger>().InstancePerLifetimeScope();

            ////Bộ nhớ đệm
            builder.RegisterType<SERedisCache>().As<ICache>().InstancePerLifetimeScope();

            //APIBộ phân tích danh tính người dùng
            builder.RegisterType<HISAPIUserIdentityResolver>().As<IUserIdentityResolver<Identity>>().InstancePerLifetimeScope();

            #region IOC注册底层
            builder.RegisterAssemblyTypes(typeof(RepositoryBase<>).Assembly).Where(t => t.Name.EndsWith("Repo")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(DmnServiceBase).Assembly).Where(t => t.Name.EndsWith("DmnService")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(FrameworkBase.MultiOrg.Application.AppBase).Assembly).Where(t => t.Name.EndsWith("App")).AsImplementedInterfaces().InstancePerLifetimeScope();

            #endregion

            #region IOC, then registe yours
            //DLLĐường dẫn tuyệt đối nơi đặt
            var repoAssembly = Assembly.Load("Newtouch.HIS.Repository");
            builder.RegisterAssemblyTypes(repoAssembly).Where(t => t.Name.EndsWith("Repo") || t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            var dmnserviceAssembly = Assembly.Load("Newtouch.HIS.DomainServices");
            builder.RegisterAssemblyTypes(dmnserviceAssembly).Where(t => t.Name.EndsWith("DmnService")).AsImplementedInterfaces().InstancePerLifetimeScope();


            var appAssembly = Assembly.Load("Newtouch.HIS.Application");
            builder.RegisterAssemblyTypes(appAssembly).Where(t => t.Name.EndsWith("App")).Where(t => !string.IsNullOrEmpty(t.Namespace)).AsImplementedInterfaces().InstancePerLifetimeScope();
            #endregion

            //Đăng kýAutofacNhà cung cấp bộ lọc.
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            // Đặt bộ phân tích phụ thuộc-Dependency Resolver
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