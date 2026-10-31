using NewtouchHIS.AuthenticationCenter.Services;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Utilities;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base;

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
ConfigInitHelper.SysConfig = builder.Configuration.GetSection("BusinessConfig").Get<BusinessConfig>();
ConfigInitHelper.DbConfig = builder.Configuration.GetSection("BusinessDB").Get<BusinessDB>();
builder.Services.AddSingleton(new RedisHelper(builder.Configuration.GetSection("RedisConfig")));

//Cấu hình dependency injection truy cập cơ sở dữ liệu
builder.Services.AddDependencyService();

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
            //Không dùng kiểu camelCasekey
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


#region Ủy quyền xác thực
app.UseAuthentication();        //ThêmjwtXác thực 
//Code thực thi từ trên xuống dưới,ở giữa có thể thêm điều kiện xét, Vấn đề cài đặt quyền
app.UseAuthorization();   //Ủy quyền
#endregion

app.MapControllers();

app.Run();
