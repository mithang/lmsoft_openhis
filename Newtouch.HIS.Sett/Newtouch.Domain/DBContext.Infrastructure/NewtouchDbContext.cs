using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.SystemManage;
using Newtouch.Infrastructure.EF;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Newtouch.HIS.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class NewtouchDbContext : DbContextBase
    {
        /// <summary>
        /// 
        /// </summary>
        public NewtouchDbContext()
            : base("NewtouchDbContext")
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention then the generated tables will have pluralized names. 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<DbBackupEntity>().ToTable("Sys_DbBackup").HasKey(p => p.Id);
            modelBuilder.Entity<SysModuleButtonEntity>().ToTable("Sys_ModuleButton").HasKey(p => p.Id);
            modelBuilder.Entity<SysModuleEntity>().ToTable("Sys_Module").HasKey(p => p.Id);
            modelBuilder.Entity<SysRoleAuthorizeEntity>().ToTable("Sys_RoleAuthorize").HasKey(p => p.Id);
            modelBuilder.Entity<SysRoleEntity>().ToTable("Sys_Role").HasKey(p => p.Id);
            modelBuilder.Entity<FilterIPEntity>().ToTable("Sys_FilterIP").HasKey(p => p.Id);
            modelBuilder.Entity<SysLogEntity>().ToTable("Sys_Log").HasKey(p => p.Id);

            modelBuilder.Entity<SysPatientBasicInfoEntity>().RegisterTable().HasKey(p => p.patid);
            modelBuilder.Entity<HospPatientBasicInfoEntity>().RegisterTable().HasKey(p => p.syxh);
            modelBuilder.Entity<SysPatientAccountEntity>().RegisterTable().HasKey(p => p.zhbh);
            modelBuilder.Entity<SysPatientAccountRevenueAndExpenseEntity>().RegisterTable().HasKey(p => p.zhszjlbh);

            modelBuilder.Entity<SysPatientChargeAdditionalEntity>().RegisterTable().HasKey(p => p.brsffjbh);
            modelBuilder.Entity<SysPatientChargeWaiverEntity>().RegisterTable().HasKey(p => p.brsfjmbh);
            modelBuilder.Entity<SysChargeAdditionalCategoryEntity>().RegisterTable().HasKey(p => p.dlbh);
            modelBuilder.Entity<SysChargeItemPriceAdjustEntity>().RegisterTable().HasKey(p => p.tjbh);
            modelBuilder.Entity<SysPatientChargeRangeEntity>().RegisterTable().HasKey(p => p.brsffwbh);

            modelBuilder.Entity<SysPatientChargeAlgorithmEntity>().RegisterTable().HasKey(p => p.brsfsfbh);
            modelBuilder.Entity<SysPatientNatureEntity>().RegisterTable().HasKey(p => p.brxzbh);
            modelBuilder.Entity<SysCashPaymentModelEntity>().RegisterTable().HasKey(p => p.xjzffsbh);
            modelBuilder.Entity<SysMedicalTechItemMappEntity>().RegisterTable().HasKey(p => p.yjxmdzbh);
            modelBuilder.Entity<SysCardEntity>().RegisterTable().HasKey(p => p.CardId);

            modelBuilder.Entity<ComprehensiveMaterialChargeItemMappEntity>().RegisterTable().HasKey(p => p.clzhsfxm);
            modelBuilder.Entity<HospSettlementPaymentModelEntity>().RegisterTable().HasKey(p => p.zyjszffsbh);
            modelBuilder.Entity<HospExecuteMedicalOrderReduceBurdenItemEntity>().RegisterTable().HasKey(p => p.jfxmbh);
            modelBuilder.Entity<SysChargeMaterialItemSynthesisEntity>().RegisterTable().HasKey(p => p.clzhxmbh);
            modelBuilder.Entity<SysPatientComprehensiveNatureEntity>().RegisterTable().HasKey(p => p.brxzzhbh);
            modelBuilder.Entity<HospItemBillingEntity>().RegisterTable().HasKey(p => p.jfbbh);
            modelBuilder.Entity<HospDrugBillingEntity>().RegisterTable().HasKey(p => p.jfbbh);
            modelBuilder.Entity<HospMultiDiagnosisEntity>().RegisterTable().HasKey(p => p.rydzdId);
            modelBuilder.Entity<HospSettlementEntity>().RegisterTable().HasKey(p => p.jsnm);//Quyết toán nội trú


            modelBuilder.Entity<HospTransactionSettlementDetailEntity>().RegisterTable().HasKey(p => p.jsmxbh);
            modelBuilder.Entity<HospTransactionSettlementEntity>().RegisterTable().HasKey(p => p.jsnm);
            modelBuilder.Entity<HospSettlementDetailEntity>().RegisterTable().HasKey(p => p.jsmxbh);
            modelBuilder.Entity<HospSettlementCategoryEntity>().RegisterTable().HasKey(p => p.jsdlId);


            modelBuilder.Entity<OutpatientRegistEntity>().RegisterTable().HasKey(p => p.ghnm);//Đăng ký khám ngoại trú
            modelBuilder.Entity<OutpatientSettlementCategoryEntity>().RegisterTable().HasKey(p => p.jsdlId);//Loại lớn quyết toán ngoại trú
            modelBuilder.Entity<OutpatientSettlementEntity>().RegisterTable().HasKey(p => p.jsnm);//Quyết toán ngoại trú
            modelBuilder.Entity<OutpatientRegistItemEntity>().RegisterTable().HasKey(p => p.xmnm);//Hạng mục đăng ký khám ngoại trú
            modelBuilder.Entity<OutpatientRegistNonAttendanceEntity>().RegisterTable().HasKey(p => p.thnm);//Trả số đăng ký khám ngoại trú
            modelBuilder.Entity<OutpatientSettlementPaymentModelEntity>().RegisterTable().HasKey(p => p.mzjszffsbh);//Phương thức thanh toán quyết toán ngoại trú
            modelBuilder.Entity<OutpatientSettlementDetailEntity>().RegisterTable().HasKey(p => p.jsmxnm);//Chi tiết quyết toán ngoại trú

            modelBuilder.Entity<OutpatientPrescriptionEntity>().RegisterTable().HasKey(p => p.cfnm);//Đơn thuốc ngoại trú
            modelBuilder.Entity<OutpatientPrescriptionDetailEntity>().RegisterTable().HasKey(p => p.cfmxId);//Chi tiết đơn thuốc ngoại trú 
            modelBuilder.Entity<OutpatientItemEntity>().RegisterTable().HasKey(p => p.xmnm);//Hạng mục ngoại trú
            modelBuilder.Entity<OupatientInvoicePrintEntity>().RegisterTable().HasKey(p => p.dybh);//In hóa đơn ngoại trú


            modelBuilder.Entity<FinanceReceiptEntity>().RegisterTable().HasKey(p => p.cwsjId); //Biên lai tài chính 
            modelBuilder.Entity<FinancialInvoiceEntity>().RegisterTable().HasKey(p => p.fpdm); //Hóa đơn tài chính
            modelBuilder.Entity<SysDrugInventoryEntity>().RegisterTable().HasKey(p => p.Kcxh);//Tồn kho thuốc

            modelBuilder.Entity<SysChargeTemplateEntity>().RegisterTable().HasKey(p => p.sfmbbh); //Mẫu thu phí hệ thống
            modelBuilder.Entity<SysChargeTemplateItemMappEntity>().RegisterTable().HasKey(p => p.sfmbxmId); //Hạng mục mẫu thu phí
            modelBuilder.Entity<SysFailedCodeMessageMappEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysUserRoleEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysOrganizeAuthorizeEntity>().RegisterTable().HasKey(p => p.Id);

            modelBuilder.Entity<SysConfigEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysChargeItemSpecialMarkEntity>().RegisterTable().HasKey(p => p.sfxmtsbzbh);
            modelBuilder.Entity<HospMedicinalOrderEntity>().RegisterTable().HasKey(p => p.yzId);
            modelBuilder.Entity<HospMedicinalOrderExecuteEntity>().RegisterTable().HasKey(p => p.yzzxId);

            modelBuilder.Entity<SysRegistSpecialDiseaseEntity>().RegisterTable().HasKey(p => p.ghzbbh);
            modelBuilder.Entity<OutpatientRegistScheduleEntity>().RegisterTable().HasKey(p => p.ghpbId);

            modelBuilder.Entity<HospAccountingPlanEntity>().RegisterTable().HasKey(p => p.jzjhId);
            modelBuilder.Entity<HospAccountingPlanDetailEntity>().RegisterTable().HasKey(p => p.jzjhmxId);

            modelBuilder.Entity<NonTreatmentItemBillingEntity>().RegisterTable().HasKey(p => p.jfbId);
            modelBuilder.Entity<SysMedicalInsuranceFilingEntity>().RegisterTable().HasKey(p => p.ybbabId);

            modelBuilder.Entity<OutpatientAccountEntity>().RegisterTable().HasKey(p => p.jzjhId);//mz_jzjh Kế hoạch ghi sổ ngoại trú
            modelBuilder.Entity<OutpatientAccountDetailEntity>().RegisterTable().HasKey(p => p.jzjhmxId);//mz_jzjhmx Chi tiết kế hoạch ghi sổ ngoại trú
            modelBuilder.Entity<CommercialInsuranceEntity>().RegisterTable().HasKey(p => p.Id);//xt_sybx Bảo hiểm thương mại
            modelBuilder.Entity<SysCommercialInsuranceFilingEntity>().RegisterTable().HasKey(p => p.sbbabId);//xt_sbbab Bảng đăng ký bảo hiểm thương mại
            modelBuilder.Entity<SysCommercialInsuranceChargeItemEntity>().RegisterTable().HasKey(p => p.sbkbxmId);//xt_sbkbxm Hạng mục được bảo hiểm thương mại thanh toán 
            modelBuilder.Entity<DoctorWorkingDaysPlanEntity>().RegisterTable().HasKey(p => p.Id);//jz_yspb Lịch trực của kỹ thuật viên điều trị
            modelBuilder.Entity<AdjustWorkingHoursEntity>().RegisterTable().HasKey(p => p.Id);//jz_sctz Điều chỉnh thời lượng

            modelBuilder.Entity<RptMonthReportDetailEntity>().RegisterTable().HasKey(p => p.Id);

            //Đồng bộ hồ sơ điều trị
            modelBuilder.Entity<SyncTreatmentServiceRecordEntity>().RegisterTable().HasKey(p => p.Id);

            //Nhắc nhở giới hạn trên số tiền
            modelBuilder.Entity<MoneyUpperLimitReminderEntity>().RegisterTable().HasKey(p => p.sxtxId);
            modelBuilder.Entity<SysShortcutMenuEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysRoleShortcutMenuEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MzghbookEntity>().RegisterTable().HasKey(p => p.BookId);
            base.OnModelCreating(modelBuilder);
        }

    }

}