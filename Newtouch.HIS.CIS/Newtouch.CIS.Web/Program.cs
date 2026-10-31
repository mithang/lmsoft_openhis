using Microsoft.AspNetCore.Builder;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base.Middleware;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Services.HttpService;
using Newtouch.CIS.Web.Hubs;

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


builder.WebHost.UseUrls("http://127.0.0.1:5234");

Newtouch.CIS.Compat.AppConfig.Init(builder.Configuration);

builder.Services.AddLibLogging();

builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
builder.Services.AddWebServiceBase(builder.Configuration);

builder.Services.AddSignalR();

// CIS services (explicit registrations, generated from the ported Infra layer:
// every concrete repo / DmnService / App class mapped to its interface).
// Framework app services above are picked up by AddWebServiceBase ->
// AddDependencyService scanning bin for IDependency.
builder.Services.AddScoped<Newtouch.Application.Interface.IBespeakRegisterApp, Newtouch.Application.Implementation.BespeakRegisterApp>();
builder.Services.AddScoped<Newtouch.Application.Interface.IInfusionApp, Newtouch.Application.Implementation.InfusionApp>();
builder.Services.AddScoped<Newtouch.Application.Interface.IInsideGeneralApp, Newtouch.Application.Implementation.InsideGeneralApp>();
builder.Services.AddScoped<Newtouch.Application.Interface.IOutpatientCmmManagerApp, Newtouch.Application.Implementation.OutpatientCmmManagerApp>();
builder.Services.AddScoped<Newtouch.Application.Interface.IPatientRyDiagnosisApp, Newtouch.Application.Implementation.PatientRyDiagnosisApp>();
builder.Services.AddScoped<Newtouch.Application.Interface.IPrepareMedicineApp, Newtouch.Application.Implementation.PrepareMedicineApp>();
builder.Services.AddScoped<Newtouch.Application.Interface.IStockInventoryApp, Newtouch.Application.Implementation.StockInventoryApp>();
builder.Services.AddScoped<Newtouch.Application.Interface.ITherapistApp, Newtouch.Application.Implementation.TherapistApp>();
builder.Services.AddScoped<Newtouch.Application.Interface.IUsageLinkageApp, Newtouch.Application.Implementation.UsageLinkageApp>();
builder.Services.AddScoped<Newtouch.Application.Interface.IVisitDeptSetApp, Newtouch.Application.Implementation.VisitDeptSetApp>();
builder.Services.AddScoped<Newtouch.Application.Interface.Inpatient.IDoctorserviceApp, Newtouch.Application.Implementation.Inpatient.DoctorserviceApp>();
builder.Services.AddScoped<Newtouch.CIS.Compat.ICache, Newtouch.CIS.Compat.CacheService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.Clinic.IClinicDmnService, Newtouch.DomainServices.Clinic.ClinicDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.DeptStorageManage.IKcKcjzDmnService, Newtouch.DomainServices.DeptStorageManage.KcKcjzDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.DeptStorageManage.IKcSyxxDmnService, Newtouch.DomainServices.DeptStorageManage.KcSyxxDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.DeptStorageManage.IKfKcxxDmnService, Newtouch.DomainServices.DeptStorageManage.KfKcxxDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.DeptStorageManage.IStockInventoryDmnService, Newtouch.DomainServices.DeptStorageManage.StockInventoryDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IAllergyManageDmnService, Newtouch.DomainServices.Outpatient.AllergyManageDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IBaseDataDmnService, Newtouch.DomainServices.BaseDataDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.ICmmHis02RecordDmnService, Newtouch.DomainServices.CmmHis02RecordDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IDietaryAdviceDmnService, Newtouch.DomainServices.Inpatient.DietaryAdviceDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IDoctorserviceDmnService, Newtouch.DomainServices.Inpatient.DoctorserviceDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IEntryRegistrationDmnService, Newtouch.DomainServices.Inpatient.EntryRegistrationDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IExceReportPrintDmnService, Newtouch.DomainServices.Inpatient.ExceReportPrintDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IGroupPackageDmnService, Newtouch.DomainServices.GroupPackageDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IIDBDmnService, Newtouch.DomainServices.IDBDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IInpatientMedReturnDmnService, Newtouch.DomainServices.InpatientMedReturnDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IInpatientOrderPackageDmnService, Newtouch.DomainServices.Inpatient.InpatientOrderPackageDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IInpatientPatientDmnService, Newtouch.DomainServices.Inpatient.InpatientPatientDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IInspectionTemplateDmnService, Newtouch.DomainServices.InspectionTemplateDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.ILevelChargeDmnService, Newtouch.DomainServices.Inpatient.LevelChargeDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IMRecordTemplateDmnService, Newtouch.DomainServices.MRecordTemplateDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IMedicalRecordDmnService, Newtouch.DomainServices.MedicalRecordDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IMzsyypxxDmnService, Newtouch.DomainServices.MzsyypxxDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IMzyyghDmnService, Newtouch.DomainServices.MzyyghDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IOperationDmnService, Newtouch.DomainServices.Inpatient.OperationDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IOrderAuditDmnService, Newtouch.DomainServices.OrderAuditDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IOrderExecutionDmnService, Newtouch.DomainServices.OrderExecutionDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IOutpatientNurseDmnServise, Newtouch.DomainServices.OutpatientNurseDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IPrepareMedicineDmnService, Newtouch.DomainServices.PrepareMedicineDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IPresTemplateDmnService, Newtouch.DomainServices.PresTemplateDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IPrescriptionDmnService, Newtouch.DomainServices.PrescriptionDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IReportDmnService, Newtouch.DomainServices.ReportDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.ISysBespeakRegisterDmnService, Newtouch.DomainServices.SysBespeakRegisterDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.ISysOrganizeDmnService, Newtouch.DomainServices.SysOrganizeDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.ISysUsageLinkageDmnService, Newtouch.DomainServices.SysUsageLinkageDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.ISysUserDmnService, Newtouch.DomainServices.SysUserDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.ITTCataloguesComparisonDmnService, Newtouch.DomainServices.TTCataloguesComparisonDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.ITreatmentDmnService, Newtouch.DomainServices.TreatmentDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.ITreatmentPrintDmnService, Newtouch.DomainServices.Inpatient.TreatmentPrintDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IVisitDeptSetDmnService, Newtouch.DomainServices.VisitDeptSetDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.IWZsldDmnService, Newtouch.DomainServices.WZsldDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.Inpatient.IDeptMedicineApplyNoDmnService, Newtouch.DomainServices.Inpatient.DeptMedicineApplyNoDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.Inpatient.IWardMaintenanceDmnService, Newtouch.DomainServices.Inpatient.WardMaintenanceDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.InterfaceSync.IHospInterfaceSyncDmnService, Newtouch.DomainServices.HospInterfaceSyncDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.InterfaceSync.ISettInterfaceSyncDmnService, Newtouch.DomainServices.SettInterfaceSyncDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.Outpatient.IElectronicPrescriptionDmnService, Newtouch.DomainServices.Outpatient.ElectronicPrescriptionDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.Outpatient.IOutpatientConsultDmnService, Newtouch.DomainServices.Outpatient.OutpatientConsultDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.Outpatient.IOutpatientQueryDmnService, Newtouch.DomainServices.Outpatient.OutpatientQueryDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IDomainServices.Queue.IQueueDmnService, Newtouch.DomainServices.Queue.QueueDmnService>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.Clinic.IClinicApplyRepo, Newtouch.Repository.Clinic.ClinicApplyRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.DeptStorageManage.IDeptKcSyyyRepo, Newtouch.Herp.Repository.KcSyyyRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.DeptStorageManage.IKcKcjzRepo, Newtouch.Herp.Repository.KcKcjzRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.DeptStorageManage.IKcPdxxRepo, Newtouch.Herp.Repository.KcPdxxRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.DeptStorageManage.IKcPdxxmxRepo, Newtouch.Herp.Repository.KcPdxxmxRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.DeptStorageManage.IKcSyxxRepo, Newtouch.Herp.Repository.KcSyxxRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.DeptStorageManage.IKfKcxxRepo, Newtouch.Herp.Repository.KfKcxxRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IAdmissionNoticeRepo, Newtouch.Repository.AdmissionNoticeRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IAllergyRepo, Newtouch.Repository.AllergyRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IBlctglRepo, Newtouch.Repository.BlctglRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.ICmmHis01Repo, Newtouch.Repository.CmmHis01Repo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.ICmmHis02RecordRepo, Newtouch.Repository.CmmHis02RecordRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IComDiagnosisRepo, Newtouch.Repository.ComDiagnosisRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientBedChargeItemRepo, Newtouch.Repository.InpatientBedChargeItemRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientBedLevelChargeItemRepo, Newtouch.Repository.InpatientBedLevelChargeItemRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientBedUseRecordRepo, Newtouch.Repository.InpatientBedUseRecordRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientChargeItemDietExeRepo, Newtouch.Repository.InpatientChargeItemDietExeRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientDietBaseRepo, Newtouch.Repository.InpatientDietBaseRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientDietDetailSplitRepo, Newtouch.Repository.InpatientDietDetailSplitRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientDietSfxmdyRepo, Newtouch.Repository.InpatientDietSfxmdyRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientDietTemplateDetailSplitRepo, Newtouch.Repository.InpatientDietTemplateDetailSplitRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientFeeDetailRepo, Newtouch.Repository.InpatientFeeDetailRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientLongTermOrderRepo, Newtouch.Repository.InpatientLongTermOrderRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientMedicineGrantRepo, Newtouch.Repository.InpatientMedicineGrantRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientMedicineReturnRepo, Newtouch.Repository.InpatientMedicineReturnRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientOperationArrangementRepo, Newtouch.Repository.InpatientOperationArrangementRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientOrderPackageDetailRepo, Newtouch.Repository.InpatientOrderPackageDetailRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientOrderPackageRepo, Newtouch.Repository.InpatientOrderPackageRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientPatientDiagnosisRepo, Newtouch.Repository.InpatientPatientDiagnosisRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientPatientDoctorRepo, Newtouch.Repository.InpatientPatientDoctorRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientPatientInfoRepo, Newtouch.Repository.InpatientPatientInfoRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientSTATOrderRepo, Newtouch.Repository.InpatientSTATOrderRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IInpatientVitalSignsRepo, Newtouch.Repository.InpatientVitalSignsRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IMRTemplateTCMDiagnosisRepo, Newtouch.Repository.MRTemplateTCMDiagnosisRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IMedicalAdviceBindingFeeRepo, Newtouch.Repository.MedicalAdviceBindingFeeRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IMzZyBarCodeRepo, Newtouch.Repository.MzZyBarCodeRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IMzsyypxxRepo, Newtouch.Repository.MzsyypxxRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IMzsyzxxxRepo, Newtouch.Repository.Outpatient.MzsyzxxxRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IMzyyghRepo, Newtouch.Repository.MzyyghRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IPacs_ExamBodyPartsRepo, Newtouch.Repository.Pacs_ExamBodyPartsRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IPrepareMedicineStockInfoRepo, Newtouch.Repository.PrepareMedicineStockInfoRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IQueueRepo, Newtouch.Repository.QueueRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IReservationRepo, Newtouch.Repository.ReservationRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.ISysBespeakRegisterRepo, Newtouch.Repository.SysBespeakRegisterRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.ISysBodyPartsRepo, Newtouch.Repository.SysBodyPartsRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.ISysConfigRepo, Newtouch.Repository.SysConfigRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.ISysDoctorRemarkRepo, Newtouch.Repository.SysDoctorRemarkRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.ISysMedicineProfitLossReasonRepo, Newtouch.Repository.SysMedicineProfitLossReasonRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.ISysNationRepo, Newtouch.Repository.SysNationRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.ISysNationalityRepo, Newtouch.Repository.SysNationalityRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.ISysObjectActionRecordRepo, Newtouch.Repository.SysObjectActionRecordRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.ISysStaffRepo, Newtouch.Repository.SysStaffRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.ITCMDiagnosisRepo, Newtouch.Repository.TCMDiagnosisRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IVisitDeptSetRepo, Newtouch.Repository.VisitDeptSetRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IXtjyjcExecRepo, Newtouch.Repository.SysManage.XtjyjcExecRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.IXtjyjcFileUploadRepo, Newtouch.Repository.SysManage.XtjyjcFileUploadRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.Inpatient.IDrug_withdrawalzy_tyjlRepo, Newtouch.Repository.Inpatient.Drug_withdrawalzy_tyjlRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.Inpatient.IInpatientBedCardRepo, Newtouch.Repository.Inpatient.InpatientBedCardRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.Inpatient.IInpatientMedicinejxCodeComparedRepo, Newtouch.Repository.Inpatient.InpatientMedicinejxCodeComparedRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.Inpatient.IQhdZnshSqtxRepo, Newtouch.Repository.Inpatient.QhdZnshSqtxRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.Outpatient.IOutpatientConsultDoctorRepo, Newtouch.Repository.Outpatient.OutpatientConsultDoctorRepo>();
builder.Services.AddScoped<Newtouch.Domain.IRepository.Outpatient.IOutpatientRegConsultRepo, Newtouch.Repository.Outpatient.OutpatientRegConsultRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IAuxiliaryDictionaryRepo, Newtouch.HIS.Repository.AuxiliaryDictionaryRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.ICfTemplateGroupPackageRepo, Newtouch.HIS.Repository.CfTemplateGroupPackageRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.ICommonChargeItemRepo, Newtouch.HIS.Repository.CommonChargeItemRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.ICommonDrugRepo, Newtouch.HIS.Repository.CommonDrugRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IGroupPackageItemRepo, Newtouch.HIS.Repository.GroupPackageItemRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IGroupPackageRepo, Newtouch.HIS.Repository.GroupPackageRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IInspectionCategoryRepo, Newtouch.HIS.Repository.InspectionCategoryRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IInspectionTemplateRepo, Newtouch.HIS.Repository.InspectionTemplateRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IMRTemplateRepo, Newtouch.HIS.Repository.MRTemplateRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IMRTemplateWMDiagnosisRepo, Newtouch.HIS.Repository.MRTemplateWMDiagnosisRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IMedicalRecordRepo, Newtouch.HIS.Repository.MedicalRecordRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IPatientVitalSignsRepo, Newtouch.HIS.Repository.PatientvitalSignsRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IPresTemplateDetailRepo, Newtouch.HIS.Repository.PrescriptionTemplateDetailRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IPresTemplateRepo, Newtouch.HIS.Repository.PrescriptionTemplateRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IPrescriptionDetailRepo, Newtouch.HIS.Repository.PrescriptionDetailRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IPrescriptionRepo, Newtouch.HIS.Repository.PrescriptionRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.ISysDepartmentRepo, Newtouch.CIS.Repository.SysDepartmentRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.ISysDiagnosisRepo, Newtouch.Repository.SysDiagnosisRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.ISysTCMSyndromeRepo, Newtouch.CIS.Repository.SysTCMSyndromeRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.ISysUsageLinkageRepo, Newtouch.Repository.SysUsageLinkageRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.ITemplateGroupPackageRepo, Newtouch.HIS.Repository.TemplateGroupPackageRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.ITreatmentRepo, Newtouch.HIS.Repository.TreatmentRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IUsedDiagnosisRepo, Newtouch.HIS.Repository.UsedDiagnosisRepo>();
builder.Services.AddScoped<Newtouch.HIS.Domain.IRepository.IWMDiagnosisRepo, Newtouch.HIS.Repository.WMDiagnosisRepo>();

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

app.MapHub<ChatHub>("/signalr");

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
