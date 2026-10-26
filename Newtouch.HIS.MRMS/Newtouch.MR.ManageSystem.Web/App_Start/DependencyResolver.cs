using Autofac;
using Autofac.Integration.Mvc;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.Core.NLogger;
using Newtouch.Core.Redis.Cache;
using System.Reflection;
using System.Web;

namespace Newtouch.MR.ManageSystem.Web.App_Start
{
    /// <summary>
    /// Cấu hình dependency injection
    /// </summary>
    public class DependencyResolver
    {
        /// <summary>
        /// Cấu hình dependency injection
        /// </summary>
        public static void RegisteWebApiComponent()
        {
            var builder = new ContainerBuilder();

            //Đăng ký ngữ cảnh EF mặc định
            builder.RegisterType<DefaultDatabaseFactory>().As<IDefaultDatabaseFactory>().InstancePerLifetimeScope();

            //Đăng ký bộ điều khiển
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.Load("FrameworkBase.MultiOrg.Web"));

            //Đăng ký nhật ký
            builder.RegisterType<NLogger>().As<ILogger>().InstancePerLifetimeScope();

            //Bộ nhớ đệm
            builder.RegisterType<SERedisCache>().As<ICache>().InstancePerLifetimeScope();

            #region IOC注册底层
            builder.RegisterAssemblyTypes(typeof(FrameworkBase.MultiOrg.Repository.RepositoryBase<>).Assembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(FrameworkBase.MultiOrg.DmnService.DmnServiceBase).Assembly)
                .Where(t => t.Name.EndsWith("DmnService"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(FrameworkBase.MultiOrg.Application.AppBase).Assembly)
                .Where(t => t.Name.EndsWith("App"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            #endregion

            #region IOC, then registe yours
            //DLLđường dẫn tuyệt đối nơi đặt
            var repoAssembly = Assembly.Load("Newtouch.MR.ManageSystem.Repository");
            builder.RegisterAssemblyTypes(repoAssembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            var dmnserviceAssembly = Assembly.Load("Newtouch.MR.ManageSystem.DomainServices");
            builder.RegisterAssemblyTypes(dmnserviceAssembly)
                .Where(t => t.Name.EndsWith("DmnService"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            var appAssembly = Assembly.Load("Newtouch.MR.ManageSystem.Application");
            builder.RegisterAssemblyTypes(appAssembly)
                .Where(t => t.Name.EndsWith("App"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            #endregion

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            System.Web.Mvc.DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Configure Deafult Instance Resolver
            DependencyDefaultInstanceResolver.Register(interfaceType =>
            {
                if (HttpContext.Current != null)
                {
                    return System.Web.Mvc.DependencyResolver.Current.GetService(interfaceType);
                }
                return null;
            });

        }
    }
}