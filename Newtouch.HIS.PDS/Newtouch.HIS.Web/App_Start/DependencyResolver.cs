using Autofac;
using Autofac.Integration.Mvc;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.Core.NLogger;
using Newtouch.Core.Redis.Cache;
using System.Reflection;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Web;

namespace Newtouch.HIS.Web.App_Start
{
    /// <summary>
    /// Cấu hình dependency injection
    /// </summary>
    public class DependencyResolver
    {
        /// <summary>
        /// 
        /// </summary>
        public static void RegisteWebApiComponent()
        {
            var builder = new ContainerBuilder();

            ////đăng ký mặc định EF ngữ cảnh
            builder.RegisterType<DefaultDatabaseFactory>().As<IDefaultDatabaseFactory>().InstancePerLifetimeScope();

            //Đăng ký controller
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.Load("FrameworkBase.MultiOrg.Web"));

            //nhật ký đăng ký
            //LoggerHelper.InitLog4net("/Configs/log4net.config");
            builder.RegisterType<NLogger>().As<ILogger>().InstancePerLifetimeScope();

            ////bộ nhớ đệm
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

            var repoAssembly = Assembly.Load("Newtouch.HIS.Repository");
            builder.RegisterAssemblyTypes(repoAssembly)
                .Where(t => t.Name.EndsWith("Repo") || t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            var dmnserviceAssembly = Assembly.Load("Newtouch.HIS.DomainServices");
            builder.RegisterAssemblyTypes(dmnserviceAssembly)
                .Where(t => t.Name.EndsWith("DmnService"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            var appAssembly = Assembly.Load("Newtouch.HIS.Application");
            builder.RegisterAssemblyTypes(appAssembly)
                .Where(t => t.Name.EndsWith("App"))
                .Where(t => !string.IsNullOrEmpty(t.Namespace))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

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