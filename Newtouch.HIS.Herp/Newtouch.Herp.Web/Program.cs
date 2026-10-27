using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Web.Services;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base.Middleware;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Services.HttpService;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://127.0.0.1:5236");

builder.Services.AddLibLogging();

builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
builder.Services.AddWebServiceBase(builder.Configuration);
// PINNED (integration fix, proven by the DepartmentManage port):
// AddWebServiceBase's AddDependencyService scans bin/*.dll in raw directory
// order and registers each IDependency implementer under the FIRST assignable
// interface it meets. In some bin layouts that is IScopedDependency for
// LoginAppService, leaving ILoginAppService unregistered and breaking
// LoginController activation. Pin the correct mapping explicitly here.
builder.Services.AddScoped<ILoginAppService, LoginAppService>();
// Herp-local IPv4-forcing helper MUST be registered after AddWebServiceBase
// so it replaces the vendor HttpClientHelper (whose default dual-mode sockets
// are intercepted by this sandbox; see HerpHttpClientHelper docs).
builder.Services.AddSingleton<IHttpClientHelper, HerpHttpClientHelper>();

// Herp services: MS DI assembly scanning (replaces Autofac DependencyResolver).
// Repos (*Repo), domain services (*DmnService) and app services (*App) in the
// Herp infra assembly are registered scoped as their "I"+class interfaces.
builder.Services.AddHerpServices();

// Session is used by LoginController.OutLogin (Session.Clear) in the original.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
});

// Static config used by Herp Constants (TopOrganizeId / AppId), set once here.
Constants.Init(
    builder.Configuration["BusinessConfig:Top_OrganizeId"],
    builder.Configuration["BusinessConfig:AppId"]);
// Web.config-style appSettings reader for ported services (e.g. tjwzDaysAgo).
Constants.AppConfigReader = key =>
    builder.Configuration["BusinessConfig:" + key] ?? builder.Configuration[key];

builder.Services.AddControllersWithViews().AddMvcOptions(options =>
{
    options.Filters.Add(typeof(LogTrackingAttribute));
    options.Filters.Add(typeof(FrameworkAuthorizationFilter));
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseMiddleware<ExceptionHandleMiddleware>();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
