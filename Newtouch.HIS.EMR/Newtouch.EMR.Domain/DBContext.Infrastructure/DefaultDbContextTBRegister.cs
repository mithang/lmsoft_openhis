using Newtouch.EMR.Domain.Entity;

using Newtouch.Infrastructure.EF;
using System.Data.Entity;

namespace Newtouch.EMR.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultDbContextTBRegister
    {
        /// <summary>
        /// Sổ đăng ký (tầng dưới đã đăng ký một phần)
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Registe(DbModelBuilder modelBuilder)
        {
            //Đăng ký bảng mới
            //modelBuilder.Entity<SysUserEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ZybrjbxxEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<BlmblbEntity>().RegisterTable().HasKey(p => p.Id);

            modelBuilder.Entity<bl_ryblEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<bl_bcjlEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<bl_hljlEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<bl_zqwsEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<BlctglEntity>().RegisterTable().HasKey(p => p.ID);
            modelBuilder.Entity<ZymeddocsrelationEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<BlmbqxkzEntity>().RegisterTable().HasKey(p => p.Id);

            modelBuilder.Entity<BlbasyEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<BabasyEntity>().RegisterTable().HasKey(p => p.zid);
            modelBuilder.Entity<BlysEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<BlysMXEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MrbasyEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MrbasyrelcodeEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MrbasyssEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MrbasyzdEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<bl_patrecordsEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<bl_bllxEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<YBInpPatRegInfoEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<YBInpCourseDiseaseEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<YBInpOutHosSummariesDiagEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<YBInpOutHosSummariesEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<YBInpPatRegInfoDiagEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MzmeddocsrelationEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<HljlDataEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<bl_ysjgnrEntity>().RegisterTable().HasKey(p => p.Id);
            //
            modelBuilder.Entity<bl_ElementDomainEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<bl_ElementDomain_DetailEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<bl_hljl_ybEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<bl_hljl_srsclEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<bl_hljl_wzEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<bl_hljl_ssEntity>().RegisterTable().HasKey(p => p.Id);

            //Quy tắc kiểm soát chất lượng
            modelBuilder.Entity<MrWritingRulesEntity>().RegisterTable().HasKey(p => p.Id);
            //Bản ghi đề nghị bệnh án
            modelBuilder.Entity<MrBlApplyRecordEntity>().RegisterTable().HasKey(p => p.Id);
        }

    }
}