using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtouch.HIS.Base.HOSP.API.Hubs;
using Newtouch.HIS.Base.HOSP.API.Services;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base.Middleware;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Services.HttpService;

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


builder.WebHost.UseUrls("http://127.0.0.1:5333");

builder.Services.AddLibLogging();

builder.Services.AddSingleton<NewtouchHIS.Lib.Services.HttpService.IHttpClientHelper, NewtouchHIS.Lib.Services.HttpService.HttpClientHelper>();
builder.Services.AddSingleton<NewtouchHIS.Framework.Web.Implementation.IAuthCenterAppService, NewtouchHIS.Framework.Web.Implementation.AuthCenterAppService>();
// Khởi tạo ConfigInitHelper (BusinessConfig/BusinessDB) như các web app:
// nếu không, static ctor của HandlerAuthorizeAttribute (Lib.Framework) NRE
// vì SysConfig null khi MVC build action descriptors.
builder.Services.AddWebServiceBase(builder.Configuration);

builder.Services.AddSingleton<TokenStore>();

// Base Infra services (same two-pass scan as the web apps): every concrete
// Infra class is registered as its Newtouch.HIS.* interfaces.
var infraAsm = typeof(Newtouch.HIS.Domain.FirstSecond).Assembly;
var claimed = new System.Collections.Generic.HashSet<System.Type>();
foreach (var type in infraAsm.GetTypes())
{
    if (!type.IsClass || type.IsAbstract || type.IsGenericTypeDefinition)
    {
        continue;
    }
    foreach (var iface in type.GetInterfaces())
    {
        if (iface.Namespace != null && iface.Namespace.StartsWith("Newtouch.HIS.")
            && iface.Name == "I" + type.Name)
        {
            builder.Services.AddScoped(iface, type);
            claimed.Add(iface);
        }
    }
}
foreach (var type in infraAsm.GetTypes())
{
    if (!type.IsClass || type.IsAbstract || type.IsGenericTypeDefinition)
    {
        continue;
    }
    foreach (var iface in type.GetInterfaces())
    {
        if (iface.Namespace != null && iface.Namespace.StartsWith("Newtouch.HIS.")
            && !claimed.Contains(iface))
        {
            builder.Services.AddScoped(iface, type);
            claimed.Add(iface);
        }
    }
}
builder.Services.AddSingleton<Newtouch.HIS.Repository.ICache, Newtouch.HIS.Repository.MemoryICache>();
// F-API-02 (cycle 167, mẫu CIS.API/PDS.API): [ApiController] mặc định chặn request bằng
// HTTP 400 problem-details trước khi vào action, và System.Text.Json chỉ parse
// Timestamp dạng ISO 8601. Formatter gốc của Web API Framework là Newtonsoft:
// chấp nhận cả "yyyy-MM-dd HH:mm:ss" (định dạng RequestBase mô tả) lẫn ISO 8601.
// SuppressModelStateInvalidFilter để lỗi deserialize (Timestamp thật sự không
// hợp lệ) rơi vào CommonExecute và trả envelope SOMEARGUMENTS_IS_REQUIRED thay
// vì 400 trần; Timestamp thiếu/null vẫn trả TIMESTAMP_IS_REQUIRED như cũ.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Converter ngày JSON (cycle 167, mirror CIS.API vòng 45) — JSON dd/MM/yyyy
    // bind đúng; KHÔNG đổi Culture toàn cục (CreateResponse serialize bằng
    // settings riêng, nhưng tránh mọi tác dụng phụ lên response).
    options.SerializerSettings.Converters.Add(new Newtouch.HIS.Base.HOSP.API.Converters.ViDateTimeJsonConverter());
    options.SerializerSettings.Converters.Add(new Newtouch.HIS.Base.HOSP.API.Converters.ViNullableDateTimeJsonConverter());
}).ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);
builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();

app.MapHub<NoticeHub>("/signalr");
app.MapControllers();

app.Run();
