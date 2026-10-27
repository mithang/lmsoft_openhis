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
using Newtouch.Sett.Repository;
using Newtouch.Sett.DomainServices;
using Newtouch.Sett.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://127.0.0.1:5221");

builder.Services.AddLibLogging();

builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
builder.Services.AddWebServiceBase(builder.Configuration);

// Sett area services (added at merge time; staging apps register their own area services here)


builder.Services.AddScoped<ILoginAppService, LoginAppService>();
builder.Services.AddScoped<NewtouchHIS.Base.Domain.Organize.ISysUserVDmnService, NewtouchHIS.Base.DomainService.SysUserVDmnService>();
builder.Services.AddScoped<NewtouchHIS.Base.Domain.Organize.ISysStaffDmnService, NewtouchHIS.Base.DomainService.SysStaffDmnService>();
builder.Services.AddScoped<NewtouchHIS.Base.Domain.Organize.ISysDepartmentDmnService, NewtouchHIS.Base.DomainService.SysDepartmentDmnService>();
builder.Services.AddScoped<NewtouchHIS.Base.Domain.Organize.ISysRoleDmnService, NewtouchHIS.Base.DomainService.SysRoleDmnService>();
builder.Services.AddScoped<ICancelOutpatientRegDmnService, CancelOutpatientRegDmnService>();
builder.Services.AddScoped<ICommonDmnService, CommonDmnService>();
builder.Services.AddScoped<ICqybMedicalInPut02Repo, CqybMedicalInPut02Repo>();
builder.Services.AddScoped<ICqybMedicalReg02Repo, CqybMedicalReg02Repo>();
builder.Services.AddScoped<ICqybSett23Repo, CqybSett23Repo>();
builder.Services.AddScoped<ICqybUpdateMedicalInput03Repo, CqybUpdateMedicalInput03Repo>();
builder.Services.AddScoped<ICqybUploadPresList04Repo, CqybUploadPresList04Repo>();
builder.Services.AddScoped<ICqybUploadInPres04Repo, CqybUploadInPres04Repo>();
builder.Services.AddScoped<IGuiAnOutpatientXnhApp, GuiAnOutpatientXnhApp>();
builder.Services.AddScoped<IMzghbookRepo, MzghbookRepo>();
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
builder.Services.AddScoped<IPhysicalexamDmnService, PhysicalexamDmnService>();
builder.Services.AddScoped<IRefundApp, RefundApp>();
builder.Services.AddScoped<IRefundDmnService, RefundDmnService>();
builder.Services.AddScoped<ISyncTreatmentServiceRecordDmnService, SyncTreatmentServiceRecordDmnService>();
builder.Services.AddScoped<ISyncTreatmentServiceRecordRepo, SyncTreatmentServiceRecordRepo>();
builder.Services.AddScoped<ISysDepartmentRepo, SysDepartmentRepo>();
builder.Services.AddScoped<ISysPatientBasicInfoRepo, SysPatientBasicInfoRepo>();
builder.Services.AddScoped<ISysRegistSpecialDiseaseRepo, SysRegistSpecialDiseaseRepo>();
builder.Services.AddScoped<ISysUserDmnService, SysUserDmnService>();
builder.Services.AddScoped<IprintOutpatientChargeBillDmnService, printOutpatientChargeBillDmnService>();
builder.Services.AddScoped<IOutpatientItemRepo, OutpatientItemRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysConfigRepo, Newtouch.Sett.Domain.SysConfigRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysStaffRepo, Newtouch.Sett.Domain.SysStaffRepoStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IMoneyUpperLimitReminderDmnService, Newtouch.Sett.Domain.MoneyUpperLimitReminderDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IPatientBasicInfoDmnService, Newtouch.Sett.Domain.PatientBasicInfoDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospSettApp, Newtouch.Sett.Domain.HospSettApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospSettCancelApp, Newtouch.Sett.Domain.HospSettCancelAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHospSettDmnService, Newtouch.Sett.Domain.HospSettDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IBookkeepInpatientApp, Newtouch.Sett.Domain.BookkeepInpatientAppStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IBookkeepInHosDmnService, Newtouch.Sett.Domain.BookkeepInHosDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IOutPatientApp, Newtouch.Sett.Domain.OutPatientAppStub>();
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
builder.Services.AddScoped<IPayDmnService, PayDmnService>();
builder.Services.AddScoped<IOrderPayInfoRepo, OrderPayInfoRepo>();
builder.Services.AddScoped<IOrderRefundInfoRepo, OrderRefundInfoRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IPayApp, Newtouch.Sett.DomainServices.PayApp>();

