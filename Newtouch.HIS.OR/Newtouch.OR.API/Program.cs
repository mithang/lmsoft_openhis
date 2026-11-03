using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtouch.OR.API.Services;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.DomainServices;
using Newtouch.OR.ManageSystem.Repository;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
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


// BusinessDB + RedisConfig dùng chung file appsettings của OR Web (cùng deployment,
// cùng trust boundary; tránh copy credential sang file mới). Launcher vẫn override
// BusinessDB DefaultConn bằng env như Web (xem run-or-api.sh).
// bin/Debug/net6.0 -> up 4 = Newtouch.HIS.OR, then sibling Newtouch.OR.Web.
var webAppsettings = Path.GetFullPath(Path.Combine(
    AppContext.BaseDirectory, "..", "..", "..", "..", "Newtouch.OR.Web", "appsettings.json"));
if (File.Exists(webAppsettings))
{
    builder.Configuration.AddJsonFile(webAppsettings, optional: true, reloadOnChange: false);
}
else
{
    Console.WriteLine("WARNING: OR Web appsettings not found at " + webAppsettings
        + " — BusinessDB/RedisConfig will be missing.");
}
// Web file is added AFTER the default env provider, so without this line it would
// shadow launcher env overrides (e.g. BusinessDB__DefaultConn with the live SA
// password). Re-adding env vars last restores the normal precedence: env > files.
builder.Configuration.AddEnvironmentVariables();

builder.WebHost.UseUrls("http://127.0.0.1:5331");

builder.Services.AddLibLogging();

// Same as OR Web: AddWebServiceBase auto-registers Framework AppServices that
// require these two singletons; without them the container fails validation.
builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
builder.Services.AddWebServiceBase(builder.Configuration);

builder.Services.AddSingleton<TokenStore>();
builder.Services.AddScoped<ISysConfigRepo, SysConfigRepo>();
builder.Services.AddScoped<IOPRegisterDmnService, OPRegisterDmnService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
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
    options.SerializerSettings.Converters.Add(new Newtouch.OR.API.Converters.ViDateTimeJsonConverter());
    options.SerializerSettings.Converters.Add(new Newtouch.OR.API.Converters.ViNullableDateTimeJsonConverter());
}).ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

var app = builder.Build();
app.UseCors("AllowAll");
app.UseRouting();
app.MapControllers();
app.Run();
