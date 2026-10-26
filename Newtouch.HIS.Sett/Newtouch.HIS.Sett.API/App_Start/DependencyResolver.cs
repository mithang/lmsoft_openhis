using Autofac;
using Autofac.Integration.WebApi;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.Core.NLogger;
using Newtouch.Core.Redis.Cache;
using Newtouch.HIS.API.Common.Interface;
using Newtouch.HIS.API.Common.Models;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Newtouch.HIS.Sett.API.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class DependencyResolver
    {
        /// <summary>
        /// chú thích đưa vào web API bộ điều khiển / bộ lọc 
        /// </summary>
        /// <param name="config">HttpConfiguration</param>
        public static void RegisteWebApiComponent()
        {
            var builder = new ContainerBuilder();
            
            // đ ăng ký webAPIĐiều khiển thiết bị 
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // đ ăng ký mặc đ ịnhEFngữ cảnh
            builder.RegisterType<DefaultDatabaseFactory>().As<IDefaultDatabaseFactory>().InstancePerLifetimeScope();

            // nhật ký đ ăng ký 
            builder.RegisterType<NLogger>().As<ILogger>().InstancePerLifetimeScope();

            // bộ nhớ đ ệm 
            builder.RegisterType<SERedisCache>().As<ICache>().InstancePerLifetimeScope();

            //API bộ phân giải thân phận người dùng 
            builder.RegisterType<HISAPIUserIdentityResolver>().As<IUserIdentityResolver<Identity>>().InstancePerLifetimeScope();

            #region IOC注册底层
            builder.RegisterAssemblyTypes(typeof(FrameworkBase.MultiOrg.Repository.RepositoryBase<>).Assembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(FrameworkBase.MultiOrg.DmnService.DmnServiceBase).Assembly)
                .Where(t => t.Name.EndsWith("DmnService"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            #endregion

            #region IOC, then registe yours
            //Đường dẫn tuyệt đối nơi đặt DLL
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
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            #endregion

            // đăng ký bộ lọc Autofac cung cấp chương trình.
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            // Đặt bộ phân giải phụ thuộc -Dependency Resolver
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