using NewtouchHIS.AuthenticationCenter.Services;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Utilities;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigInitHelper.SysConfig = builder.Configuration.GetSection("BusinessConfig").Get<BusinessConfig>();
ConfigInitHelper.DbConfig = builder.Configuration.GetSection("BusinessDB").Get<BusinessDB>();
builder.Services.AddSingleton(new RedisHelper(builder.Configuration.GetSection("RedisConfig")));

//Cau hinh dependency injection de truy cap co so du lieu
builder.Services.AddDependencyService();

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
    options.Filters.Add(typeof(AppResultFilter));
    options.Filters.Add(typeof(LogTrackingAttribute));
}).AddNewtonsoftJson(
        options =>
        {
            //Bo qua tham chieu vong
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss.fff";//Xu ly toan cuc dinh dang thoi gian tra ve
            //Khong su dung kieu camelCase chokey
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        }
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenNewtonsoftSupport();
var jwtOptions = builder.Configuration.GetSection("JWT");
builder.Services.Configure<JWTOptions>(jwtOptions);
builder.Services.AddTransient<IJWTService, HSJWTService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


#region Xac thuc - Uy quyen
app.UseAuthentication();        //themjwtxac thuc 
//Ma thuc thi tu tren xuong duoi£¬o giua co the them phan doan£¬ van de cai dat quyen
app.UseAuthorization();   //uy quyen
#endregion

app.MapControllers();

app.Run();
