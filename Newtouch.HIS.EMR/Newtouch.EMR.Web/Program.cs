using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base.Middleware;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Services.HttpService;
using System;
using System.Linq;
using System.Reflection;

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


builder.WebHost.UseUrls("http://127.0.0.1:5235");

builder.Services.AddLibLogging();

builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
builder.Services.AddWebServiceBase(builder.Configuration);
EmrConfig.Config = builder.Configuration;

// EMR services: assembly scanning (mirrors the old Autofac scanning by name suffix:
// *Repo / *DmnService / *App -> AsImplementedInterfaces, scoped).
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

/// <summary>
/// Static configuration shim replacing Framework ConfigurationHelper.GetAppConfigValue.
/// Reads the AppSettings section of appsettings.json (populated from the old
/// system.config), falling back to BusinessConfig keys and then empty string.
/// </summary>
public static class EmrConfig
{
    public static IConfiguration Config { get; set; }

    public static string GetAppConfigValue(string key)
    {
        if (Config == null) return "";
        return Config["AppSettings:" + key]
            ?? Config["BusinessConfig:" + key]
            ?? Config[key]
            ?? "";
    }

    public static string TopOrganizeId
    {
        get
        {
            if (Config == null) return "";
            return Config["BusinessConfig:Top_OrganizeId"] ?? "";
        }
    }
}
