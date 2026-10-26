using Newtouch.Infrastructure.EF;
using Newtouch.OR.ManageSystem.Domain.Entity;
using System.Data.Entity;

namespace Newtouch.OR.ManageSystem.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultDbContextTBRegister
    {
        /// <summary>
        /// Bảng đăng ký（Tầng dưới đã đăng ký một phần）
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Registe(DbModelBuilder modelBuilder)
        {
            //Đăng ký bảng mới
            //modelBuilder.Entity<SysUserEntity>().RegisterTable().HasKey(p => p.Id);

            //
            modelBuilder.Entity<ORApplyInfoEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<OROperationEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ORRoomEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ORAnesthesiaEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ORStaffEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ORNotchGradeEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ORArrangementEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<OROpStaffRecordEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ORRegistrationEntity>().RegisterTable().HasKey(p => p.Id);
			modelBuilder.Entity<ORApplyInfoExpandEntity>().RegisterTable().HasKey(p => p.Id);
		}

    }
}