builder.Services.AddScoped<Newtouch.Sett.Domain.ICommercialInsuranceRepo, Newtouch.Sett.DomainServices.CommercialInsuranceRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IDRGTradUploadDmnService, Newtouch.Sett.DomainServices.DRGTradUploadDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IDRGUploadDmnService, Newtouch.Sett.DomainServices.DRGUploadDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IJGTradUploadDmnService, Newtouch.Sett.DomainServices.JGTradUploadDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IJGUploadDmnService, Newtouch.Sett.DomainServices.JGUploadDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IMedicalInsuranceDmnService, Newtouch.Sett.DomainServices.MedicalInsuranceDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.INonTreatmentItemBillingRepo, Newtouch.Sett.DomainServices.NonTreatmentItemBillingRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.INonTreatmentItemDmnService, Newtouch.Sett.DomainServices.NonTreatmentItemDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysCommercialInsuranceDmnService, Newtouch.Sett.DomainServices.SysCommercialInsuranceDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysCommercialInsuranceFilingRepo, Newtouch.Sett.DomainServices.SysCommercialInsuranceFilingRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysMedicalInsuranceFilingRepo, Newtouch.Sett.DomainServices.SysMedicalInsuranceFilingRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IDaySettleDmnService, Newtouch.Sett.DomainServices.DaySettleDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IDbBackupApp, Newtouch.Sett.DomainServices.DbBackupApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IDeanInquiryDmnService, Newtouch.Sett.DomainServices.DeanInquiryDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IFilterIPApp, Newtouch.Sett.DomainServices.FilterIPApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IGRSCostEarningDmnService, Newtouch.Sett.DomainServices.GRSCostEarningDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ILogApp, Newtouch.Sett.DomainServices.LogApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IMonthProfitShareConfigDmnService, Newtouch.Sett.DomainServices.MonthProfitShareConfigDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysOrganizeDmnService, Newtouch.Sett.DomainServices.SysOrganizeDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ITherapeutistCompleteDmnService, Newtouch.Sett.DomainServices.TherapeutistCompleteDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ITherapistSuggestionDmnService, Newtouch.Sett.DomainServices.TherapistSuggestionDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.Ijgss_AttachmentInfoRepo, Newtouch.Sett.DomainServices.jgss_AttachmentInfoRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.Ijgss_EarningInfoRepo, Newtouch.Sett.DomainServices.jgss_EarningInfoRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IBaseGaXnhS30Repo, Newtouch.Sett.DomainServices.BaseGaXnhS30Repo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ICache, Newtouch.Sett.DomainServices.Cache>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IDataAnalysisDmnService, Newtouch.Sett.DomainServices.DataAnalysisDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IG_yb_daysrg_trt_list_bRepo, Newtouch.Sett.DomainServices.G_yb_daysrg_trt_list_bRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IGuianRybl21OutInfoRepo, Newtouch.Sett.DomainServices.GuianRybl21OutInfoRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IGuianXnhS04InfoRepo, Newtouch.Sett.DomainServices.GuianXnhS04InfoRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IHighchartsDmnService, Newtouch.Sett.DomainServices.HighchartsDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IPatientCenterDmnService, Newtouch.Sett.DomainServices.PatientCenterDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysAccountRepo, Newtouch.Sett.DomainServices.SysAccountRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysCardApp, Newtouch.Sett.DomainServices.SysCardApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysCardRepo, Newtouch.Sett.DomainServices.SysCardRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysDiagnosisRepo, Newtouch.Sett.DomainServices.SysDiagnosisRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysPatAccApp, Newtouch.Sett.DomainServices.SysPatAccApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysPatBasicInfoApp, Newtouch.Sett.DomainServices.SysPatBasicInfoApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysPatientMedicalRecordDmnService, Newtouch.Sett.DomainServices.SysPatientMedicalRecordDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysPatientNatureRepo, Newtouch.Sett.DomainServices.SysPatientNatureRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysWardRepo, Newtouch.Sett.DomainServices.SysWardRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IPatientListPrintDmnService, Newtouch.Sett.DomainServices.PatientListPrintDmnServiceStub>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IAreaApp, Newtouch.Sett.DomainServices.AreaApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IDutyApp, Newtouch.Sett.DomainServices.DutyApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IEchartsDmnService, Newtouch.Sett.DomainServices.EchartsDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IFinanceReceiptRepo, Newtouch.Sett.DomainServices.FinanceReceiptRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IFinancialInvoiceRepo, Newtouch.Sett.DomainServices.FinancialInvoiceRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IInpatientAccountManageApp, Newtouch.Sett.DomainServices.InpatientAccountManageApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IInpatientAccountRepo, Newtouch.Sett.DomainServices.InpatientAccountRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IItemsApp, Newtouch.Sett.DomainServices.ItemsApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IItemsDetailApp, Newtouch.Sett.DomainServices.ItemsDetailApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IModuleApp, Newtouch.Sett.DomainServices.ModuleApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IModuleButtonApp, Newtouch.Sett.DomainServices.ModuleButtonApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IMoneyUpperLimitReminderRepo, Newtouch.Sett.DomainServices.MoneyUpperLimitReminderRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IReportCommonDmnService, Newtouch.Sett.DomainServices.ReportCommonDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IRoleApp, Newtouch.Sett.DomainServices.RoleApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IRoleAuthorizeApp, Newtouch.Sett.DomainServices.RoleAuthorizeApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IRptrptMzRjbRepo, Newtouch.Sett.DomainServices.RptrptMzRjbRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysAgInsurChargeCategApp, Newtouch.Sett.DomainServices.SysAgInsurChargeCategApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysCIRemindContentApp, Newtouch.Sett.DomainServices.SysCIRemindContentApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysCISpecialMarkApp, Newtouch.Sett.DomainServices.SysCISpecialMarkApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysChargeAddCategApp, Newtouch.Sett.DomainServices.SysChargeAddCategApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysChargeCategoryRepo, Newtouch.Sett.DomainServices.SysChargeCategoryRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysChargeClassifyApp, Newtouch.Sett.DomainServices.SysChargeClassifyApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysChargeItemApp, Newtouch.Sett.DomainServices.SysChargeItemApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysChargeItemDmnService, Newtouch.Sett.DomainServices.SysChargeItemDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysChargeItemRepo, Newtouch.Sett.DomainServices.SysChargeItemRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysChargeMajorClassApp, Newtouch.Sett.DomainServices.SysChargeMajorClassApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysChargeProjPriceAdjApp, Newtouch.Sett.DomainServices.SysChargeProjPriceAdjApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysDepartmentBindingDmnService, Newtouch.Sett.DomainServices.SysDepartmentBindingDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysDepartmentBindingRepo, Newtouch.Sett.DomainServices.SysDepartmentBindingRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysFailedCodeMessageMappRepo, Newtouch.Sett.DomainServices.SysFailedCodeMessageMappRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysFeeDmnService, Newtouch.Sett.DomainServices.SysFeeDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysFinancialDmnService, Newtouch.Sett.DomainServices.SysFinancialDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysForCashPayApp, Newtouch.Sett.DomainServices.SysForCashPayApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysMRChargeClassApp, Newtouch.Sett.DomainServices.SysMRChargeClassApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysMSItemComparedApp, Newtouch.Sett.DomainServices.SysMSItemComparedApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysMedicinApp, Newtouch.Sett.DomainServices.SysMedicinApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysModuleDmnService, Newtouch.Sett.DomainServices.SysModuleDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysModuleExRepo, Newtouch.Sett.DomainServices.SysModuleExRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysParameterConfigApp, Newtouch.Sett.DomainServices.SysParameterConfigApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysPatiChargeAddDmnService, Newtouch.Sett.DomainServices.SysPatiChargeAddDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysPatiChargeLogicApp, Newtouch.Sett.DomainServices.SysPatiChargeLogicApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysPatiChargeRangeApp, Newtouch.Sett.DomainServices.SysPatiChargeRangeApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysPatiNatureApp, Newtouch.Sett.DomainServices.SysPatiNatureApp>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysRoleRepo, Newtouch.Sett.DomainServices.SysRoleRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysRoleShortcutMenuDmnService, Newtouch.Sett.DomainServices.SysRoleShortcutMenuDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysShortcutMenuRepo, Newtouch.Sett.DomainServices.SysShortcutMenuRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ISysUserRoleRepo, Newtouch.Sett.DomainServices.SysUserRoleRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ITTCataloguesComparisonDetailRepo, Newtouch.Sett.DomainServices.TTCataloguesComparisonDetailRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ITTCataloguesComparisonDmnService, Newtouch.Sett.DomainServices.TTCataloguesComparisonDmnService>();
builder.Services.AddScoped<Newtouch.Sett.Domain.ITTCataloguesComparisonMainRepo, Newtouch.Sett.DomainServices.TTCataloguesComparisonMainRepo>();
builder.Services.AddScoped<Newtouch.Sett.Domain.IUserRoleAuthDmnService, Newtouch.Sett.DomainServices.UserRoleAuthDmnService>();
builder.Services.AddControllersWithViews().AddMvcOptions(options =>
{
    options.Filters.Add(typeof(LogTrackingAttribute));
    options.Filters.Add(typeof(FrameworkAuthorizationFilter));
});
var app = builder.Build();
Newtouch.Sett.Web.Helpers.SettPathHelper.ContentRoot = app.Environment.ContentRootPath;

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
