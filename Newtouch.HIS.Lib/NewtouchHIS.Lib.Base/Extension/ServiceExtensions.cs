using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using SqlSugar;
using System.Net;
using System.Reflection;
using System.Text;

namespace NewtouchHIS.Lib.Base.Extension
{
    public static class ServiceCollectionExt
    {
        #region config
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton(new RedisHelper(configuration.GetSection("RedisConfig")));

            ConfigInitHelper.DbConfig = configuration.GetSection("BusinessDB").Get<BusinessDB>();
            ConfigInitHelper.SysConfig = configuration.GetSection("BusinessConfig").Get<BusinessConfig>();
            #region đang kiểm tra 
            //services.AddSingleton(new AppSettings(configuration));
            //ConfigInitHelper.RedisConfig = configuration.GetSection("RedisConfig").Get<RedisDB>();
            #endregion
            return services;
        }

        #endregion

        #region IOC        
        public static IServiceCollection AddDependencyService(this IServiceCollection services)
        {
            #region tiêm phụ thuộc
            var baseType = typeof(IDependency);
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var getFiles = Directory.GetFiles(path, "*.dll");  //.Where(o=>o.Match())
            var referencedAssemblies = getFiles.Select(Assembly.LoadFrom).ToList();  //.Select(o=> Assembly.LoadFrom(o))         

            //var ss = referencedAssemblies.SelectMany(o => o.GetTypes());
            var types = referencedAssemblies
                        .SelectMany(a => a.DefinedTypes)
                        .Select(type => type.AsType())
                        .Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToList();
            var implementTypes = types.Where(x => x.IsClass).ToList();
            var interfaceTypes = types.Where(x => x.IsInterface).ToList();
            foreach (var implementType in implementTypes)
            {
                if (typeof(IScopedDependency).IsAssignableFrom(implementType))
                {
                    var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                    if (interfaceType != null)
                        services.AddScoped(interfaceType, implementType);
                }
                else if (typeof(ISingletonDependency).IsAssignableFrom(implementType))
                {
                    var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                    if (interfaceType != null)
                        services.AddSingleton(interfaceType, implementType);
                }
                else
                {
                    var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                    if (interfaceType != null)
                        services.AddTransient(interfaceType, implementType);
                }
            }
            //AddAutoMapDependencyService(services, referencedAssemblies);
            AddMapSterDependencyService(services, referencedAssemblies);
            return services;
            #endregion
        }

