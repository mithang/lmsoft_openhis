using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Services.HttpService;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Reflection;

// EMR.API host (Web API 2 -> ASP.NET Core 6), port 5335.
// Local re-implementation of the binary FrameworkBase.API.ApiControllerBase surface
// (see ApiControllerBase.cs); the API uses token auth, not the web session filter.
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


builder.WebHost.UseUrls("http://127.0.0.1:5335");

builder.Services.AddLibLogging();

builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
builder.Services.AddWebServiceBase(builder.Configuration);

// Redis for API token storage (SaveIdentity / Identity lookup).
var redisConn = builder.Configuration.GetValue<string>("RedisConfig:Connection");
if (!string.IsNullOrWhiteSpace(redisConn))
{
    builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConn));
}

// Same assembly scanning as the EMR web (Autofac *Repo/*DmnService/*App -> scoped).
foreach (var asmName in new[] { "Newtouch.EMR.Infrastructure", "NewtouchHIS.Base.Repository", "NewtouchHIS.Base.DomainService" })
{
    try
    {
        var asm = Assembly.Load(asmName);
        foreach (var type in asm.GetTypes().Where(t => t.IsClass && !t.IsAbstract &&
            (t.Name.EndsWith("Repo") || t.Name.EndsWith("DmnService") || t.Name.EndsWith("App"))))
        {
            foreach (var iface in type.GetInterfaces().Where(i =>
                i.Namespace != null && (i.Namespace.StartsWith("Newtouch.EMR") || i.Namespace.StartsWith("NewtouchHIS"))))
            {
                builder.Services.AddScoped(iface, type);
            }
        }
    }
    catch
    {
        // Assembly not present in this deployment; the EMR layer covers its own services.
    }
}

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Cycle 46: converter ngày JSON (mirror Sett.API vòng 44 / CIS.API vòng 45)
    // — JSON dd/MM/yyyy bind đúng; KHÔNG đổi Culture toàn cục (mọi response
    // đều qua CreateResponse với JsonConvert.SerializeObject mặc định nên
    // không bị ảnh hưởng, nhưng tránh mọi tác dụng phụ lên response).
    // F-API-02 (cycle 167, mẫu CIS.API/PDS.API): SuppressModelStateInvalidFilter
    // để lỗi deserialize JSON (body sai định dạng, Timestamp không hợp lệ) rơi
    // vào CommonExecute và trả envelope thay vì HTTP 400 problem-details trần
    // của [ApiController]; BljgDataController đã guard model null (xem action).
    options.SerializerSettings.Converters.Add(new Newtouch.EMR.API.Converters.ViDateTimeJsonConverter());
    options.SerializerSettings.Converters.Add(new Newtouch.EMR.API.Converters.ViNullableDateTimeJsonConverter());
}).ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);
var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.Run();
