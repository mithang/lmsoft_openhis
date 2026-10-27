using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtouch.HIS.Base.HOSP.Web.Hubs;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base.Middleware;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Services.HttpService;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://127.0.0.1:5233");

builder.Services.AddLibLogging();

builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
// Explicit: AddWebServiceBase's bin scan does not pick up Framework.Web's
// LoginAppService in this app (MRQC resolves it, but the scan's assembly set
// differs here); register it directly so LoginController can activate.
builder.Services.AddScoped<ILoginAppService, LoginAppService>();
builder.Services.AddWebServiceBase(builder.Configuration);

// Base Infra services (mirror of the old Autofac assembly scan): every concrete
// Infra class is registered as its Newtouch.HIS.* interfaces. Exact-name matches
// ("I" + class name) win; remaining interfaces are registered only if no exact
// match claimed them (e.g. SysMedicineRepoBase -> ISysMedicineBaseRepo).
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
builder.Services.AddControllersWithViews().AddMvcOptions(options =>
{
    options.Filters.Add(typeof(LogTrackingAttribute));
    options.Filters.Add(typeof(FrameworkAuthorizationFilter));
});
builder.Services.AddSignalR();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseMiddleware<ExceptionHandleMiddleware>();
app.UseAuthorization();

app.MapHub<NoticeHub>("/signalr");

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
