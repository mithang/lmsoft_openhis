using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base.Middleware;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Services.HttpService;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.DomainServices;
using Newtouch.MR.ManageSystem.Infrastructure;
using Newtouch.MR.ManageSystem.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://127.0.0.1:5232");

builder.Services.AddLibLogging();

builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
builder.Services.AddWebServiceBase(builder.Configuration);

// MRMS domain services
builder.Services.AddScoped<ICommonDmnService, CommonDmnService>();
builder.Services.AddScoped<IEMRDmnService, EMRDmnService>();
builder.Services.AddScoped<IMainRecordDmnService, MainRecordDmnService>();
builder.Services.AddScoped<IMrDeptDmnService, MrDeptDmnService>();
builder.Services.AddScoped<IMrFeeDmnService, MrFeeDmnService>();
builder.Services.AddScoped<IMrFeeRelDmnService, MrFeeRelDmnService>();
builder.Services.AddScoped<IPatientHistoryDmnService, PatientHistoryDmnService>();
builder.Services.AddScoped<IRecordDmnService, RecordDmnService>();
builder.Services.AddScoped<IZybrjbxxDmnService, ZybrjbxxDmnService>();

// MRMS repositories (incl. shared-dictionary repos re-created on the Base DB)
builder.Services.AddScoped<IMrbasyRepo, MrbasyRepo>();
builder.Services.AddScoped<IMrbasyrelcodeRepo, MrbasyrelcodeRepo>();
builder.Services.AddScoped<IMrbasyssRepo, MrbasyssRepo>();
builder.Services.AddScoped<IMrbasyzdRepo, MrbasyzdRepo>();
builder.Services.AddScoped<IMrdicblzdRepo, MrdicblzdRepo>();
builder.Services.AddScoped<IMrdicdeptRepo, MrdicdeptRepo>();
builder.Services.AddScoped<IMrreldeptRepo, MrreldeptRepo>();
builder.Services.AddScoped<IMrFeeRepo, MrFeeRepo>();
builder.Services.AddScoped<IMrFeeRelRepo, MrFeeRelRepo>();
builder.Services.AddScoped<ISysConfigRepo, SysConfigRepo>();
builder.Services.AddScoped<ISysDiagnosisRepo, SysDiagnosisRepo>();
builder.Services.AddScoped<ISysDepartmentRepo, SysDepartmentRepo>();

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
