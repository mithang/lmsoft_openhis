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
using Newtouch.Sett.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://127.0.0.1:5223");

builder.Services.AddLibLogging();

builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
builder.Services.AddWebServiceBase(builder.Configuration);

// Sett area services (added at merge time; staging apps register their own area services here)
// HospitalizationManage (staging, port 5223): real impls = SysConfigRepo,
// PatientBasicInfoDmnService, HospSettApp (fee-classify list), settlement/item/
// drug-billing/multi-diagnosis repos; everything else is an explicit-error stub.
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysConfigRepo, Newtouch.Sett.Domain.SysConfigRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysStaffRepo, Newtouch.Sett.Domain.SysStaffRepoStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysUserDmnService, Newtouch.Sett.Domain.SysUserDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IMoneyUpperLimitReminderDmnService, Newtouch.Sett.Domain.MoneyUpperLimitReminderDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISyncTreatmentServiceRecordDmnService, Newtouch.Sett.Domain.SyncTreatmentServiceRecordDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IRefundDmnService, Newtouch.Sett.Domain.RefundDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IPatientBasicInfoDmnService, Newtouch.Sett.Domain.PatientBasicInfoDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospSettApp, Newtouch.Sett.Domain.HospSettApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospSettCancelApp, Newtouch.Sett.Domain.HospSettCancelAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospSettDmnService, Newtouch.Sett.Domain.HospSettDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IBookkeepInpatientApp, Newtouch.Sett.Domain.BookkeepInpatientAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IBookkeepInHosDmnService, Newtouch.Sett.Domain.BookkeepInHosDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IOutPatientDmnService, Newtouch.Sett.Domain.OutPatientDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IOutPatientApp, Newtouch.Sett.Domain.OutPatientAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IOutPatChargeApp, Newtouch.Sett.Domain.OutPatChargeAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysPatiChargeAddApp, Newtouch.Sett.Domain.SysPatiChargeAddAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysPatiChargeWaiverApp, Newtouch.Sett.Domain.SysPatiChargeWaiverAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospItemFeeApp, Newtouch.Sett.Domain.HospItemFeeAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospMedicinFeeApp, Newtouch.Sett.Domain.HospMedicinFeeAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHosPatAccDmnService, Newtouch.Sett.Domain.HosPatAccDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IAccountManageApp, Newtouch.Sett.Domain.AccountManageAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IItemDmnService, Newtouch.Sett.Domain.ItemDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysChargeTemplateRepo, Newtouch.Sett.Domain.SysChargeTemplateRepoStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysChargeTemplateItemMappRepo, Newtouch.Sett.Repository.SysChargeTemplateItemMappRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospPatientBasicInfoRepo, Newtouch.Sett.Repository.HospPatientBasicInfoRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospItemBillingRepo, Newtouch.Sett.Repository.HospItemBillingRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospDrugBillingRepo, Newtouch.Sett.Repository.HospDrugBillingRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospSettlementRepo, Newtouch.Sett.Repository.HospSettlementRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospAccountingPlanDetailRepo, Newtouch.Sett.Repository.HospAccountingPlanDetailRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospMultiDiagnosisRepo, Newtouch.Sett.Repository.HospMultiDiagnosisRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ICqybSett05Repo, Newtouch.Sett.Repository.CqybSett05Repo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospSettlementGAYBZYMXXRFeeRepo, Newtouch.Sett.Repository.HospSettlementGAYBZYMXXRFeeRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospSimulateSettlementGAYBFeeRepo, Newtouch.Sett.Repository.HospSimulateSettlementGAYBFeeRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospFeeDmnService, Newtouch.Sett.Domain.HospFeeDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IDischargeSettleApp, Newtouch.Sett.Domain.DischargeSettleAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IDischargeSettleDmnService, Newtouch.Sett.Domain.DischargeSettleDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IInpatientRefundApp, Newtouch.Sett.Domain.InpatientRefundAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IInpatientRefundDmnService, Newtouch.Sett.Domain.InpatientRefundDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospitalRefundApp, Newtouch.Sett.Domain.HospitalRefundAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IBookingDmnService, Newtouch.Sett.Domain.BookingDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IInpatientApp, Newtouch.Sett.Domain.InpatientAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IPayApp, Newtouch.Sett.Domain.PayAppStub>();

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