        /// <summary>
        /// automapper tiêm vào
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapDependencyService(this IServiceCollection services, List<Assembly> assemblies)
        {
            var baseType = typeof(Profile);
            var profiles = assemblies.SelectMany(a => a.DefinedTypes)
                        .Select(type => type.AsType()).Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToArray();
            //services.AddAutoMapper(typeof(MappingProfile));
            //services.AddAutoMapper(profiles);
            return services;
        }
        /// <summary>
        /// Mapstertiêm vào
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMapSterDependencyService(this IServiceCollection services, List<Assembly> assemblies)
        {
            var config = new TypeAdapterConfig();
            // Or
            // var config = TypeAdapterConfig.GlobalSettings;
            services.AddSingleton(config);
            var baseType = typeof(IRegister);
            var profiles = assemblies.SelectMany(a => a.DefinedTypes)
                  .Select(type => type.AsType()).Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToArray();
            config.Scan(assemblies.ToArray());

            //services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
        #endregion

        #region JWT
        /// <summary>
        /// jwtcấu hình logic xác thực
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enable"></param>
        /// <param name="jwtOptions"></param>
        /// ủy quyềnAPI lấy thông tin tải trọng
        /// base.HttpContext.AuthenticatAsync().Result.Principal.Claims
        /// <returns></returns>
        public static IServiceCollection JWTAuthentication(this IServiceCollection services, bool enable, SSOAuthIdentity jwtOptions)
        {
            if (enable)
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,//có xác minh bên phát hành hay không
                        ValidateAudience = false,//có xác minh đối tượng nhận hay không
                        ValidateLifetime = true,//có xác minh thời gian hết hiệu lực hay không
                        ValidateIssuerSigningKey = true,//có xác minh khóa ký hay không
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.AppSecret)),
                        AudienceValidator = (m, n, z) => { return m != null; },
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => { return true; },
                    };
                    options.Events = new JwtBearerEvents
                    {
                        //OnAuthenticationFailed = context => { 

                        //},
                        //kích hoạt khi xác minh quyền thất bại
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = StatusCodes.Status200OK;
                            var data = new Result
                            {
                                HttpStatus = HttpStatusCode.Unauthorized,
                                Message = $"token校验失败:{context.Error}",   //{context.AuthenticateFailure?.Message}

                            };
                            context.Response.WriteAsync(data.ToJson());
                            return Task.FromResult(0);
                        },
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Headers["Authorization"].ToString();
                            var path = context.HttpContext.Request.Path;
                            var msgurl = AppSettings.GetConfig("MessageCenter:Hub");
                            if (path.StartsWithSegments(msgurl))
                            {
                                if (string.IsNullOrWhiteSpace(accessToken) && context.Request.Cookies.ContainsKey("accesstoken"))
                                {
                                    var querys = context.Request.Cookies.Where(p => p.Key.Contains("accesstoken")).FirstOrDefault();
                                    accessToken = querys.Value;
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(accessToken) && (path.StartsWithSegments(msgurl)))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            }
            return services;
        }


        #endregion

        #region SqlSugar
        public static IServiceCollection AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var BusinessDBConf = configuration.GetSection("BusinessDB").Get<BusinessDB>();
            if (BusinessDBConf != null && BusinessDBConf.DBConf != null && BusinessDBConf.DBConf.Count > 0 && !string.IsNullOrWhiteSpace(BusinessDBConf.MainDB))
            {
                var configConnection = new List<ConnectionConfig>();
                BusinessDBConf.DBConf.ForEach(option =>
                {
                    configConnection.Add(new ConnectionConfig()
                    {
                        ConfigId = option.ConnId/*.ToLower()*/,
                        ConnectionString = option.DBConn,
                        DbType = (DbType)option.DBType,
                        IsAutoCloseConnection = true,
                        MoreSettings = new ConnMoreSettings()
                        {
                            IsAutoRemoveDataCache = true,
                            IsWithNoLockQuery = true,
                            DisableNvarchar = true,
                        }
                        //InitKeyType = InitKeyType.SystemTable
                    });
                });


                services.AddSingleton<ISqlSugarClient>(o =>
                {
                    //thêm lặp đa thuê baoAOP
                    SqlSugarScope sqlSugar = new SqlSugarScope(configConnection);
                    configConnection.ForEach(c =>
                    {
                        SqlSugarScopeProvider client = sqlSugar.GetConnectionScope(c.ConfigId);
                        //mỗi lầnSqlsự kiện trước khi thực hiện
                        client.Aop.OnLogExecuting = (sql, pars) =>
                        {
                            //var queryString = new KeyValuePair<string, SugarParameter[]>(sql, pars);
                            //if (sql.StartsWith("UPDATE") || sql.StartsWith("INSERT"))
                            //{
                            //    Console.ForegroundColor = ConsoleColor.Blue;
                            //    Console.WriteLine($"==============thêm mới/thao tác sửa đổi==============");
                            //    Console.WriteLine(ToSqlExplain.GetSql(queryString));
                            //}
                            //if (sql.StartsWith("DELETE"))
                            //{
                            //    Console.ForegroundColor = ConsoleColor.Red;
                            //    Console.WriteLine($"==============thao tác xóa==============");
                            //}
                            //if (sql.StartsWith("SELECT"))
                            //{
                            //    Console.ForegroundColor = ConsoleColor.Green;
                            //    Console.WriteLine($"==============thao tác truy vấn==============");
                            //}
                            //Console.WriteLine(ToSqlExplain.GetSql(queryString));
                            //Console.ForegroundColor = ConsoleColor.White;
                        };
                        //mỗi lầnSqlsự kiện sau khi thực hiện
                        client.Aop.OnLogExecuted = (sql, pars) =>
                        {
                            //thời gian thực hiện vượt quá10giây
                            if (client.Ado.SqlExecutionTime.TotalSeconds > 5)
                            {
                                Console.WriteLine(sql);
                            }
                        };

                        ////SQLbáo lỗi
                        //client.Aop.OnError = (exp) =>
                        //{
                        //    var data = new Result
                        //    {
                        //        HttpStatus = HttpStatusCode.ServiceUnavailable,
                        //        Message = exp!.Message,
                        //        BusData = exp!.Sql

                        //    };
                        //};
                    });
                    return sqlSugar;
                });
            }
            return services;
        }


        #endregion

        #region Swagger
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            #region thêmswaggerchú thích
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo
                //{
                //    Version = "v1",
                //    Title = "OpenHIStổng hợp mã nguồn mởApi"
                //});
                c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{AppDomain.CurrentDomain.FriendlyName}.xml"), true);//WebAPIdự ánXMLtệp
                //c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SugarProjectCore.xml"), true);//những thứ các dự án khác cầnXMLtệp

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Value: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                  {
                    new OpenApiSecurityScheme
                    {
                      Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                      },Scheme = "oauth2",Name = "Bearer",In = ParameterLocation.Header,
                    },new List<string>()
                  }
                });
            });
            return services;
            #endregion
        }
        #endregion


        /// <summary>
        /// thêm nhật ký
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLibLogging(this IServiceCollection services)
        {
            services.AddLogging(options =>
            {
                options.AddLog4Net("Configs/Log4Net.config");
                options.AddConsole();
            });
            return services;
        }



        /// <summary>
        /// Web thành phần tùy chỉnh chung
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebServiceBase(this IServiceCollection services, IConfiguration config)
        {
            ConfigServiceBase(services, config);
            services.AddDependencyService();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //HttpContext thành phần truy cập
            HttpWebHelper.servicesCollection = services;
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config.GetConnectionString("RedisConfig:Connection");
                options.InstanceName = "OpenhisInstance";
            });

            return services;
        }

        /// <summary>
        /// giao diện thành phần tùy chỉnh chung
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsService(this IServiceCollection services)
        {
            //cấu hình dịch vụ đa miền
            services.AddCors(options =>
            {
                options.AddPolicy("Cors", p =>
                {
                    p.WithOrigins(ConfigInitHelper.SysConfig.CorsWithOrigins!.Split(',')).AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                });
            });
            return services;
        }
        public static IServiceCollection AddInterfaceServiceBase(this IServiceCollection services, IConfiguration config)
        {
            ConfigServiceBase(services, config);

            services.AddLibLogging();
            services.AddHttpContextAccessor();
            services.AddDependencyService();
            return services;
        }



        #region private
        private static IServiceCollection ConfigServiceBase(this IServiceCollection services, IConfiguration config)
        {
            #region cấu hình toàn cục
            ConfigInitHelper.DbConfig = config.GetSection("BusinessDB").Get<BusinessDB>();
            ConfigInitHelper.SysConfig = config.GetSection("BusinessConfig").Get<BusinessConfig>();
            ConfigInitHelper.AccessSetting = config.GetSection("BusinessConfig").Get<AccessConfig>();
            services.AddSingleton(new RedisHelper(config.GetSection("RedisConfig")));
            services.AddSingleton(new AppSettings(config));
            #endregion
            return services;
        }
        #endregion

        #region tạm dừng sử dụng
        public static IServiceCollection AddSqlsugarBaseDB(this IServiceCollection services, IConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(ConfigInitHelper.DbConfig!.DefaultConn) && ConfigInitHelper.DbConfig.DBConf.Count > 0)
            {
                return services;
            }
            var dbMain = ConfigInitHelper.DbConfig.DBConf.FirstOrDefault();
            //đăng kýSqlSugar
            services.AddSingleton<ISqlSugarClient>(s =>
            {
                SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.SqlServer,
                    ConnectionString = string.Format(ConfigInitHelper.DbConfig.DefaultConn ?? "", dbMain!.DBName),
                    IsAutoCloseConnection = true,
                    ConfigId = dbMain!.ConnId ?? "1"
                },
               db =>
               {
                   //cấu hình tham số đơn lẻ，có hiệu lực với mọi ngữ cảnh
                   db.Aop.OnLogExecuting = (sql, pars) =>
                   {
                       //lấy tácIOCđối tượng phạm vi
                       var appServive = s.GetService<IHttpContextAccessor>();
                       var obj = appServive?.HttpContext?.RequestServices.GetService<ILogger>();
                       Console.WriteLine("AOP" + obj!.GetHashCode());
                   };
               });
                return sqlSugar;
            });
            //thư viện nghiệp vụ được thêm động，chỉ cần tiêm thư viện chính là được
            return services;
        }

        #region IOC
        /// <summary>
        /// tiêm tầng dịch vụ
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyString">tên assembly（dùng tải assembly bằng phản chiếu）</param>
        /// <param name="endsWithName">chuẩn hóa tên kết thúc lớp tầng dịch vụ，mặc định theoServicekết thúc，bỏ qua chữ hoa chữ thường</param>
        /// <returns></returns>
        public static IServiceCollection ServiceInjection(this IServiceCollection services, string assemblyString = "", string endsWithName = "")
        {
            if (string.IsNullOrWhiteSpace(assemblyString) || string.IsNullOrWhiteSpace(endsWithName))
            {
                var dic = services.BuildServiceProvider().GetService<IOptions<DependencyInjectionConfigure>>();
                if (dic != null && dic.Value != null)
                {
                    assemblyString = string.IsNullOrWhiteSpace(assemblyString) ? dic.Value.ServiceAssemblyName : assemblyString;
                    endsWithName = string.IsNullOrWhiteSpace(endsWithName) ? dic.Value.ServiceEndsWith : endsWithName;
                }
                endsWithName = string.IsNullOrWhiteSpace(endsWithName) ? "Service" : endsWithName;
            }
            if (string.IsNullOrWhiteSpace(assemblyString))
            {
                return services;
            }
            try
            {
                var serviceAssembly = Assembly.Load(assemblyString);
                var serviceList = serviceAssembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType && t.Name.EndsWith(endsWithName, StringComparison.OrdinalIgnoreCase)).ToList();
                foreach (var type in serviceList)
                {
                    var interFaceList = type.GetInterfaces();
                    if (interFaceList.Any())
                    {
                        foreach (var i in interFaceList)
                        {
                            services.AddScoped(i, type);
                        }
                        //services.AddScoped(interFaceList.Where(p => p.Name.EndsWith(endsWithName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault(), type);
                    }
                }
            }
            catch (Exception)
            {

            }
            return services;
        }

        /// <summary>
        /// tiêm tầng kho
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyString">tên assembly（dùng tải assembly bằng phản chiếu）</param>
        /// <param name="endsWithName">chuẩn hóa tên kết thúc lớp tầng kho，mặc định theoRepositorykết thúc，bỏ qua chữ hoa chữ thường</param>
        /// <returns></returns>
        public static IServiceCollection RepositoryInjection(this IServiceCollection services, string assemblyString = "", string endsWithName = "")
        {
            if (string.IsNullOrWhiteSpace(assemblyString) || string.IsNullOrWhiteSpace(endsWithName))
            {
                var dic = services.BuildServiceProvider().GetService<IOptions<DependencyInjectionConfigure>>();
                if (dic != null && dic.Value != null)
                {
                    assemblyString = string.IsNullOrWhiteSpace(assemblyString) ? dic.Value.RepositoryAssemblyName : assemblyString;
                    endsWithName = string.IsNullOrWhiteSpace(endsWithName) ? dic.Value.RepositoryEndsWith : endsWithName;
                }
                endsWithName = string.IsNullOrWhiteSpace(endsWithName) ? "Repository" : endsWithName;
            }
            if (string.IsNullOrWhiteSpace(assemblyString))
            {
                return services;
            }

            var serviceAssembly = Assembly.Load(assemblyString);
            var repositoryList = serviceAssembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType && t.Name.EndsWith(endsWithName, StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var type in repositoryList)
            {
                var interFaceList = type.GetInterfaces();
                if (interFaceList.Any())
                {
                    services.AddScoped(interFaceList.FirstOrDefault(p => p.Name.EndsWith(endsWithName, StringComparison.OrdinalIgnoreCase)), type);
                }
            }
            return services;
        }

        #endregion
        #endregion
    }
}