using Newtouch.Infrastructure.EF;
using Newtouch.MRQC.Domain.Entity.QcItemManage;
using System.Data.Entity;

namespace Newtouch.MRQC.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultDbContextTBRegister
    {
        /// <summary>
        /// bảng đăng ký（tầng dưới đã đăng ký một phần）
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Registe(DbModelBuilder modelBuilder)
        {
            //đăng ký bảng mới
            //modelBuilder.Entity<SysUserEntity>().RegisterTable().HasKey(p => p.Id);

            //hạng mục kiểm soát chất lượng
            modelBuilder.Entity<QcItemDataEntity>().RegisterTable().HasKey(p => p.Id);
            //đề nghị bệnh án
            modelBuilder.Entity<MrApplyEntity>().RegisterTable().HasKey(p => p.Id);
            //chấm điểm bệnh án
            modelBuilder.Entity<MrQcScoreEntity>().RegisterTable().HasKey(p => p.Id);
        }

    }
}