using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo: chl
    /// Ngày: 2020-03-13 11:29
    /// Mô tả: Bản ghi trang đầu hồ sơ bệnh án
    /// </summary>
    public class MrbasyRepo : RepositoryBase<MrbasyEntity>, IMrbasyRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public MrbasyRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

    }
}