using Newtouch.Infrastructure;
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


builder.WebHost.UseUrls("http://127.0.0.1:5237");

builder.Services.AddLibLogging();

builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
builder.Services.AddWebServiceBase(builder.Configuration);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<Newtouch.Core.Common.Interface.ICache, Newtouch.Core.Common.Interface.MemoryCacheService>();

// PDS services (explicit singletons: controllers and DmnServices resolve
// private readonly interface fields from the root provider via ServiceLocator)
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IApplyDmnService, Newtouch.HIS.DomainServices.BillManage.ApplyDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IBaseDataDmnService, Newtouch.HIS.DomainServices.BaseDataDmnService>();
builder.Services.AddSingleton<Newtouch.Core.Common.Interface.ICache, Newtouch.Core.Common.Interface.MemoryCacheService>();
builder.Services.AddSingleton<FrameworkBase.MultiOrg.Infrastructure.IDefaultDatabaseFactory, FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure.DefaultDatabaseFactory>();
builder.Services.AddSingleton<Newtouch.Infrastructure.IBaseDatabaseFactory, FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure.DefaultDatabaseFactory>();
builder.Services.AddSingleton<Newtouch.Infrastructure.IDatabaseFactory, FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure.DefaultDatabaseFactory>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IDeliveryDirectDmnService, Newtouch.HIS.DomainServices.OutOrInStoredOperate.DeliveryDirectDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IDeliveryToDepartmentDmnService, Newtouch.HIS.DomainServices.OutOrInStoredOperate.DeliveryToDepartmentDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IDepartmentMedicineInfoDmnService, Newtouch.HIS.DomainServices.Medicine.DepartmentMedicineInfoDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IDispenseIndexInfoDmnService, Newtouch.HIS.DomainServices.DispenseIndexInfoDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IDispensingDmnService, Newtouch.HIS.DomainServices.DispensingDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.IDrugStorageApp, Newtouch.HIS.Application.Implementation.DrugStorageApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IDrugStorageDmnService, Newtouch.HIS.DomainServices.DrugStorageDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IHandOutMedicineDmnService, Newtouch.HIS.DomainServices.HandOutMedicineDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IHospMedicinalOrderExecuteBatchNumberRepo, Newtouch.HIS.Repository.HospMedicinalOrderExecuteBatchNumberRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IHospitalDrugRepercussionDetailRepo, Newtouch.HIS.Repository.HospitalDrugRepercussionDetailRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.IHospitalizationPharmacyApp, Newtouch.HIS.Application.Implementation.HospitalizationPharmacyApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IInStorageDmnService, Newtouch.HIS.DomainServices.OutOrInStoredOperate.InStorageDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IItemDmnService, Newtouch.HIS.DomainServices.ItemDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IKcxxDmnService, Newtouch.HIS.DomainServices.OutOrInStoredOperate.KcxxDmnService>();
builder.Services.AddSingleton<NLog.Client.ILog>(Newtouch.Infrastructure.Log.LogProxy.GetLogger("PDS"));
builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.IMedicineApp, Newtouch.HIS.Application.Implementation.MedicineApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IMedicineDmnService, Newtouch.HIS.DomainServices.MedicineDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IMedicineInfoDmnService, Newtouch.HIS.DomainServices.MedicineInfoDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Application.IModuleApp, Newtouch.HIS.Application.ModuleApp>();
builder.Services.AddSingleton<Newtouch.HIS.Application.IModuleButtonApp, Newtouch.HIS.Application.ModuleButtonApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IMzCfRepo, Newtouch.HIS.Repository.MzCfRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IMzCfmxRepo, Newtouch.HIS.Repository.MzCfmxRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IMzCfypczjlRepo, Newtouch.HIS.Repository.MzCfypczjlRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IOrganizeRepo, Newtouch.HIS.Repository.OrganizeRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.IOutPatientDispensingApp, Newtouch.HIS.Application.Implementation.OutPatientDispensingApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IOutpatientDrugRepercussionDetailRepo, Newtouch.HIS.Repository.OutpatientDrugRepercussionDetailRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IOutpatientPrescriptionDetailBatchNumberDmnService, Newtouch.HIS.DomainServices.OutpatientPrescriptionDetailBatchNumberDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IOutpatientPrescriptionDetailBatchNumberRepo, Newtouch.HIS.Repository.OutpatientPrescriptionDetailBatchNumberRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.IPatientBaseInfoApp, Newtouch.HIS.Application.Implementation.PatientBaseInfoApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage.IPharmacyDmnService, Newtouch.HIS.DomainServices.PharmacyDrugStorage.PharmacyDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.IPharmacyDrugStorageApp, Newtouch.HIS.Application.Implementation.PharmacyDrugStorageApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IPharmacyDrugStorageDmnService, Newtouch.HIS.DomainServices.PharmacyDrugStorageDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.IPurchaseApp, Newtouch.HIS.Application.Implementation.PurchaseApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.Purchase.IPurchaseBillDetailRepo, Newtouch.HIS.Repository.Purchase.PurchaseBillDetailRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.Purchase.IPurchaseBillRepo, Newtouch.HIS.Repository.Purchase.PurchaseBillRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.PharmacyDrugStorage.IPurchaseDetailRepo, Newtouch.HIS.Repository.PharmacyDrugStorage.PurchaseDetailRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage.IPurchaseDmnService, Newtouch.HIS.DomainServices.PharmacyDrugStorage.PurchaseDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.Purchase.IPurchaseLocationRepo, Newtouch.HIS.Repository.Purchase.PurchaseLocationRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.PharmacyDrugStorage.IPurchaseRepo, Newtouch.HIS.Repository.PharmacyDrugStorage.PurchaseRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IPyDmnService, Newtouch.HIS.DomainServices.OutPatientPharmacy.pyDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IRequisitionDmnService, Newtouch.HIS.DomainServices.RequisitionDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.IResourcesOperateApp, Newtouch.HIS.Application.Implementation.ResourcesOperateApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IReturnInwardDmnService, Newtouch.HIS.DomainServices.OutOrInStoredOperate.ReturnInwardDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IReturnInwardToGysDmnService, Newtouch.HIS.DomainServices.OutOrInStoredOperate.ReturnInwardToGysDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Application.IRoleApp, Newtouch.HIS.Application.RoleApp>();
builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.IRoleAuthorizeApp, Newtouch.HIS.Application.RoleAuthorizeApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage.ISunPurchaseDmnService, Newtouch.HIS.DomainServices.PharmacyDrugStorage.SunPurchaseDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysChargeCategoryRepo, Newtouch.HIS.Repository.SysChargeCategoryRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.ISysConfigApp, Newtouch.HIS.Application.Implementation.SysConfigApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysConfigRepo, Newtouch.HIS.Repository.SysConfigRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysDepartmentRepo, Newtouch.HIS.Repository.SysDepartmentRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysDispensingaConfigRepo, Newtouch.HIS.Repository.SysDispensingaConfigRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysFailedCodeMessageMappRepo, Newtouch.HIS.Repository.SysFailedCodeMessageMappRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysItemsDetailRepo, Newtouch.HIS.Repository.SysItemsDetailRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysItemsRepo, Newtouch.HIS.Repository.SysItemsRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicalOrderFrequencyRepo, Newtouch.HIS.Repository.SysMedicalOrderFrequencyRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage.ISysMedicineAntibioticTypeDmnService, Newtouch.HIS.DomainServices.PharmacyDrugStorage.SysMedicineAntibioticTypeDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage.ISysMedicineBaseDmnService, Newtouch.HIS.DomainServices.PharmacyDrugStorage.SysMedicineBaseDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineClassificationRepo, Newtouch.HIS.Repository.SysMedicineClassificationRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.ISysMedicineCrkfsDmnService, Newtouch.HIS.DomainServices.SysMedicineCrkfsDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage.ISysMedicineDmnService, Newtouch.HIS.DomainServices.PharmacyDrugStorage.SysMedicineDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.ISysMedicineExDmnService, Newtouch.HIS.DomainServices.SysMedicineExDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineFormulationRepo, Newtouch.HIS.Repository.SysMedicineFormulationRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineInventoryDetailRepo, Newtouch.HIS.Repository.SysMedicineInventoryDetailRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineInventoryRepo, Newtouch.HIS.Repository.SysMedicineInventoryRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicinePriceAdjustmentProfitLossRepo, Newtouch.HIS.Repository.SysMedicinePriceAdjustmentProfitLossRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicinePriceAdjustmentRepo, Newtouch.HIS.Repository.SysMedicinePriceAdjustmentRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineProfitLossReasonRepo, Newtouch.HIS.Repository.SysMedicineProfitLossReasonRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineProfitLossRepo, Newtouch.HIS.Repository.SysMedicineProfitLossRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicinePropertyRepo, Newtouch.HIS.Repository.SysMedicinePropertyRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineReceiptApprovalRepo, Newtouch.HIS.Repository.SysMedicineReceiptApprovalRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.ISysMedicineReceiptDmnService, Newtouch.HIS.DomainServices.SysMedicineReceiptDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineReceiptRepo, Newtouch.HIS.Repository.SysMedicineReceiptRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineRepo, Newtouch.HIS.Repository.SysMedicineRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineRequisitionDetailRepo, Newtouch.HIS.Repository.SysMedicineRequisitionDetailRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineRequisitionRepo, Newtouch.HIS.Repository.SysMedicineRequisitionRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.ISysMedicineStockCarryDownDmnService, Newtouch.HIS.DomainServices.SysMedicineStockCarryDownDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineStockCarryDownRepo, Newtouch.HIS.Repository.PharmacyDrugStorage.SysMedicineStockCarryDownRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineStockCarryOverRepo, Newtouch.HIS.Repository.SysMedicineStockCarryOverRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.DrugStorage.ISysMedicineStockInfoDmnService, Newtouch.HIS.DomainServices.DrugStorage.SysMedicineStockInfoDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineStockInfoRepo, Newtouch.HIS.Repository.SysMedicineStockInfoRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineStorageIOModeRepo, Newtouch.HIS.Repository.SysMedicineStorageIOModeRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineStorageIOReceiptDetailRepo, Newtouch.HIS.Repository.SysMedicineStorageIOReceiptDetailRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineStorageIOReceiptRepo, Newtouch.HIS.Repository.SysMedicineStorageIOReceiptRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineSupplierRepo, Newtouch.HIS.Repository.SysMedicineSupplierRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineTemporaryInternalDispenseDetailRepo, Newtouch.HIS.Repository.SysMedicineTemporaryInternalDispenseDetailRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineTemporaryProfitLossRepo, Newtouch.HIS.Repository.SysMedicineTemporaryProfitLossRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineUnitRepo, Newtouch.HIS.Repository.SysMedicineUnitRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysMedicineUsageRepo, Newtouch.HIS.Repository.SysMedicineUsageRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysModuleButtonRepo, Newtouch.HIS.Repository.SysModuleButtonRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.ISysModuleDmnService, Newtouch.HIS.DomainServices.SysModuleDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysModuleRepo, Newtouch.HIS.Repository.SysModuleRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysOrganizeAuthorizeRepo, Newtouch.HIS.Repository.SysOrganizeAuthorizeRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.ISysOrganizeDmnService, Newtouch.HIS.DomainServices.SysOrganizeDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.ISysPharmacyDepartmentApp, Newtouch.HIS.Application.Implementation.SysPharmacyDepartmentApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.ISysPharmacyDepartmentBaseDmnService, Newtouch.HIS.DomainServices.PharmacyDrugStorage.SysPharmacyDepartmentBaseDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.ISysPharmacyDepartmentDmnService, Newtouch.HIS.DomainServices.SysPharmacyDepartmentDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysPharmacyDepartmentMedicineRepo, Newtouch.HIS.Repository.SysPharmacyDepartmentMedicineRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysPharmacyDepartmentOpenMedicineRepo, Newtouch.HIS.Repository.SysPharmacyDepartmentOpenMedicineRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysPharmacyDepartmentRepo, Newtouch.HIS.Repository.SysPharmacyDepartmentRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage.ISysPharmacyWindowDmnService, Newtouch.HIS.DomainServices.PharmacyDrugStorage.SysPharmacyWindowDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysPharmacyWindowRepo, Newtouch.HIS.Repository.SysPharmacyWindowRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysRoleAuthorizeRepo, Newtouch.HIS.Repository.SysRoleAuthorizeRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.ISysRoleDmnService, Newtouch.HIS.DomainServices.SysRoleDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysRoleRepo, Newtouch.HIS.Repository.SysRoleRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysStaffRepo, Newtouch.HIS.Repository.SysStaffRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.ISysUserApp, Newtouch.HIS.Application.Implementation.SysUserApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.ISysUserDmnService, Newtouch.HIS.DomainServices.SysUserDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.ISysUserExDmnService, Newtouch.HIS.DomainServices.SysUserExDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysUserLogOnRepo, Newtouch.HIS.Repository.SysUserLogOnRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysUserRepo, Newtouch.HIS.Repository.SysUserRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysUserRoleRepo, Newtouch.HIS.Repository.SysUserRoleRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysWardRepo, Newtouch.HIS.Repository.SysWardRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.ISysYpksfypzEntityRepo, Newtouch.HIS.Repository.SysYpksfypzEntityRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.ITestMultithreadTransApp, Newtouch.HIS.Application.Implementation.TestMultithreadTransApp>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IUserRoleAuthDmnService, Newtouch.HIS.DomainServices.UserRoleAuthDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IXtypcrkfsRepo, Newtouch.HIS.Repository.XtypcrkfsRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IZyReturnDrugApplyBillDetailRepo, Newtouch.HIS.Repository.ZyReturnDrugApplyBillDetailRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IZyReturnDrugApplyBillRepo, Newtouch.HIS.Repository.ZyReturnDrugApplyBillRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IZyTymxRepo, Newtouch.HIS.Repository.ZyTymxRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IZyYpyzczjlRepo, Newtouch.HIS.Repository.ZyYpyzczjlRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IZyYpyzxxRepo, Newtouch.HIS.Repository.ZyYpyzzxRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IRepository.IZyYpyzzxphRepo, Newtouch.HIS.Repository.ZyYpyzzxphRepo>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.IfyDmnService, Newtouch.HIS.DomainServices.fyDmnService>();
builder.Services.AddSingleton<Newtouch.HIS.Domain.IDomainServices.ItyDmnService, Newtouch.HIS.DomainServices.OutPatientPharmacy.tyDmnService>();

builder.Services.AddSingleton<Newtouch.HIS.Application.Interface.IHomePageStatisticsApp, Newtouch.HIS.Application.Implementation.HomePageStatisticsApp>();

builder.Services.AddControllersWithViews().AddMvcOptions(options =>
{
    options.Filters.Add(typeof(LogTrackingAttribute));
    options.Filters.Add(typeof(FrameworkAuthorizationFilter));
});
var app = builder.Build();

Newtouch.Infrastructure.ServiceLocator.Provider = app.Services;
Newtouch.Core.Common.Utils.ConfigurationHelper.Init(app.Configuration);

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
