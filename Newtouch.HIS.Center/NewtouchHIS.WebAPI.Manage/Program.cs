using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtonsoft.Json.Serialization;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Services;
using NewtouchHIS.Lib.Services.HttpService;
using Microsoft.AspNetCore.Mvc.Authorization;
using NewtouchHIS.Lib.Services.DrgGroup;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Services.SignalR;

var builder = WebApplication.CreateBuilder(args);

// --- Việt Nam hóa: culture mặc định vi-VN -------------------------------------
// Đặt DefaultThreadCurrentCulture/UICulture = vi-VN để:
//   * model binding parse ngày theo dd/MM/yyyy (thay vì en-US MM/dd/yyyy),
//   * decimal dùng dấu '.' phân cách hàng nghìn / ',' thập phân,
//   * ToString("d") và {0:d} render đúng định dạng Việt Nam.
// Không đổi AddControllers/AddControllersWithViews hiện có; nếu muốn thông báo
// lỗi model binding tiếng Việt thì bổ sung options.ModelBindingMessageProvider.
var vi = new System.Globalization.CultureInfo("vi-VN");
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = vi;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = vi;
// ---------------------------------------------------------------------------


// Add services to the container.
builder.Services.AddLibLogging();
builder.Services.AddHttpContextAccessor();

var config = builder.Configuration;
ConfigInitHelper.DbConfig = config.GetSection("BusinessDB").Get<BusinessDB>();
ConfigInitHelper.SysConfig = config.GetSection("BusinessConfig").Get<BusinessConfig>();
builder.Services.AddSingleton(new RedisHelper(config.GetSection("RedisConfig")));
builder.Services.AddSingleton(new AppSettings(builder.Configuration));

//Cấu hình dependency injection truy cập cơ sở dữ liệu
builder.Services.AddDependencyService();
//ThêmIApiUserSessionLớp triển khai
builder.Services.AddSingleton<IIdentityCache, IdentityCachePrincipal>();
builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IHttpWebRequestHelper, HttpWebRequestHelper>();

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
    options.Filters.Add(typeof(AppResultFilter));
    options.Filters.Add(typeof(LogTrackingAttribute));
}).AddNewtonsoftJson(
        options =>
        {
            //Bỏ qua tham chiếu vòng
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss.fff";//Xử lý toàn cục Định dạng thời gian trả về
            //options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;//Xử lý toàn cục Nhận thời gian và xử lý bản địa hóa
            //var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter
            //{
            //    DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            //}

            //options.SerializerSettings.Converters.Add(dateConverter);
            //Không dùng kiểu camelCasekey
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();

            //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();//Đặt tên camelCase viết thường chữ cái đầu
            //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; Xử lý giá trị rỗng
        }
);
//Mọi controller đều bật xác thực danh tính
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AuthorizeFilter());//MọiMVCdịch vụ mặc định thêm nhãn ủy quyền
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddSwaggerGenNewtonsoftSupport();

var jwtOptions = config.GetSection("SSOAuthIdentity")?.Get<List<SSOAuthIdentity>>();
builder.Services.JWTAuthentication(true, jwtOptions?.FirstOrDefault());

builder.Services.AddTransient<IDrgGroupService, DrgGroupService>();
builder.Services.AddTransient<IChsDrg11Policy, ChsDrg11Policy>();
builder.Services.AddTransient<IDrgWuhan2022Policy, DrgWuhan2022Policy>();

//Cấu hình dịch vụ cross-domain
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", p =>
    {
        p.WithOrigins(ConfigInitHelper.SysConfig.CorsWithOrigins!.Split(',')).AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();

    });
});
//ThêmSignalRCấu hình injection
//builder.Services.AddOhSignalR();
builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;//Thông tin ngoại lệ chi tiết
    o.HandshakeTimeout = TimeSpan.FromSeconds(30);//Thời gian quá hạn bắt tay
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region jwt
app.UseAuthentication();        //ThêmjwtXác thực 
//Code thực thi từ trên xuống dưới,ở giữa có thể thêm điều kiện xét, Vấn đề cài đặt quyền
app.UseAuthorization();   //Ủy quyền
#endregion
app.UseNewtouchExceptionHandler();
app.MapControllers();
app.UseCors("Cors");
//hubs/msgcenter
app.MapHub<HisNoticeHub>(config.GetSection("MessageCenter:Hub").Get<string>()).RequireCors("Cors"); ;

app.Run();
