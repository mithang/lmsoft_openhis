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
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.DomainServices;
using Newtouch.OR.ManageSystem.Repository;

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


builder.WebHost.UseUrls("http://127.0.0.1:5231");

builder.Services.AddLibLogging();

builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
builder.Services.AddWebServiceBase(builder.Configuration);

// OR domain services
builder.Services.AddScoped<ICommonDmnService, CommonDmnService>();
builder.Services.AddScoped<IOPApplyDmnService, OPApplyDmnService>();
builder.Services.AddScoped<IOPRegisterDmnService, OPRegisterDmnService>();
builder.Services.AddScoped<IOpArrangementDmnService, OpArrangementDmnService>();
builder.Services.AddScoped<ISysChargeItemDmnService, SysChargeItemDmnService>();

// OR repositories (incl. the local SystemManage replacements against Base DB)
builder.Services.AddScoped<IORApplyInfoRepo, ORApplyInfoRepo>();
builder.Services.AddScoped<IORApplyInfoExpandRepo, ORApplyInfoExpandRepo>();
builder.Services.AddScoped<IORArrangementRepo, ORArrangementRepo>();
builder.Services.AddScoped<IORRegistrationRepo, ORRegistrationRepo>();
builder.Services.AddScoped<IOROperationRepo, OROperationRepo>();
builder.Services.AddScoped<IORRoomRepo, ORRoomRepo>();
builder.Services.AddScoped<IORStaffRepo, ORStaffRepo>();
builder.Services.AddScoped<IORAnesthesiaRepo, ORAnesthesiaRepo>();
builder.Services.AddScoped<IORNotchGradeRepo, ORNotchGradeRepo>();
builder.Services.AddScoped<IOROpStaffRecordRepo, OROpStaffRecordRepo>();
builder.Services.AddScoped<ITemporary_ordersERepo, Temporary_ordersRepo>();
builder.Services.AddScoped<ISysConfigRepo, SysConfigRepo>();
builder.Services.AddScoped<ISysStaffRepo, SysStaffRepo>();
builder.Services.AddScoped<ISysDiagnosisRepo, SysDiagnosisRepo>();

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
