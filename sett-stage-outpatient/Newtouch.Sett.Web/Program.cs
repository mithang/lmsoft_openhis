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
using Newtouch.Sett.Domain;
using Newtouch.Sett.DomainServices;
using Newtouch.Sett.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://127.0.0.1:5222");

builder.Services.AddLibLogging();

builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
builder.Services.AddWebServiceBase(builder.Configuration);

// Login chain: AddDependencyService's bin scan registers only the FIRST
// matching interface per class, which is directory-order dependent and missed
// ILoginAppService in this bin (MRQC's bin happened to pick it). Register
// explicitly so login resolves deterministically (last registration wins).
builder.Services.AddScoped<ILoginAppService, LoginAppService>();
builder.Services.AddScoped<NewtouchHIS.Base.Domain.Organize.ISysUserVDmnService, NewtouchHIS.Base.DomainService.SysUserVDmnService>();
builder.Services.AddScoped<NewtouchHIS.Base.Domain.Organize.ISysStaffDmnService, NewtouchHIS.Base.DomainService.SysStaffDmnService>();
builder.Services.AddScoped<NewtouchHIS.Base.Domain.Organize.ISysDepartmentDmnService, NewtouchHIS.Base.DomainService.SysDepartmentDmnService>();
builder.Services.AddScoped<NewtouchHIS.Base.Domain.Organize.ISysRoleDmnService, NewtouchHIS.Base.DomainService.SysRoleDmnService>();

// Sett area services (OutpatientManage staging)
builder.Services.AddScoped<ICancelOutpatientRegDmnService, CancelOutpatientRegDmnService>();
builder.Services.AddScoped<ICommonDmnService, CommonDmnService>();
builder.Services.AddScoped<ICqybMedicalInPut02Repo, CqybMedicalInPut02Repo>();
builder.Services.AddScoped<ICqybMedicalReg02Repo, CqybMedicalReg02Repo>();
builder.Services.AddScoped<ICqybSett05Repo, CqybSett05Repo>();
builder.Services.AddScoped<ICqybSett23Repo, CqybSett23Repo>();
builder.Services.AddScoped<ICqybUpdateMedicalInput03Repo, CqybUpdateMedicalInput03Repo>();
builder.Services.AddScoped<ICqybUploadPresList04Repo, CqybUploadPresList04Repo>();
builder.Services.AddScoped<ICqybUploadInPres04Repo, CqybUploadInPres04Repo>();
builder.Services.AddScoped<IGuiAnOutpatientXnhApp, GuiAnOutpatientXnhApp>();
builder.Services.AddScoped<IMzghbookRepo, MzghbookRepo>();
builder.Services.AddScoped<IOrderPayInfoRepo, OrderPayInfoRepo>();
builder.Services.AddScoped<IOutBookDateRepo, OutBookDateRepo>();
builder.Services.AddScoped<IOutBookDmnService, OutBookDmnService>();
builder.Services.AddScoped<IOutBookRepo, OutBookRepo>();
builder.Services.AddScoped<IOutBookScheduleRepo, OutBookScheduleRepo>();
builder.Services.AddScoped<IOutBookScheduleService, OutBookScheduleService>();
builder.Services.AddScoped<IOutPatChargeApp, OutPatChargeApp>();
builder.Services.AddScoped<IOutPatChargeDmnService, OutPatChargeDmnService>();
builder.Services.AddScoped<IOutPatienChargeQueryApp, OutPatienChargeQueryApp>();
builder.Services.AddScoped<IOutPatientChargeQueryDmnService, OutPatientChargeQueryDmnService>();
builder.Services.AddScoped<IOutPatientDmnService, OutPatientDmnService>();
builder.Services.AddScoped<IOutPatientSettleDmnService, OutPatientSettleDmnService>();
builder.Services.AddScoped<IOutPatientUniversalDmnService, OutPatientUniversalDmnService>();
builder.Services.AddScoped<IOutpatientPrescriptionRepo, OutpatientPrescriptionRepo>();
builder.Services.AddScoped<IOutpatientRegApp, OutpatientRegApp>();
builder.Services.AddScoped<IOutpatientRegistRepo, OutpatientRegistRepo>();
builder.Services.AddScoped<IOutpatientRegistScheduleRepo, OutpatientRegistScheduleRepo>();
builder.Services.AddScoped<IOutpatientSettlementGAYBFeeRepo, OutpatientSettlementGAYBFeeRepo>();
builder.Services.AddScoped<IOutpatientSettlementRepo, OutpatientSettlementRepo>();
builder.Services.AddScoped<IPatientBasicInfoDmnService, PatientBasicInfoDmnService>();
builder.Services.AddScoped<IPayApp, PayApp>();
builder.Services.AddScoped<IPhysicalexamDmnService, PhysicalexamDmnService>();
builder.Services.AddScoped<IRefundApp, RefundApp>();
builder.Services.AddScoped<IRefundDmnService, RefundDmnService>();
builder.Services.AddScoped<ISyncTreatmentServiceRecordDmnService, SyncTreatmentServiceRecordDmnService>();
builder.Services.AddScoped<ISyncTreatmentServiceRecordRepo, SyncTreatmentServiceRecordRepo>();
builder.Services.AddScoped<ISysConfigRepo, SysConfigRepo>();
builder.Services.AddScoped<ISysDepartmentRepo, SysDepartmentRepo>();
builder.Services.AddScoped<ISysPatientBasicInfoRepo, SysPatientBasicInfoRepo>();
builder.Services.AddScoped<ISysRegistSpecialDiseaseRepo, SysRegistSpecialDiseaseRepo>();
builder.Services.AddScoped<ISysUserDmnService, SysUserDmnService>();
builder.Services.AddScoped<IprintOutpatientChargeBillDmnService, printOutpatientChargeBillDmnService>();
builder.Services.AddScoped<IOutpatientItemRepo, OutpatientItemRepo>();

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
