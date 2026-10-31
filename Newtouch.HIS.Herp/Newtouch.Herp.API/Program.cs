using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtouch.Herp.API.Common;
using Newtouch.Herp.API.Services;
using Newtouch.Herp.Infrastructure;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Middleware;
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


// TEMP port for this copy: 5336. Final port after parent integration is 5336:
// change this line AND ASPNETCORE_URLS in run-5336.sh (renamed to the final
// run script) to http://127.0.0.1:5336. Never 5236 (parent Herp web app).
builder.WebHost.UseUrls("http://127.0.0.1:5336");

builder.Services.AddLibLogging();

builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddWebServiceBase(builder.Configuration);
// Herp-local IPv4-forcing helper MUST be registered after AddWebServiceBase
// so it replaces the vendor HttpClientHelper (whose default dual-mode sockets
// are intercepted by this sandbox; see HerpHttpClientHelper docs). Any future
// server-to-server HTTP call from this host MUST go through the injected
// IHttpClientHelper (this implementation) — never a default HttpClient.
builder.Services.AddSingleton<IHttpClientHelper, HerpHttpClientHelper>();

// Herp services: MS DI assembly scanning (same as the Herp web app).
builder.Services.AddHerpServices();

// Token store / identity resolver for the API base (Redis, 20-minute tokens).
builder.Services.AddSingleton<ITokenStore, RedisTokenStore>();
builder.Services.AddScoped<IUserIdentityResolver<Identity>, UserIdentityResolver>();

// Static config used by Herp Constants (TopOrganizeId / AppId), set once here.
Constants.Init(
    builder.Configuration["BusinessConfig:Top_OrganizeId"],
    builder.Configuration["BusinessConfig:AppId"]);
Constants.AppConfigReader = key =>
    builder.Configuration["BusinessConfig:" + key] ?? builder.Configuration[key];

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    // Mirrors the original WebApiConfig EnableCors("*", "*", "*").
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseRouting();
app.UseCors();
app.UseMiddleware<ExceptionHandleMiddleware>();
app.UseAuthorization();

app.MapControllers();

// Liveness endpoint (the original API host had no UI and no health action;
// this is additive and does not change any original route).
app.MapGet("/", () => Results.Json(new
{
    service = "Newtouch.Herp.API",
    status = "ok",
    port = 5336,
    note = "Final port after parent integration: 5336"
}));

app.Run();
